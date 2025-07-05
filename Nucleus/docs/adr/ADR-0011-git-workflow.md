# ADR-0011: Git Workflow and Branch Management Strategy

## Status
**Accepted**

## Context
The Nucleus project requires a Git workflow that supports:
- Team collaboration with multiple developers working simultaneously
- Integration with GitHub Issues for project management and traceability
- Frequent commits and safe merging practices
- Clear branch naming conventions that link to project management artifacts
- Prevention of merge conflicts and data corruption
- Scalable workflow that works with both small and large teams

Traditional Git workflows like Git Flow, GitHub Flow, and trunk-based development each have different trade-offs:
- **Git Flow**: Complex with multiple long-lived branches, good for release management but overkill for continuous delivery
- **GitHub Flow**: Simple but lacks structured release management
- **Trunk-based Development**: Fast but requires high discipline and can be risky for complex features

Our project needs a workflow that balances simplicity with proper project management integration and team coordination.

## Decision
We will use a **GitHub Issues-driven feature branch workflow** with the following characteristics:

### Branch Strategy
- **Main branch**: `main` - always deployable, contains the latest stable code
- **Feature branches**: `feature/issue-{number}-{description}` - created for each GitHub Issue
- **No long-lived development branches** - all work happens in feature branches
- **Direct merges to main** - feature branches are merged via pull requests

### Branch Naming Convention
- **Format**: `feature/issue-{number}-{description}`
- **Examples**: 
  - `feature/issue-123-production-deployment`
  - `feature/issue-124-authentication-system`
- **Requirement**: Every branch must have a corresponding GitHub Issue
- **Fallback**: If no issue exists, create one first, then create the branch

### Workflow Process
1. **Issue Creation**: Create GitHub Issue for any task without one
2. **Branch Creation**: Create feature branch with issue number in name
3. **Development**: Work in feature branch with frequent commits
4. **Synchronization**: Pull and merge main before pushing
5. **Pull Request**: Create PR with issue number in title and description
6. **Review and Merge**: Merge to main via pull request
7. **Cleanup**: Delete feature branch after merge

### Commit and Push Strategy
- **Frequent commits**: Commit often with descriptive messages
- **Conventional commits**: Use conventional commit format when possible
- **Pull before push**: Always `git pull origin main` before pushing
- **Resolve conflicts**: Handle merge conflicts locally before pushing
- **Issue linking**: Reference GitHub Issues in commit messages using `#issue-number`

### Team Synchronization
- **GitHub Issues**: Central source of truth for all work
- **Personal tasklogs**: Namespaced by username to prevent conflicts
- **Team coordination**: Sprint summaries and traceability matrix
- **Documentation updates**: Keep all docs in sync with GitHub Issues

## Consequences

### Positive Consequences
- **Clear traceability**: Every branch links to a GitHub Issue
- **No merge conflicts**: Feature branches prevent concurrent file modifications
- **Team coordination**: GitHub Issues provide centralized project management
- **Frequent integration**: Regular pulls from main reduce integration debt
- **Simple workflow**: Easy to understand and follow
- **Scalable**: Works with any team size
- **GitHub integration**: Leverages GitHub's project management features

### Negative Consequences
- **Issue overhead**: Requires creating GitHub Issues for all work
- **Branch proliferation**: Many short-lived branches
- **Documentation burden**: Must keep GitHub Issues and local docs in sync
- **Learning curve**: Team must understand both Git and GitHub Issues workflow

### Neutral Consequences
- **No release branches**: Simpler but less structured release management
- **Direct to main**: Faster but requires good testing practices
- **Issue dependency**: Work is tied to GitHub Issues structure

## Rationale

### Why Not Git Flow?
- **Overkill**: Git Flow is designed for complex release management
- **Branch complexity**: Too many long-lived branches for our needs
- **Slower integration**: Feature branches live longer, increasing integration debt
- **Team size**: We don't need the complexity of release/hotfix branches

### Why Not Trunk-Based Development?
- **Risk**: Direct commits to main can be risky for complex features
- **Team coordination**: Harder to track work without feature branches
- **Review process**: Pull requests provide better code review opportunities
- **Project management**: No natural integration with GitHub Issues

### Why This Approach?
- **Balanced complexity**: Simple enough for small teams, scalable for growth
- **GitHub integration**: Natural fit with our GitHub Issues-based project management
- **Traceability**: Every piece of work is linked to project management artifacts
- **Team safety**: Feature branches prevent accidental overwrites
- **Frequent integration**: Regular pulls from main reduce integration problems

### Key Trade-offs Evaluated
- **Simplicity vs. Structure**: Chose structured approach with GitHub Issues
- **Branch overhead vs. Safety**: Chose safety with feature branches
- **Centralized vs. Distributed**: Chose centralized GitHub Issues with distributed personal tasklogs
- **Speed vs. Coordination**: Chose coordination through proper issue management

## Related Decisions
- [ADR-0009-vue-admin-dashboard.md](ADR-0009-vue-admin-dashboard.md) - Frontend architecture decisions
- [ADR-0010-development-environment-configuration.md](ADR-0010-development-environment-configuration.md) - Development setup
- Team collaboration guide in `docs/tasks/team-collaboration-guide.md`
- Product management process in `docs/guides/product-management.md`

## References
- [Git Flow](https://nvie.com/posts/a-successful-git-branching-model/) - Original Git Flow documentation
- [GitHub Flow](https://guides.github.com/introduction/flow/) - GitHub's recommended workflow
- [Trunk-Based Development](https://trunkbaseddevelopment.com/) - Alternative workflow approach
- [Conventional Commits](https://www.conventionalcommits.org/) - Commit message format
- [GitHub Issues](https://docs.github.com/en/issues) - GitHub Issues documentation 