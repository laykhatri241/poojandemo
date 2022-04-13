using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MWTDbContext.Models
{
    public class UserRoles
    {
        [Key]
        public int id{ get; set; }
        [Required, MaxLength(16)]
        public string rolename { get; set; }

    }
}
