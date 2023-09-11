using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("UsersRoles")]
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; } 
        public Role role { get; set; }
    }
}
