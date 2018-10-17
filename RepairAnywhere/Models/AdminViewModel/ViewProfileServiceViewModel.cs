using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.AdminViewModel
{
    public class ViewProfileServiceViewModel : AdminModel
    {
        public Repairman repairman { get; set; }
        public IEnumerable<Request> requests { get; set; }
        public int completed { get; set; }
        public int pending { get; set; }
        public Customer[] customers = new Customer[1000];
    }
}