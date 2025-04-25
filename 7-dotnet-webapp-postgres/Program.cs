using Microsoft.EntityFrameworkCore;
using postgres;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SalesContext>(options =>
    options.UseNpgsql().UseSeeding((context,_) =>
    {
        // read a sql script from file system in a string
        var sql = File.ReadAllText("PopulateDatabase/populatedb.sql");
        context.Database.ExecuteSqlRaw(sql);
    }));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<SalesContext>();
    applicationDbContext.Database.EnsureDeleted();
    applicationDbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
