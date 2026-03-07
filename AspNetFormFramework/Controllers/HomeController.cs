using System.Diagnostics;
using System.Reflection;
using AspNetFormFramework.FormGeneration;
using Microsoft.AspNetCore.Mvc;
using AspNetFormFramework.Models;

namespace AspNetFormFramework.Controllers;

public class HomeController : Controller, IFormController
{
    
    [Route("/Index")]
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Form()
    {
        string type = HttpContext.Request.Path.ToString().Replace("/", "");
        type = type.Substring(0,1).ToUpper() + type.Substring(1);
        Type? viewModel = Type.GetType("AspNetFormFramework.Forms." + type );
        return View(model: viewModel.GetCustomAttributes(typeof(Form)).Cast<Form>().Single());
    }
}