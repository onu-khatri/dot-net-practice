# Exception Filters in C#

## ?? Overview

**Exception filters** are a feature in C# that allow you to specify a condition directly in the `catch` clause using the `when` keyword. This enables exceptions to be caught only if the condition evaluates to `true`.

They provide a clean and efficient way to handle exceptions selectively, based on runtime data, without polluting your `catch` block with conditional logic.

---

## ? Benefits of Exception Filters Over Traditional Exception Handling

### 1. **Improved Performance**
- Exception filters evaluate the condition **before** entering the `catch` block.
- If the condition evaluates to `false`, the `catch` block is **skipped entirely**.
- This means:
  - The **stack trace is not captured**
  - **No extra allocations** are made
  - The exception continues its propagation with **minimal overhead**

In contrast, traditional `catch` blocks always capture stack traces and enter the block even if the exception is ultimately ignored or rethrown.

---

### 2. **Cleaner and More Readable Code**
- Exception filters allow you to keep filtering logic **separate** from handling logic.
- This improves code clarity and avoids nested `if` statements within `catch` blocks.
- Your code communicates **when** an exception is handled, not just **how**.

---

### 3. **Safer Exception Propagation**
- When using traditional `catch`, selectively ignoring or rethrowing exceptions requires careful handling to preserve stack traces.
- With exception filters, if the condition fails, the exception is **never caught**, so stack trace and context remain untouched.

---

### 4. **More Expressive and Flexible**
- Conditions can be based on:
  - Exception type or message
  - Application state
  - Runtime context (user roles, configuration, etc.)
- This allows writing exception handling logic that is **context-aware and declarative**.

---

## ?? When Not to Use
- If you always intend to catch and handle an exception, and no condition is needed, traditional `catch` blocks are sufficient.
- If the filtering condition requires complex logic or external dependencies (e.g. database calls), consider evaluating it within a normal `catch`.

---

## ?? Summary

| Aspect                      | Traditional `catch` | `catch` with Filter (`when`) |
|-----------------------------|---------------------|-------------------------------|
| Stack trace capture         | Always              | Only if condition is true     |
| Memory allocation           | Always              | Only if condition is true     |
| Readability                 | Lower (more nested) | Higher                        |
| Flexibility in handling     | Manual inside block | Declarative at catch site     |
| Stack trace preservation    | Manual (rethrowing) | Automatic if not caught       |

---
