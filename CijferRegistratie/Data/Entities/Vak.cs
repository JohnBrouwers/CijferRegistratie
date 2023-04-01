using System.ComponentModel.DataAnnotations;

namespace CijferRegistratie.Data.Entities
{
    public class Vak
    {
        public int Id { get; set; }

        [Required, MinLength(6)]
        public string Naam { get; set; } = string.Empty;

        public int EC { get; set; }

        public ICollection<Poging> Pogingen { get; set; } = new List<Poging>();
    }
}
