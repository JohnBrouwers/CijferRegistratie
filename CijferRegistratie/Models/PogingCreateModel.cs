namespace CijferRegistratie.Models
{
    public class PogingCreateModel
    {
        public string Vak { get; set; } = string.Empty;
        public int VakId { get; set; }

        public int Jaar { get; set; }
        public int Resultaat{ get; set; }
    }
}
