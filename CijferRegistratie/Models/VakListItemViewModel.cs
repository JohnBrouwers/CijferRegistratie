using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CijferRegistratie.Models
{
    public class VakListItemViewModel
    {
        public VakListItemViewModel(int id, string naam, int eC, int aantalPogingen, int? hoogsteResultaat)
        {
            Id = id;
            Naam = naam;
            EC = eC;
            AantalPogingen = aantalPogingen;
            HoogsteResultaat = hoogsteResultaat;
        }

        public int Id { get; set; }

        public string Naam { get; set; }

        public int EC { get; set; }

        public string Status { get; set; } = "Geen";

        public int AantalPogingen { get; set; }

        public int? HoogsteResultaat { get; set; }

        public bool Behaald => AantalPogingen > 0 && HoogsteResultaat >= 6;

        public string? CreateSpecialLinkText => HoogsteResultaat.HasValue ? HoogsteResultaat.ToString(): "Poging Toevoegen";
    }
}
