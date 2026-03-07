using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AspNetFormFramework.FormGeneration;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class Form : Attribute
{
    public string? Name { get; set; }
    public string? Route { get; set; }
    public List<Input> FormInputs { get; set; } =  new List<Input>();
    
    public Form(string? name = null, string? url = null)
    {
        Name = name;
        Route = url;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Input : Attribute
    {
        public string? InputType { get; set; }
        public string? Label { get; set; }

        public const string Text = "text";
        public const string Password = "password";
        public const string Email = "email";
        public const string Number = "number";

        public Input(string? label = null, string? inputType = Text)
        {
            Label = label;
            InputType = inputType;
        }
    }
}