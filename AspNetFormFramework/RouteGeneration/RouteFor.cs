using System.Reflection;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.FormGeneration.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

namespace AspNetFormFramework.RouteGeneration;

public class RouteFor: RouteAttribute
{
    public RouteFor(string controller, Type type) : base(new AttributeFinder(type).FindPattern(controller) + "/send")
    {
    }
}