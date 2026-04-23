using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AspNetFormFramework.RouteGeneration;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class PostRouteFor : Attribute, IRouteTemplateProvider, IActionHttpMethodProvider
{
    public string? Template { get; set; }
    public int? Order { get; } = 0;
    public string? Name { get; } = null;
    public Type FormType { get; set; }
    public IEnumerable<string> HttpMethods { get; } = ["POST"];
    public PostRouteFor(Type type)
    {
        FormType = type;
    }
}