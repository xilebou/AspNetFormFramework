using System.Reflection;
using AspNetFormFramework.FormGeneration;
using AspNetFormFramework.FormGeneration.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

namespace AspNetFormFramework.RouteGeneration;

public class PostRouteFor(string controller, Type type)
    : RouteAttribute(new FormAttributeFinder(type).FindPattern(controller) + "/send");