# Task Management

## Purpose
Task management for Nucleus combines GitHub Issues for team-level tracking with namespaced personal tasklogs for individual work. This approach enables effective team collaboration while maintaining personal productivity.

## New Team Collaboration Approach

### Team Level: GitHub Issues
- **PRD Requirements**: High-level product requirements tracked as GitHub Issues
- **CRD Epics**: Implementation plans and major features tracked as GitHub Issues
- **Labels**: `prd`, `epic`, `crd`, `phase-1`, `phase-2`, `phase-3`, `phase-4`
- **Templates**: GitHub Issue templates for consistent formatting
- **Integration**: Connects with GitHub Actions, PRs, and project boards

### Personal Level: Namespaced Tasklogs
- **Location**: `docs/tasks/personal/{username}/tasklog-{date}.md`
- **Format**: Daily work logs with GitHub Issue references (`#issue-number`)
- **Scope**: Individual tasks, progress tracking, and detailed notes
- **Benefits**: No merge conflicts, personal space for detailed work tracking

## Directory Structure
```
docs/tasks/
├── personal/
│   ├── charl/                    # Personal tasklogs for charl
│   │   ├── tasklog-2025-07-05.md
│   │   └── tasklog-template.md
│   └── {username}/               # Personal tasklogs for other team members
├── team/
│   └── sprint-2025-07-05.md      # Team-level sprint summaries
└── team-collaboration-guide.md   # Complete workflow documentation
```

## Usage

### For Team Members
1. **Daily Work**: Create personal tasklog in `docs/tasks/personal/{username}/`
2. **Link Issues**: Reference GitHub Issues with `#issue-number`
3. **Track Progress**: Update task status as work progresses
4. **Document Work**: Include details, challenges, and solutions

### For Team Leads
1. **Create Epics**: Use GitHub Issue templates for major features
2. **Assign Work**: Assign team members to GitHub Issues
3. **Monitor Progress**: Review personal tasklogs and issue updates
4. **Sprint Planning**: Use GitHub Projects or Milestones

## Migration Status
- ✅ **Completed**: PRD Requirements converted to GitHub Issues (#100, #101, #102, #103)
- ✅ **Completed**: CRD Epics converted to GitHub Issues (#121, #122, #123, #124)
- ✅ **Completed**: Namespaced personal tasklog structure created
- ✅ **Completed**: GitHub Issue templates created
- ✅ **Completed**: Team collaboration guide documented
- ✅ **Completed**: Traceability matrix created

## Related Docs
- [Team Collaboration Guide](team-collaboration-guide.md) — Complete workflow documentation
- [Traceability Matrix](../github-issues/traceability-matrix.md) — Complete issue relationships
- [CRDs](../roadmap/change-requests/readme.md) — Change Request Documents
- [PRDs](../roadmap/product-requirements/readme.md) — Product Requirements Documents
- [GitHub Issues](../github-issues/) — Issue content and templates 