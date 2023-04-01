using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CijferRegistratie.Data.Entities
{
    public class Poging
    {
        public int Id { get; set; }

        public int Jaar { get; set; }

        public int Resultaat { get; set; }


        [DisplayName("Vaknaam")]
        [ForeignKey(nameof(Vak))]
        public int VakId { get; set; }
        public Vak? Vak { get; set; }

        public string? StudentType { get; set; }
    }
}
