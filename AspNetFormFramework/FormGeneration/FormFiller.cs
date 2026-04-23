using Microsoft.AspNetCore.Mvc;

namespace AspNetFormFramework.FormGeneration;

public class FormFiller: IFormFiller
{

    public void Fill(object formModel, ControllerContext context)
    {
        foreach (var property in formModel.GetType().GetProperties())
        {
            property.SetValue(formModel, ConvertStringTo(context.HttpContext.Request.Form[property.Name],  property.PropertyType));
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