# Task Management System (TMS)

# Tms.WebApi

**Tms.WebApi** is a .NET 9 Web API project designed for task management. It leverages PostgreSQL as the primary database, RabbitMQ for messaging (using the Outbox Pattern with .NET CAP library), and runs with full support for Docker and Docker Compose. Database migrations are applied automatically through a dedicated service.



---

## ⚙️ Technologies Used

- [.NET 9 (ASP.NET Core)](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- PostgreSQL 15
- RabbitMQ 3 (with Management UI)
- Entity Framework Core
- Docker & Docker Compose

---

---

## Project Structure

```
.
├── Tms.WebApi/           # Main ASP.NET Core Web API
├── Tms.Services/         # Business logic
├── Tms.Db/               # EF Core DbContext and migrations
├── Tms.Common/           # Shared models and helpers
├── migrations.Dockerfile # Dockerfile for running DB migrations (new migrations can be created using the `dotnet ef migrations add SomeMigrationName --project Tms.Db --startup-project Tms.WebApi` command)
├── docker-compose.yml    # Service orchestration
└── .env                  # Environment configuration
```
---

## 🚀 Getting Started

### 1. OS Preparation

- Install git https://git-scm.com/downloads
- Install Docker https://www.docker.com/

### 2. Clone the Repository

In the folder where the repository is going to be cloned, open the terminal and execute the following commands
- git clone https://github.com/OstapPankevych/task-management-system.git
- cd task-management-system

### 3. Execution environment preparation

The task-management-system folder contains files to support Docker execution

- .env This is a file with the configuration of dependencies
(The environment variable values could be modified depending on the needs)

Env: 
`
    ENV=Development
`
    
RabbitMQ:
`
    RABBIT_PORT=5672 // port of the RabbitMQ
    RABBIT_PORT_UI=15672 // port of the Rabbit MQ UI dashboard
    RABBIT_USER=guest // user name
    RABBIT_PASS=guest // password
`
  
Postgres DB:
`
    POSTGRES_PORT=5432 // port where postgres db will be executed
    POSTGRES_DB=tms-db // db name for TMS
    POSTGRES_USER=myuser // user name
    POSTGRES_PASS=mypassword // password
`
    
Task Management System Web API
`
    TMS_WEBAPI_PORT=5003 // port where the TMS web api will be executed
`

- compose.yaml this is a file of docker-compose that will be executed in pair with .env by default
- migrations. Dockerfile is a file with a list of instructions to execute the migrations for the DB

### 4. Run the application

To execute the application the command ```docker-compose up --build```
After the successful execution, the console should display
```
tms.webapi        | info: DotNetCore.CAP.Processor.CapProcessingServer[0]
tms.webapi        |       Starting the processing server.
tms.webapi        | info: DotNetCore.CAP.Internal.ConsumerRegister[0]
tms.webapi        |       RabbitMQ consumer registered. --> amq.ctag-z2-q-aoRVI-ichVaSUwsVg
tms.webapi        | info: DotNetCore.CAP.IBootstrapper[0]
tms.webapi        |       ### CAP started!
```
and the Docker containers created
<img width="1428" alt="image" src="https://github.com/user-attachments/assets/7192930a-4280-4d10-9039-8a488b83b382" />

Also, verification of the health status could be performed by the `http://localhost:{TMS_WEBAPI_PORT}/health` api.

### 5. API execution

To execute the API of the Task Management System, the Postman tool can be used. The repository contains a **/postman** folder with an API collection.
1. Export files from the `/postman` folder to the postman collection
2. after success the postman should have the ```debug``` and ```docker``` environment in pair with ```TaskManagementSystem``` collection

To execute the APIs running in Docker, use the `docker` environment. 
The ```debug``` environment is for debugging the TMS APIs running directly from the IDE

In addition, the RabbitMQ and CAP dashboard are available by the ```http://localhost:{RABBIT_PORT_UI}``` and ```http://localhost:{TMS_WEBAPI_PORT}/cap``` urls properly

### 6. Removing

The Docker containers with all TMS infrastructure could be removed by the following command: `docker-compose down -v`
