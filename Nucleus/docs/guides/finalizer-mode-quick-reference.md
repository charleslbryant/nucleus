# Finalizer Mode Quick Reference

## Trigger Commands
- `finalize` - Primary command
- `finish` - Alternative command

## 10-Step Finalization Checklist

### 1. Memory Bank Review
- [ ] Read all memory bank files
- [ ] Understand current project state
- [ ] Identify documentation gaps

### 2. Task and Implementation Review
- [ ] Verify all requirements met
- [ ] Check for incomplete features
- [ ] Validate against objectives

### 3. Git Status and Change Recap
- [ ] Run `git status`
- [ ] Review all changes
- [ ] Create change summary
- [ ] Identify files to exclude

### 4. Documentation Updates
- [ ] **GitHub Issues**: Update PRDs, CRDs, personal tasks
- [ ] **ADRs**: Create for architectural changes
- [ ] **Guides**: Update user and development docs
- [ ] **Issue Linking**: Use `#issue-number` format

### 5. Memory Bank Updates
- [ ] Update `activeContext.md`
- [ ] Update `progress.md`
- [ ] Update `systemPatterns.md` (if needed)
- [ ] Update `techContext.md` (if needed)

### 6. Git Operations
- [ ] Stage changes: `git add .`
- [ ] Write comprehensive commit message
- [ ] Include issue references: `#issue-number`
- [ ] Squash commits if needed: `git rebase -i HEAD~{n}`
- [ ] Pull latest: `git pull origin main`
- [ ] Push branch: `git push origin {branch}`

### 7. Pull Request Creation
- [ ] Use GitHub CLI: `gh pr create --title "[issue-{number}] {description}" --body "{description}" --base main --head {current-branch} --label {labels} --web`
- [ ] Title format: `[issue-{number}] {description}`
- [ ] Link all related issues
- [ ] Add labels and assignees
- [ ] Include testing notes
- [ ] Open PR in browser for final review

### 8. Personal Task Log Updates
- [ ] Move completed tasks to "Done"
- [ ] Update in-progress tasks
- [ ] Add completion notes
- [ ] Link to issues and PRs

### 9. Quality Assurance
- [ ] Verify tests pass
- [ ] Check code quality
- [ ] Review security concerns
- [ ] Validate against standards

### 10. Handoff Preparation
- [ ] Identify next priority tasks
- [ ] Update task status
- [ ] Prepare next session context
- [ ] Document known issues

## Commit Message Template
```
feat: implement {feature-name}

- {change 1}
- {change 2}
- {change 3}

Closes #{issue-number}
```

## PR Template
```markdown
## Summary
{feature description}

## Changes
- {change 1}
- {change 2}
- {change 3}

## Testing
- ✅ {test 1}
- ✅ {test 2}

## Related Issues
Closes #{issue-number}
```

## Mode Handoff
After finalization, switch to:
- **Plan**: For planning next features
- **Act**: For immediate implementation
- **Code**: For focused coding

## Common Commands
```bash
# Check status
git status

# Stage all changes
git add .

# Squash commits
git rebase -i HEAD~{number}

# Pull latest
git pull origin main

# Push branch
git push origin {branch-name}

# Force push (after squash)
git push origin {branch-name} --force-with-lease

# Create PR with GitHub CLI
gh pr create --title "[issue-{number}] {description}" --body "{description}" --base main --head {current-branch} --label {labels} --web
```

## Success Checklist
- [ ] All changes documented
- [ ] GitHub Issues updated
- [ ] PR created and ready
- [ ] Memory bank current
- [ ] Task log updated
- [ ] Repository clean
- [ ] Tests passing

---

*For detailed instructions, see [Finalizer Mode Guide](finalizer-mode-guide.md)* 