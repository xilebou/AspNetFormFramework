using AspNetFormFramework.Attribute;

namespace TestApp.Forms;

[Form]
public class NouveauFormulaire
{
    public string? VotreNom { get; set; }
    public string? VotrePrenom { get; set; }
    public int? VotreAge { get; set; }
}