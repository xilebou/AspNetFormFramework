namespace AspNetFormFramework.ViewModels;

public class FormInfo
{
    public string? Name { get; set; }
    public List<(string label, string inputType)>? Inputs { get; set; }
}