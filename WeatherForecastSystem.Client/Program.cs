using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using WeatherForecastSystem.ClientLogic.Abstraction;
using WeatherForecastSystem.ClientLogic.Implementation;
using WeatherForecastSystem.MediatR.Handlers;
using WeatherForecastSystem.RedisLogic.Abstraction;
using WeatherForecastSystem.RedisLogic.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
    opt.InstanceName = "WeatherForecastSystem/";
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CityActionHandler).Assembly));
builder.Services.AddTransient<IRedisService, RedisService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICityForecastService, CityForecastService>();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();