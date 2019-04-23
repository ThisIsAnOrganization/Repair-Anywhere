using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.CustomerViewModel
{
    public class MyServicesViewModel:CustomerModel
    {
        public IEnumerable<Request> requests { get; set; }
        public Repairman[] repairmen = new Repairman[1000];
        public int[] count = new int[1000];
        public int[] flagR = new int[1000];
        public int[] completecount = new int[1000];
        public double[] rating = new double[1000];
        public string[] details = new string[1000];
        public string[] drivers = new string[1000];

    }
}