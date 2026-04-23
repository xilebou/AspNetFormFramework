namespace AspNetFormFramework.ViewModels;

public class FormViewModel
{
    public string? Name { get; set; }
    public string? PostRoute { get; set; }
    public string? BaseUrl { get; set; }
    public string? FormId { get; set; }
    public List<(string label, string inputType, string name)>? Inputs { get; set; }
    
}