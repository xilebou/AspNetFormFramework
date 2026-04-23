using AspNetFormFramework;
using AspNetFormFramework.FormGeneration;
using TestApp.Controller;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFormFiller, FormFiller>();
builder.Services.AddSingleton<FormStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();

app.UseForms<TestController>(); // <-- the key part
app.MapStaticAssets();

app.MapControllers();

app.MapDefaultControllerRoute();
app.Run();


