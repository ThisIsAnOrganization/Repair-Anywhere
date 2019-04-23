using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Entities
{
    public class Repairman
    {
        [Key]
        public int RepairmanID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public double Rating { get; set; }
        public DateTime MemberSince { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
