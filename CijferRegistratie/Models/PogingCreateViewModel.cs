using Microsoft.AspNetCore.Mvc;

namespace CijferRegistratie.Models
{
    public class PogingCreateViewModel
    {
        [HiddenInput]
        public int VakId { get; set; }
        public string Vak { get; set; }
        public int Jaar { get; set; }
        public int Resultaat { get; set; }

        public PogingCreateViewModel(int vakId, string vak)
        {
            VakId= vakId;
            Vak = vak;
            Jaar = DateTime.Now.Year;
        }
    }
}
