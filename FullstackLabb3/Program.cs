
using FullstackLabb3.Data;
using FullstackLabb3.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackLabb3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PortfolioDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<PortfolioService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/Portfolio", async (Portfolio portfolio, PortfolioService service) =>
            {
                await service.AddPortfolio(portfolio);
                return Results.Ok();
            });

            app.MapGet("/Portfolios", async (PortfolioService service) =>
            {
                var getAll = await service.GetPortfolio();
                return Results.Ok(getAll);
            });

            app.MapPut("/Portfolio/{id}", async (int id, Portfolio portfolio, PortfolioService service) =>
            {
                var updatePortfolio = await service.UpdatePortfolio(id, portfolio);
                if (updatePortfolio == null)
                    return Results.NotFound("Information not found");

                return Results.Ok(updatePortfolio);
            });

            app.MapDelete("/Portfolio/{id}", async (int id, PortfolioService service) =>
            {
                var isDeleted = await service.DeletePortfolio(id);
                if (isDeleted == null)
                {
                    return Results.NotFound("Information not found");
                }
                return Results.Ok(isDeleted);
            });

            app.Run();
        }
    }
}
