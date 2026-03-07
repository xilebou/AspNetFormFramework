namespace AspNetFormFramework.ViewModels;

public class FormInfo
{
    public string? Name { get; set; }
    public string? PostRoute { get; set; }
    public string? BaseUrl { get; set; }
    public List<(string label, string inputType)>? Inputs { get; set; }
    
}