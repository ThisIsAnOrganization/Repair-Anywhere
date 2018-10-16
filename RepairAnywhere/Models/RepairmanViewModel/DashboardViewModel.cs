using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.RepairmanViewModel
{
    public class DashboardViewModel : RepairmanModel
    {
        public Request request { get; set; }
        public Customer customer { get; set; }
    }
}