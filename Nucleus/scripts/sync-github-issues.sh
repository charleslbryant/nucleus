#!/bin/bash

# Script to sync local GitHub Issue files with actual GitHub Issues
# Run this from the Nucleus directory

echo "Syncing local GitHub Issue files with GitHub..."

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

# Create sync directory
mkdir -p docs/github-issues/sync

echo "Fetching GitHub Issues..."

# Fetch all issues and save to local files
gh issue list --limit 100 --json number,title,body,labels,assignees,state,createdAt,updatedAt --jq '.[] | "\(.number) \(.title) \(.state)"' | while read number title state; do
    echo "Syncing Issue #$number: $title"
    
    # Get full issue content
    gh issue view $number --json number,title,body,labels,assignees,state,createdAt,updatedAt > "docs/github-issues/sync/issue-$number.json"
    
    # Create markdown version
    gh issue view $number --json number,title,body,labels,assignees,state,createdAt,updatedAt --jq '
    "# Issue #\(.number): \(.title)\n\n" +
    "## Status\n" +
    "- **State**: \(.state)\n" +
    "- **Created**: \(.createdAt)\n" +
    "- **Updated**: \(.updatedAt)\n" +
    "- **Labels**: \(.labels | join(", "))\n" +
    "- **Assignees**: \(.assignees | join(", "))\n\n" +
    "## Content\n\n" +
    (.body // "No content")
    ' > "docs/github-issues/sync/issue-$number.md"
done

echo "Creating sync summary..."
cat > docs/github-issues/sync/README.md << 'EOF'
# GitHub Issues Sync

This directory contains automatically synced copies of GitHub Issues.

## Last Sync
$(date)

## Files
- `issue-*.json`: Raw JSON data from GitHub API
- `issue-*.md`: Markdown formatted issue content
- `README.md`: This file

## Usage
- These files are automatically generated and should not be edited manually
- Use them for offline reference or documentation
- The source of truth is always GitHub Issues

## Sync Command
Run `./scripts/sync-github-issues.sh` to update these files.
EOF

echo "✓ Sync completed successfully!"
echo ""
echo "Files updated:"
echo "- docs/github-issues/sync/issue-*.json (raw data)"
echo "- docs/github-issues/sync/issue-*.md (markdown)"
echo "- docs/github-issues/sync/README.md (summary)"
echo ""
echo "Note: These are read-only copies. Edit issues directly on GitHub." 