using System.Reflection;
using DbUp;
using DbUp.Postgresql;

var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Mcp334264!;Database=posette2025";

EnsureDatabase.For.PostgresqlDatabase(connectionString);

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