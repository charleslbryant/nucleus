# Finalizer Mode Guide

## Overview

The **Finalizer Mode** is a comprehensive task completion workflow that ensures all changes are properly documented, tested, and ready for team review. It complements the [Initiator Mode](../guides/cursor-setup.md) to provide complete development cycle coverage.

## When to Use Finalizer Mode

Use Finalizer Mode when:
- ✅ A feature implementation is complete
- ✅ All requirements have been met
- ✅ Code is tested and working
- ✅ Ready to create a pull request
- ✅ Need to update documentation and handoff

**Trigger Commands:**
- `finalize` - Primary command
- `finish` - Alternative command

## Finalization Workflow

### 1. Memory Bank Review

**Purpose:** Understand current project state and context

**Actions:**
- Read all memory bank files:
  - `projectbrief.md` - Project goals and scope
  - `activeContext.md` - Current focus and recent changes
  - `progress.md` - Completed work and current status
  - `systemPatterns.md` - Architecture patterns in use
  - `techContext.md` - Technology choices and constraints
  - `productContext.md` - Product vision and user needs

**Output:** Clear understanding of project context and any documentation gaps

### 2. Task and Implementation Review

**Purpose:** Verify all requirements have been met

**Actions:**
- Review current task and implementation plans
- Verify all requirements have been met
- Check for any incomplete or partially implemented features
- Validate against original task objectives
- Identify any scope creep or missing functionality

**Output:** Confirmation that implementation matches requirements

### 3. Git Status and Change Recap

**Purpose:** Understand all changes made during the session

**Actions:**
- Run `git status` to see all changes
- Review staged and unstaged changes
- Create comprehensive summary of modifications
- Identify any files that shouldn't be committed
- Check for sensitive information or temporary files

**Output:** Complete understanding of all changes to be committed

### 4. Documentation Updates

#### GitHub Issues Updates

**Purpose:** Keep team informed of progress and completion

**Actions:**
- **PRD Issues**: Update with any requirement changes or clarifications
- **CRD Issues**: Add completed implementation details and acceptance criteria
- **Personal Task Issues**: Update status, add progress comments, close completed issues
- **Issue Linking**: Ensure all changes are properly linked using `#issue-number`

**Example Issue Update:**
```
✅ Implementation Complete

**Completed:**
- JWT authentication system implemented
- Login/logout functionality working
- Frontend integration complete
- Backend API endpoints functional
- Tests passing

**Ready for review and merge.**
```

#### Architecture Decision Records (ADRs)

**Purpose:** Document significant technical decisions for future reference

**Actions:**
- Create new ADRs for significant architectural changes
- Use template from `docs/adr/ADR-template.md`
- Document decision context, consequences, and alternatives considered
- Store with sequential numbering in `docs/adr/`

**When to Create ADRs:**
- New technology choices
- Architecture pattern changes
- Database schema modifications
- API design decisions
- Security implementation choices

#### User and Development Guides

**Purpose:** Keep documentation current with implementation

**Actions:**
- Update user guides for any new features or changed workflows
- Update development guides for new patterns or technical decisions
- Ensure documentation reflects current implementation state
- Add examples and troubleshooting information

### 5. Memory Bank Updates

**Purpose:** Preserve current state for next development session

**Actions:**
- **activeContext.md**: Update with current focus and recent changes
- **progress.md**: Move completed work to appropriate sections
- **systemPatterns.md**: Document any new patterns that emerged
- **techContext.md**: Update if technology choices changed

### 6. Git Operations

#### Commit Strategy

**Purpose:** Create clean, traceable commit history

**Actions:**
- Stage all changes: `git add .`
- Write comprehensive commit message following conventional commits format
- Include issue references: `#issue-number`
- Describe the complete feature implementation

**Commit Message Format:**
```
feat: implement JWT authentication system

- Add JWT token generation and validation
- Implement login/logout endpoints
- Create authentication middleware
- Add frontend authentication store
- Update user entity with authentication fields

Closes #8
```

#### Branch Management

**Purpose:** Prepare clean branch for pull request

**Actions:**
- Squash commits if multiple commits exist for single feature
- Use `git rebase -i HEAD~{number-of-commits}` for squashing
- Write comprehensive commit message for squashed changes
- Force push squashed branch: `git push origin {current-branch} --force-with-lease`

#### Remote Operations

**Purpose:** Sync with remote repository and resolve conflicts

**Actions:**
- Pull latest changes from remote: `git pull origin main`
- Resolve any merge conflicts locally
- Push feature branch: `git push origin {current-branch}`

### 7. Pull Request Creation

**Purpose:** Submit changes for team review

**Actions:**
- Use GitHub CLI to automatically create pull request: `gh pr create --title "[issue-{number}] {description}" --body "{detailed-description}" --base main --head {current-branch} --label {appropriate-labels} --web`
- Include branch name in PR title: `[issue-{number}] {description}`
- Link all related GitHub Issues in PR description
- Add appropriate labels and assignees
- Include implementation summary and testing notes
- Open PR in browser for final review and submission

**PR Template:**
```markdown
## Summary
Implements JWT authentication system for Nucleus admin dashboard.

## Changes
- Added JWT token generation and validation
- Implemented login/logout API endpoints
- Created authentication middleware
- Added frontend authentication store with Pinia
- Updated user entity with authentication fields

## Testing
- ✅ Backend API tests passing
- ✅ Frontend integration working
- ✅ Login/logout flow tested
- ✅ Token validation working

## Related Issues
Closes #8

## Screenshots
[Add screenshots if UI changes]
```

### 8. Personal Task Log Updates

**Purpose:** Track personal progress and maintain task history

**Actions:**
- Move completed tasks to "Done" section
- Update status of in-progress tasks
- Add completion notes and lessons learned
- Link to GitHub Issues and PRs

### 9. Quality Assurance

**Purpose:** Ensure code meets project standards

**Actions:**
- Verify all tests pass
- Check for any linting or code quality issues
- Review for security concerns
- Validate against project standards

### 10. Handoff Preparation

**Purpose:** Prepare for next development session

**Actions:**
- Identify next priority tasks
- Update task status in personal tasklog
- Prepare context for next development session
- Document any known issues or technical debt

## Mode Handoff

After completing the Finalizer workflow, switch Cursor from Finalizer mode to the appropriate working mode:
- **Plan**: For planning next features or tasks
- **Act**: For immediate implementation work
- **Code**: For focused coding sessions

## Best Practices

### Do's
- ✅ Complete all steps thoroughly
- ✅ Write clear, descriptive commit messages
- ✅ Link all changes to GitHub Issues
- ✅ Update documentation comprehensively
- ✅ Test thoroughly before finalizing
- ✅ Create meaningful PR descriptions

### Don'ts
- ❌ Skip documentation updates
- ❌ Rush through quality assurance
- ❌ Forget to link issues
- ❌ Leave incomplete implementations
- ❌ Ignore merge conflicts
- ❌ Create vague commit messages

## Troubleshooting

### Common Issues

**Merge Conflicts:**
- Resolve conflicts locally before pushing
- Use `git status` to identify conflicted files
- Test after resolving conflicts

**Failed Tests:**
- Fix all failing tests before finalizing
- Update tests if implementation changed
- Document any test changes

**Documentation Gaps:**
- Identify missing documentation during review
- Create ADRs for architectural decisions
- Update guides for new features

**Git Issues:**
- Use `git status` to understand current state
- Check for unstaged changes
- Verify branch name and remote configuration

## Integration with Team Workflow

### Relationship to Other Modes
- **Initiator Mode**: Session kickoff and planning
- **Recap Protocol**: Session-level summary
- **Finalizer Mode**: Task-level completion and handoff

### Team Coordination
- Update GitHub Issues for team visibility
- Create clear PR descriptions for reviewers
- Link all changes to relevant issues
- Update task logs for progress tracking

## Success Metrics

A successful finalization should result in:
- ✅ All changes properly documented
- ✅ GitHub Issues updated and linked
- ✅ Pull request created and ready for review
- ✅ Memory bank reflects current state
- ✅ Personal tasklog accurately updated
- ✅ Repository in clean state
- ✅ Tests passing and code quality maintained

## Complete Finalizer Mode Instruction Prompt

Here's the complete instruction prompt for configuring Finalizer mode in Cursor:

```
You are in "Finalizer" mode for the Nucleus project. Your job is to guide the user through the comprehensive task completion and handoff workflow.

When the user says "finalize" or "finish", start a conversational review process:

**Review Process:**
1. **Greet and confirm completion**: "🤖 Hi! I'm ready to help you finalize your work. What task have you completed today?"
2. **Check task status**: "Are all the requirements for this task met and tested?"
3. **Verify GitHub Issue**: "What's the GitHub Issue number for this completed task?"
4. **Confirm readiness**: Summarize what you learned and ask if ready to proceed with finalization

**After gathering context, automatically execute the 10-step finalization workflow:**

**Step 1: Memory Bank Review**
- Read all memory bank files (projectbrief.md, activeContext.md, progress.md, systemPatterns.md, techContext.md, productContext.md)
- Understand current project state and identify any documentation gaps

**Step 2: Task and Implementation Review**
- Review current task and implementation plans
- Verify all requirements have been met
- Check for any incomplete or partially implemented features
- Validate against original task objectives

**Step 3: Git Status and Change Recap**
- Run `git status` to see all changes made
- Review staged and unstaged changes
- Create comprehensive summary of modifications
- Identify any files that shouldn't be committed

**Step 4: Documentation Updates**
- **GitHub Issues Updates**: Update PRD issues with requirement changes, CRD issues with implementation details, personal task issues with status changes
- **Architecture Decision Records (ADRs)**: Create new ADRs for significant architectural changes using template from `docs/adr/ADR-template.md`
- **User and Development Guides**: Update guides for new features or changed workflows
- **Issue Linking**: Ensure all changes are properly linked using `#issue-number`

**Step 5: Memory Bank Updates**
- Update activeContext.md with current focus and recent changes
- Update progress.md with completed work and current status
- Update systemPatterns.md if new patterns emerged
- Update techContext.md if technology choices changed

**Step 6: Git Operations**
- Stage all changes: `git add .`
- Write comprehensive commit message following conventional commits format with issue references
- Squash commits if multiple commits exist for single feature: `git rebase -i HEAD~{number-of-commits}`
- Pull latest changes: `git pull origin main`
- Push feature branch: `git push origin {current-branch}`

**Step 7: Pull Request Creation**
- Use GitHub CLI to automatically create pull request: `gh pr create --title "[issue-{number}] {description}" --body "{detailed-description}" --base main --head {current-branch} --label {appropriate-labels} --web`
- Include branch name in PR title: `[issue-{number}] {description}`
- Link all related GitHub Issues in PR description using `#issue-number`
- Add appropriate labels and assignees
- Include implementation summary and testing notes
- Open PR in browser for final review and submission

**Step 8: Personal Task Log Updates**
- Move completed tasks to "Done" section
- Update status of in-progress tasks
- Add completion notes and lessons learned
- Link to GitHub Issues and PRs

**Step 9: Quality Assurance**
- Verify all tests pass
- Check for any linting or code quality issues
- Review for security concerns
- Validate against project standards

**Step 10: Handoff Preparation**
- Identify next priority tasks
- Update task status in personal tasklog
- Prepare context for next development session
- Document any known issues or technical debt

**IMPORTANT RULES:**
- Never push directly to main - always use feature branches and PRs
- Every branch must have a corresponding GitHub Issue
- Follow .cursorrules workflow and ADR-0013 Finalizer mode
- Work only in the user's personal tasklog directory
- Never modify other users' tasklogs
- Always create comprehensive commit messages and PR descriptions
- Ensure all changes are properly documented and traceable

**Be conversational and helpful - guide them through each step naturally, explaining what you're doing and why.**

### Step 4: Save and Test
1. Save the mode configuration
2. Select "Finalizer" mode from the dropdown
3. Test with a simple "finalize" message

## Usage

### Simple Task Finalization
Just say:
```
finalize
```
or
```
finish
```

The AI will then guide you through:
- Confirming what task you completed
- Verifying all requirements are met
- Checking the GitHub Issue number
- Executing the complete 10-step finalization workflow

### Example Conversation Flow
```
User: finalize
AI: 🤖 Hi! I'm ready to help you finalize your work. What task have you completed today?

User: JWT authentication system
AI: Are all the requirements for this task met and tested?

User: yes, everything is working
AI: What's the GitHub Issue number for this completed task?

User: #8
AI: Perfect! Let me help you finalize this authentication system implementation...

[AI then executes the 10-step finalization workflow automatically]
```

### Key Differences from Initiator Mode
- **Focus**: Task completion vs. task planning
- **Workflow**: 10-step comprehensive finalization vs. session kickoff
- **Outcome**: Pull request creation and handoff vs. branch creation and planning
- **Documentation**: Updates existing docs vs. creates new task entries
- **Git Operations**: Commits, squashes, and creates PRs vs. creates new branches
```

## Configuration Steps

### Step 1: Create Mode Configuration
1. Open Cursor settings
2. Navigate to AI modes or custom modes section
3. Create a new mode called "Finalizer"

### Step 2: Add the Instruction Prompt
1. Copy the complete instruction prompt above
2. Paste it into the mode configuration
3. Save the configuration

### Step 3: Set Trigger Commands
1. Configure trigger commands: `finalize`, `finish`
2. Set mode behavior to execute the workflow automatically
3. Ensure proper handoff to other modes after completion

### Step 4: Test the Mode
1. Select "Finalizer" mode from the dropdown
2. Test with a simple "finalize" message
3. Verify the conversational flow works correctly
4. Test the automatic execution of the 10-step workflow

---

*For more information, see the [Architecture Decision Record](../adr/ADR-0013-finalizer-mode.md) and [Cursor Rules](../../.cursorrules).* 