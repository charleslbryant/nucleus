# Nucleus Prototype

## Purpose

**Nucleus** is a lightweight observability and evaluation layer for AgenticOPS AI-enabled automation workflows. It integrates with tools like **n8n**, **Make**, **Power Automate**, or custom systems to:

-   Capture and log node executions
-   Automatically evaluate outputs using an LLM as judge
-   Optionally trigger human feedback (via IM/email message)
-   Provide actionable insights for monitoring and improving workflows

## Goals

1.  Track node performance within external workflows
2.  Score and log the quality of outputs automatically
3.  Trigger manual review when output quality is low
4.  Use a single API endpoint and a reusable HTTP node pattern
5.  Use clean schema, internal PKs, and allow multi-platform compatibility

## Workflow

**Example use case:** Outlook email → summarize → evaluate → human feedback (optional)

```plaintext
[Trigger] New Outlook Email
   ⬇
[Node]    OpenAI Summarize
   ⬇
[Node]    Nucleus Evaluate (HTTP POST to /api/evaluate)
   ⬇
[IF Node] If score < 3.5 → Notify human via Slack/email
```

## Schema

### `workflow_run`

| Column                | Type        | Description                     |
|-----------------------|-------------|---------------------------------|
| id (PK)               | UUID        | Internal unique ID              |
| platform              | TEXT        | Platform name (e.g., n8n)       |
| external_workflow_id  | TEXT        | External workflow ID            |
| workflow_name         | TEXT        | Workflow display name           |
| external_execution_id | TEXT        | External execution ID           |
| session_id            | TEXT        | Optional session correlation ID |
| triggered_by          | TEXT        | Source (webhook, cron, etc.)    |
| mode                  | TEXT        | test or production              |
| started_at            | TIMESTAMPTZ | Start time                      |
| completed_at          | TIMESTAMPTZ | End time                        |
| success               | BOOLEAN     | Whether the workflow succeeded  |
| error_message         | TEXT        | Error details if failed         |

### `model_run`

| Column           | Type        | Description                      |
|------------------|-------------|----------------------------------|
| id (PK)          | UUID        | Internal unique ID               |
| workflow_run_id  | UUID        | FK to workflow_run               |
| platform         | TEXT        | Platform name                    |
| external_node_id | TEXT        | AI node name or ID               |
| task             | TEXT        | summarize, draft, classify, etc. |
| model_name       | TEXT        | gpt-4, claude, etc.              |
| model_provider   | TEXT        | openai, anthropic, etc.          |
| prompt_version   | TEXT        | Optional version of prompt used  |
| input_data       | JSONB       | Input payload sent to model      |
| output_data      | JSONB       | Output returned by model         |
| created_at       | TIMESTAMPTZ | Timestamp of model execution     |

### `evaluation`

| Column         | Type        | Description                      |
|----------------|-------------|----------------------------------|
| id (PK)        | UUID        | Internal unique ID               |
| model_run_id   | UUID        | FK to model_run                  |
| evaluator_type | TEXT        | llm, human, rule                 |
| score          | NUMERIC     | 0–5 scale evaluation             |
| pass           | BOOLEAN     | Whether output passed evaluation |
| feedback       | TEXT        | Rationale for score              |
| created_at     | TIMESTAMPTZ | Evaluation timestamp             |

## Roadmap

### Phase 1: Foundation

-   [ ] Create schema with internal UUIDs, external metadata fields
    -   Design tables: `workflow_run`, `model_run`, `evaluation`
    -   Use PostgreSQL with JSONB for flexible payload storage
-   [ ] Create `/api/evaluate` endpoint:
    -   Accept POST requests from workflow systems (e.g. n8n)
    -   Validate and extract metadata and model input/output
    -   Log all activity to the database
    -   Return evaluation results (score, pass/fail, feedback)

### Phase 2: Platform Integration (n8n)

-   [ ] Use existing **HTTP Request Node** to call `/api/evaluate`
    -   Assemble input/output, workflow and node metadata in JSON
    -   Send it directly to Nucleus for evaluation
-   [ ] Provide JSON templates for inputs
    -   Make reusable snippets for common AI tasks (summarize, classify, generate)
-   [ ] Use native **If Node** to branch based on evaluation score
    -   Configure threshold (e.g. score \< 3.5) to decide next steps
-   [ ] Notify humans via Slack/email when score \< threshold
    -   Use standard Slack or email nodes with templated message

### Phase 3: Observability

-   [ ] Create admin dashboard
    -   View recent evaluations and score trends
-   [ ] Build evaluation detail view for model_run + evaluation
    -   Include original input/output and feedback
-   [ ] Visualize score distribution and failure trends
    -   Track which workflows or tasks are underperforming
-   [ ] Enable workflow/node filtering with timestamp range
    -   Allow filtering by platform, task, model, etc.
-   [ ] Add export functionality for CSV or JSON
    -   Let users download model runs and evaluations for analysis
-   [ ] Define user roles and permissions for dashboard access
    -   Admin, reviewer, guest views
-   [ ] View model runs, evaluation scores, failed outputs
-   [ ] Filter by platform, workflow, task, and evaluator
-   [ ] Use existing **HTTP Request Node** to call `/api/evaluate`
-   [ ] Provide JSON templates for inputs
-   [ ] Use native **If Node** to branch based on evaluation score
-   [ ] Notify humans via Slack/email when score \< threshold

### Phase 3: Observability

-   [ ] Create admin dashboard
-   [ ] Build evaluation detail view for model_run + evaluation
-   [ ] Visualize score distribution and failure trends
-   [ ] Enable workflow/node filtering with timestamp range
-   [ ] Add export functionality for CSV or JSON
-   [ ] Define user roles and permissions for dashboard access
-   [ ] View model runs, evaluation scores, failed outputs
-   [ ] Filter by platform, workflow, task, and evaluator

## Task List

| ID   | Task                                                 | Status | Assignee |
|------|------------------------------------------------------|--------|----------|
| T001 | Create DB schema with Nucleus keys and external refs | ⏳     | Human    |
| T002 | Implement `POST /api/evaluate` (log + score)         | ⏳     | Agent    |
| T003 | Add LLM-based auto-evaluation module (OpenAI GPT)    | ⏳     | Agent    |
| T004 | Generate JSON input template for n8n HTTP node       | ⏳     | Agent    |
| T005 | Create Slack/email human feedback trigger node       | ⏳     | Human    |
| T006 | Design evaluation dashboard (basic UI or Metabase)   | ⏳     | Human    |
| T007 | Create examples for multiple platforms (n8n, Make)   | ⏳     | Agent    |

## Prompt Templates

### Prompt: Implement API Endpoint

**Prompt:**

>   Build an ASP.NET Core controller with route `/api/evaluate`. It should accept a POST body with fields: `workflow_id`, `workflow_name`, `platform`, `execution_id`, `node_id`, `task`, `model_name`, `input_data`, `output_data`. It should log to DB and call OpenAI GPT to evaluate the `output_data`. Return `{ score, pass, feedback }`.

**Data Tables:** See schema overview above.

### Prompt: Generate Evaluation Schema

**Prompt:**

>   Generate a simplified schema using PostgreSQL with the following tables:

>   `workflow_run` (internal PK, external IDs)

>   `model_run` (FK to workflow_run)

>   `evaluation` (FK to model_run) Use internal UUIDs and JSONB for input/output fields.

**Data Tables:** See column/type/description in schema section.

### Prompt: JSON Template for HTTP Request Node (n8n)

**Prompt:**

>   Generate a JSON payload that maps n8n variables to a Nucleus API input. Include workflow ID, execution ID, task, model name, and input/output.

```json
{
  "workflow_id": "{{ $workflow.id }}",
  "workflow_name": "{{ $workflow.name }}",
  "execution_id": "{{ $execution.id }}",
  "platform": "n8n",
  "node_id": "summarize_email_node",
  "task": "summarize",
  "model_name": "gpt-4",
  "input_data": {{ $json.email_body }},
  "output_data": {{ $('Summarize Email').json.summary }}
}
```

### Prompt: Evaluate Output with GPT

**Prompt:**

>   Given the input: [TEXT], and the output: [TEXT], evaluate the quality of the output on a scale of 1–5. Return the score, whether it passes a threshold of 3.5, and one short sentence of feedback.
