# DD1_CheckForZeroDivisionDecision

## Status

Accepted

## Context

It's possible to divide by zero. Without mitigation, a DivideByZeroException will be thrown.

## Decision

The implementation shall check for zero division. If so, it will return 0.

## Consequences

- The user will have to handle the possibility of 0 being returned
- The user not have to catch DivideByZeroException