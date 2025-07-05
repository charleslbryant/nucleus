import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '../views/Dashboard.vue'
import Evaluations from '../views/Evaluations.vue'
import EvaluationDetail from '../views/EvaluationDetail.vue'
import Settings from '../views/Settings.vue'
import Login from '../views/Login.vue'

const routes = [
  {
    path: '/',
    redirect: '/dashboard'
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
    meta: { title: 'Login', requiresGuest: true }
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Dashboard,
    meta: { title: 'Dashboard', requiresAuth: true }
  },
  {
    path: '/evaluations',
    name: 'Evaluations',
    component: Evaluations,
    meta: { title: 'Evaluations', requiresAuth: true }
  },
  {
    path: '/evaluations/:id',
    name: 'EvaluationDetail',
    component: EvaluationDetail,
    meta: { title: 'Evaluation Detail', requiresAuth: true }
  },
  {
    path: '/settings',
    name: 'Settings',
    component: Settings,
    meta: { title: 'Settings', requiresAuth: true }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach(async (to, from, next) => {
  document.title = `Nucleus - ${to.meta.title || 'Admin Dashboard'}`
  
  // Import auth store dynamically to avoid circular dependencies
  const { useAuthStore } = await import('../stores/auth')
  const authStore = useAuthStore()
  
  // Initialize auth if we have a token
  if (authStore.token && !authStore.user) {
    await authStore.initializeAuth()
  }
  
  // Handle authentication requirements
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next('/login')
  } else if (to.meta.requiresGuest && authStore.isAuthenticated) {
    next('/dashboard')
  } else {
    next()
  }
})

export default router 