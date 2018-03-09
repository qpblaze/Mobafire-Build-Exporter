using System.ComponentModel.DataAnnotations;

namespace LoLSets.Web.ViewModels
{
    public class GetSetViewModel
    {
        [Required]
        public string Link { get; set; }
        public string Title { get; set; }
    }
}