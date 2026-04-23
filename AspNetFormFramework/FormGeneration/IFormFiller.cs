using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public interface IFormFiller
{
    public void Fill(object formModel, ControllerContext controllerContext);
}