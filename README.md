# Calendar
Calendar API with ASP.NET Restful API.

This project was developed by clean architecture, mediatr and CQRS pattern. To read the data using the query and to change the data using the command.
Also use Repository pattern and EF Core for the CRUD.

For run the project: docker-compose up

### Database Configuration
after run project database created automatically.

## Documentation
Help for executing the API:
https://documenter.getpostman.com/view/5287501/UUxxhp6j

Swagger Url: http://localhost:9080/swagger/index.html

API Url: http://localhost:9080/

## Technologies

* ASP.NET Core 5
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [Docker](https://www.docker.com/)
* SeriLog, Seq

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebAPI

This layer is a Endpoint of the project and provide Restful api. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure and Services are only to support dependency injection. 
