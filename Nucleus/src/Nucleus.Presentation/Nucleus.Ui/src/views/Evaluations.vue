<template>
  <AppLayout>
    <div class="space-y-6">
      <!-- Page header -->
      <div class="flex items-center justify-between">
        <div>
          <h2 class="text-2xl font-bold text-gray-900">Evaluations</h2>
          <p class="mt-1 text-sm text-gray-500">
            View and analyze AI workflow evaluation results
          </p>
        </div>
        <div class="flex space-x-3">
          <button
            @click="exportData('csv')"
            class="btn-secondary"
            :disabled="loading"
          >
            Export CSV
          </button>
          <button
            @click="exportData('json')"
            class="btn-secondary"
            :disabled="loading"
          >
            Export JSON
          </button>
        </div>
      </div>

      <!-- Filters -->
      <div class="card">
        <div class="flex items-center justify-between mb-4">
          <h3 class="text-lg font-medium text-gray-900">Filters</h3>
          <button
            @click="clearFilters"
            class="text-sm text-primary-600 hover:text-primary-700"
          >
            Clear all
          </button>
        </div>
        
        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
          <!-- Platform filter -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">
              Platform
            </label>
            <select
              v-model="filters.platform"
              class="input-field"
            >
              <option value="">All platforms</option>
              <option
                v-for="platform in platforms"
                :key="platform"
                :value="platform"
              >
                {{ platform }}
              </option>
            </select>
          </div>

          <!-- Workflow filter -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">
              Workflow
            </label>
            <input
              v-model="filters.workflow"
              type="text"
              placeholder="Search workflows..."
              class="input-field"
            />
          </div>

          <!-- Node filter -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">
              Node
            </label>
            <input
              v-model="filters.node"
              type="text"
              placeholder="Search nodes..."
              class="input-field"
            />
          </div>

          <!-- Status filter -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">
              Status
            </label>
            <select
              v-model="filters.passed"
              class="input-field"
            >
              <option :value="null">All</option>
              <option :value="true">Passed</option>
              <option :value="false">Failed</option>
            </select>
          </div>

          <!-- Date range -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">
              From Date
            </label>
            <input
              v-model="filters.dateFrom"
              type="date"
              class="input-field"
            />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">
              To Date
            </label>
            <input
              v-model="filters.dateTo"
              type="date"
              class="input-field"
            />
          </div>

          <!-- Score range -->
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">
              Min Score
            </label>
            <input
              v-model="filters.scoreMin"
              type="number"
              min="0"
              max="5"
              step="0.1"
              placeholder="0"
              class="input-field"
            />
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 mb-1">
              Max Score
            </label>
            <input
              v-model="filters.scoreMax"
              type="number"
              min="0"
              max="5"
              step="0.1"
              placeholder="5"
              class="input-field"
            />
          </div>
        </div>
      </div>

      <!-- Results summary -->
      <div class="flex items-center justify-between">
        <div class="text-sm text-gray-500">
          Showing {{ filteredEvaluations.length }} of {{ evaluations.length }} evaluations
        </div>
        <div class="flex items-center space-x-4 text-sm">
          <span class="text-gray-500">
            Average: <span class="font-medium">{{ averageScore }}/5</span>
          </span>
          <span class="text-gray-500">
            Pass Rate: <span class="font-medium">{{ passRate }}%</span>
          </span>
        </div>
      </div>

      <!-- Evaluations table -->
      <div class="card">
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
                  Task
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
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Actions
                </th>
              </tr>
            </thead>
            <tbody class="bg-white divide-y divide-gray-200">
              <tr v-if="loading">
                <td colspan="8" class="px-6 py-4 text-center">
                  <div class="animate-spin rounded-full h-6 w-6 border-b-2 border-primary-600 mx-auto"></div>
                </td>
              </tr>
              <tr v-else-if="error">
                <td colspan="8" class="px-6 py-4 text-center text-red-600">
                  {{ error }}
                </td>
              </tr>
              <tr v-else-if="filteredEvaluations.length === 0">
                <td colspan="8" class="px-6 py-4 text-center text-gray-500">
                  No evaluations found matching your filters
                </td>
              </tr>
              <tr
                v-else
                v-for="evaluation in filteredEvaluations"
                :key="evaluation.id"
                class="hover:bg-gray-50"
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
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {{ evaluation.task }}
                </td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                  <div class="flex items-center">
                    <span class="font-medium">{{ evaluation.score }}/5</span>
                    <div class="ml-2 w-16 bg-gray-200 rounded-full h-2">
                      <div
                        :class="[
                          'h-2 rounded-full',
                          evaluation.score >= 4 ? 'bg-green-500' :
                          evaluation.score >= 3 ? 'bg-yellow-500' : 'bg-red-500'
                        ]"
                        :style="{ width: `${(evaluation.score / 5) * 100}%` }"
                      ></div>
                    </div>
                  </div>
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
                <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
                  <button
                    @click="viewEvaluation(evaluation.id)"
                    class="text-primary-600 hover:text-primary-900"
                  >
                    View Details
                  </button>
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
import { onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { format } from 'date-fns'
import AppLayout from '../components/Layout/AppLayout.vue'
import { useEvaluationStore } from '../stores/evaluation.js'

export default {
  name: 'Evaluations',
  components: {
    AppLayout
  },
  setup() {
    const router = useRouter()
    const evaluationStore = useEvaluationStore()

    const {
      evaluations,
      filteredEvaluations,
      scoreDistribution,
      averageScore,
      passRate,
      platforms,
      loading,
      error,
      filters,
      fetchEvaluations,
      clearFilters,
      exportData
    } = evaluationStore

    const formatDate = (dateString) => {
      return format(new Date(dateString), 'MMM d, yyyy HH:mm')
    }

    const viewEvaluation = (id) => {
      router.push(`/evaluations/${id}`)
    }

    onMounted(() => {
      fetchEvaluations()
    })

    return {
      evaluations,
      filteredEvaluations,
      scoreDistribution,
      averageScore,
      passRate,
      platforms,
      loading,
      error,
      filters,
      formatDate,
      viewEvaluation,
      clearFilters,
      exportData
    }
  }
}
</script> 