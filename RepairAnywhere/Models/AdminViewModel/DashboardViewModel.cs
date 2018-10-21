using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.AdminViewModel
{
    public class DashboardViewModel : AdminModel
    {
        public IEnumerable<Request> requests { get; set; }
        public Customer[] customers = new Customer[1000];
        public int activeCount { get; set; }
        public int completeCount { get; set; }
    }
}