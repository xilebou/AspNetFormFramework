using AspNetFormFramework;
using AspNetFormFramework.RouteGeneration;
using AspNetFormFramework.Services;
using TestApp.Controller;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(
    options => options.Conventions.Add(new PostRouteForConvention()));
builder.Services.AddSingleton<FormStore>();
builder.Services.AddValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

// app.UseMiddleware<>();  
app.UseForms<TestController>(); // <-- the key part
app.MapStaticAssets();

app.MapControllers();

app.MapDefaultControllerRoute();
app.Run();


