using System.Collections.Generic;

namespace EF_Practices.Models
{
    public class TUserRole
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public virtual ICollection<TUser> Users { get; set; }
    }
}