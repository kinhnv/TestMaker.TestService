using AspNetCore.Environment.Extensions;
using Microsoft.EntityFrameworkCore;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Extensions;
using Serilog;
using TestMaker.Common.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .AddACS();

var source = builder.Configuration.GetSection("ACS").Get<AdditionalConfigurationSourceArray>();

// Add services to the container.
builder.Services.AddControllers();
// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
// Add ApplcicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration["Mssql:ConnectionString"]);
});
// Add Services and repositories
builder.Services.AddTransientInfrastructure();

// Add ?
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

// Add Serilog
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.ReadFrom.Configuration(builder.Configuration).WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    });
});

// Add Bearer Authentication
builder.Services.AddBearerAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();