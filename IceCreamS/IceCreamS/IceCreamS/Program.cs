using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IceCreamS.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DataBase
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME"); ;
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); ;
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true";
builder.Services.AddDbContext<IceCreamSContext>(options => options.UseSqlServer(connectionString));

// builder.Services.AddDbContext<IceCreamSContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("IceCreamSContext") ?? throw new InvalidOperationException("Connection string 'IceCreamSContext' not found.")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
