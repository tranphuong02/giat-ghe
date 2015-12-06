using System.ComponentModel.DataAnnotations.Schema;

namespace CL.Transverse.Model.Post
{
    public class P_Post : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public int ResourceId { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("ResourceId")]
        public virtual  P_Resource Resource { get; set; }

        [ForeignKey("CategoryId")]
        public virtual  P_Category Category { get; set; }
    }
}
