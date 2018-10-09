using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.CustomerViewModel
{
    public class DashboardViewModel:CustomerModel
    {
        public IEnumerable<Request> ActiveRequests { get; set; }
        public IEnumerable<Request> PendingRequests { get; set; }
        public Repairman[] ActiveRepairman = new Repairman[1000];
    }
}