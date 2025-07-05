# Nucleus Admin Dashboard

A modern Vue.js admin dashboard for monitoring and analyzing AI workflow evaluation results from the Nucleus platform.

## Features

### 📊 Dashboard Overview
- **Real-time Metrics**: Total evaluations, pass rate, average score, and active platforms
- **Score Distribution Chart**: Visual representation of evaluation scores (0-5 scale)
- **Recent Activity Feed**: Latest evaluation results with quick status indicators
- **Recent Evaluations Table**: Detailed table with filtering and sorting capabilities

### 🔍 Evaluation Management
- **Advanced Filtering**: Filter by platform, workflow, node, date range, score range, and status
- **Search & Sort**: Find specific evaluations quickly with search and sort functionality
- **Export Capabilities**: Export filtered data as CSV or JSON
- **Detailed Views**: Comprehensive evaluation details including input/output data and feedback

### ⚙️ Settings & Configuration
- **General Settings**: Pass threshold, auto-refresh intervals, notifications
- **API Configuration**: Base URL, timeouts, retry settings
- **Display Preferences**: Items per page, date formats, chart visibility
- **System Information**: Version, build date, API status monitoring

## Tech Stack

- **Frontend Framework**: Vue.js 3 with Composition API
- **State Management**: Pinia
- **Routing**: Vue Router 4
- **Styling**: Tailwind CSS with custom design system
- **Charts**: Chart.js with vue-chartjs
- **Icons**: Heroicons
- **HTTP Client**: Axios
- **Build Tool**: Vite
- **Date Handling**: date-fns

## Project Structure

```
src/
├── components/
│   ├── Charts/
│   │   └── ScoreDistributionChart.vue
│   └── Layout/
│       ├── AppLayout.vue
│       ├── Header.vue
│       └── Sidebar.vue
├── stores/
│   └── evaluation.js
├── views/
│   ├── Dashboard.vue
│   ├── Evaluations.vue
│   ├── EvaluationDetail.vue
│   └── Settings.vue
├── router/
│   └── index.js
├── App.vue
├── main.js
└── style.css
```

## Getting Started

### Prerequisites
- Node.js 18+ 
- npm or yarn
- Nucleus API running on `https://localhost:7001`

### Installation

1. **Install Dependencies**
   ```bash
   cd src/Nucleus.Presentation/Nucleus.Ui
   npm install
   ```

2. **Start Development Server**
   ```bash
   npm run dev
   ```

3. **Access the Dashboard**
   Open your browser and navigate to `http://localhost:3000`

### Building for Production

```bash
npm run build
```

The built files will be in the `dist/` directory.

## API Integration

The dashboard connects to the Nucleus API with the following endpoints:

- `GET /api/evaluations` - Get all evaluations with filtering
- `GET /api/evaluations/{id}` - Get specific evaluation details
- `GET /api/evaluations/stats` - Get evaluation statistics
- `GET /api/evaluate/health` - Health check

### API Response Format

The dashboard expects evaluation data in this format:

```json
{
  "id": "uuid",
  "platform": "n8n",
  "workflow_name": "Email Summarization",
  "external_node_id": "AI Node",
  "task": "summarize",
  "model_name": "gpt-4",
  "model_provider": "openai",
  "score": 4.2,
  "pass": true,
  "feedback": "Excellent summary with good coverage...",
  "evaluator_type": "llm",
  "created_at": "2024-01-15T10:30:00Z"
}
```

## Configuration

### Environment Variables

Create a `.env` file in the project root:

```env
VITE_API_BASE_URL=https://localhost:7001
VITE_APP_TITLE=Nucleus Admin Dashboard
```

### Settings

The dashboard includes a comprehensive settings panel where users can configure:

- **Pass Threshold**: Minimum score required to pass evaluations (default: 3.5)
- **Auto-refresh**: Automatic data refresh intervals
- **API Configuration**: Base URL, timeouts, retry logic
- **Display Preferences**: Date formats, items per page, chart visibility

## Features in Detail

### Dashboard Overview

The main dashboard provides at-a-glance insights:

1. **Metrics Cards**: Key performance indicators
2. **Score Distribution**: Bar chart showing score distribution
3. **Recent Activity**: Latest evaluation results
4. **Quick Actions**: Links to detailed views

### Evaluation Management

Comprehensive evaluation viewing and filtering:

1. **Advanced Filters**: Multiple filter options for precise data selection
2. **Real-time Search**: Instant filtering as you type
3. **Export Functionality**: Download filtered data in multiple formats
4. **Detailed Views**: Complete evaluation information including metadata

### Settings Management

Centralized configuration management:

1. **General Settings**: Core application preferences
2. **API Configuration**: Connection and timeout settings
3. **Display Settings**: UI customization options
4. **System Information**: Version and status monitoring

## Development

### Adding New Features

1. **Create Components**: Add new Vue components in the `components/` directory
2. **Update Store**: Extend the Pinia store for new data management
3. **Add Routes**: Update the router configuration for new pages
4. **Update API**: Add corresponding API endpoints if needed

### Styling Guidelines

- Use Tailwind CSS utility classes
- Follow the established design system
- Use the custom CSS classes defined in `style.css`
- Maintain responsive design principles

### State Management

The application uses Pinia for state management:

- **evaluation store**: Manages evaluation data, filtering, and API calls
- **Reactive data**: All state is reactive and automatically updates the UI
- **Computed properties**: Efficient derived state calculations

## Troubleshooting

### Common Issues

1. **API Connection Errors**
   - Verify the Nucleus API is running on `https://localhost:7001`
   - Check CORS configuration in the API
   - Test the health endpoint: `GET /api/evaluate/health`

2. **Build Errors**
   - Ensure all dependencies are installed: `npm install`
   - Check Node.js version compatibility
   - Clear node_modules and reinstall if needed

3. **Data Not Loading**
   - Check browser console for API errors
   - Verify API endpoints are accessible
   - Check network tab for failed requests

### Debug Mode

Enable debug logging by setting the browser's localStorage:

```javascript
localStorage.setItem('nucleus-debug', 'true')
```

## Contributing

1. Follow Vue.js best practices
2. Use TypeScript for better type safety
3. Write unit tests for new components
4. Update documentation for new features
5. Follow the established code style

## License

This project is part of the Nucleus platform and follows the same licensing terms. 