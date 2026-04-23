using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.Services;

public interface IFormFiller
{
    public void Fill(object formModel, ControllerContext controllerContext);
}