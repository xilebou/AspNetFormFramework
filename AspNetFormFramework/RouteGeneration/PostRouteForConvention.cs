using AspNetFormFramework.FormGeneration.Utils;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AspNetFormFramework.RouteGeneration;

public class PostRouteForConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            foreach (var action in controller.Actions)
            {
                var attr = action.Attributes.OfType<PostRouteFor>().FirstOrDefault();
                if (attr == null) continue;

                attr.Template = BuildPattern(controller.ControllerType, attr.FormType);
            }
        }
    }

    private string BuildPattern(Type controllerType, Type formType)
    {
        return new FormAttributeFinder(formType).FindPattern(controllerType.Name);
    }
}