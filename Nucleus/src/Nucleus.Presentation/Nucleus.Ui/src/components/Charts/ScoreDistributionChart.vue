<template>
  <div class="card">
    <h3 class="text-lg font-medium text-gray-900 mb-4">Score Distribution</h3>
    <div class="h-64">
      <Bar
        :data="chartData"
        :options="chartOptions"
      />
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { Bar } from 'vue-chartjs'
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
} from 'chart.js'

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
)

export default {
  name: 'ScoreDistributionChart',
  components: { Bar },
  props: {
    scoreDistribution: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const chartData = computed(() => ({
      labels: ['0', '1', '2', '3', '4', '5'],
      datasets: [
        {
          label: 'Evaluations',
          data: [
            props.scoreDistribution[0] || 0,
            props.scoreDistribution[1] || 0,
            props.scoreDistribution[2] || 0,
            props.scoreDistribution[3] || 0,
            props.scoreDistribution[4] || 0,
            props.scoreDistribution[5] || 0
          ],
          backgroundColor: [
            '#ef4444', // red-500
            '#f97316', // orange-500
            '#eab308', // yellow-500
            '#22c55e', // green-500
            '#3b82f6', // blue-500
            '#8b5cf6'  // violet-500
          ],
          borderColor: [
            '#dc2626', // red-600
            '#ea580c', // orange-600
            '#ca8a04', // yellow-600
            '#16a34a', // green-600
            '#2563eb', // blue-600
            '#7c3aed'  // violet-600
          ],
          borderWidth: 1
        }
      ]
    }))

    const chartOptions = {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          display: false
        },
        tooltip: {
          callbacks: {
            label: function(context) {
              return `${context.parsed.y} evaluations`
            }
          }
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            stepSize: 1
          }
        }
      }
    }

    return {
      chartData,
      chartOptions
    }
  }
}
</script> 