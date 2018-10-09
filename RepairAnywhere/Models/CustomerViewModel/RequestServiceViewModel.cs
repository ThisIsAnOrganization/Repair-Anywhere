﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.CustomerViewModel
{
    public class RequestServiceViewModel:CustomerModel
    {
        public int flag { get; set; }
        public string category { get; set; }
        public string address { get; set; }
        public string prob { get; set; }
    }
}