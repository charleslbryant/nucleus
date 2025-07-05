import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import axios from 'axios'

export const useEvaluationStore = defineStore('evaluation', () => {
  // State
  const evaluations = ref([])
  const loading = ref(false)
  const error = ref(null)
  const filters = ref({
    platform: '',
    workflow: '',
    node: '',
    dateFrom: '',
    dateTo: '',
    scoreMin: '',
    scoreMax: '',
    passed: null
  })

  // Getters
  const filteredEvaluations = computed(() => {
    let filtered = evaluations.value

    if (filters.value.platform) {
      filtered = filtered.filter(e => e.platform === filters.value.platform)
    }
    if (filters.value.workflow) {
      filtered = filtered.filter(e => e.workflow_name?.includes(filters.value.workflow))
    }
    if (filters.value.node) {
      filtered = filtered.filter(e => e.external_node_id?.includes(filters.value.node))
    }
    if (filters.value.dateFrom) {
      filtered = filtered.filter(e => new Date(e.created_at) >= new Date(filters.value.dateFrom))
    }
    if (filters.value.dateTo) {
      filtered = filtered.filter(e => new Date(e.created_at) <= new Date(filters.value.dateTo))
    }
    if (filters.value.scoreMin !== '') {
      filtered = filtered.filter(e => e.score >= parseFloat(filters.value.scoreMin))
    }
    if (filters.value.scoreMax !== '') {
      filtered = filtered.filter(e => e.score <= parseFloat(filters.value.scoreMax))
    }
    if (filters.value.passed !== null) {
      filtered = filtered.filter(e => e.pass === filters.value.passed)
    }

    return filtered
  })

  const scoreDistribution = computed(() => {
    const distribution = { 0: 0, 1: 0, 2: 0, 3: 0, 4: 0, 5: 0 }
    filteredEvaluations.value.forEach(e => {
      const score = Math.floor(e.score)
      distribution[score] = (distribution[score] || 0) + 1
    })
    return distribution
  })

  const averageScore = computed(() => {
    if (filteredEvaluations.value.length === 0) return 0
    const sum = filteredEvaluations.value.reduce((acc, e) => acc + e.score, 0)
    return (sum / filteredEvaluations.value.length).toFixed(2)
  })

  const passRate = computed(() => {
    if (filteredEvaluations.value.length === 0) return 0
    const passed = filteredEvaluations.value.filter(e => e.pass).length
    return ((passed / filteredEvaluations.value.length) * 100).toFixed(1)
  })

  const platforms = computed(() => {
    return [...new Set(evaluations.value.map(e => e.platform))].filter(Boolean)
  })

  // Actions
  const fetchEvaluations = async () => {
    loading.value = true
    error.value = null
    try {
      const response = await axios.get('/api/evaluations')
      // Transform the API response to match the frontend expectations
      evaluations.value = response.data.map(evaluation => ({
        id: evaluation.id,
        platform: evaluation.workflowRun.platform,
        workflow_name: evaluation.workflowRun.workflowName,
        external_node_id: evaluation.modelRun.nodeId,
        task: evaluation.modelRun.task,
        model_name: evaluation.modelRun.modelName,
        model_provider: evaluation.modelRun.modelProvider,
        score: evaluation.score,
        pass: evaluation.pass,
        feedback: evaluation.feedback,
        evaluator_type: evaluation.evaluatorType,
        created_at: evaluation.createdAt,
        // Additional workflow run info
        external_execution_id: evaluation.workflowRun.externalExecutionId,
        external_workflow_id: evaluation.workflowRun.externalWorkflowId,
        session_id: null, // Not available in current API
        triggered_by: null, // Not available in current API
        mode: null, // Not available in current API
        started_at: null, // Not available in current API
        completed_at: null, // Not available in current API
        success: null, // Not available in current API
        error_message: null, // Not available in current API
        input_data: null, // Not available in current API
        output_data: null // Not available in current API
      }))
    } catch (err) {
      error.value = err.response?.data?.message || 'Failed to fetch evaluations'
      console.error('Error fetching evaluations:', err)
    } finally {
      loading.value = false
    }
  }

  const fetchEvaluationById = async (id) => {
    loading.value = true
    error.value = null
    try {
      const response = await axios.get(`/api/evaluations/${id}`)
      const evaluation = response.data
      // Transform the API response to match the frontend expectations
      return {
        id: evaluation.id,
        platform: evaluation.workflowRun.platform,
        workflow_name: evaluation.workflowRun.workflowName,
        external_node_id: evaluation.modelRun.nodeId,
        task: evaluation.modelRun.task,
        model_name: evaluation.modelRun.modelName,
        model_provider: evaluation.modelRun.modelProvider,
        score: evaluation.score,
        pass: evaluation.pass,
        feedback: evaluation.feedback,
        evaluator_type: evaluation.evaluatorType,
        created_at: evaluation.createdAt,
        // Additional workflow run info
        external_execution_id: evaluation.workflowRun.externalExecutionId,
        external_workflow_id: evaluation.workflowRun.externalWorkflowId,
        session_id: null, // Not available in current API
        triggered_by: null, // Not available in current API
        mode: null, // Not available in current API
        started_at: null, // Not available in current API
        completed_at: null, // Not available in current API
        success: null, // Not available in current API
        error_message: null, // Not available in current API
        input_data: null, // Not available in current API
        output_data: null // Not available in current API
      }
    } catch (err) {
      error.value = err.response?.data?.message || 'Failed to fetch evaluation'
      console.error('Error fetching evaluation:', err)
      return null
    } finally {
      loading.value = false
    }
  }

  const updateFilters = (newFilters) => {
    filters.value = { ...filters.value, ...newFilters }
  }

  const clearFilters = () => {
    filters.value = {
      platform: '',
      workflow: '',
      node: '',
      dateFrom: '',
      dateTo: '',
      scoreMin: '',
      scoreMax: '',
      passed: null
    }
  }

  const exportData = (format = 'csv') => {
    const data = filteredEvaluations.value
    if (format === 'csv') {
      const headers = ['ID', 'Platform', 'Workflow', 'Node', 'Task', 'Score', 'Passed', 'Created At', 'Feedback']
      const csvContent = [
        headers.join(','),
        ...data.map(e => [
          e.id,
          e.platform,
          e.workflow_name,
          e.external_node_id,
          e.task,
          e.score,
          e.pass,
          e.created_at,
          `"${e.feedback?.replace(/"/g, '""') || ''}"`
        ].join(','))
      ].join('\n')
      
      const blob = new Blob([csvContent], { type: 'text/csv' })
      const url = window.URL.createObjectURL(blob)
      const a = document.createElement('a')
      a.href = url
      a.download = `evaluations-${new Date().toISOString().split('T')[0]}.csv`
      a.click()
      window.URL.revokeObjectURL(url)
    } else if (format === 'json') {
      const blob = new Blob([JSON.stringify(data, null, 2)], { type: 'application/json' })
      const url = window.URL.createObjectURL(blob)
      const a = document.createElement('a')
      a.href = url
      a.download = `evaluations-${new Date().toISOString().split('T')[0]}.json`
      a.click()
      window.URL.revokeObjectURL(url)
    }
  }

  return {
    // State
    evaluations,
    loading,
    error,
    filters,
    
    // Getters
    filteredEvaluations,
    scoreDistribution,
    averageScore,
    passRate,
    platforms,
    
    // Actions
    fetchEvaluations,
    fetchEvaluationById,
    updateFilters,
    clearFilters,
    exportData
  }
}) 