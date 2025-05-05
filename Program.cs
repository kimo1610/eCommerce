using eCommerce.EF.Data;
using Microsoft.EntityFrameworkCore;
using eCommerce.EF;
using eCommerce.Core.Interfaces;
using eCommerce.EF.Repository;
using Microsoft.Extensions.Configuration;

public  class Program
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


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}