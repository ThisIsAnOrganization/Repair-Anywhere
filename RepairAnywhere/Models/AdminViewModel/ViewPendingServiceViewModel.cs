using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.AdminViewModel
{
    public class ViewPendingServiceViewModel : AdminModel
    {
        public Request request { get; set; }
        public Customer customer { get; set; }
        public IEnumerable<Repairman> repairmen { get; set; }
    }
}