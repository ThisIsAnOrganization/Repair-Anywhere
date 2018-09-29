using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Entities
{
    public class Request
    {
        [Key]
        public int RequestID { get; set; }
        public string Category { get; set; }
        public string RequestDescription { get; set; }
        public string Cost { get; set; }
        public int CustomerID { get; set; }
        public int RepairmanID { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
