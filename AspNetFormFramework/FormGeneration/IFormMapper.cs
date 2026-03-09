using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public interface IFormMapper
{
    public void MapForms(Type controllerType);
}