# Architecture Decision Records (ADRs)

This directory will contain your Architecture Decision Records for TrackFlow.

## What is an ADR?

An Architecture Decision Record (ADR) documents an important architectural choice, the context that led to it, alternatives considered, and the consequences of adopting it. ADRs create a historical trail of **why** the system looks the way it does.

## When to Write an ADR

Write an ADR when:
- The decision would be expensive to reverse (affects multiple modules or layers)
- Future maintainers will need to understand why this path was chosen
- Multiple viable alternatives existed and a choice had to be justified
- The decision creates architectural constraints or enables new capabilities

## Standard 7-Section ADR Template

```markdown
# ADR XXXX: [Title - Noun Phrase]

## Status
[Proposed | Accepted | Deprecated | Superseded by ADR-YYYY]

## Context
What is the architectural challenge or decision point we face?
- What forces are at play? (technical, organizational, cost, timeline)
- What quality attributes matter most here?
- What constraints exist?

## Decision
We will [clear, declarative statement of the choice made].

## Consequences

### Positive
- What becomes easier or better?

### Negative
- What becomes harder or what do we lose?

### Risks
- What could go wrong? Under what conditions might we need to revisit this?

## Compliance
How will we ensure this decision is respected in the codebase?
(e.g., automated tests, code review checklist, CI pipeline checks)

## Alternatives Considered

### Alternative 1: [Name]
- **Description:** ...
- **Pros:** ...
- **Cons:** ...
- **Why rejected:** ...

### Alternative 2: [Name]
- **Description:** ...
- **Pros:** ...
- **Cons:** ...
- **Why rejected:** ...
```

## Your ADRs (Week 4+)

As you progress through the course, you will author ADRs documenting key architectural decisions in TrackFlow:

- **ADR-0001:** (You will write this in Lab 4 - Week 4)
- **ADR-0002:** (You will write this in Lab 4 - Week 4)
- **ADR-0003:** (You will write this in Lab 6 - Week 6)

Each ADR must follow the 7-section template above. Quality is measured by:
1. Depth of "Alternatives Considered" section (demonstrates genuine evaluation)
2. Honesty in "Consequences - Negative" (every choice has trade-offs)
3. Specificity in "Compliance" (how you will enforce the decision)

---

**Pro tip:** Use AI tools (ChatGPT, Claude) to **brainstorm alternatives** and draft initial ADRs, but you must critically evaluate the output. AI-generated ADRs often skip the "Negative Consequences" or propose unrealistic "Compliance" mechanisms — your job is to make them honest and grounded in reality.
