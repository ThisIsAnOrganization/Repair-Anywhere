using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.RepairmanViewModel
{
    public class CompletedRepairViewModel : RepairmanModel
    {
        public Request[] requests = new Request[1000];
        public Customer[] customers = new Customer[1000];
    }
}