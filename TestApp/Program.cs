using AspNetFormFramework.FormGeneration;
using TestApp.Controller;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseForms<TestController>();

app.MapControllers();

app.MapDefaultControllerRoute();
app.Run();


static class ApplicationFormExtension
{
    public static void UseForms<T>(this WebApplication app) where T : IFormController
    {
        FormMapper mapper = new FormMapper(app);
        mapper.MapForms(typeof(T));
    }
}