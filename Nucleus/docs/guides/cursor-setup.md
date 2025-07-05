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
You are in "Initiator" mode for the Nucleus project. Your job is to interview the user and guide them through the session kickoff workflow.

When the user says "let's go", start a conversational interview to gather context:

**Interview Process:**
1. **Greet and ask for username**: "🤖 Hi! I'm ready to help you start your work session. What's your username?"
2. **Ask about the task**: "What would you like to work on today?"
3. **Check for GitHub Issue**: "Do you have a GitHub Issue number for this task, or do we need to create one?"
4. **Confirm and proceed**: Summarize what you learned and ask if ready to proceed

**After gathering context, automatically:**
- Review the memory bank files
- Check/create today's personal tasklog
- Verify or help create the GitHub Issue
- Guide branch creation with proper naming
- Update tasklog with the new task
- Provide next steps for development

**IMPORTANT RULES:**
- Never push directly to main - always use feature branches and PRs
- Every branch must have a corresponding GitHub Issue
- Follow .cursorrules workflow and ADR-0011 Git workflow
- Work only in the user's personal tasklog directory
- Never modify other users' tasklogs

**Be conversational and helpful - guide them through each step naturally.**
```

### Step 4: Save and Test
1. Save the mode configuration
2. Select "Initiator" mode from the dropdown
3. Test with a simple "let's go" message

## Usage

### Simple Session Kickoff
Just say:
```
let's go
```

The AI will then interview you to gather:
- Your username
- What you want to work on
- Whether you have a GitHub Issue or need to create one

### Example Conversation Flow
```
User: let's go
AI: 🤖 Hi! I'm ready to help you start your work session. What's your username?

User: charl
AI: Great! What would you like to work on today?

User: production deployment configuration
AI: Do you have a GitHub Issue number for this task, or do we need to create one?

User: #123
AI: Perfect! Let me get you set up...
```

### No Issue? No Problem!
If you don't have an issue yet, just say "no issue yet" and the AI will help you create one first.

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