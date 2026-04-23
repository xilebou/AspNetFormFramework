using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.Controllers;

public interface IFormController
{
    /// <summary>
    /// Default GET route for forms
    /// </summary>
    /// <returns></returns>
    public IActionResult Form();
}