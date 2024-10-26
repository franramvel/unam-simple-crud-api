using DB.Command.Interfaces;
using DB.Command;
using DB.Query;
using DB.Query.Interfaces;
using Serilog;
using FiltersAttributesAndMiddleware.Filters;
using Services;
using Microsoft.EntityFrameworkCore;
using DB.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
string MyCors = "MyCors";
// Add services to the container.
var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "DevelopmentAM"}.json", optional: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "QA"}.json", optional: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();
var allowedSites = configuration.GetSection("AllowedSites").Get<string[]>()!;
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCors, builder =>
    {
        builder.WithOrigins(allowedSites).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});
builder.Services.AddDbContext<MainDbContext>(cfg =>
cfg.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"), providerOptions =>
{
    providerOptions.CommandTimeout(180);
})
);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Services.AddControllers(cfg =>
{
    cfg.Filters.Add<GlobalExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
//Configuracion de Dispatcher y Handlers
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.Scan(selector =>
{
    selector.FromAssemblyOf<QueryDispatcher>()
            .AddClasses(filter =>
            {
                filter.AssignableTo(typeof(IQueryHandler<,>));
            })
            .AsImplementedInterfaces()
            .WithScopedLifetime();
});

builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.Scan(selector =>
{
    selector.FromAssemblyOf<CommandDispatcher>()
            .AddClasses(filter =>
            {
                filter.AssignableTo(typeof(ICommandHandler<,>));
            })
            .AsImplementedInterfaces()
            .WithScopedLifetime();
});

builder.Services.AddScoped<IEmpleadoServices, EmpleadoServices>();

var app = builder.Build();
app.UseCors(MyCors);

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
