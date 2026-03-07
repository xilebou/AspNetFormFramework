using AspNetFormFramework.FormGeneration;

namespace AspNetFormFramework.Forms;

using static FormGeneration.Form;

[Form("Test", "Test")]
public class Test
{
    [Input(Input.Text)]
    public string? YourName { get; set; }
    
    public int? YourAge { get; set; }
}