using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.CustomerViewModel
{
    public class ViewServiceProviderViewModel:CustomerModel
    {
        public Repairman repairman { get; set; }
        public IEnumerable<Review> reviews { get; set; }
        public int completed { get; set; }
        public int flag { get; set; }
        public string experience { get; set; }

        public Customer[] customers = new Customer[1000];

    }
}