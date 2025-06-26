# TaskManager

Command for migration
dotnet ef migrations add Init --project TaskManager.Infrastructure --startup-project TaskManager.API
dotnet ef database update --project TaskManager.Infrastructure --startup-project TaskManager.API