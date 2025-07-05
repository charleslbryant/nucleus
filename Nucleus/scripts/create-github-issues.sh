#!/bin/bash

# Script to create GitHub Issues for Nucleus project
# Run this from the Nucleus directory

echo "Creating GitHub Issues for Nucleus project..."

# Check if we're in the right directory
if [ ! -f "Nucleus.sln" ]; then
    echo "Error: Please run this script from the Nucleus directory"
    exit 1
fi

# Check if gh is authenticated
if ! gh auth status >/dev/null 2>&1; then
    echo "Error: GitHub CLI not authenticated. Please run 'gh auth login' first"
    exit 1
fi

echo "Creating PRD Issues..."

# Issue #100: Nucleus Evaluate API Endpoint
echo "Creating Issue #100: Nucleus Evaluate API Endpoint..."
gh issue create \
  --title "[PRD] Nucleus Evaluate API Endpoint" \
  --body-file "docs/github-issues/issue-100-nucleus-evaluate-endpoint.md" \
  --label "prd,requirement,product,completed,phase-1,api"

# Issue #101: n8n Workflow Integration
echo "Creating Issue #101: n8n Workflow Integration..."
gh issue create \
  --title "[PRD] n8n Workflow Integration" \
  --body-file "docs/github-issues/issue-101-n8n-integration.md" \
  --label "prd,requirement,product,completed,phase-2,integration"

# Issue #102: Admin Dashboard
echo "Creating Issue #102: Admin Dashboard..."
gh issue create \
  --title "[PRD] Admin Dashboard" \
  --body-file "docs/github-issues/issue-102-admin-dashboard.md" \
  --label "prd,requirement,product,completed,phase-3,frontend,dashboard"

# Issue #103: Production Deployment
echo "Creating Issue #103: Production Deployment..."
gh issue create \
  --title "[PRD] Production Deployment" \
  --body-file "docs/github-issues/issue-103-production-deployment.md" \
  --label "prd,requirement,product,in-progress,phase-4,production,security"

echo "Creating CRD Issues..."

# Issue #121: Human Feedback System
echo "Creating Issue #121: Human Feedback System..."
gh issue create \
  --title "[CRD] Human Feedback System" \
  --body-file "docs/github-issues/issue-121-human-feedback-system.md" \
  --label "epic,crd,completed,phase-2"

# Issue #122: Admin Dashboard Implementation
echo "Creating Issue #122: Admin Dashboard Implementation..."
gh issue create \
  --title "[CRD] Admin Dashboard Implementation" \
  --body-file "docs/github-issues/issue-122-admin-dashboard.md" \
  --label "epic,crd,completed,phase-3,frontend"

# Issue #123: Production Deployment Configuration
echo "Creating Issue #123: Production Deployment Configuration..."
gh issue create \
  --title "[CRD] Production Deployment Configuration" \
  --body-file "docs/github-issues/issue-123-production-deployment.md" \
  --label "epic,crd,in-progress,phase-4,production,high-priority"

# Issue #124: Authentication System
echo "Creating Issue #124: Authentication System..."
gh issue create \
  --title "[CRD] Authentication System" \
  --body-file "docs/github-issues/issue-124-authentication-system.md" \
  --label "epic,crd,in-progress,phase-4,security,high-priority"

echo "GitHub Issues created successfully!"
echo ""
echo "Next steps:"
echo "1. Visit your GitHub repository to see the created issues"
echo "2. Update issue numbers in your personal tasklogs"
echo "3. Set assignees and milestones as needed"
echo "4. Test the new workflow with your team" 