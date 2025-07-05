<template>
  <div id="app" class="min-h-screen bg-gray-50">
    <router-view />
  </div>
</template>

<script>
import { onMounted } from 'vue'
import { useAuthStore } from './stores/auth'

export default {
  name: 'App',
  setup() {
    const authStore = useAuthStore()

    onMounted(async () => {
      // Initialize authentication on app startup
      await authStore.initializeAuth()
    })
  }
}
</script> 