# ADR-0013: Finalizer Mode for Task Completion

## Status
Proposed

## Context
The Nucleus project uses a structured workflow with Initiator mode for session kickoff and task planning. However, there's a need for a complementary Finalizer mode that ensures comprehensive task completion, documentation updates, and proper handoff to the next development cycle.

## Decision
Implement a **Finalizer mode** that provides a systematic approach to completing tasks, updating documentation, and preparing for the next development iteration. This mode will be triggered by the "finalize" command and will execute a comprehensive checklist of completion activities.

## Consequences

### Positive
- **Complete Documentation**: Ensures all changes are properly documented and traceable
- **Quality Assurance**: Reviews implementation against requirements and plans
- **Knowledge Preservation**: Captures architectural decisions and implementation details
- **Team Coordination**: Updates GitHub Issues and task logs for team visibility
- **Clean Handoff**: Prepares repository state for next development session
- **Compliance**: Ensures adherence to project workflow and documentation standards

### Negative
- **Time Investment**: Requires significant time to complete all finalization steps
- **Process Overhead**: May feel bureaucratic for simple changes
- **Dependency on Tools**: Requires access to GitHub, git, and other tools
- **Learning Curve**: Team members need to learn and follow the finalization process

## Implementation

### Trigger Command
- **Primary**: "finalize" - initiates the complete finalization workflow
- **Alternative**: "finish" - same as finalize

### Finalization Workflow Steps

#### 1. Memory Bank Review
- Read all memory bank files (projectbrief.md, activeContext.md, progress.md, systemPatterns.md, techContext.md, productContext.md)
- Understand current project state and context
- Identify any gaps in documentation

#### 2. Task and Implementation Review
- Review current task and implementation plans
- Verify all requirements have been met
- Check for any incomplete or partially implemented features
- Validate against original task objectives

#### 3. Git Status and Change Recap
- Run `git status` to see all changes made
- Review staged and unstaged changes
- Create comprehensive summary of modifications
- Identify any files that shouldn't be committed

#### 4. Documentation Updates

##### GitHub Issues Updates
- **PRD Issues**: Update with any requirement changes or clarifications
- **CRD Issues**: Add completed implementation details and acceptance criteria
- **Personal Task Issues**: Update status, add progress comments, close completed issues
- **Issue Linking**: Ensure all changes are properly linked to relevant issues

##### Architecture Decision Records (ADRs)
- Create new ADRs for significant architectural changes
- Use template from `docs/adr/ADR-template.md`
- Document decision context, consequences, and alternatives considered
- Store with sequential numbering in `docs/adr/`

##### User and Development Guides
- Update user guides for any new features or changed workflows
- Update development guides for new patterns or technical decisions
- Ensure documentation reflects current implementation state

#### 5. Memory Bank Updates
- **activeContext.md**: Update with current focus and recent changes
- **progress.md**: Move completed work to appropriate sections
- **systemPatterns.md**: Document any new patterns that emerged
- **techContext.md**: Update if technology choices changed

#### 6. Git Operations

##### Commit Strategy
- Stage all changes: `git add .`
- Write comprehensive commit message following conventional commits format
- Include issue references: `#issue-number`
- Describe the complete feature implementation

##### Branch Management
- Squash commits if multiple commits exist for single feature
- Use `git rebase -i HEAD~{number-of-commits}` for squashing
- Write comprehensive commit message for squashed changes
- Force push squashed branch: `git push origin {current-branch} --force-with-lease`

##### Remote Operations
- Pull latest changes from remote: `git pull origin main`
- Resolve any merge conflicts locally
- Push feature branch: `git push origin {current-branch}`

#### 7. Pull Request Creation
- Create pull request with detailed description
- Include branch name in PR title: `[issue-{number}] {description}`
- Link all related GitHub Issues in PR description
- Add appropriate labels and assignees
- Include implementation summary and testing notes

#### 8. Personal Task Log Updates
- Move completed tasks to "Done" section
- Update status of in-progress tasks
- Add completion notes and lessons learned
- Link to GitHub Issues and PRs

#### 9. Quality Assurance
- Verify all tests pass
- Check for any linting or code quality issues
- Review for security concerns
- Validate against project standards

#### 10. Handoff Preparation
- Identify next priority tasks
- Update task status in personal tasklog
- Prepare context for next development session
- Document any known issues or technical debt

### Mode Handoff Rule
After completing the Finalizer workflow, switch Cursor from Finalizer mode to the appropriate working mode (Plan, Act, or Code) before starting any new implementation work.

## Integration with Existing Workflow

### Relationship to Initiator Mode
- **Initiator**: Session kickoff, context review, task planning
- **Finalizer**: Task completion, documentation, handoff preparation
- **Complementary**: Both modes ensure complete development cycle coverage

### Relationship to Recap Protocol
- **Recap**: Session-level summary and documentation
- **Finalizer**: Task-level completion and handoff
- **Distinction**: Finalizer is more comprehensive and includes PR creation

## Success Criteria
- All changes are properly documented and traceable
- GitHub Issues reflect current implementation state
- Memory bank is updated with latest context
- Pull request is created and ready for review
- Personal tasklog accurately reflects completed work
- Repository is in clean state for next development session

## Future Considerations
- Automate parts of the finalization process where possible
- Create templates for common finalization scenarios
- Integrate with CI/CD pipelines for automated quality checks
- Develop metrics for finalization effectiveness

---

*This ADR complements [ADR-0012: Session Kickoff Mode](ADR-0012-session-kickoff-mode.md) to provide complete development cycle coverage.* 