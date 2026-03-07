using AspNetFormFramework.FormGeneration;

namespace AspNetFormFramework.Forms;

using static FormGeneration.Form;

[Form("Test", "Test")]
public class Test
{
    [Input(Input.Text)]
    public string? YourName { get; set; }
    
    [Input("Password", Input.Password)]
    public int? YourAge { get; set; }
}