using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.CustomerViewModel
{
    public class TripsDeailsViewModel:CustomerModel
    {
        public Request request { get; set; }
        public Repairman repairman { get; set; }
        public int count { get; set; }
        public double rating { get; set; }
    }
}