using AspNetFormFramework.Attribute;
using AspNetFormFramework.FormGeneration;

namespace TestApp.Forms;

[Form(Name = "Form#42")]
public class MyCustomForm
{
    [FormAttribute.Input(Label = "Enter your Name: ", InputType = "text")]
    public string? Name { get; set; }

    public string? YourFirstName { get; set; }

    [FormAttribute.Input(InputType = "password")]
    public int? YourNewPin { get; set; }
}