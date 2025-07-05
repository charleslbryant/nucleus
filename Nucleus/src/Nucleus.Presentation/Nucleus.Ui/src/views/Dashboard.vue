<template>
  <AppLayout>
    <div class="space-y-6">
      <!-- Page header -->
      <div>
        <h2 class="text-2xl font-bold text-gray-900">Dashboard</h2>
        <p class="mt-1 text-sm text-gray-500">
          Monitor AI workflow quality and performance metrics
        </p>
      </div>

      <!-- Stats cards -->
      <div class="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4">
        <div class="card">
          <div class="flex items-center">
            <div class="flex-shrink-0">
              <div class="w-8 h-8 bg-primary-100 rounded-lg flex items-center justify-center">
                <ChartBarIcon class="w-5 h-5 text-primary-600" />
              </div>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-500">Total Evaluations</p>
              <p class="text-2xl font-semibold text-gray-900">{{ filteredEvaluations.length }}</p>
            </div>
          </div>
        </div>

        <div class="card">
          <div class="flex items-center">
            <div class="flex-shrink-0">
              <div class="w-8 h-8 bg-green-100 rounded-lg flex items-center justify-center">
                <CheckCircleIcon class="w-5 h-5 text-green-600" />
              </div>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-500">Pass Rate</p>
              <p class="text-2xl font-semibold text-gray-900">{{ passRate }}%</p>
            </div>
          </div>
        </div>

        <div class="card">
          <div class="flex items-center">
            <div class="flex-shrink-0">
              <div class="w-8 h-8 bg-blue-100 rounded-lg flex items-center justify-center">
                <StarIcon class="w-5 h-5 text-blue-600" />
              </div>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-500">Average Score</p>
              <p class="text-2xl font-semibold text-gray-900">{{ averageScore }}/5</p>
            </div>
          </div>
        </div>

        <div class="card">
          <div class="flex items-center">
            <div class="flex-shrink-0">
              <div class="w-8 h-8 bg-purple-100 rounded-lg flex items-center justify-center">
                <ServerIcon class="w-5 h-5 text-purple-600" />
              </div>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-500">Active Platforms</p>
              <p class="text-2xl font-semibold text-gray-900">{{ platforms.length }}</p>
            </div>
          </div>
        </div>
      </div>

      <!-- Charts -->
      <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <ScoreDistributionChart :score-distribution="scoreDistribution" />
        
        <div class="card">
          <h3 class="text-lg font-medium text-gray-900 mb-4">Recent Activity</h3>
          <div class="space-y-4">
            <div v-if="loading" class="text-center py-8">
              <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-primary-600 mx-auto"></div>
              <p class="mt-2 text-sm text-gray-500">Loading recent evaluations...</p>
            </div>
            <div v-else-if="error" class="text-center py-8">
              <p class="text-red-600">{{ error }}</p>
            </div>
            <div v-else-if="recentEvaluations.length === 0" class="text-center py-8">
              <p class="text-gray-500">No evaluations found</p>
            </div>
            <div v-else class="space-y-3">
              <div
                v-for="evaluation in recentEvaluations"
                :key="evaluation.id"
                class="flex items-center justify-between p-3 bg-gray-50 rounded-lg"
              >
                <div class="flex items-center space-x-3">
                  <div
                    :class="[
                      'w-3 h-3 rounded-full',
                      evaluation.pass ? 'bg-green-500' : 'bg-red-500'
                    ]"
                  ></div>
                  <div>
                    <p class="text-sm font-medium text-gray-900">
                      {{ evaluation.platform }} - {{ evaluation.workflow_name }}
                    </p>
                    <p class="text-xs text-gray-500">
                      {{ formatDate(evaluation.created_at) }}
                    </p>
                  </div>
                </div>
                <div class="text-right">
                  <p class="text-sm font-medium text-gray-900">
                    Score: {{ evaluation.score }}/5
                  </p>
                  <p class="text-xs text-gray-500">
                    {{ evaluation.external_node_id }}
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Recent evaluations table -->
      <div class="card">
        <div class="flex items-center justify-between mb-4">
          <h3 class="text-lg font-medium text-gray-900">Recent Evaluations</h3>
          <router-link
            to="/evaluations"
            class="btn-primary text-sm"
          >
            View All
          </router-link>
        </div>
        
        <div class="overflow-x-auto">
          <table class="min-w-full divide-y divide-gray-200">
            <thead class="bg-gray-50">
              <tr>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Platform
                </th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Workflow
                </th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Node
                </th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Score
                </th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Status
                </th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Date
                </th>
              </tr>
            </thead>
            <tbody class="bg-white divide-y divide-gray-200">
              <tr v-if="loading">
                <td colspan="6" class="px-6 py-4 text-center">
                  <div class="animate-spin rounded-full h-6 w-6 border-b-2 border-primary-600 mx-auto"></div>
                </td>
              </tr>
              <tr v-else-if="error">
                <td colspan="6" class="px-6 py-4 text-center text-red-600">
                  {{ error }}
                </td>
              </tr>
              <tr v-else-if="filteredEvaluations.length === 0">
                <td colspan="6" class="px-6 py-4 text-center text-gray-500">
                  No evaluations found
                </td>
              </tr>
              <tr
                v-else
                v-for="evaluation in filteredEvaluations.slice(0, 10)"
                :key="evaluation.id"
                class="hover:bg-gray-50 cursor-pointer"
                @click="viewEvaluation(evaluation.id)"
              >
                <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                  {{ evaluation.platform }}
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ evaluation.workflow_name }}
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ evaluation.external_node_id }}
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                  {{ evaluation.score }}/5
                </td>
                <td class="px-6 py-4 whitespace-nowrap">
                  <span
                    :class="[
                      'inline-flex px-2 py-1 text-xs font-semibold rounded-full',
                      evaluation.pass
                        ? 'bg-green-100 text-green-800'
                        : 'bg-red-100 text-red-800'
                    ]"
                  >
                    {{ evaluation.pass ? 'Passed' : 'Failed' }}
                  </span>
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ formatDate(evaluation.created_at) }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </AppLayout>
</template>

<script>
import { onMounted, computed } from 'vue'
import { useRouter } from 'vue-router'
import { format } from 'date-fns'
import {
  ChartBarIcon,
  CheckCircleIcon,
  StarIcon,
  ServerIcon
} from '@heroicons/vue/24/outline'
import AppLayout from '../components/Layout/AppLayout.vue'
import ScoreDistributionChart from '../components/Charts/ScoreDistributionChart.vue'
import { useEvaluationStore } from '../stores/evaluation.js'

export default {
  name: 'Dashboard',
  components: {
    AppLayout,
    ScoreDistributionChart,
    ChartBarIcon,
    CheckCircleIcon,
    StarIcon,
    ServerIcon
  },
  setup() {
    const router = useRouter()
    const evaluationStore = useEvaluationStore()

    const recentEvaluations = computed(() => {
      return evaluationStore.filteredEvaluations?.slice(0, 5) || []
    })

    const formatDate = (dateString) => {
      return format(new Date(dateString), 'MMM d, yyyy HH:mm')
    }

    const viewEvaluation = (id) => {
      router.push(`/evaluations/${id}`)
    }

    onMounted(() => {
      console.log('Dashboard component loaded successfully')
      evaluationStore.fetchEvaluations()
    })

    return {
      filteredEvaluations: evaluationStore.filteredEvaluations,
      scoreDistribution: evaluationStore.scoreDistribution,
      averageScore: evaluationStore.averageScore,
      passRate: evaluationStore.passRate,
      platforms: evaluationStore.platforms,
      loading: evaluationStore.loading,
      error: evaluationStore.error,
      recentEvaluations,
      formatDate,
      viewEvaluation
    }
  }
}
</script> 