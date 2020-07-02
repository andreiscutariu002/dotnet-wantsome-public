# .NET EF Core/Dapper Workshop

### Conference Planner Data Access

### Database Schema

![](https://github.com/andreiscutariu002/wantsome-dotnet-public/blob/master/img/06/schema.png?raw=true)

## Exercices

### 1. [] Setup EF Core Project

- Code first   
    - [] Create ApplicationDbContext
    - [] Add connection string to DB, add DbSets
    - [] Run migrations   

- Database first without Model Scaffoliding
    - [] Setup DbUp project ##6# todo
    - [] Add scripts
    - [] Run the migrations
    - [] Create ApplicationDbContext 

- [] Validate ApplicationDbContext with a simple query 

### 2. [] Seed

- https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
- on Tracks table, add PHP and C# tracks with a seed
- add a migration, run the migration

### 3. [] Modify Domain Model + Add migration

- on Attendee model, add a new property, date of birth
- add a migration, run the migration
- insert then read a Attendee, check if its working

### 4. [] Write a repository

- have a look on ConferencePlanner.Services and ISessionRepository
- implement the reposity inside the Data project

#### Repository pattern

* https://codewithshadman.com/repository-pattern-csharp/#generic-repository-pattern-csharp
* https://www.thereformedprogrammer.net/is-the-repository-pattern-useful-with-entity-framework-core/
* https://www.thereformedprogrammer.net/is-the-repository-pattern-useful-with-entity-framework/

### 5. [] Modify domain model, add a breaking change, use expand and contract techinique - https://medium.com/continuousdelivery/expand-contract-pattern-and-continuous-delivery-of-databases-4cfa00c23d2e

- rename the Speaker.Name to Speaker.FullName
- ##6## todo , how to do a migration?

### 6. [] Linq queries (Get from database)

- all Sessions that title contains ".NET"
- number of sessions for each speaker
- 

### 7. [] Lazy, Eager load ex. Discuss about differences

- get all sessions for one speaker

- discussion on 
    - eager loading
    - include
    - lazy loading: 
        - https://github.com/aspnet/EntityFrameworkCore/issues/3797 
        - https://csharp.christiannagel.com/2019/01/30/lazyloading/ (check home if its working)

----------

### 8. [] Introduce Dapper

- create a separte project for dapper
- implement queries at point 7 / implement the ISpeakerRepository using dapper

### 9. [] Create a view 

```sql
create view AllSessionsAndSpeakersView as
select ses.Title, sp.Name, ses.StartTime from Sessions ses
join SessionSpeaker ss on ses.Id = ss.SessionId
join Speakers sp on sp.Id = ss.SpeakerId
```

* use the view from Dapper
* display all information at console

### 10. Q/A Session