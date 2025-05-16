using eCommerce.Core.Interfaces;
using eCommerce.Core.Services;
using eCommerce.EF.Data;
using eCommerce.EF.Repository;
using eCommerce.EF.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace eCommerce
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //apply Generic Repositry
            builder.Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));

            //apply Unit Of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // apply DBContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //apply AutoMapper 
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //apply Image Managment Service
            builder.Services.AddScoped<IImageManagmentService, ImageManagmentService>();

            //apply IFileProvider Servic
            builder.Services.AddScoped<IFileProvider>(provider =>
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Images")));
                

            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            ;
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}