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
    private IFormFiller _formFiller;

    public TestController([FromServices] IFormFiller formFiller, [FromServices] FormStore formStore) : base(formStore)
    {
        _formFiller = formFiller;
    }


    [PostRouteFor(Name, typeof(Bob))]
    [HttpPost]
    public IActionResult GetBob(Bob bob)
    {
        _formFiller.Fill(bob, ControllerContext);
        return Json(bob);
    }

    [Route("myRoute")]
    public IActionResult GetMyRoute()
    {
        return Content("MyRoute");
    }
}