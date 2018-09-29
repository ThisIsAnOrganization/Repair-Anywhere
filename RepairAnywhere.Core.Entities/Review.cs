﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Entities
{
    class Review
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int RepairmanID { get; set; }
        public string RepairmanName { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}