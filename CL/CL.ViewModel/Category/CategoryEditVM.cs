using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CL.Transverse.Model.Post;

namespace CL.ViewModel.Category
{
    public class CategoryEditVM
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }

        [Display(Name = "Keyword")]
        [Required(ErrorMessage = "Keyword is required")]
        public string Keyword { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }

        public List<P_Category> Categories { get; set; }
    }
}
