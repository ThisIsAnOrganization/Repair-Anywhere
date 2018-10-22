using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.AdminViewModel
{
    public class RepairmanReviewViewModel : AdminModel
    {
        public IEnumerable<Review> reviews { get; set; }
        public Customer[] customers = new Customer[1000];
    }
}