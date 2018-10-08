using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Entities
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; } 
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int UserID { get; set; }
    }
}
