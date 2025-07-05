import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import axios from 'axios'

export const useAuthStore = defineStore('auth', () => {
  // State
  const user = ref(null)
  const token = ref(localStorage.getItem('token'))
  const refreshToken = ref(localStorage.getItem('refreshToken'))
  const loading = ref(false)
  const error = ref(null)

  // Getters
  const isAuthenticated = computed(() => !!token.value && !!user.value)
  const isAdmin = computed(() => user.value?.role === 'Admin')
  const isReviewer = computed(() => user.value?.role === 'Reviewer' || user.value?.role === 'Admin')
  const isGuest = computed(() => user.value?.role === 'Guest')

  // Actions
  const login = async (credentials) => {
    loading.value = true
    error.value = null
    try {
      // Send username as email field since backend accepts either
      const loginData = {
        email: credentials.username,
        password: credentials.password
      }
      const response = await axios.post('/api/auth/login', loginData)
      const { accessToken, refreshToken: newRefreshToken, user: userData } = response.data
      
      // Store tokens
      token.value = accessToken
      refreshToken.value = newRefreshToken
      localStorage.setItem('token', accessToken)
      localStorage.setItem('refreshToken', newRefreshToken)
      
      // Store user data
      user.value = userData
      
      // Set default authorization header for future requests
      axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`
      
      return true
    } catch (err) {
      error.value = err.response?.data?.message || 'Login failed'
      console.error('Login error:', err)
      return false
    } finally {
      loading.value = false
    }
  }

  const register = async (userData) => {
    loading.value = true
    error.value = null
    try {
      const response = await axios.post('/api/auth/register', userData)
      const { accessToken, refreshToken: newRefreshToken, user: newUser } = response.data
      
      // Store tokens
      token.value = accessToken
      refreshToken.value = newRefreshToken
      localStorage.setItem('token', accessToken)
      localStorage.setItem('refreshToken', newRefreshToken)
      
      // Store user data
      user.value = newUser
      
      // Set default authorization header for future requests
      axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`
      
      return true
    } catch (err) {
      error.value = err.response?.data?.message || 'Registration failed'
      console.error('Registration error:', err)
      return false
    } finally {
      loading.value = false
    }
  }

  const logout = () => {
    user.value = null
    token.value = null
    refreshToken.value = null
    localStorage.removeItem('token')
    localStorage.removeItem('refreshToken')
    delete axios.defaults.headers.common['Authorization']
  }

  const refreshAuthToken = async () => {
    if (!refreshToken.value) {
      logout()
      return false
    }

    try {
      const response = await axios.post('/api/auth/refresh', {
        refreshToken: refreshToken.value
      })
      
      const { accessToken, refreshToken: newRefreshToken } = response.data
      
      token.value = accessToken
      refreshToken.value = newRefreshToken
      localStorage.setItem('token', accessToken)
      localStorage.setItem('refreshToken', newRefreshToken)
      axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`
      
      return true
    } catch (err) {
      console.error('Token refresh failed:', err)
      logout()
      return false
    }
  }

  const getCurrentUser = async () => {
    if (!token.value) return false
    
    try {
      const response = await axios.get('/api/auth/me')
      user.value = response.data
      return true
    } catch (err) {
      console.error('Failed to get current user:', err)
      if (err.response?.status === 401) {
        logout()
      }
      return false
    }
  }

  const initializeAuth = async () => {
    if (token.value) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token.value}`
      await getCurrentUser()
    }
  }

  const clearError = () => {
    error.value = null
  }

  return {
    // State
    user,
    token,
    refreshToken,
    loading,
    error,
    
    // Getters
    isAuthenticated,
    isAdmin,
    isReviewer,
    isGuest,
    
    // Actions
    login,
    register,
    logout,
    refreshAuthToken,
    getCurrentUser,
    initializeAuth,
    clearError
  }
}) 