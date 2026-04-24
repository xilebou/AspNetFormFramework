using AspNetFormFramework.Attribute;
using AspNetFormFramework.Controllers;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.Services;
using Microsoft.AspNetCore.Mvc;
using TestApp.Forms;

namespace TestApp.Controller;

public class TestController: BaseFormController
{
    private FormStore _formStore;
    public TestController([FromServices] FormStore formStore) : base(formStore)
    {
        _formStore = formStore;
    }


    [PostRouteFor(typeof(MyCustomForm))]
    public IActionResult GetForm(MyCustomForm nouveauFormulaire)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = 400;
            FormViewModel formViewModel = new FormViewModelFactory(_formStore).CreateForm(Request.Path.Value.Replace("/send", ""), nouveauFormulaire);
            return View("Form", formViewModel);
        }
        Console.WriteLine("BONJOUR BOB!");
        return Json(nouveauFormulaire);
    }
}