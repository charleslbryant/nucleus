import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '../views/Dashboard.vue'
import Evaluations from '../views/Evaluations.vue'
import EvaluationDetail from '../views/EvaluationDetail.vue'
import Settings from '../views/Settings.vue'

const routes = [
  {
    path: '/',
    redirect: '/dashboard'
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Dashboard,
    meta: { title: 'Dashboard' }
  },
  {
    path: '/evaluations',
    name: 'Evaluations',
    component: Evaluations,
    meta: { title: 'Evaluations' }
  },
  {
    path: '/evaluations/:id',
    name: 'EvaluationDetail',
    component: EvaluationDetail,
    meta: { title: 'Evaluation Detail' }
  },
  {
    path: '/settings',
    name: 'Settings',
    component: Settings,
    meta: { title: 'Settings' }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  document.title = `Nucleus - ${to.meta.title || 'Admin Dashboard'}`
  next()
})

export default router 