using System.ComponentModel.DataAnnotations;
using AspNetFormFramework.Attribute;
using AspNetFormFramework.FormGeneration;

namespace TestApp.Forms;

[Form(Name = "Form#42")]
public class MyCustomForm
{
    [FormAttribute.Input(Label = "Enter your Name: ", InputType = "text")]
    [StringLength(5)]
    public string? Name { get; set; }

    public string? YourFirstName { get; set; }

    [FormAttribute.Input(InputType = "password")]
    public int YourNewPin { get; set; }
    
    public int YourNewPinValidation { get; set; }
}