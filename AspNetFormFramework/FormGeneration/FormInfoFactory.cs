using System.Reflection.Emit;
using AspNetFormFramework.ViewModels;

namespace AspNetFormFramework.FormGeneration;

public class FormInfoFactory
{
    public FormInfo CreateForm(Type? formType)
    {
        if (formType is null) throw new ArgumentNullException("formType");
        FormInfo formInfo = new FormInfo();

        // get name of form
        Form formAttribute = (Form)formType.GetCustomAttributes(typeof(Form), true).First();
        formInfo.Name = formAttribute.Name;

        // get inputs of form

        List<(string Label, string InputType)> inputs = new List<(string Label, string InputType)>();
        foreach (var property in formType.GetProperties())
        {
            if (!property.GetCustomAttributes(true).Contains(typeof(Form.Ignore)))
            {
                Form.Input? input = property.GetCustomAttributes(typeof(Form.Input), true).OfType<Form.Input>()
                    .FirstOrDefault();

                inputs.Add(
                    (
                        input?.Label ?? property.Name,
                        input?.InputType ?? "text"
                        )
                );
            }
        }

        formInfo.Inputs = inputs;

        return formInfo;
    }
}