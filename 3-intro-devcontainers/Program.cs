using System;
using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        // Connection string to connect to the PostgreSQL database
        var connectionString = "Host=localhost;Username=;Password=;Database=";

        try
        {
            // Create a connection to the database
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            // Execute a query to get the PostgreSQL version
            using var command = new NpgsqlCommand("SELECT version();", connection);
            var version = command.ExecuteScalar();

            // Print the PostgreSQL version
            Console.WriteLine($"PostgreSQL version: {version}");
        }
        catch (Exception ex)
        {
            // Handle any errors
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}