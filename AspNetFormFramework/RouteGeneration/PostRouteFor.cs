using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AspNetFormFramework.RouteGeneration;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class PostRouteFor : Attribute, IRouteTemplateProvider
{
    public string? Template { get; set; }
    public int? Order { get; }
    public string? Name { get; }
    public Type FormType { get; set; }

    public PostRouteFor(Type type)
    {
        FormType = type;
    }

    private static string GetPattern(Type type)
    {
        return "";
    }


}