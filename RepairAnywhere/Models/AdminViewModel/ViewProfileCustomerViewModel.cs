using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.AdminViewModel
{
    public class ViewProfileCustomerViewModel : AdminModel
    {
        public Customer customer { get; set; }
        public IEnumerable<Request> requests { get; set; }
        public int completed { get; set; }
        public int pending { get; set; }
        public Repairman[] repairmen = new Repairman[1000];
    }
}