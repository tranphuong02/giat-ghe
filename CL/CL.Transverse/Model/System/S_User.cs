namespace CL.Transverse.Model.System
{
    public class S_User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}
