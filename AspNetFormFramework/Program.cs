using System.Reflection;
using AspNetFormFramework.Controllers;
using AspNetFormFramework.FormGeneration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseForms();
app.MapStaticAssets();

app.MapControllers();

app.MapDefaultControllerRoute();
app.Run();


static class ApplicationFormExtension
{
    public static void UseForms(this WebApplication app)
    {
        FormGenerator<HomeController> generator = new FormGenerator<HomeController>(app);
        generator.MapForm();
    }
}