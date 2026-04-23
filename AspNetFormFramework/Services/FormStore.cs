namespace AspNetFormFramework.Services;

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
        return FormFromRoute[route.ToLower()];
    }

    public void RegisterForm(long id, string route, Type formType)
    {
        FormFromIds.Add(id, formType);
        FormFromRoute.Add(route.ToLower(), formType);
    }
}