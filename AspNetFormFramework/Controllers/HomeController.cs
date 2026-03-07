using System.Diagnostics;
using System.Reflection;
using AspNetFormFramework.FormGeneration;
using Microsoft.AspNetCore.Mvc;
using AspNetFormFramework.ViewModels;

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
        string type = HttpContext.Request.Path.ToString().Split("/").Last();
        type = type.Substring(0,1).ToUpper() + type.Substring(1);
        Type? formType = Type.GetType("AspNetFormFramework.Forms." + type );
        FormInfo viewModel = new FormInfoFactory().CreateForm(formType);
        
        return View(model: viewModel);
    }
}

