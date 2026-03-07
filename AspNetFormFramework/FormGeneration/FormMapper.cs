using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using AspNetFormFramework.FormGeneration.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public class FormMapper<T>(WebApplication app)
    where T : IFormController
{
    private static Dictionary<Type, Dictionary<string, Type>> _formTypes = new();
    
    
    public void MapForm()
    {
        Assembly assembly = typeof(T).Assembly;
        
        foreach (Type formType in GetTypesWithFormAttribute(assembly))
        {
            string controllerName = GetControllerName();
            (string name, string pattern) nameAndPattern = GetNameAndPattern(formType, controllerName);

            app.MapControllerRoute(
                name: nameAndPattern.name,
                pattern: nameAndPattern.pattern,
                defaults: new { controller = controllerName, action = "Form" }
            );


            app.MapControllerRoute(
                name: "Send" + nameAndPattern.name,
                pattern: nameAndPattern.pattern + "/send"
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
            Dictionary<string, Type> patternToTypes = new();
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
        AttributeFinder attributeFinder = new AttributeFinder(type);
        return (attributeFinder.FindName(), attributeFinder.FindPattern(controllerName));
    }

    private IEnumerable<Type> GetTypesWithFormAttribute(Assembly assembly)
    {
        foreach (Type type in assembly.GetTypes()
                     .Where(t => t.GetCustomAttributes()
                         .Any(attribute => attribute.GetType() == typeof(Form))))
        {
            if (type.GetCustomAttributes(typeof(Form), true).Length > 0)
            {
                yield return type;
            }
        }
    }
}