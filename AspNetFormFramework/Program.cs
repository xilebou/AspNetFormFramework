using System.Reflection;
using System.Runtime.CompilerServices;
using AspNetFormFramework.Controllers;
using AspNetFormFramework.FormGeneration;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseForms<HomeController>();
app.MapStaticAssets();

app.MapControllers();

app.MapDefaultControllerRoute();
app.Run();


static class ApplicationFormExtension
{
    public static void UseForms<T>(this WebApplication app) where T : IFormController
    {
        FormMapper<T> mapper = new FormMapper<T>(app);
        mapper.MapForm();
    }
}