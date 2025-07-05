# Strategic Alignment Score (SAS)

## What is the Strategic Alignment Score (SAS)?

The **Strategic Alignment Score (SAS)** is a framework for prioritizing work items based on their alignment with strategic and operational goals. By assigning a quantitative score, SAS helps optimize resource allocation, maintain focus on impactful work items, and balance competing priorities such as urgency, revenue, cycle time, and strategic objectives.

## Why Do We Need SAS?

### Objective Decision-Making

SAS provides a consistent, data-driven approach to prioritize work items, reducing bias and ensuring alignment with strategic intent.

### Strategic Focus

By quantifying strategic alignment, SAS ensures that resources are directed toward work items that drive the most value and align with strategic intent.

### Efficiency and Optimization

SAS helps allocate resources effectively, minimizing wasted effort and focusing on the highest-value work items.

### Adaptability

SAS can dynamically adjust to changes in priorities, such as shifting from cycle-time optimization to revenue maximization.

### Transparency

SAS fosters understanding and collaboration by clearly articulating how priorities are determined across teams and stakeholders.

## Benefits of SAS

### Comprehensive Prioritization

Balances impact, urgency, and feasibility to ensure high-value initiatives are prioritized.

### Flexibility

Adapts dynamically to changes in strategy, workload, or market conditions.

### Increased Transparency

Provides clear, defensible prioritization decisions based on measurable criteria.

### Data-Driven Optimization

Enables continuous improvement through historical analysis and predictive insights.

## Components of SAS

SAS integrates multiple dimensions into a single score to balance impact, feasibility, and urgency:

1.  **Strategic Alignment (SA)**  
    Measures how well the work item aligns with the North Star Goal, agency objectives, and client objectives.
2.  \*\*Client Priority (CP)  
    \*\*Gauges how desirable the work item is to the client or users.
3.  **Value Delivered (VD)**  
    Quantifies the expected impact of the work item on revenue, customer satisfaction, or other key outcomes.
4.  **Viability (VY)**  
    Assesses feasibility based on resource availability, timeline, and technical complexity.
5.  **Complexity (CX)**  
    Captures the effort required, including cross-functional collaboration, technical challenges, and dependencies.
6.  **Urgency (UR)**  
    Evaluates the time sensitivity of the work item and the consequences of delay.
7.  **Resource Availability (RA)**  
    Reflects the capacity of the team to execute the work item based on skills, workload, and WIP limits.
8.  **Deadline Weight (DW)**  
    Adds importance to time-sensitive work items by accounting for proximity to due dates.

## SAS Calculation

### Scale

#### **Strategic Alignment (SA)**

How well does the work item support agency strategic goals?

| **Score** | **Description**                                          | Definition                                           |
|-----------|----------------------------------------------------------|------------------------------------------------------|
| **1**     | Does not align with any goals.                           | Minimal or no alignment with strategic intent.       |
| **2**     | Partially aligns with minor objectives.                  | Weak alignment; minor relevance to strategic intent. |
| **3**     | Supports a general strategic direction.                  | Reasonably aligned with strategic intent.            |
| **4**     | Directly aligns with a key strategic objective.          | Strongly aligned with key strategic intent.          |
| **5**     | Directly aligns with a top-priority strategic objective. | Fully aligns with strategic intent.                  |

#### **Client Priority (CP)**

How important is the work item to the client?

| **Score** | **Description** | **Definition**                                                                  |
|-----------|-----------------|---------------------------------------------------------------------------------|
| 0         | Won't Have      | The work item is not needed and will not be prioritized.                        |
| 1         | Nice to Have    | The work item is desirable but has minimal impact or urgency.                   |
| 2         | Like to Have    | The work item adds value and has moderate importance but is not critical.       |
| 3         | Must Have       | The work item is essential and critical to achieving strategic or client goals. |

#### **Value Delivered (VA)**

How much value does this work item provide to users?

| **Score** | **Description**                 | **Definition**                                               |
|-----------|---------------------------------|--------------------------------------------------------------|
| **1**     | Minimal or no value delivered.  | Delivers negligible or no measurable impact.                 |
| **2**     | Low value; limited impact.      | Delivers limited or weak benefit.                            |
| **3**     | Moderate value; some benefit.   | Provides noticeable benefit or improvement.                  |
| **4**     | High value; clear benefit.      | Delivers significant benefit with clear measurable outcomes. |
| **5**     | Maximum value; critical impact. | Provides critical value with substantial measurable impact.  |

#### **Viability (VY)**

How feasible is this work item given current resources and constraints?

| **Score** | **Description**                                 | **Definition**                                                     |
|-----------|-------------------------------------------------|--------------------------------------------------------------------|
| **1**     | Extremely difficult; major risks.               | Significant challenges and risks; barely achievable.               |
| **2**     | Difficult; multiple risks.                      | Requires overcoming multiple challenges and risks.                 |
| **3**     | Feasible with manageable challenges.            | Achievable with manageable challenges and risks.                   |
| **4**     | Highly feasible; minor challenges.              | Easily achievable with minor challenges.                           |
| **5**     | Easily feasible with no foreseeable challenges. | Straightforward execution with no foreseeable challenges or risks. |

#### **Complexity (CX)**

What is the level of effort required to complete this work item?

| **Score** | **Description**                                     | **Definition**                                                          |
|-----------|-----------------------------------------------------|-------------------------------------------------------------------------|
| **1**     | Very high complexity; significant resources needed. | Requires significant resources, time, or cross-functional coordination. |
| **2**     | High complexity; requires substantial resources.    | Involves substantial effort and resources but is manageable.            |
| **3**     | Moderate complexity; manageable effort.             | Requires a reasonable amount of effort and resources.                   |
| **4**     | Low complexity; minimal effort needed.              | Minimal effort and resources required.                                  |
| **5**     | Very low complexity; easily completed.              | Straightforward execution with negligible effort or resources.          |

#### **Urgency (UR)**

How time-sensitive is the work item?

| **Score** | **Description**                                                  | **Definition**                                                              |
|-----------|------------------------------------------------------------------|-----------------------------------------------------------------------------|
| **1**     | No urgency; long-term flexibility.                               | No immediate need; action can be deferred indefinitely.                     |
| **2**     | Low urgency; can be delayed without consequence.                 | Action can be delayed without negative consequences.                        |
| **3**     | Moderate urgency; should be completed in a reasonable timeframe. | Should be addressed within a reasonable timeframe to avoid impacts.         |
| **4**     | High urgency; requires prompt attention.                         | Requires prompt attention to prevent negative consequences.                 |
| **5**     | Critical urgency; must be addressed immediately.                 | Demands immediate action to address pressing issues or seize opportunities. |

#### Resource Availability (RA)

How available are resources to work on work item?

| **Score** | **Description**                            |
|-----------|--------------------------------------------|
| 1         | Team member has no current tasks assigned  |
| 0.75      | Team member has 50% of their capacity used |
| 0.6       | Team member is at 90% of their capacity    |
| 0.5       | Team member exceeds their WIP limit        |

##### Factors Influencing RAW

1.  **WIP (Work in Progress):**
    1.  If team members are nearing or exceeding their WIP limits, RAW decreases to reflect reduced capacity.
2.  **Skill Matching:**
    1.  If the required skills for the task match the team's expertise, RAW remains high. Mismatches lower RAW, as additional effort might be required to train or onboard the team.
3.  **Resource Utilization:**
    1.  The overall availability of resources (e.g., hours, equipment, budget) affects RAW. High utilization rates lead to lower RAW scores.

##### Calculating RAW

RAW can be calculated dynamically based on real-time factors:

RAW = 1.0 - (Utilized Capacity Total Capacity)

-   **Utilized Capacity:** Current workload (WIP, assigned hours, etc.).
-   **Total Capacity:** Maximum workload the team can handle.
-   Minimum RAW is capped at **0.5** to avoid zeroing out SAS.

##### RAW's Role in SAS

RAW adjusts the calculated SAS to prioritize work items that can realistically be completed given current resource constraints. Incorporating RAW ensures that high-priority items don't stall due to resource bottlenecks.

###### Adjusted SAS Formula:

SAS_Adjusted = SAS x RAW

-   High RAW (e.g., 1.0): SAS remains unaffected, emphasizing the item's strategic alignment.
-   Low RAW (e.g., 0.5): SAS is halved, deprioritizing the item due to resource constraints.

##### Benefits of RAW

-   **Proactive Resource Management:** Highlights potential bottlenecks and reallocates resources more effectively.
-   **Real-Time Adaptation:** Adjusts priorities dynamically based on team capacity and workload.
-   **Improved Flow Efficiency:** Reduces overcommitment and improves the predictability of delivery timelines.

By incorporating RAW, WarRoom ensures that strategic priorities align with operational realities.

### Normalization

Scores are normalized by taking the score value and dividing it by the number of possible values for the score.

Example: If we have a score of 2 and the possible range of values is 1 to 5. The normalized value is 2 ÷ 4 = 0.4.

### Weights

-   **SAW**: Strategic Alignment Weight (agency and client objectives).
-   **VAW**: Value Alignment Weight (client priority and desired outcomes).
-   **VYW**: Viability Weight (feasibility and risk).
-   **URW**: Urgency Weight is a multiplier reflecting urgency and proximity to deadlines.
-   **CPW**: Complexity Weight reduces the SAS for high-complexity tasks.
-   **RAW**: Resource Availability Weight adjusts SAS based on available team capacity and matching skills.

### Formula

#### Epics SAS

Epic SAS = (SA Normalized \* SAW) + (CP Normalized \* CPW)

#### Feature SAS

Feature SAS = Epic SAS + (CP Normalized \* CPW) + (VD Normalized \* VDW)

#### Story SAS

Story SAS = Feature SAS + (VD Normalized \* VDW) + (VY Normalized \* VYW) + (CX Normalized \* CXW) + (UR Normalized \* URW)

### Example

#### Feature-Level Calculation

For a feature aimed at improving customer satisfaction:

-   **Client Priority (CP)**: 3- Must have feature.
-   **Strategic Alignment (SA)**: 4 – Directly supports a key objective.
-   **Value Delivered (VD)**: 5 – Expected to significantly improve customer retention.
-   **Viability (VY)**: 3 – Feasible but involves cross-functional effort.
-   **Complexity (CX)**: 3 – Moderate technical complexity.
-   **Urgency (UR)**: 4 – Must be implemented within the next quarter.

| **Criterion**       | **Score** | **Weight** | **Weighted Score** |
|---------------------|-----------|------------|--------------------|
| Client Priority     | 3         | 30%        | 0.9                |
| Strategic Alignment | 4         | 20%        | 0.8                |
| Value               | 5         | 25%        | 1.25               |
| Viability           | 3         | 10%        | 0.3                |
| Complexity          | 3         | 10%        | 0.3                |
| Urgency             | 4         | 5%         | 0.2                |
| **Total SAS**       | **22**    | **100%**   | **3.75**           |

#### Story-Level Calculation

| **Feature SAS** | **RAW (1.0 - 0.5)** | **Complexity (1 - 0.2)** | **Adjusted SAS** |
|-----------------|---------------------|--------------------------|------------------|
| 4.18            | 0.8                 | 0.9                      | **3.01**         |

## Implementing SAS

1.  **Define Criteria and Weighting**  
    Tailor the weights and scoring guidelines to align with organizational goals.
2.  **Score Work Items**  
    Evaluate work items against criteria using quantitative and qualitative inputs.
3.  **Calculate and Rank SAS**  
    Use the SAS formula to rank work items and inform prioritization decisions.
4.  **Incorporate Feedback Loops**  
    Continuously improve SAS accuracy and relevance by integrating insights from historical performance and evolving goals.

## Dynamic SAS Adjustments

SAS adapts to changing priorities:

-   **Optimizing for Revenue**: Increase weights for Value Alignment and Strategic Alignment.
-   **Optimizing for Cycle Time**: Prioritize Viability, Resource Availability, and Complexity factors.
-   **Urgent Deliverables**: Amplify the impact of deadlines through Urgency Weight.

## Data Integration and Storage

### Storage Strategy

-   **Epics** and **Features**: Store SAS directly, including individual scores for Strategic Alignment, Value, and Viability.
-   **Stories** and **Tasks**: Calculate SAS dynamically, leveraging Feature SAS and real-time Resource Availability data.

### Example Schema

```json
{
  "WorkItemId": "12345",
  "WorkItemType": "Feature",
  "StrategicAlignmentScore": 4.18,
  "Scores": {
    "StrategicAlignment": 4,
    "Value": 5,
    "Viability": 3
  },
  "Weights": {
    "SAW": 0.3,
    "VAW": 0.4,
    "VYW": 0.2
  },
  "UrgencyWeight": 0.1
}
```

## Challenges and Solutions

1.  **Subjectivity in Scoring**
    1.  **Solution**: Use clear guidelines and automated inputs for consistent scoring.
2.  **Frequent Changes in Priorities**
    1.  **Solution**: Update SAS regularly and integrate real-time metrics.
3.  **Complex Calculations**
    1.  **Solution**: Automate SAS computation within WarRoom to ensure efficiency.

## Conclusion

SAS is a robust, flexible system for prioritizing work items across strategic, operational, and tactical levels. By integrating metrics for alignment, value, urgency, feasibility, complexity, and resource availability, SAS drives data-driven decision-making and keeps the organization aligned with its goals.
