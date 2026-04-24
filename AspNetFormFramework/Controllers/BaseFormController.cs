using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.Controllers;

public abstract class BaseFormController : Controller, IFormController
{
    private FormStore _formStore;

    public BaseFormController([FromServices] FormStore formStore)
    {
        _formStore = formStore;
    }

    /// <summary>
    /// Basic implementation of GET route for forms.
    /// It will return the model specified in the request url return by <see cref="GetFormType"/>
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Form()
    {
        FormViewModel formViewModel;
        if (TempData.ContainsKey("ViewModel"))
        {
            formViewModel = (FormViewModel)TempData["ViewModel"]!;
        }
        else
        {
            formViewModel = new FormViewModelFactory(_formStore)
                .CreateForm(HttpContext.Request.Path.ToString());
        }

        return View(formViewModel);
    }
}