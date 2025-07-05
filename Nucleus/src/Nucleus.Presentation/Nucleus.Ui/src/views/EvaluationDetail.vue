<template>
  <AppLayout>
    <div class="space-y-6">
      <!-- Page header -->
      <div class="flex items-center justify-between">
        <div>
          <div class="flex items-center space-x-3">
            <button
              @click="$router.back()"
              class="text-gray-400 hover:text-gray-600"
            >
              <ArrowLeftIcon class="w-5 h-5" />
            </button>
            <h2 class="text-2xl font-bold text-gray-900">Evaluation Details</h2>
          </div>
          <p class="mt-1 text-sm text-gray-500">
            Detailed view of evaluation results and metadata
          </p>
        </div>
        <div class="flex items-center space-x-3">
          <span
            :class="[
              'inline-flex px-3 py-1 text-sm font-semibold rounded-full',
              evaluation?.pass
                ? 'bg-green-100 text-green-800'
                : 'bg-red-100 text-red-800'
            ]"
          >
            {{ evaluation?.pass ? 'Passed' : 'Failed' }}
          </span>
        </div>
      </div>

      <div v-if="loading" class="text-center py-12">
        <div class="animate-spin rounded-full h-12 w-12 border-b-2 border-primary-600 mx-auto"></div>
        <p class="mt-4 text-gray-500">Loading evaluation details...</p>
      </div>

      <div v-else-if="error" class="text-center py-12">
        <p class="text-red-600">{{ error }}</p>
        <button
          @click="loadEvaluation"
          class="mt-4 btn-primary"
        >
          Try Again
        </button>
      </div>

      <div v-else-if="evaluation" class="space-y-6">
        <!-- Evaluation summary -->
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Score</h3>
            <div class="text-center">
              <div class="text-4xl font-bold text-gray-900 mb-2">
                {{ evaluation.score }}/5
              </div>
              <div class="w-full bg-gray-200 rounded-full h-3 mb-2">
                <div
                  :class="[
                    'h-3 rounded-full',
                    evaluation.score >= 4 ? 'bg-green-500' :
                    evaluation.score >= 3 ? 'bg-yellow-500' : 'bg-red-500'
                  ]"
                  :style="{ width: `${(evaluation.score / 5) * 100}%` }"
                ></div>
              </div>
              <p class="text-sm text-gray-500">
                {{ getScoreDescription(evaluation.score) }}
              </p>
            </div>
          </div>

          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Platform Info</h3>
            <dl class="space-y-3">
              <div>
                <dt class="text-sm font-medium text-gray-500">Platform</dt>
                <dd class="text-sm text-gray-900">{{ evaluation.platform }}</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">Workflow</dt>
                <dd class="text-sm text-gray-900">{{ evaluation.workflow_name }}</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">Node</dt>
                <dd class="text-sm text-gray-900">{{ evaluation.external_node_id }}</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">Task</dt>
                <dd class="text-sm text-gray-900">{{ evaluation.task }}</dd>
              </div>
            </dl>
          </div>

          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Model Info</h3>
            <dl class="space-y-3">
              <div>
                <dt class="text-sm font-medium text-gray-500">Model</dt>
                <dd class="text-sm text-gray-900">{{ evaluation.model_name }}</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">Provider</dt>
                <dd class="text-sm text-gray-900">{{ evaluation.model_provider }}</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">Evaluator</dt>
                <dd class="text-sm text-gray-900">{{ evaluation.evaluator_type }}</dd>
              </div>
              <div>
                <dt class="text-sm font-medium text-gray-500">Created</dt>
                <dd class="text-sm text-gray-900">{{ formatDate(evaluation.created_at) }}</dd>
              </div>
            </dl>
          </div>
        </div>

        <!-- Feedback -->
        <div class="card">
          <h3 class="text-lg font-medium text-gray-900 mb-4">Evaluation Feedback</h3>
          <div class="bg-gray-50 rounded-lg p-4">
            <p class="text-gray-900 whitespace-pre-wrap">{{ evaluation.feedback || 'No feedback provided' }}</p>
          </div>
        </div>

        <!-- Input/Output Data -->
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Input Data</h3>
            <div class="bg-gray-50 rounded-lg p-4">
              <pre class="text-sm text-gray-900 overflow-x-auto">{{ formatJson(evaluation.input_data) }}</pre>
            </div>
          </div>

          <div class="card">
            <h3 class="text-lg font-medium text-gray-900 mb-4">Output Data</h3>
            <div class="bg-gray-50 rounded-lg p-4">
              <pre class="text-sm text-gray-900 overflow-x-auto">{{ formatJson(evaluation.output_data) }}</pre>
            </div>
          </div>
        </div>

        <!-- Workflow Run Details -->
        <div class="card">
          <h3 class="text-lg font-medium text-gray-900 mb-4">Workflow Run Details</h3>
          <dl class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div>
              <dt class="text-sm font-medium text-gray-500">Execution ID</dt>
              <dd class="text-sm text-gray-900">{{ evaluation.external_execution_id }}</dd>
            </div>
            <div>
              <dt class="text-sm font-medium text-gray-500">Session ID</dt>
              <dd class="text-sm text-gray-900">{{ evaluation.session_id || 'N/A' }}</dd>
            </div>
            <div>
              <dt class="text-sm font-medium text-gray-500">Triggered By</dt>
              <dd class="text-sm text-gray-900">{{ evaluation.triggered_by }}</dd>
            </div>
            <div>
              <dt class="text-sm font-medium text-gray-500">Mode</dt>
              <dd class="text-sm text-gray-900">{{ evaluation.mode }}</dd>
            </div>
            <div>
              <dt class="text-sm font-medium text-gray-500">Started At</dt>
              <dd class="text-sm text-gray-900">{{ formatDate(evaluation.started_at) }}</dd>
            </div>
            <div>
              <dt class="text-sm font-medium text-gray-500">Completed At</dt>
              <dd class="text-sm text-gray-900">{{ formatDate(evaluation.completed_at) }}</dd>
            </div>
            <div>
              <dt class="text-sm font-medium text-gray-500">Success</dt>
              <dd class="text-sm text-gray-900">
                <span
                  :class="[
                    'inline-flex px-2 py-1 text-xs font-semibold rounded-full',
                    evaluation.success
                      ? 'bg-green-100 text-green-800'
                      : 'bg-red-100 text-red-800'
                  ]"
                >
                  {{ evaluation.success ? 'Yes' : 'No' }}
                </span>
              </dd>
            </div>
            <div v-if="evaluation.error_message" class="md:col-span-2">
              <dt class="text-sm font-medium text-gray-500">Error Message</dt>
              <dd class="text-sm text-red-600">{{ evaluation.error_message }}</dd>
            </div>
          </dl>
        </div>
      </div>
    </div>
  </AppLayout>
</template>

<script>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { format } from 'date-fns'
import { ArrowLeftIcon } from '@heroicons/vue/24/outline'
import AppLayout from '../components/Layout/AppLayout.vue'
import { useEvaluationStore } from '../stores/evaluation.js'

export default {
  name: 'EvaluationDetail',
  components: {
    AppLayout,
    ArrowLeftIcon
  },
  setup() {
    const route = useRoute()
    const evaluationStore = useEvaluationStore()
    
    const evaluation = ref(null)
    const loading = ref(false)
    const error = ref(null)

    const loadEvaluation = async () => {
      loading.value = true
      error.value = null
      
      try {
        const data = await evaluationStore.fetchEvaluationById(route.params.id)
        if (data) {
          evaluation.value = data
        } else {
          error.value = 'Evaluation not found'
        }
      } catch (err) {
        error.value = err.message || 'Failed to load evaluation'
      } finally {
        loading.value = false
      }
    }

    const formatDate = (dateString) => {
      if (!dateString) return 'N/A'
      return format(new Date(dateString), 'MMM d, yyyy HH:mm:ss')
    }

    const formatJson = (data) => {
      if (!data) return 'No data'
      try {
        return JSON.stringify(data, null, 2)
      } catch {
        return String(data)
      }
    }

    const getScoreDescription = (score) => {
      if (score >= 4.5) return 'Excellent'
      if (score >= 4) return 'Very Good'
      if (score >= 3.5) return 'Good'
      if (score >= 3) return 'Acceptable'
      if (score >= 2) return 'Poor'
      return 'Very Poor'
    }

    onMounted(() => {
      loadEvaluation()
    })

    return {
      evaluation,
      loading,
      error,
      loadEvaluation,
      formatDate,
      formatJson,
      getScoreDescription
    }
  }
}
</script> 