using System.Reflection;
using AspNetFormFramework.Attribute;
using AspNetFormFramework.FormGeneration;

namespace AspNetFormFramework.Services;

public class FormMapper(WebApplication app, FormStore formStore): IFormMapper
{
    private int _formAmount = 0;
    
    public void MapForms(Type controller)
    {
        Assembly assembly = controller.Assembly;
        
        foreach (Type formType in GetTypesWithFormAttribute(assembly))
        {
            string controllerName = controller.Name.Replace("Controller", "");
            (string name, string pattern) nameAndPattern = GetNameAndPattern(formType, controllerName);

            app.MapControllerRoute(
                name: nameAndPattern.name,
                pattern: nameAndPattern.pattern,
                defaults: new { controller = controllerName, action = "Form" }
            );

            RegisterFormType(nameAndPattern.pattern, formType);
        }
    }

    private void RegisterFormType(string route, Type formType)
    {
        formStore.RegisterForm(_formAmount++, route, formType);
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
                         .Any(attribute => attribute.GetType() == typeof(FormAttribute))))
        {
            if (type.GetCustomAttributes(typeof(FormAttribute), true).Length > 0)
            {
                yield return type;
            }
        }
    }
}