
using System.Text.Json;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repository;
using Services;
using Services.MappingProfiles;
using ServicesAbstractions;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region DI

            // Add services to the container.
           
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();
            builder.Services.ServiceJWT(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddAuthorization();

            #endregion

            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var ObjectDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            ObjectDataSeeding.DataSeed();
            ObjectDataSeeding.IdentityDataSeed();

            #region MidelWare
            app.UseMiddleware<CustomExceptionHandlerMiddlWare>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(option => 
                {
                    option.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration = true,
                    };
                    option.DocumentTitle = "E-Commerce API";
                    option.JsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        
                    };
                    option.DocExpansion(DocExpansion.None);
                    option.EnableFilter();
                    option.EnablePersistAuthorization();
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
