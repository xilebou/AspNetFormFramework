using System.Reflection.Emit;
using AspNetFormFramework.FormGeneration.Utils;
using AspNetFormFramework.ViewModels;

namespace AspNetFormFramework.FormGeneration;

public class FormInfoFactory
{
    public FormInfo CreateForm(Type? formType, string controllerName, string baseUrl)
    {
        if (formType is null) throw new ArgumentNullException("formType");
        FormInfo formInfo = new FormInfo();

        // get name of form
        Form formAttribute = (Form)formType.GetCustomAttributes(typeof(Form), true).First();
        formInfo.Name = formAttribute.Name ?? formType.Name;

        // get inputs of form
        List<(string Label, string InputType)> inputs = new List<(string Label, string InputType)>();
        inputs.AddRange(ExtractInputs(formType));
        formInfo.Inputs = inputs;
        
        // get route of form
        formInfo.PostRoute = new AttributeFinder(formType).FindPattern(controllerName);

        // get base url
        formInfo.BaseUrl = baseUrl;
        
        return formInfo;
    }

    private IEnumerable<(string, string)> ExtractInputs(Type formType)
    {
        foreach (var property in formType.GetProperties())
        {
            if (!property.GetCustomAttributes(true).Contains(typeof(Form.Ignore)))
            {
                Form.Input? input = property.GetCustomAttributes(typeof(Form.Input), true).OfType<Form.Input>()
                    .FirstOrDefault();

                yield return
                    (
                        input?.Label ?? property.Name,
                        input?.InputType ?? "text"
                    )
                    ;
            }
        }
    }
}