namespace AspNetFormFramework.FormGeneration.Utils;

public class ControllerNameParser
{
    public static string GetControllerName(Type t)
    {
        string controllerName = t.Name;
        controllerName = controllerName.Replace("Controller", "");
        return controllerName;
    }
}