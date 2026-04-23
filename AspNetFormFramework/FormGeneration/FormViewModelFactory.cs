using System.Reflection;
using AspNetFormFramework.Attribute;
using AspNetFormFramework.Services;

namespace AspNetFormFramework.FormGeneration;

public class FormViewModelFactory
{
    private FormStore _formStore;
    public FormViewModelFactory(FormStore formStore)
    {
        _formStore = formStore;
    }
    
    public FormViewModel CreateForm(string url)
    {
        Type formType = _formStore.GetFormTypeFromRoute(url);

        if (formType is null) throw new ArgumentNullException("formType");
        FormViewModel formViewModel = new FormViewModel();

        // get name of form
        FormAttribute formAttributeAttribute = (FormAttribute)formType.GetCustomAttributes(typeof(FormAttribute), true).First();
        formViewModel.Name = formAttributeAttribute.Name ?? formType.Name;

        // get inputs of form
        List<(string Label, string InputType, string Name)> inputs = new ();
        inputs.AddRange(ExtractInputs(formType));
        formViewModel.Inputs = inputs;
        
        // get route of form
        formViewModel.PostRoute = url;
        
        return formViewModel;
    }

    private IEnumerable<(string, string, string)> ExtractInputs(Type formType)
    {
        foreach (var property in formType.GetProperties())
        {
            if (!property.GetCustomAttributes(true).Contains(typeof(FormAttribute.Ignore)))
            {
                FormAttribute.Input? input = property.GetCustomAttributes(typeof(FormAttribute.Input), true).OfType<FormAttribute.Input>()
                    .FirstOrDefault();

                yield return
                    (
                        input?.Label ?? property.Name,
                        input?.InputType ?? GetInputTypeFromReturnType(property),
                        property.Name
                    );
            }
        }
    }

    private string GetInputTypeFromReturnType(PropertyInfo property)
    {
        Type inputReturnType = property.PropertyType;
        switch (inputReturnType.Name)
        {
            case "String": return "text";
            case "Double": return "number";
            case "Decimal": return "number";
            case "Int32": return "number";
            case "Int64": return "number";
            case "Int16": return "number";
            case "Int8": return "number";
            case "Boolean": return "boolean";
            case "DateTime": return "date";
            case "Guid": return "guid";
            default: return "text";
        }
    }
}