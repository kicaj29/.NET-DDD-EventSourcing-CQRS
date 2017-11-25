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
2. aggregato should handle commands or rather it should have simple function like Activate()?
3. does it mean that command only produces events and Apply only updates the state?
4. Apply function should not call exception because we cannot reject things the happened?
