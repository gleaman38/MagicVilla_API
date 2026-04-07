
//using Serilog;

using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MagicVilla_VillaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*
            create custom logger configuration instead of the default
            console logger, and configure it to write logs to a file with daily rolling
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.File("log/VillaLogs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            */

            //use serilog as the logging provider for the application 
            //builder.Host.UseSerilog();
            
            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"))
            );

            //accept only json or xml in the request body, return 406 not acceptable
            //if client requests a format that is not supported
            //builder.Services.AddControllers();
            builder.Services.AddControllers(option =>
            {
                option.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

            //to use sql server as the database provider for the application, and get the connection string from the configuration file
            //builder.Services.AddSqlServer<VillaContext>(builder.Configuration.GetConnectionString("DefaultSQLConnection"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //added to use custom logging service instead of the default console
            //logger, and inject it into the controller for debugging and logging purposes
            builder.Services.AddSingleton<ILogging,Logging.Logging>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
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
