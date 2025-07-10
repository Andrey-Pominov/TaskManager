# TaskManager

## Overview

TaskManager is a web application for managing tasks, built with ASP.NET Core and C# 10.0 targeting .NET 6. It uses GraphQL as its API layer, supporting advanced querying, filtering, sorting, and secure role-based access.

## Project Structure

- **TaskManager.API**  
  Main entry for API configuration.

- **TaskManager.Application**  
  Application/business logic, including use cases, service implementations, and orchestration between domain and infrastructure.

- **TaskManager.Domain**  
  Core business entities, enums, interfaces, and domain logic.

- **TaskManager.GraphQl**  
  GraphQL schema – types, queries, mutations, filters, and schema configuration.

- **TaskManager.Infrastructure**  
  Data persistence, external service integrations, and concrete repository implementations.

- **TaskManager.Shared**  
  Shared utilities, extensions, constants, and types used across multiple projects.

## Features

- **GraphQL API**: Query and mutate tasks and users via a single flexible endpoint.
- **Authentication & Authorization**: Secure actions with user roles and policies.
- **Filtering & Sorting**: Rich querying options for lists of tasks and users.
- **Custom Directives & Types**: Extendible API schema with task status, custom filters, and directives like `@skip` and `@include`.

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Supported database system (such as SQL Server or PostgreSQL)

### Setup Steps

1. **Clone the repository:**
    ```sh
    git clone https://github.com/your-organization/TaskManager.git
    cd TaskManager
    ```

2. **Restore dependencies:**
    ```sh
    dotnet restore
    ```

3. **Configure the database:**
    - Update your connection string in `TaskManager.API/appsettings.json` or the relevant configuration file.

4. **Apply database migrations (if used):**
    ```sh
    dotnet ef database update --project TaskManager.Infrastructure
    ```

5. **Run the API:**
    ```sh
    dotnet run --project TaskManager.API
    ```

6. **Access the GraphQL endpoint:**
    - The default endpoint is usually at `https://localhost:<port>/graphql`.
    - You can test queries using Banana Cake Pop, Postman, or any GraphQL client.
    - [Postman api for tests](https://growthvision.postman.co/workspace/Personal_Tracker~85cb1ffb-f89d-42c8-9a04-a136c9603dfd/collection/21536087-20e124f0-a333-4d88-801c-cea3694fabee?action=share&creator=21536087&active-environment=21536087-dd0ced44-41f7-4183-9c72-3c81b2520dee)

## GraphQL Highlights

- **Query and Mutation Types**: Easily fetch, filter, and modify tasks and users.
- **Custom Status Enum**: Built-in task statuses (`TODO`, `IN_PROGRESS`, `DONE`).
- **Security**: Endpoints protected by policy-based authorization.

## Customization

You can extend the API by adding new queries, mutations, types, or directives within the `TaskManager.GraphQl` project. Register additional services or middleware in the API project's startup configuration.


## RabbitMQ 
if you wanna use it broker need create image on docker 
   ```sh
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management
   ```
## Contributing

1. Fork the repository.
2. Create a new branch: `git checkout -b feature/YourFeature`
3. Commit and push your changes.
4. Open a pull request for review.