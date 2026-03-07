using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public interface IFormController
{
    public IActionResult Form();
}