using AspNetCore.Environment.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestMaker.TestService.Infrastructure.Entities;
using TestMaker.TestService.Infrastructure.Extensions;
using Serilog;

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

// Add Serilog
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration.ReadFrom.Configuration(builder.Configuration).WriteTo.Console();
});
builder.Services.AddTransient();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:60001";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
        options.RequireHttpsMetadata = false;
    });

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