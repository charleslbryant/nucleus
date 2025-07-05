# Team Collaboration Guide

## Overview
This guide explains the new team collaboration approach for Nucleus, which combines GitHub Issues for team-level tracking with namespaced personal tasklogs for individual work.

## Architecture

### Team Level: GitHub Issues
- **PRD Requirements**: High-level product requirements tracked as GitHub Issues
- **CRD Epics**: Implementation plans and major features tracked as GitHub Issues
- **Labels**: `prd`, `crd`, `epic`, `phase-1`, `phase-2`, `phase-3`, `phase-4`
- **Assignees**: Team members responsible for implementation
- **Milestones**: Group related issues by sprint or phase

### Personal Level: Namespaced Tasklogs
- **Location**: `docs/tasks/personal/{username}/tasklog-{date}.md`
- **Format**: Daily work logs with GitHub Issue references
- **Scope**: Individual tasks and progress tracking
- **Linking**: Reference GitHub Issues with `#issue-number`

## Workflow

### 1. Creating New Features
1. **Create PRD Requirement**: Use GitHub Issue template for product requirements
2. **Create CRD Epic**: Use GitHub Issue template for implementation plans
3. **Link PRD to CRD**: Reference PRD issue in CRD issue
4. **Define Acceptance Criteria**: Clear deliverables and success metrics
5. **Assign Team Members**: Who will work on this feature

### 2. Personal Task Management
1. **Create Personal Tasklog**: Daily file in `docs/tasks/personal/{username}/`
2. **Link to Issues**: Reference GitHub Issues with `#issue-number`
3. **Track Progress**: Update task status as work progresses
4. **Document Work**: Include details, challenges, and solutions

### 3. Team Coordination
1. **Sprint Planning**: Use GitHub Projects or Milestones
2. **Daily Updates**: Update personal tasklogs and issue comments
3. **Sprint Review**: Create team sprint summary in `docs/tasks/team/`
4. **Retrospective**: Document learnings and process improvements

## File Structure

```
docs/tasks/
├── personal/
│   ├── charl/
│   │   ├── tasklog-2025-07-05.md
│   │   └── tasklog-2025-07-06.md
│   └── sarah/
│       ├── tasklog-2025-07-05.md
│       └── tasklog-2025-07-06.md
├── team/
│   ├── sprint-2025-07-05.md
│   └── sprint-2025-07-06.md
└── personal/
    └── tasklog-template.md
```

## GitHub Issue Templates

### PRD Requirement Template
- **Purpose**: Track high-level product requirements
- **Labels**: `prd`, `requirement`, `product`, `phase-X`
- **Content**: Overview, jobs-to-be-done, user journey, requirements, success criteria

### CRD Epic Template
- **Purpose**: Track implementation plans and major features
- **Labels**: `epic`, `crd`, `phase-X`
- **Content**: Overview, PRD link, acceptance criteria, tasks

### Personal Task Template
- **Purpose**: Individual tasks linked to CRD Epics
- **Labels**: `task`, `personal`
- **Content**: Task description, parent epic, acceptance criteria

## Best Practices

### For Team Members
1. **Update Daily**: Keep personal tasklogs current
2. **Link Issues**: Always reference GitHub Issues in personal logs
3. **Comment on Issues**: Update progress in issue comments
4. **Use Labels**: Apply appropriate labels for organization
5. **Document Decisions**: Capture important decisions and context

### For Team Leads
1. **Review Sprint Summaries**: Monitor team progress
2. **Update Epics**: Keep CRD Epics current with progress
3. **Coordinate Dependencies**: Manage blocking issues
4. **Plan Sprints**: Use GitHub Projects or Milestones
5. **Facilitate Communication**: Ensure team alignment

### For Documentation
1. **Keep PRDs Current**: Update as requirements evolve
2. **Maintain CRDs**: Keep change requests synchronized with issues
3. **Update Memory Bank**: Reflect current state in project documentation
4. **Link Everything**: Maintain traceability between docs and issues

## Migration from Old System

### Completed Migration
- ✅ **PRD Requirements**: Converted to GitHub Issues (#100, #101, #102, #103)
- ✅ **CRD Epics**: Converted to GitHub Issues (#121, #122, #123, #124)
- ✅ **Personal Tasklogs**: Created namespaced structure
- ✅ **Templates**: Created GitHub Issue templates
- ✅ **Documentation**: Updated task management approach
- ✅ **Traceability Matrix**: Complete chain from PRD to personal tasks

### Next Steps
1. **Create Remaining Issues**: Convert any remaining CRDs/PRDs to GitHub Issues
2. **Set Up GitHub Projects**: Create project board for sprint management
3. **Configure Labels**: Set up label system for organization
4. **Train Team**: Ensure all team members understand the new workflow
5. **Monitor Effectiveness**: Track how well the new system works
6. **Use Traceability Matrix**: Reference the matrix for complete issue relationships

## Benefits

### Team Collaboration
- ✅ **No Conflicts**: Namespaced personal logs prevent merge conflicts
- ✅ **Clear Ownership**: GitHub Issues show who's responsible
- ✅ **Better Visibility**: Team can see all work in one place
- ✅ **Automated Workflows**: GitHub Actions for CI/CD integration

### Individual Productivity
- ✅ **Personal Space**: Individual tasklogs for personal work tracking
- ✅ **Flexible Format**: Personal logs can include detailed notes
- ✅ **Easy Linking**: Simple reference to team-level work
- ✅ **No Interference**: Work independently without conflicts

### Project Management
- ✅ **Traceability**: Link personal work to team goals
- ✅ **Progress Tracking**: Clear visibility into project status
- ✅ **Scalability**: Works with any team size
- ✅ **Integration**: Connects with existing GitHub workflow

## Troubleshooting

### Common Issues
1. **Merge Conflicts**: Use namespaced personal logs to avoid conflicts
2. **Missing Links**: Always reference GitHub Issues in personal logs
3. **Outdated Information**: Keep both issues and docs synchronized
4. **Team Coordination**: Use sprint summaries for team alignment

### Getting Help
- Check this guide for workflow questions
- Review existing personal tasklogs for examples
- Ask team leads for clarification on process
- Use GitHub Issues for team communication 