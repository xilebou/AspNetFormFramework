using AspNetFormFramework.Controllers;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controller;

public class TestController: BaseFormController
{

    protected override string GetName()
    {
        return "Test";
    }

    protected override Type? GetFormType()
    {
        string type = HttpContext.Request.Path.ToString();
        return FormMapper<TestController>.GetFormType(this.GetType(), type);
    }
}