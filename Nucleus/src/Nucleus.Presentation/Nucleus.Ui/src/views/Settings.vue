<template>
  <AppLayout>
    <div class="space-y-6">
      <!-- Page header -->
      <div>
        <h2 class="text-2xl font-bold text-gray-900">Settings</h2>
        <p class="mt-1 text-sm text-gray-500">
          Configure system settings and preferences
        </p>
      </div>

      <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <!-- General Settings -->
        <div class="lg:col-span-2 space-y-6">
          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">General Settings</h3>
            <div class="space-y-4">
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">
                  Default Pass Threshold
                </label>
                <input
                  v-model="settings.passThreshold"
                  type="number"
                  min="0"
                  max="5"
                  step="0.1"
                  class="input-field w-32"
                />
                <p class="mt-1 text-sm text-gray-500">
                  Minimum score required to pass evaluations (0-5 scale)
                </p>
              </div>

              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">
                  Auto-refresh Interval
                </label>
                <select v-model="settings.autoRefresh" class="input-field w-48">
                  <option value="0">Disabled</option>
                  <option value="30">30 seconds</option>
                  <option value="60">1 minute</option>
                  <option value="300">5 minutes</option>
                  <option value="600">10 minutes</option>
                </select>
                <p class="mt-1 text-sm text-gray-500">
                  Automatically refresh dashboard data
                </p>
              </div>

              <div>
                <label class="flex items-center">
                  <input
                    v-model="settings.enableNotifications"
                    type="checkbox"
                    class="rounded border-gray-300 text-primary-600 shadow-sm focus:border-primary-500 focus:ring-primary-500"
                  />
                  <span class="ml-2 text-sm text-gray-700">Enable notifications</span>
                </label>
                <p class="mt-1 text-sm text-gray-500">
                  Show notifications for failed evaluations
                </p>
              </div>

              <div>
                <label class="flex items-center">
                  <input
                    v-model="settings.enableExport"
                    type="checkbox"
                    class="rounded border-gray-300 text-primary-600 shadow-sm focus:border-primary-500 focus:ring-primary-500"
                  />
                  <span class="ml-2 text-sm text-gray-700">Enable data export</span>
                </label>
                <p class="mt-1 text-sm text-gray-500">
                  Allow exporting evaluation data
                </p>
              </div>
            </div>
          </div>

          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">API Configuration</h3>
            <div class="space-y-4">
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">
                  API Base URL
                </label>
                <input
                  v-model="settings.apiBaseUrl"
                  type="url"
                  class="input-field"
                  placeholder="https://localhost:7001"
                />
                <p class="mt-1 text-sm text-gray-500">
                  Base URL for the Nucleus API
                </p>
              </div>

              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">
                  Request Timeout
                </label>
                <input
                  v-model="settings.requestTimeout"
                  type="number"
                  min="1000"
                  max="60000"
                  step="1000"
                  class="input-field w-32"
                />
                <p class="mt-1 text-sm text-gray-500">
                  API request timeout in milliseconds
                </p>
              </div>

              <div>
                <label class="flex items-center">
                  <input
                    v-model="settings.enableRetry"
                    type="checkbox"
                    class="rounded border-gray-300 text-primary-600 shadow-sm focus:border-primary-500 focus:ring-primary-500"
                  />
                  <span class="ml-2 text-sm text-gray-700">Enable request retry</span>
                </label>
                <p class="mt-1 text-sm text-gray-500">
                  Automatically retry failed API requests
                </p>
              </div>
            </div>
          </div>

          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Display Settings</h3>
            <div class="space-y-4">
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">
                  Items per page
                </label>
                <select v-model="settings.itemsPerPage" class="input-field w-32">
                  <option value="10">10</option>
                  <option value="25">25</option>
                  <option value="50">50</option>
                  <option value="100">100</option>
                </select>
                <p class="mt-1 text-sm text-gray-500">
                  Number of evaluations to show per page
                </p>
              </div>

              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">
                  Date format
                </label>
                <select v-model="settings.dateFormat" class="input-field w-48">
                  <option value="MMM d, yyyy HH:mm">MMM d, yyyy HH:mm</option>
                  <option value="MM/dd/yyyy HH:mm">MM/dd/yyyy HH:mm</option>
                  <option value="dd/MM/yyyy HH:mm">dd/MM/yyyy HH:mm</option>
                  <option value="yyyy-MM-dd HH:mm">yyyy-MM-dd HH:mm</option>
                </select>
                <p class="mt-1 text-sm text-gray-500">
                  Format for displaying dates
                </p>
              </div>

              <div>
                <label class="flex items-center">
                  <input
                    v-model="settings.showCharts"
                    type="checkbox"
                    class="rounded border-gray-300 text-primary-600 shadow-sm focus:border-primary-500 focus:ring-primary-500"
                  />
                  <span class="ml-2 text-sm text-gray-700">Show charts on dashboard</span>
                </label>
                <p class="mt-1 text-sm text-gray-500">
                  Display score distribution charts
                </p>
              </div>
            </div>
          </div>
        </div>

        <!-- System Info -->
        <div class="space-y-6">
          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">System Information</h3>
            <dl class="space-y-3">
              <div>
                <dt class="text-sm font-medium text-gray-500">Version</dt>
                <dd class="text-sm text-gray-900">1.0.0</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">Build Date</dt>
                <dd class="text-sm text-gray-900">{{ buildDate }}</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">Environment</dt>
                <dd class="text-sm text-gray-900">{{ environment }}</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">API Status</dt>
                <dd class="text-sm">
                  <span
                    :class="[
                      'inline-flex px-2 py-1 text-xs font-semibold rounded-full',
                      apiStatus === 'healthy' ? 'bg-green-100 text-green-800' : 'bg-red-100 text-red-800'
                    ]"
                  >
                    {{ apiStatus }}
                  </span>
                </dd>
              </div>
            </dl>
          </div>

          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Quick Actions</h3>
            <div class="space-y-3">
              <button
                @click="testApiConnection"
                class="w-full btn-secondary text-sm"
                :disabled="testingApi"
              >
                <span v-if="testingApi" class="flex items-center justify-center">
                  <div class="animate-spin rounded-full h-4 w-4 border-b-2 border-primary-600 mr-2"></div>
                  Testing...
                </span>
                <span v-else>Test API Connection</span>
              </button>
              
              <button
                @click="clearCache"
                class="w-full btn-secondary text-sm"
              >
                Clear Cache
              </button>
              
              <button
                @click="exportSettings"
                class="w-full btn-secondary text-sm"
              >
                Export Settings
              </button>
              
              <button
                @click="resetSettings"
                class="w-full btn-secondary text-sm text-red-600 hover:text-red-700"
              >
                Reset to Defaults
              </button>
            </div>
          </div>

          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Save Settings</h3>
            <div class="space-y-3">
              <button
                @click="saveSettings"
                class="w-full btn-primary"
                :disabled="saving"
              >
                <span v-if="saving" class="flex items-center justify-center">
                  <div class="animate-spin rounded-full h-4 w-4 border-b-2 border-white mr-2"></div>
                  Saving...
                </span>
                <span v-else>Save Settings</span>
              </button>
              
              <button
                @click="discardChanges"
                class="w-full btn-secondary"
                :disabled="!hasChanges"
              >
                Discard Changes
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </AppLayout>
</template>

<script>
import { ref, computed, onMounted } from 'vue'
import { format } from 'date-fns'
import AppLayout from '../components/Layout/AppLayout.vue'

export default {
  name: 'Settings',
  components: {
    AppLayout
  },
  setup() {
    const settings = ref({
      passThreshold: 3.5,
      autoRefresh: 60,
      enableNotifications: true,
      enableExport: true,
      apiBaseUrl: 'https://localhost:7001',
      requestTimeout: 10000,
      enableRetry: true,
      itemsPerPage: 25,
      dateFormat: 'MMM d, yyyy HH:mm',
      showCharts: true
    })

    const originalSettings = ref({})
    const saving = ref(false)
    const testingApi = ref(false)
    const apiStatus = ref('unknown')

    const buildDate = computed(() => {
      return format(new Date(), 'MMM d, yyyy')
    })

    const environment = computed(() => {
      return import.meta.env.MODE || 'development'
    })

    const hasChanges = computed(() => {
      return JSON.stringify(settings.value) !== JSON.stringify(originalSettings.value)
    })

    const saveSettings = async () => {
      saving.value = true
      try {
        // Save to localStorage for now
        localStorage.setItem('nucleus-settings', JSON.stringify(settings.value))
        originalSettings.value = JSON.parse(JSON.stringify(settings.value))
        
        // In a real app, you'd save to the backend
        await new Promise(resolve => setTimeout(resolve, 1000))
        
        // Show success message
        console.log('Settings saved successfully')
      } catch (error) {
        console.error('Failed to save settings:', error)
      } finally {
        saving.value = false
      }
    }

    const discardChanges = () => {
      settings.value = JSON.parse(JSON.stringify(originalSettings.value))
    }

    const resetSettings = () => {
      if (confirm('Are you sure you want to reset all settings to defaults?')) {
        settings.value = {
          passThreshold: 3.5,
          autoRefresh: 60,
          enableNotifications: true,
          enableExport: true,
          apiBaseUrl: 'https://localhost:7001',
          requestTimeout: 10000,
          enableRetry: true,
          itemsPerPage: 25,
          dateFormat: 'MMM d, yyyy HH:mm',
          showCharts: true
        }
      }
    }

    const testApiConnection = async () => {
      testingApi.value = true
      try {
        const response = await fetch(`${settings.value.apiBaseUrl}/api/evaluate/health`)
        if (response.ok) {
          apiStatus.value = 'healthy'
        } else {
          apiStatus.value = 'unhealthy'
        }
      } catch (error) {
        apiStatus.value = 'unreachable'
      } finally {
        testingApi.value = false
      }
    }

    const clearCache = () => {
      if (confirm('Are you sure you want to clear the cache?')) {
        localStorage.removeItem('nucleus-cache')
        console.log('Cache cleared')
      }
    }

    const exportSettings = () => {
      const dataStr = JSON.stringify(settings.value, null, 2)
      const dataBlob = new Blob([dataStr], { type: 'application/json' })
      const url = URL.createObjectURL(dataBlob)
      const link = document.createElement('a')
      link.href = url
      link.download = `nucleus-settings-${format(new Date(), 'yyyy-MM-dd')}.json`
      link.click()
      URL.revokeObjectURL(url)
    }

    onMounted(() => {
      // Load settings from localStorage
      const savedSettings = localStorage.getItem('nucleus-settings')
      if (savedSettings) {
        try {
          settings.value = { ...settings.value, ...JSON.parse(savedSettings) }
        } catch (error) {
          console.error('Failed to load settings:', error)
        }
      }
      
      originalSettings.value = JSON.parse(JSON.stringify(settings.value))
      
      // Test API connection on load
      testApiConnection()
    })

    return {
      settings,
      saving,
      testingApi,
      apiStatus,
      buildDate,
      environment,
      hasChanges,
      saveSettings,
      discardChanges,
      resetSettings,
      testApiConnection,
      clearCache,
      exportSettings
    }
  }
}
</script> 