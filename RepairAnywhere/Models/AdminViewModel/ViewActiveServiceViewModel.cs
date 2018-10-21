using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.AdminViewModel
{
    public class ViewActiveServiceViewModel : AdminModel
    {
        public IEnumerable<Request> requests { get; set; }
        public Customer[] customers = new Customer[1000];
        public Repairman[] repairmen = new Repairman[1000];
        public string status { get; set; }

    }
}