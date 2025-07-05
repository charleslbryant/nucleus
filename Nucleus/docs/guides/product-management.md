# Product Management

I want to organize our product development. I want to add PRDs (product requirement documents) to \\docs\\roadmap\\product-requirements. Write a readme.md about our PRD, a template for PRDs, and PRDs for Nucleus. PRDs are our feature requirements, so we need one for each product feature. Features solve user problems for various jobs-to-be-done. Features implement a journey to fulfill desired needs and wants to cure some pain extracting value from the journey.

We prioritize PRDs in a roadmap that order PRDs into buckets of value to deliver now, next, and in the future. Write a readme.md about our roadmap, a template for a roadmap, and write the current roadmap. We will maintain one roadmap in docs\\roadmap.

PRDs prioritized to do now will get CRDs (change request documents) in docs\\roadmap\\change-requests. Write a readme.md about our CRD, a template for CRDs, and the CRDs for the PRD we are doing now. CRDs define the tasks we need to do to deliver a PRD.

Tasks are tracked in task logs in \\docs\\tasks. Write a readme.md about task logs, a template for task logs, and the tasklog for today. We should log tasks in separate lists by task status: in progress - tasks being actively worked on, on hold - moved from in progress to to an inactive state, to do - tasks in backlog, done - tasks completed today, cancelled - the tasks removed from CRD.

I want to implement this product management strategy. I also want to add this to the cursorrules so you remember how we manage the product.

At the beginning of a session you will review the memory bank, review the roadmap, current prd, crds for the prd, and the task log. If there is no task log for today copy the last task log to today. Then determine what task should be worked on next with the operator. Create a plan in chat, then work with the operator to execute the plan. Once all steps of the plan are complete or when the operator requests one, do a recap which means review what was done in the chat session, update task log, CRD, and PRD as necessary, do a git add, git status, write commit message and commit, push, and create a pull request. We need to update the current process to blend all of this into the cursorrules.
