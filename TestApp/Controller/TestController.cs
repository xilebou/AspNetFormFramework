using System.Runtime.CompilerServices;
using AspNetFormFramework.Controllers;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.RouteGeneration;
using AspNetFormFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TestApp.Forms;

namespace TestApp.Controller;

public class TestController: BaseFormController
{
    public const string Name = "Test";

    protected override string GetName()
    {
        return Name;
    }

    protected override Type? GetFormType()
    {
        string type = HttpContext.Request.Path.ToString();
        return FormMapper<TestController>.GetFormType(this.GetType(), type);
    }

    [RouteFor(Name, typeof(Bob))]
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