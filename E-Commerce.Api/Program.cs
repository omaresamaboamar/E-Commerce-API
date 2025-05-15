
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repository;
using Services;
using Services.MappingProfiles;
using ServicesAbstractions;

namespace E_Commerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region DI

            // Add services to the container.
            builder.Services.AddDbContext<StoreDbContext>(option=>{
                var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                option.UseSqlServer(ConnectionString);
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            #endregion

            var app = builder.Build();

            using var Scope = app.Services.CreateScope();
            var ObjectDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            ObjectDataSeeding.DataSeed();

            #region MidelWare

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
