# Git Feature-Branch Workflow & Pull Request Process

**Expected Knowledge for Year 3 Students**

This document provides a reference guide for the Git workflow used throughout the Software Engineering course. You are expected to be familiar with these concepts from previous coursework.

---

## 📋 Core Workflow: Feature Branch + Pull Request

### Step 1: Always Start from `main`

```bash
# Ensure you're on main and have the latest changes
git checkout main
git pull origin main
```

### Step 2: Create a Feature Branch

Branch naming convention: `feature/short-description` or `lab/week-X-task-name`

```bash
# Example for Lab 4 TimeEntry implementation
git checkout -b feature/timeentry-crud

# Example for Lab 6 evolutionary improvement
git checkout -b lab/week6-observability-enhancement
```

### Step 3: Work on Your Feature

Make frequent, small commits with clear messages:

```bash
git add .
git commit -m "Add TimeEntry entity and DTO definitions"

# Later...
git add .
git commit -m "Implement CreateTimeEntry command and handler"
```

**Commit message best practices:**
- Use imperative mood: "Add", "Implement", "Fix", not "Added", "Fixed"
- Be specific: "Add CreateTimeEntry endpoint" not "Update API"
- Reference issues if applicable: "Fix validation bug in TimeEntry (#23)"

### Step 4: Push Your Feature Branch

```bash
git push origin feature/timeentry-crud
```

### Step 5: Create a Pull Request (PR)

1. Go to your GitHub repository in the browser
2. GitHub will show a yellow banner: **"Compare & pull request"** → Click it
3. Fill in the PR template:
   - **Title:** Clear summary (e.g., "Implement TimeEntry CRUD - Lab 4")
   - **Description:** 
     - What does this PR do?
     - Which lab requirement does it fulfill?
     - How to test it?
   - **Reviewers:** (In a team setting, assign reviewers. For solo lab work, you can skip this.)

4. Click **"Create Pull Request"**

### Step 6: Merge the Pull Request

**After you (or your reviewer) approve:**

1. Click **"Merge pull request"** → **"Confirm merge"**
2. **Delete the feature branch** (GitHub will prompt you — click "Delete branch")

### Step 7: Update Your Local `main`

```bash
git checkout main
git pull origin main
git branch -d feature/timeentry-crud  # Delete local feature branch
```

---

## 🔄 Handling Merge Conflicts

If `main` has moved ahead while you were working on your feature branch:

```bash
# On your feature branch
git checkout feature/timeentry-crud
git fetch origin
git merge origin/main
```

If conflicts occur:
1. Git will mark conflicted files (look for `<<<<<<<`, `=======`, `>>>>>>>`)
2. Open the files, resolve conflicts manually
3. Stage the resolved files: `git add <filename>`
4. Complete the merge: `git commit`
5. Push: `git push origin feature/timeentry-crud`

---

## 📊 GitHub Projects Integration (Lab 2)

**Creating and organizing your backlog:**

1. Go to your repository → **Projects** tab → **New project**
2. Choose **"Board"** template
3. Create columns:
   - **Backlog** — all unstarted tasks
   - **In Progress** — what you're working on now
   - **Done** — completed tasks

4. Add issues or draft cards:
   - Click **"+ Add item"** in Backlog
   - Title: User story or task (e.g., "As a Manager, I want to create a Project")
   - Description: Acceptance criteria

5. Move cards across columns as you work

**Linking PRs to Project items:**
- In your PR description, reference the issue: `Closes #12` or `Resolves #5`
- GitHub will automatically move the card to **Done** when the PR is merged

---

## ✅ Best Practices

1. **Never commit directly to `main`** — always use a feature branch
2. **Keep branches short-lived** — merge within 1-2 days to avoid divergence
3. **Write descriptive PR descriptions** — help your future self and reviewers understand the change
4. **Run tests before pushing** — don't push broken code
5. **Delete merged branches** — keeps your repository clean

---

## 🚫 Common Mistakes to Avoid

| ❌ Don't | ✅ Do |
|---------|-------|
| `git commit -m "fixes"` | `git commit -m "Fix validation error in CreateTimeEntry endpoint"` |
| Work on `main` directly | Work on `feature/descriptive-name` |
| Push 50 commits at once | Commit frequently, push regularly |
| Leave merged branches undeleted | Delete branches after merge |
| Forget to pull before starting | Always `git pull origin main` first |

---

## 📚 Additional Resources

- [GitHub Flow Guide](https://guides.github.com/introduction/flow/) — Official GitHub workflow
- [Pro Git Book (Free)](https://git-scm.com/book/en/v2) — Chapters 2-3 cover branching
- [Conventional Commits](https://www.conventionalcommits.org/) — Advanced commit message format

---

**Questions?** Ask your instructor or refer to Git documentation: `git --help <command>`
