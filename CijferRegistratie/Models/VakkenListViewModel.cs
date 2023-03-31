using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CijferRegistratie.Models
{
    public class VakkenListViewModel
    {
        public ICollection<VakListItemViewModel> VakListItems { get; set; }

        [DisplayName("Overzicht mutaties in deze sessie")]
        public string[] Mutaties { get; set; }
    }
}
