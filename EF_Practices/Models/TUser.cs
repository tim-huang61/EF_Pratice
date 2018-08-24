namespace EF_Practices.Models
{
    public class TUser
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string Name { get; set; }

        public string Account { get; set; }

        public string Password { get; set; }

        public virtual TUserRole UserRole { get; set; }
    }
}