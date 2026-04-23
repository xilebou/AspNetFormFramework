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
                var route = action.Attributes.OfType<PostRouteFor>().FirstOrDefault();

                route?.Template = BuildPattern(controller.ControllerType, route.FormType);
                foreach (var selector in action.Selectors)
                {
                    selector.AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = route?.Template,
                    };
                }
            }
        }
    }

    private string BuildPattern(Type controllerType, Type formType)
    {
        return new FormAttributeFinder(formType).FindPattern(ControllerNameParser.GetControllerName(controllerType)) + "/send";
    }
}