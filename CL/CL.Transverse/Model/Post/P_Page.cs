namespace CL.Transverse.Model.Post
{
    public class P_Page : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
    }
}
