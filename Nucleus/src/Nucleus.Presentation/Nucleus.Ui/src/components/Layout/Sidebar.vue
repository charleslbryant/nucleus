<template>
  <div class="flex h-full">
    <!-- Sidebar -->
    <div class="hidden md:flex md:w-64 md:flex-col">
      <div class="flex flex-col flex-grow pt-5 bg-white overflow-y-auto border-r border-gray-200">
        <div class="flex items-center flex-shrink-0 px-4">
          <h1 class="text-xl font-semibold text-gray-900">Nucleus</h1>
        </div>
        <div class="mt-5 flex-grow flex flex-col">
          <nav class="flex-1 px-2 pb-4 space-y-1">
            <router-link
              v-for="item in navigation"
              :key="item.name"
              :to="item.href"
              :class="[
                $route.path === item.href
                  ? 'bg-primary-100 border-primary-500 text-primary-700'
                  : 'border-transparent text-gray-600 hover:bg-gray-50 hover:text-gray-900',
                'group flex items-center px-2 py-2 text-sm font-medium rounded-md border-l-4'
              ]"
            >
              <component
                :is="item.icon"
                :class="[
                  $route.path === item.href ? 'text-primary-500' : 'text-gray-400 group-hover:text-gray-500',
                  'mr-3 flex-shrink-0 h-6 w-6'
                ]"
                aria-hidden="true"
              />
              {{ item.name }}
            </router-link>
          </nav>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { 
  HomeIcon, 
  ChartBarIcon, 
  CogIcon 
} from '@heroicons/vue/24/outline'

export default {
  name: 'Sidebar',
  setup() {
    const navigation = [
      { name: 'Dashboard', href: '/dashboard', icon: HomeIcon },
      { name: 'Evaluations', href: '/evaluations', icon: ChartBarIcon },
      { name: 'Settings', href: '/settings', icon: CogIcon },
    ]

    return {
      navigation
    }
  }
}
</script> 