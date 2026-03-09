using System.Runtime.CompilerServices;
using AspNetFormFramework.Controllers;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.FormGeneration.Utils;
using AspNetFormFramework.RouteGeneration;
using AspNetFormFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TestApp.Forms;

namespace TestApp.Controller;

public class TestController: BaseFormController
{
    public const string Name = "Test";



    [PostRouteFor(Name, typeof(Bob))]
    [HttpPost]
    public IActionResult GetBob(Bob bob)
    {
        FormFiller<Bob> mapper = new FormFiller<Bob>(HttpContext);
        mapper.Fill(bob);
        return Json(bob);
    }

    [Route("myRoute")]
    public IActionResult GetMyRoute()
    {
        return Content("MyRoute");
    }
}