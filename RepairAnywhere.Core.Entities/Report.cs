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
        public DateTime ReportDate { get; set; }
        public string ReportDescription { get; set; }
        public int RepairmanID { get; set; }
    }
}
