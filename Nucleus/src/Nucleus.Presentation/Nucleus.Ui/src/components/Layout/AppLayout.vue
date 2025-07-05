<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Mobile menu button -->
    <div class="lg:hidden">
      <div class="fixed inset-0 flex z-40">
        <div
          v-show="sidebarOpen"
          class="fixed inset-0 bg-gray-600 bg-opacity-75"
          @click="sidebarOpen = false"
        ></div>
        <div
          :class="[
            sidebarOpen ? 'translate-x-0' : '-translate-x-full',
            'relative flex-1 flex flex-col max-w-xs w-full bg-white transform transition-transform duration-300 ease-in-out'
          ]"
        >
          <div class="absolute top-0 right-0 -mr-12 pt-2">
            <button
              type="button"
              class="ml-1 flex items-center justify-center h-10 w-10 rounded-full focus:outline-none focus:ring-2 focus:ring-inset focus:ring-white"
              @click="sidebarOpen = false"
            >
              <span class="sr-only">Close sidebar</span>
              <XMarkIcon class="h-6 w-6 text-white" aria-hidden="true" />
            </button>
          </div>
          <Sidebar />
        </div>
      </div>
    </div>

    <!-- Static sidebar for desktop -->
    <div class="hidden lg:flex lg:w-64 lg:flex-col lg:fixed lg:inset-y-0">
      <Sidebar />
    </div>

    <!-- Main content -->
    <div class="lg:pl-64 flex flex-col flex-1">
      <Header />
      <main class="flex-1">
        <div class="py-6">
          <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <slot />
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue'
import { XMarkIcon } from '@heroicons/vue/24/outline'
import Sidebar from './Sidebar.vue'
import Header from './Header.vue'

export default {
  name: 'AppLayout',
  components: {
    Sidebar,
    Header,
    XMarkIcon
  },
  setup() {
    const sidebarOpen = ref(false)

    return {
      sidebarOpen
    }
  }
}
</script> 