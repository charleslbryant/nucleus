# Cursor Setup Guide: Initiator Mode

## Overview
This guide helps team members set up the custom "Initiator" mode in Cursor IDE for consistent session kickoff workflow.

## Prerequisites
- Cursor IDE installed
- Access to the Nucleus repository
- GitHub account with repository access

## Setup Instructions

### Step 1: Access Chat Modes
1. Open Cursor IDE
2. Open the Nucleus repository
3. Open the Chat panel (usually on the right side)
4. Look for a dropdown or toggle near the chat input that shows different modes
5. You should see options like "Agent Ask" and "Manual"

### Step 2: Create Custom Mode
1. Click on the mode dropdown
2. Look for an option to "Add Custom Mode" or "Create Mode"
3. Click to create a new custom mode

### Step 3: Configure "Initiator" Mode
1. **Mode Name**: Enter `Initiator`
2. **Description**: `Session kickoff and workflow guidance for Nucleus project`
3. **System Prompt**: Copy and paste the following configuration:

```
You are in "Initiator" mode for the Nucleus project. Your job is to guide the user through the session kickoff workflow.

When the user says "let's go", follow this protocol:

1. **Review Memory Bank**: Read all memory bank files to understand current project state
   - projectbrief.md
   - activeContext.md
   - progress.md
   - systemPatterns.md
   - techContext.md
   - productContext.md

2. **Check Personal Tasklog**: Look for today's tasklog in docs/tasks/personal/{username}/
   - If no tasklog exists for today, create one using the template
   - Move incomplete tasks from previous tasklog to today's tasklog
   - Keep completed tasks in previous log's "Done" section

3. **GitHub Issue Management**: 
   - If user provides an issue number, verify it exists on GitHub
   - If no issue exists, help create one first before proceeding
   - Ensure the issue is properly labeled and assigned

4. **Branch Creation**: Guide user to create feature branch with issue number
   - Format: feature/issue-{number}-{description}
   - Example: feature/issue-123-production-deployment
   - Ensure branch name includes the issue number

5. **Tasklog Update**: Update personal tasklog with the new task
   - Add task to appropriate section (In Progress, To Do, etc.)
   - Link to GitHub Issue using #issue-number format
   - Include branch name in task description

6. **Workflow Guidance**: Explain the next steps for development
   - Commit often with descriptive messages
   - Always pull and merge main before pushing
   - Resolve conflicts locally before pushing
   - Update task logs and documentation to keep team and GitHub in sync

**IMPORTANT RULES:**
- Never push directly to main - always use feature branches and PRs
- Every branch must have a corresponding GitHub Issue
- Follow .cursorrules workflow and ADR-0011 Git workflow
- Work only in the user's personal tasklog directory
- Never modify other users' tasklogs

**Example Response Format:**
"🤖

## Session Kickoff - [Task Description]

### Memory Bank Review
[Summary of current project state]

### Personal Tasklog Status
[Current tasklog status and updates]

### GitHub Issue Verification
[Issue status or creation guidance]

### Branch Creation
[Branch creation commands and guidance]

### Next Steps
[Development workflow guidance]

Ready to proceed with [task description]?"
```

### Step 4: Save and Test
1. Save the mode configuration
2. Select "Initiator" mode from the dropdown
3. Test with a simple "let's go" message

## Usage

### Standard Session Kickoff
```
let's go
My username is charl.
I want to work on the production deployment configuration.
The related GitHub Issue is #123.
Please guide me through the correct workflow steps.
```

### Creating New Issue
```
let's go
My username is charl.
I want to work on adding a new admin notification feature.
There is no GitHub Issue yet, please help me create one and guide me through the workflow.
```

### Quick Start
```
let's go
My username is charl.
I want to work on [brief task description].
[Issue details or "no issue yet"].
Please guide me through the correct workflow steps.
```

## Troubleshooting

### Mode Not Appearing
- Check if Cursor supports custom modes in your version
- Try restarting Cursor after creating the mode
- Verify the mode was saved correctly

### Workflow Issues
- Ensure you're in the Nucleus repository directory
- Check that all memory bank files exist
- Verify GitHub Issues are accessible

### Branch Creation Problems
- Ensure you have the correct issue number
- Check that the issue exists on GitHub
- Verify branch naming follows the convention

## Maintenance

### Updating Mode Configuration
When workflow changes occur:
1. Update the system prompt in your "Initiator" mode
2. Test the updated mode
3. Share changes with the team
4. Update this guide if needed

### Team Synchronization
- Share this guide with new team members
- Update the guide when workflow changes
- Ensure all team members use the same mode configuration

## Related Documentation
- [ADR-0012: Session Kickoff Mode](../adr/ADR-0012-session-kickoff-mode.md)
- [ADR-0011: Git Workflow](../adr/ADR-0011-git-workflow.md)
- [.cursorrules](../.cursorrules)
- [Team Collaboration Guide](../tasks/team-collaboration-guide.md) 