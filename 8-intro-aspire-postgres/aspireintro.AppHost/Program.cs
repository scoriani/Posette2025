using Projects;

var builder = DistributedApplication.CreateBuilder(args);

if (builder.ExecutionContext.IsPublishMode)
{
    var postgresAzure = builder.AddAzurePostgresFlexibleServer("postgres").WithPasswordAuthentication();
    var dbAzure = postgresAzure.AddDatabase("aspireintro");

    var webapiAzure = builder.AddProject<aspireintro_WebApi>("webapi")
    .WithExternalHttpEndpoints()
    .WithReference(dbAzure)
    .WaitFor(dbAzure);
}
else
{
    var postgres = builder.AddPostgres("postgres")
        .WithPgAdmin();
    var db = postgres.AddDatabase("aspireintro");

    var webapiLocal = builder.AddProject<aspireintro_WebApi>("webapi")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    .WaitFor(postgres);
}

builder.Build().Run();

