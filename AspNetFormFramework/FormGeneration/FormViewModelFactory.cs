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

    public FormViewModel CreateForm(string url, object formData)
    {
        FormViewModel vm = CreateForm(url);
        List<(string, string, string, string)> newInputs = new ();
        foreach (var property in formData.GetType().GetProperties())
        {
            if (property.GetValue(formData) is not null)
            {
                var input = vm.Inputs
                    .Find(s => s.name == property.Name);
                input.value = property.GetValue(formData)?.ToString();
                newInputs.Add(input);
            }
        }
        vm.Inputs = newInputs;
        return vm;
    }

    public FormViewModel CreateForm(string url)
    {
        Type formType = _formStore.GetFormTypeFromRoute(url);

        if (formType is null) throw new ArgumentNullException("formType");
        FormViewModel formViewModel = new FormViewModel();

        // get name of form
        FormAttribute formAttribute =
            (FormAttribute)formType.GetCustomAttributes(typeof(FormAttribute), true).First();
        formViewModel.Name = formAttribute.Name ?? formType.Name;
        
        // get title of form
        formViewModel.Title = formAttribute.Title ?? formType.Name;

        // get inputs of form
        List<(string Label, string InputType, string Name, string Value)> inputs = new();
        inputs.AddRange(ExtractInputs(formType));
        formViewModel.Inputs = inputs;

        // get route of form
        formViewModel.PostRoute = url +"/send";

        return formViewModel;
    }

    private IEnumerable<(string, string, string, string)> ExtractInputs(Type formType)
    {
        foreach (var property in formType.GetProperties())
        {
            if (!property.GetCustomAttributes(true).Contains(typeof(FormAttribute.Ignore)))
            {
                FormAttribute.Input? input = property
                    .GetCustomAttributes(typeof(FormAttribute.Input), true)
                    .OfType<FormAttribute.Input>()
                    .FirstOrDefault();

                yield return
                (
                    input?.Label ?? property.Name,
                    input?.InputType ?? GetInputTypeFromReturnType(property),
                    property.Name,
                    ""
                );
            }
        }
    }

    private string GetInputTypeFromReturnType(PropertyInfo property)
    {
        Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        if (t == typeof(string)) return "text";
        if (t == typeof(int)) return "number";
        if (t == typeof(long)) return "number";
        if (t == typeof(double)) return "decimal";
        if (t == typeof(float)) return "decimal";
        if (t == typeof(bool)) return "boolean";
        if (t == typeof(DateTime)) return "date";
        if (t == typeof(Guid)) return "guid";

        return "text";
    }
}