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
        public string ReportID { get; set; }
        public string CustomerID { get; set; }
        public string ReportDate { get; set; }
        public string ReportDescription { get; set; }
        public string RepairmanID { get; set; }
    }
}
