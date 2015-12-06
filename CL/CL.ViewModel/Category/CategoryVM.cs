namespace CL.ViewModel.Category
{
    public class CategoryVM : BaseVM
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public int Parent { get; set; }
        public int? ParentId { get; set; }
    }
}
