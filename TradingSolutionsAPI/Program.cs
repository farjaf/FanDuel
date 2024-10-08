using TradingSolutionsCore.Common;
using TradingSolutionsCore.Models;
using TradingSolutionsCore.Repositories;
using TradingSolutionsCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Initialize the Sport instance and add teams
var sport = new Sport(AppConstants.Sports.NFL);
sport.AddTeam(AppConstants.Teams.TampaBayBuccaneers);

// Register the Sport instance as a singleton
builder.Services.AddSingleton(sport);

// Register the repository and service with scoped lifetime
builder.Services.AddScoped<IDepthChartRepository, DepthChartRepository>();
builder.Services.AddScoped<IDepthChartService, DepthChartService>();

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