
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("GameStore");
        // Dbcontext has a scoped services lifetime becuse :
        //1. it ensures that new instance of DbContext is created per request, which is important for handling concurrent requests and maintaining data integrity.
        //2.DB connectiom are limited and expensive resources, 
        //3.Dbcontext is not thread safe . scoped avoid concurrency issues
        //4. Makes it easier to manage transactions and ensure data consistency
        // 5. Reusing a Dbcontext instance can lead to increased memory usage
        builder.Services.AddDbContext<GameStoreContext>(options =>
     {
    options.UseSqlServer(connectionString);
         }
        );

    }
     public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();

        // Seed data
        if (!dbContext.Genres.Any())
        {
            dbContext.Genres.AddRange(
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Strategy" },
                new Genre { Name = "Sports" }
            );

            dbContext.SaveChanges();
        }
    }


}
