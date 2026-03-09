namespace AspNetFormFramework.FormGeneration;

public class FormFiller<T>(HttpContext httpContext)
{
    private HttpContext _httpContext = httpContext;

    public void Fill(T formModel)
    {
        foreach (var property in formModel.GetType().GetProperties())
        {
            property.SetValue(formModel, ConvertStringTo(_httpContext.Request.Form[property.Name],  property.PropertyType));
        }
    }

    private object? ConvertStringTo(string? value, Type type)
    {
        if (value == null) return null;
        if (type == typeof(string)) return value;
        else if (type == typeof(Int32)) return Int32.Parse(value);
        else if (type == typeof(Int64)) return Int64.Parse(value);
        return null;
    }
}