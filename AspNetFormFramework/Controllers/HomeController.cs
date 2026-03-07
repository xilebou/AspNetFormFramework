using System.Diagnostics;
using System.Reflection;
using AspNetFormFramework.FormGeneration;
using Microsoft.AspNetCore.Mvc;
using AspNetFormFramework.ViewModels;

namespace AspNetFormFramework.Controllers;

public class HomeController : Controller, IFormController
{
    
    public IActionResult Form()
    {
        string type = HttpContext.Request.Path.ToString();
        FormInfo viewModel = new FormInfoFactory().CreateForm(
            FormMapper<HomeController>.GetFormType(typeof(HomeController), type)
            );
        
        return View(model: viewModel);
    }
}

