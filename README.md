# .NET-DDD-EventSourcing-CQRS
Repository with examples for DDD, EventSourcing, CQRS

## cqrsnu-tutorial
Tutorial based on http://cqrs.nu/tutorial/cs/01-design
https://github.com/edumentab/cqrs-starter-kit   

BDD tests:
GIVEN: events that happened
WHEN: command
THEN: events that should happen or expected exception

Questions:
1. where should be defined commands? In domain or outside?
Rather in application thx to this we can implement some aspects in application like 
handling transations. It is also ok to have application commands and domain commands.

2. aggregate should handle commands or rather it should have simple function like Activate()?
Aggregate can handle domain commands.

3. does it mean that command only produces events and Apply only updates the state?
YES.

4. apply function should not call exception because we cannot reject things the happened?
YES.

---
Aggregate should not have references to other agregates but we can inject to aggregate some domain service to
execute some operations.

Events should be defined in domain because domain triggers these events.
