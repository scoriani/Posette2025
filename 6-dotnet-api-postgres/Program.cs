using System.Reflection;
using DbUp;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

SetupDb(connectionString);
PerformUpgrade(connectionString);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();


app.MapGet("/customers", () =>
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    using var command = new NpgsqlCommand("SELECT * FROM sales.customers", connection);
    using var reader = command.ExecuteReader();

    var customers = new List<object>();
    while (reader.Read())
    {
        var customer = new
        {
            Id = reader["customerid"],
            Name = reader["customername"],
            ContactName = reader["contactname"]
        };
        customers.Add(customer);
    }

    return Results.Ok(customers);
});

app.Run();

void SetupDb(string? connectionString)
{
    EnsureDatabase.For.PostgresqlDatabase(connectionString);
}
void PerformUpgrade(string? connectionString)
{
    var upgrader = DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();

    var result = upgrader.PerformUpgrade();
    if (!result.Successful)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(result.Error);
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Database upgrade successful!");
        Console.ResetColor();
    }
}
