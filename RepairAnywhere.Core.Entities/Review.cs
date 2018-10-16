using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Entities
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        public int CustomerID { get; set; }
        public int RepairmanID { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
