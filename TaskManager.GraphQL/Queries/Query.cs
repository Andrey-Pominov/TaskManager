using HotChocolate.Authorization;

namespace TaskManager.GraphQL.Queries;

public class Query
{
    [Authorize] // любой авторизованный пользователь
    public string Hello() => "Hello, authenticated user!";
}