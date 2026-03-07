using System.Diagnostics;
using System.Reflection;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.Forms;
using AspNetFormFramework.RouteGeneration;
using Microsoft.AspNetCore.Mvc;
using AspNetFormFramework.ViewModels;

namespace AspNetFormFramework.Controllers;

public class HomeController : Controller, IFormController
{

    public IActionResult Form()
    {
        string type = HttpContext.Request.Path.ToString();
        FormInfo viewModel = new FormInfoFactory().CreateForm(
            FormMapper<HomeController>.GetFormType(typeof(HomeController), type),
            "Home",
            (Request.Scheme + "://" + Request.Host)
        );

        return View(model: viewModel);
    }

    [RouteFor("Home", typeof(Test))]
    public IActionResult TestSend()
    {
        return Content("Send success");
    }

}

