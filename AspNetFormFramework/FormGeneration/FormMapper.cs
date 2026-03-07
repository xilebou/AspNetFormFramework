using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public class FormMapper<T>(WebApplication app)
    where T : ControllerBase
{
    public void MapForm()
    {
        foreach (Type type in GetTypesWithFormAttribute())
        {
            string controllerName = GetControllerName();
            (string name, string pattern) nameAndPattern = GetNameAndPattern(type, controllerName);

            app.MapControllerRoute(
                name: nameAndPattern.name,
                pattern: nameAndPattern.pattern,
                defaults: new { controller = controllerName, action = "Form" }
            );
        }
    }

    private string GetControllerName()
    {
        string controllerName = typeof(T).Name;
        controllerName = controllerName.Replace("Controller", "");
        return controllerName;
    }

    private (string name, string pattern) GetNameAndPattern(Type type, string controllerName)
    {
        var attributes = type.GetCustomAttributes();
        string name = "";
        string pattern = controllerName + "/";
        foreach (var attribute in attributes)
        {
            if (attribute is Form formAttribute)
            {
                name += formAttribute.Name ?? type.Name;
                pattern += formAttribute.Route ??  type.Name;
            }
        }
        return (name, pattern);
    }

    private IEnumerable<Type> GetTypesWithFormAttribute()
    {
        foreach (Type type in Assembly.GetAssembly(typeof(Form)).GetTypes())
        {
            if (type.GetCustomAttributes(typeof(Form), true).Length > 0)
            {
                yield return type;
            }
        }
    }
}