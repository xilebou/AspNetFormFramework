using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public class FormMapper<T>(WebApplication app)
    where T : IFormController
{
    private static Dictionary<Type, Dictionary<string, Type>> _formTypes = new (); 
    
    public void MapForm()   
    {
        foreach (Type formType in GetTypesWithFormAttribute())
        {
            string controllerName = GetControllerName();
            (string name, string pattern) nameAndPattern = GetNameAndPattern(formType, controllerName);

            app.MapControllerRoute(
                name: nameAndPattern.name,
                pattern: nameAndPattern.pattern,
                defaults: new { controller = controllerName, action = "Form" }
            );


            RegisterFormType(nameAndPattern, formType);
        }
    }

    private void RegisterFormType((string name, string pattern) nameAndPattern, Type formType)
    {
        if (_formTypes.ContainsKey(typeof(T))) // if form already contains controller
        {
            _formTypes[typeof(T)].Add(nameAndPattern.pattern, formType);
        }
        else // else create new dictionary for controller
        {
            Dictionary<string, Type> patternToTypes = new ();
            patternToTypes.Add(nameAndPattern.pattern, formType);
            _formTypes.Add(typeof(T), patternToTypes);
        }
    }

    public static Type? GetFormType(Type controllerType, string pattern)
    {
        if (_formTypes.ContainsKey(controllerType))
        {
            if (_formTypes[controllerType].ContainsKey(pattern))
            {
                return _formTypes[controllerType][pattern];
            }
        } 
        return null;
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
        string pattern = "/" + controllerName.ToLower() + "/";
        foreach (var attribute in attributes)
        {
            if (attribute is Form formAttribute)
            {
                name += formAttribute.Name ?? type.Name;
                pattern += formAttribute.Route?.ToLower() ??  type.Name.ToLower();
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