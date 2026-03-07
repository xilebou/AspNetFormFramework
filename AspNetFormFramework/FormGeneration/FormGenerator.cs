using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public class FormGenerator<T> where T : ControllerBase
{
    private WebApplication _app;

    public FormGenerator(WebApplication app)
    {
        _app = app;
    }
    
    public void CreateForm()
    {
        foreach (Type type in GetTypesWithFormAttribute())
        {
            var attributes= type.GetCustomAttributes();
            string name = type.Name;
            string pattern = type.Name;
            foreach (var attribute in attributes)
            {
                if (attribute is Form formAttribute)
                {
                    name = formAttribute.Name;
                    pattern = formAttribute.Route; 
                    
                    
                    foreach (var property in type.GetProperties())
                    {
                        foreach (var propretyAttribute in property.GetCustomAttributes())
                        {
                            if (propretyAttribute is Form.Input inputAttribute)
                            {
                                formAttribute.FormInputs.Add(inputAttribute);
                            }
                        }
                    }
                }
            }


            
            string controllerName = typeof(T).Name;
            controllerName = controllerName.Replace("Controller", "");

            _app.MapControllerRoute(
                name: name,
                pattern: pattern,
                defaults: new { controller = controllerName, action = "Form" }
            );
        }
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