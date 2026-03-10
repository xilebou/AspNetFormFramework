using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controller;

public class IndexController: Microsoft.AspNetCore.Mvc.Controller
{
    [Route("")]
    public IActionResult Index()
    {
        return View();
    } 
}