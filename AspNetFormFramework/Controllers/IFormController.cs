using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public interface IFormController
{
    /// <summary>
    /// Default GET route for forms
    /// </summary>
    /// <returns></returns>
    public IActionResult Form();
}