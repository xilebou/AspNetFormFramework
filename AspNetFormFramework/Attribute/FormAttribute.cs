namespace AspNetFormFramework.Attribute;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class FormAttribute : System.Attribute
{
    public string? Name { get; set; }
    public string? Route { get; set; }
    
    public FormAttribute(string? name = null, string? url = null)
    {
        Name = name;
        Route = url;
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Input : System.Attribute
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

    public class Ignore : System.Attribute
    {
        
    }
}