using System.Reflection;

namespace AspNetFormFramework.FormGeneration.Utils;

public class AttributeFinder
{
    private Type _formType;
    public AttributeFinder(Type formType)
    {
        _formType = formType;
    }
    
    public string FindPattern(string controllerName)
    {
        var attributes = _formType.GetCustomAttributes();
        string pattern = "/" + controllerName.ToLower() + "/";
        foreach (var attribute in attributes)
        {
            if (attribute is Form formAttribute)
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
            if (attribute is Form formAttribute)
            {
                name += formAttribute.Name ?? _formType.Name;
            }
        }
        return name;
    }
}