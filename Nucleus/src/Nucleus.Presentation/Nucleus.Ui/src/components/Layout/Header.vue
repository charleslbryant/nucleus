<template>
  <header class="bg-white shadow-sm border-b border-gray-200">
    <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
      <div class="flex justify-between h-16">
        <div class="flex">
          <div class="flex-shrink-0 flex items-center">
            <h1 class="text-xl font-semibold text-gray-900">{{ pageTitle }}</h1>
          </div>
        </div>
        <div class="flex items-center space-x-4">
          <!-- User info -->
          <div class="flex items-center space-x-2 text-sm text-gray-600">
            <span>{{ user?.username }}</span>
            <span class="px-2 py-1 text-xs bg-gray-100 rounded-full">{{ user?.role }}</span>
          </div>

          <!-- Notifications -->
          <button
            type="button"
            class="bg-white p-1 rounded-full text-gray-400 hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500"
          >
            <span class="sr-only">View notifications</span>
            <svg class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 17h5l-5 5v-5z" />
            </svg>
          </button>

          <!-- Profile dropdown -->
          <div class="ml-3 relative">
            <div>
              <button
                @click="profileMenuOpen = !profileMenuOpen"
                class="max-w-xs bg-white flex items-center text-sm rounded-full focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500"
              >
                <span class="sr-only">Open user menu</span>
                <div class="h-8 w-8 rounded-full bg-primary-600 flex items-center justify-center">
                  <span class="text-sm font-medium text-white">{{ userInitials }}</span>
                </div>
              </button>
            </div>
            
            <!-- Dropdown menu -->
            <div
              v-if="profileMenuOpen"
              class="origin-top-right absolute right-0 mt-2 w-48 rounded-md shadow-lg py-1 bg-white ring-1 ring-black ring-opacity-5 focus:outline-none z-50"
            >
              <div class="px-4 py-2 text-sm text-gray-700 border-b border-gray-100">
                <div class="font-medium">{{ user?.username }}</div>
                <div class="text-gray-500">{{ user?.role }}</div>
              </div>
              <router-link
                to="/settings"
                class="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                @click="profileMenuOpen = false"
              >
                Settings
              </router-link>
              <button
                @click="handleLogout"
                class="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
              >
                Sign out
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
import { computed, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuthStore } from '../../stores/auth'

export default {
  name: 'Header',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const authStore = useAuthStore()
    const profileMenuOpen = ref(false)
    
    const pageTitle = computed(() => {
      return route.meta.title || 'Admin Dashboard'
    })

    const user = computed(() => authStore.user)

    const userInitials = computed(() => {
      if (!user.value?.username) return 'U'
      return user.value.username.charAt(0).toUpperCase()
    })

    const handleLogout = () => {
      authStore.logout()
      profileMenuOpen.value = false
      router.push('/login')
    }

    return {
      pageTitle,
      profileMenuOpen,
      user,
      userInitials,
      handleLogout
    }
  }
}
</script> 