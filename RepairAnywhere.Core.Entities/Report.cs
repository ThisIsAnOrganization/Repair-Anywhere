using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Entities
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int RepairmanID { get; set; }
    }
}
