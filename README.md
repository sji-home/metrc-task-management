- This is a project that a company called Metrc gave as an interview project.  I had 3 days to complete it.  
It is a .NET 8 REST Web API that does CRUD operations for tasks.  It does not represent
the final form of an application that would be ready for Production due to the limited time I had to write it, but I did take a MVP approach to add as many best practices
as I could in the time-frame I had to work with.

- Things to add to be more Production ready:
  - (Fluent) validation
  - More unit tests
  - Extending IServiceCollection for a cleaner program.cs
  - CORS configuration
  - SQL injection protection
  - XSS protection
  - CSRF protection
  - Rate Limiting and Throttling
  - Logging
  
- This project uses:
  - .NET 8
  - Web API REST with controllers and no views
  - Swagger with ability to add JWT token
  - Role-based access control
  - postgresql backup from PGAdmin
  - Dapper mini orm
