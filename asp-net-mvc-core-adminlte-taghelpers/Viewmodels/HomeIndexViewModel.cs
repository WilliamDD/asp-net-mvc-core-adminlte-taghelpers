using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace asp_net_mvc_core_adminlte_taghelpers.Viewmodels
{
    public class HomeIndexViewModel
    {
        [DisplayName("Aantal projecten")]
        [Description("Aantal, bv 1,2,3,..")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dit veld is verplicht")]
        [MinLength(5, ErrorMessage = "De tekst is te kort")]
        [MaxLength(10, ErrorMessage = "De tekst is te lang")]
        public int ProjectsCount { get; internal set; }

        [DisplayName("Aantal competenties")]
        [Description("Aantal, bv 1,2,3,..")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dit veld is verplicht")]
        [MinLength(5, ErrorMessage = "De tekst is te kort")]
        [MaxLength(10, ErrorMessage = "De tekst is te lang")]
        public int CompetenciesCount { get; set; }

        [DisplayName("Aantal contactpersonen")]
        [Description("Aantal, bv 1,2,3,..")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dit veld is verplicht")]
        [MinLength(5, ErrorMessage = "De tekst is te kort")]
        [MaxLength(10, ErrorMessage = "De tekst is te lang")]
        public int ContactsCount { get; set; }

        [DisplayName("Aantal categorieen")]
        [Description("Aantal, bv 1,2,3,..")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dit veld is verplicht")]
        [MinLength(5, ErrorMessage = "De tekst is te kort")]
        [MaxLength(10, ErrorMessage = "De tekst is te lang")]
        public int ProductCategoriesCount { get; internal set; }

        [DisplayName("Aantal producten")]
        [Description("Aantal, bv 1,2,3,..")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dit veld is verplicht")]
        [MinLength(5, ErrorMessage = "De tekst is te kort")]
        [MaxLength(10, ErrorMessage = "De tekst is te lang")]
        public int ProductsCount { get; internal set; }

        [DisplayName("Aantal mijpalen")]
        [Description("Aantal, bv 1,2,3,..")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dit veld is verplicht")]
        [MinLength(5, ErrorMessage = "De tekst is te kort")]
        [MaxLength(10, ErrorMessage = "De tekst is te lang")]
        public int ProjectMileStonesCount { get; internal set; }

        [DisplayName("Aantal bedrijven")]
        [Description("Aantal, bv 1,2,3,..")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dit veld is verplicht")]
        [MinLength(5, ErrorMessage = "De tekst is te kort")]
        [MaxLength(10, ErrorMessage = "De tekst is te lang")]
        public int BusinessCount { get; internal set; }

        [DisplayName("Aantal medewerkers")]
        [Description("Aantal, bv 1,2,3,..")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Dit veld is verplicht")]
        [MinLength(5, ErrorMessage = "De tekst is te kort")]
        [MaxLength(10, ErrorMessage = "De tekst is te lang")]
        public int EmployeesCount { get; internal set; }

        [DisplayName("Ben ik ingelogd")]
        public bool IsIngelogd { get; set; }

        [DisplayName("Ben ik uitgelogd")]
        public bool IsUitgelogd { get; set; }

        [DisplayName("Lead engineer")]
        public int UserId { get; set; }

        [DisplayName("Lead engineer")]
        public int UserIdDis { get; set; }

        [DisplayName("Teamleden")]
        public IEnumerable<int> Teamleden { get; set; }

        [DisplayName("Teamleden")]
        public IEnumerable<int> TeamledenDis { get; set; }

        [DisplayName("Geboorte Datum")]
        [DataType(DataType.Date)]
        public DateTime GeboorteDatum { get; set; } = DateTime.Now;

        [DisplayName("Geboorte Datum2")]
        [DataType(DataType.Time)]
        public DateTime GeboorteDatum2 { get; set; } = DateTime.Now;

        [DisplayName("Geboorte Datum3")]
        [DataType(DataType.DateTime)]
        public DateTime GeboorteDatum3 { get; set; } = DateTime.Now;

        public HomeIndexViewModel()
        {
            SelectList = new List<SelectListItem>();
            SelectList.Add(new SelectListItem("Disabled", "1", false, true));
            SelectList.Add(new SelectListItem("Optie 1", "2", true, false));
            SelectList.Add(new SelectListItem("Optie 2", "3", false, false));
            SelectList.Add(new SelectListItem("Optie 3", "4", true, false));
        }

        public List<SelectListItem> SelectList { get; set; }


        [DisplayName("Bedrijf")]
        public int BusinessId { get; set; }

        [DisplayName("Contactpersoon")]
        public int ContactId { get; set; }
    }
}
