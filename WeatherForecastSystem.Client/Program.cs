using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WeatherForecastSystem.Client.Data;
using MudBlazor.Services;
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
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddSingleton<WeatherForecastService>();
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