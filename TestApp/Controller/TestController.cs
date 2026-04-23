using AspNetFormFramework.Attribute;
using AspNetFormFramework.Controllers;
using AspNetFormFramework.Services;
using Microsoft.AspNetCore.Mvc;
using TestApp.Forms;

namespace TestApp.Controller;

public class TestController: BaseFormController
{


    public TestController([FromServices] FormStore formStore) : base(formStore)
    {
    }


    [PostRouteFor(typeof(NouveauFormulaire))]
    public IActionResult GetBob(NouveauFormulaire nouveauFormulaire)
    {
        Console.WriteLine("BONJOUR BOB!");
        return Json(nouveauFormulaire);
    }

    [Route("myRoute")]
    public IActionResult GetMyRoute()
    {
        return Content("MyRoute");
    }
}