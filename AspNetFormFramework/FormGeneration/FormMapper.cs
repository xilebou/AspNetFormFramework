using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using AspNetFormFramework.FormGeneration.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public class FormMapper(WebApplication app): IFormMapper
{
    private static Dictionary<Type, Dictionary<string, Type>> _formTypes = new();
    
    
    public void MapForms(Type controller)
    {
        Assembly assembly = controller.Assembly;
        
        foreach (Type formType in GetTypesWithFormAttribute(assembly))
        {
            string controllerName = ControllerNameParser.GetControllerName(controller);
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

            RegisterFormType(nameAndPattern, formType, controller);
        }
    }

    private void RegisterFormType((string name, string pattern) nameAndPattern, Type formType, Type controllerType)
    {
        if (_formTypes.ContainsKey(controllerType)) // if form already contains controller
        {
            _formTypes[controllerType].Add(nameAndPattern.pattern, formType);
        }
        else // else create new dictionary for controller
        {
            Dictionary<string, Type> patternToTypes = new();
            patternToTypes.Add(nameAndPattern.pattern, formType);
            _formTypes.Add(controllerType, patternToTypes);
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

    private (string name, string pattern) GetNameAndPattern(Type type, string controllerName)
    {
        FormAttributeFinder formAttributeFinder = new FormAttributeFinder(type);
        return (formAttributeFinder.FindName(), formAttributeFinder.FindPattern(controllerName));
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

// public class FormMapper: IFormMapper
// {
//     private FormMapper<IFormController> _formMapper;
//
//     public FormMapper(WebApplication app)
//     {
//         _formMapper = new FormMapper<IFormController>(app);
//     }
//
//     public void MapForms()
//     {
//         _formMapper.MapForms();
//     }
// }