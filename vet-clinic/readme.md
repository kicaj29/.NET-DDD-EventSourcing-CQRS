This example uses [onion architecture](https://www.codeguru.com/csharp/csharp/cs_misc/designtechniques/understanding-onion-architecture.html#:~:text=Onion%20Architecture%20is%20based%20on,on%20the%20actual%20domain%20models.).

# Bounded context

To bounded contexts:
![bounded context](./images/001-bounded-context.png)

It is recommended that every bounded context has own DB schema, DB,  is developed in separated repo, maybe even by different team.

In this example are 3 bounded contexts: AppointmentScheduling, ClientPatientManagement, ResourceScheduling.

# Context Maps

![context maps](./images/002-context-maps.png)

# Ubiquitous language

* client is a person who:
  * makes appointments
  * pays the bills
  * brings pets to clinic
  
* patient is an animal for whom medical records are kept
  
* appointment is a scheduled time for:
  * surgeries
  * any type of regular office visit

# resources
.
https://app.pluralsight.com/library/courses/domain-driven-design-fundamentals/table-of-contents   
https://structuremap.github.io/registration/auto-registration-and-conventions/   
https://stackoverflow.com/questions/66528541/structuremap-does-not-see-types-in-asp-net-core-net5   