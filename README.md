# task-management-system (TMS)

# TMS.WebApi

**TMS.WebApi** is a .NET 9 Web API project designed for task management. It leverages PostgreSQL as the primary database, RabbitMQ for messaging, and runs with full support for Docker and Docker Compose. Database migrations are applied automatically through a dedicated service.

---

## ⚙️ Technologies Used

- [.NET 9 (ASP.NET Core)](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- PostgreSQL 15
- RabbitMQ 3 (with Management UI)
- Entity Framework Core
- Docker & Docker Compose

---

## 🚀 Getting Started

### 1. OS Preparation

-> install git https://git-scm.com/downloads
-> install docker https://www.docker.com/

### 2. Clone the Repository

on the folder when the repository is going to be cloned open the terminal and execute the following commands
-> git clone https://github.com/OstapPankevych/task-management-system.git
-> cd task-management-system

### 3. Execution environment preparation

the task-management-system folder contains files to support Docker execution

- .env this is a file with the configuration of dependencies
(the environment variable values could be modified depends on the needs)

  -> Env: 
```
    ENV=Development
```
    
  -> RabbitMq:
```
    RABBIT_PORT=5672 // port of the Rabbit MQ
    RABBIT_PORT_UI=15672 // port of the Rabbit MQ UI dashboard
    RABBIT_USER=guest // user name
    RABBIT_PASS=guest // password
```
  
  -> Postgres DB:
```
    POSTGRES_PORT=5432 // port where postgres db will be executed
    POSTGRES_DB=tms-db // db name for TMS
    POSTGRES_USER=myuser // user name
    POSTGRES_PASS=mypassword // password
```
    
  -> Task Management System Web API
```
    TMS_WEBAPI_PORT=5003 // port where tms web api will be executed
```

- compose.yaml this is a file of docker-compose that will be executed in pair with .env by default
- migrations.Dockerfile this is a file with the list of instructions to execute the migrations for the DB

### 4. Run the application

To execute the application the command ```docker-compose up --build```
After the successfult execution the console should display
```
tms.webapi        | info: DotNetCore.CAP.Processor.CapProcessingServer[0]
tms.webapi        |       Starting the processing server.
tms.webapi        | info: DotNetCore.CAP.Internal.ConsumerRegister[0]
tms.webapi        |       RabbitMQ consumer registered. --> amq.ctag-z2-q-aoRVI-ichVaSUwsVg
tms.webapi        | info: DotNetCore.CAP.IBootstrapper[0]
tms.webapi        |       ### CAP started!
```
and the docker containers created
<img width="1428" alt="image" src="https://github.com/user-attachments/assets/7192930a-4280-4d10-9039-8a488b83b382" />

### 5. API execution

To execute the API of Tasm Management System the Postman tool can be used. The repository contains **/postman** folder with API collection.
1. export files from /postman folder to postman collection
2. after success the postman should have the ```debug``` and ```docker``` environment in pair with ```TaskManagementSystem``` collection

To execute the APIs runing in docker use ```docker``` environment. 
```debug``` environment is for debug of the TMS APIs running directly from the IDE

In addition, the rabbit MQ and CAP dasborad are available by the ```http://localhost:{RABBIT_PORT_UI}``` and ```http://localhost:{TMS_WEBAPI_PORT}/cap``` urls properly

### 6. Removing

The docker containers with all TMS infrastructure could be removed by the following command ```docker-compose down -v```
