# Trigger and Respond: A simple AgenticOps Automated Workflow with n8n

In this post, we’re going to build a basic workflow using n8n to form the basis for building out a complete Work OS.

It starts when someone submits a Google Form. The system sends them a confirmation email. That’s it.

It’s a small but complete system behavior. One input. One output. Enough to show how a Work OS starts to take shape and how n8n handles events, data, and actions without you having to manually manage any of it.

## What we’re building

We’ll connect a Google Form to a Google Sheet. Google does that part automatically. Then we’ll use n8n to monitor that sheet for new responses and send a confirmation email to the person who filled it out.

This example covers:

-   Input: the form submission
-   Processing: pulling the email and formatting a message
-   Output: sending a real email, automatically

It’s your first feedback loop. Something happens. Your system responds.

## Setting up n8n

If you want to get started quickly, use [n8n Cloud](https://docs.n8n.io/user-management/cloud-setup/). If you prefer to self-host, here’s the [setup guide](https://docs.n8n.io/hosting/). You can run it with Docker, install it via npm, or host it using services like Railway or Fly.io.

Once you’re in, open the n8n canvas. This is where your workflows live.

## Building the workflow

Start by adding the Google Sheets node. Set it to “Watch Rows.” Connect your Google account and point it at the sheet that collects form responses.

>   If you need help setting up a Google Form that saves to a Google Sheet, try out the GPT for this post, the ["Form Assistant: AgenticOps Work OS GPT"](https://chatgpt.com/g/g-68534ccfe554819187e009cb94533e59-form-assistant-agenticops-work-os).

Next, add an Email node. Use the email from the form as the recipient. Write a short message using dynamic fields, like:

>   Hi {{\$json["name"]}}, thanks for filling out the form. We’ll be in touch soon.

You now have two nodes. One watches. One responds.

Once you have everything setup, click “Execute workflow” and submit a test form. The system should catch the new entry and send the email automatically.

## What’s happening behind the scenes

The system is responding. That’s the core idea here.

It detects an external event. It receives structured data. It applies logic. It takes action.

This is how a Work OS starts to think. Not with scripts. Not with hacks. With designed structured behavior. Today, it’s not a superintelligent AI that just does whatever you ask it to do. It is engineered automated workflows, with or without code and AI.

You define what the system should do when something happens. n8n handles the flow.

You’re not just copying data. You’re shaping a response. That’s the core habit of system design. Something happens. The system replies with intent.

## What comes next

You can stop here. Or you can keep building.

You might log the submission to another sheet. Send it to an API. Post a message to Slack. Add logic and error handling.

That’s the idea. We’re not just automating. You’re designing your system of work. One workflow at a time.

Next, we’ll build on this and add more outputs, so the system starts acting across more tools and channels.

If you tried this or want to explore how your own work could behave more intelligently, let’s talk about it 💬, drop a comment below or message me on [X](https://x.com/charleslbryant) or [LinkedIn](https://www.linkedin.com/in/charleslbryant/).
