using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public class FormStore
{
    Dictionary<long, Type> FormFromIds { get; } = new ();
    Dictionary<string, Type> FormFromRoute { get; } = new ();

    public Type GetFormTypeFromId(long formId)
    {
        return FormFromIds[formId];
    }

    public Type GetFormTypeFromRoute(string route)
    {
        return FormFromRoute[route];
    }

    public void RegisterForm(long id, string route, Type formType)
    {
        FormFromIds.Add(id, formType);
        FormFromRoute.Add(route, formType);
    }
}