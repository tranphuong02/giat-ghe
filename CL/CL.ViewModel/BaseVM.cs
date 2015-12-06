using System;
using System.ComponentModel.DataAnnotations;

namespace CL.ViewModel
{
    public class BaseVM
    {
        public int Id { get; set; }

        [Display(Name = "Is Active")]
        public bool IsPublish { get; set; }

        [Display(Name = "Is Delete")]
        public bool IsDelete { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; }
    }
}
