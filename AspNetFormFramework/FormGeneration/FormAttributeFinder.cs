using System.Reflection;
using AspNetFormFramework.Attribute;

namespace AspNetFormFramework.FormGeneration;

public class FormAttributeFinder
{
    private Type _formType;
    public FormAttributeFinder(Type formType)
    {
        _formType = formType;
    }
    
    public string FindPattern(string controllerName)
    {
        var attributes = _formType.GetCustomAttributes();
        string pattern = "/" + controllerName.ToLower() + "/";
        foreach (var attribute in attributes)
        {
            if (attribute is FormAttribute formAttribute)
            {
                pattern += formAttribute.Route?.ToLower() ??  _formType.Name.ToLower();
            }
        }
        return pattern;
    }

    public string FindName()
    {
        var attributes = _formType.GetCustomAttributes();
        string name = "";
        foreach (var attribute in attributes)
        {
            if (attribute is FormAttribute formAttribute)
            {
                name += formAttribute.Name ?? _formType.Name;
            }
        }
        return name;
    }
}