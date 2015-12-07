using System.ComponentModel.DataAnnotations.Schema;

namespace CL.Transverse.Model.Post
{
    public class P_Category : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public P_Category Parent { get; set; }
    }
}
