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
    private IFormFiller _formFiller;

    public TestController([FromServices] IFormFiller formFiller, [FromServices] FormStore formStore) : base(formStore)
    {
        _formFiller = formFiller;
    }


    [PostRouteFor(typeof(NouveauFormulaire))]
    // [Route("test/nouveauformulaire/send")]
    public IActionResult GetBob(NouveauFormulaire nouveauFormulaire)
    {
        _formFiller.Fill(nouveauFormulaire, ControllerContext);
        Console.WriteLine("BONJOUR BOB!");
        return Json(nouveauFormulaire);
    }

    [Route("myRoute")]
    public IActionResult GetMyRoute()
    {
        return Content("MyRoute");
    }
}