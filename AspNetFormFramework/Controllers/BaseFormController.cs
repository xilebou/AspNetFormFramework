using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.FormGeneration.Utils;
using AspNetFormFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace AspNetFormFramework.Controllers;

public abstract class  BaseFormController: Controller, IFormController
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
        FormViewModel viewModel = new FormViewModelFactory(_formStore).CreateForm(
            HttpContext.Request.Path.ToString()
        );

        return View(viewModel);
    }
}