#!/bin/bash

# Script to archive legacy PRD and CRD markdown files
# Run this from the Nucleus directory

echo "Archiving legacy PRD and CRD markdown files..."

# Check if we're in the right directory
if [ ! -f "Nucleus.sln" ]; then
    echo "Error: Please run this script from the Nucleus directory"
    exit 1
fi

# Create archive directory
mkdir -p docs/archive/legacy-docs

# Archive PRD files
echo "Archiving PRD files..."
if [ -d "docs/roadmap/product-requirements" ]; then
    mv docs/roadmap/product-requirements docs/archive/legacy-docs/
    echo "✓ Moved PRD files to docs/archive/legacy-docs/product-requirements/"
else
    echo "⚠️  PRD directory not found"
fi

# Archive CRD files
echo "Archiving CRD files..."
if [ -d "docs/roadmap/change-requests" ]; then
    mv docs/roadmap/change-requests docs/archive/legacy-docs/
    echo "✓ Moved CRD files to docs/archive/legacy-docs/change-requests/"
else
    echo "⚠️  CRD directory not found"
fi

# Create archive README
cat > docs/archive/legacy-docs/README.md << 'EOF'
# Legacy Documentation Archive

This directory contains archived PRD and CRD markdown files that were used before migrating to GitHub Issues.

## Migration Date
$(date)

## What's Archived
- **PRD Files**: Product Requirements Documents (now managed as GitHub Issues)
- **CRD Files**: Change Request Documents (now managed as GitHub Issues)

## Current Workflow
- **PRDs**: Created as GitHub Issues using the PRD template
- **CRDs**: Created as GitHub Issues using the CRD Epic template
- **Tasks**: Personal tasklogs in `docs/tasks/personal/{username}/`
- **Team Coordination**: GitHub Issues with labels and assignees

## GitHub Issues
- PRD Issues: #100, #101, #102, #103
- CRD Issues: #121, #122, #123, #124
- Personal Tasks: #125, #126, #127, #128

## Related Documentation
- [Team Collaboration Guide](../tasks/team-collaboration-guide.md)
- [Traceability Matrix](../github-issues/traceability-matrix.md)
- [Task Management](../tasks/readme.md)

## Note
These files are kept for historical reference only. All active work is now managed through GitHub Issues.
EOF

echo "✓ Created archive README"

echo ""
echo "Legacy documentation archived successfully!"
echo ""
echo "Next steps:"
echo "1. Review the archived files in docs/archive/legacy-docs/"
echo "2. Update any remaining documentation references"
echo "3. Commit the changes to git"
echo "4. Update team on the new GitHub Issues workflow" 