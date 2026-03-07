using AspNetFormFramework.FormGeneration;

namespace AspNetFormFramework.Forms;

using static FormGeneration.Form;

[Form("TestingForm")]
public class Test
{
    [Input(Input.Text)]
    public string? YourName { get; set; }
    
    [Input("Password", Input.Number)]
    public int? YourAge { get; set; }
}