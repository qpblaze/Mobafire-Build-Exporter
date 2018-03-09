using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoLSets.Web.ViewModels
{
    public class GetSetViewModel
    {
        [Required(ErrorMessage = "Please enter the link.")]
        public string Link { get; set; }

        [DisplayName("Build Title")]
        public string Title { get; set; }
    }
}