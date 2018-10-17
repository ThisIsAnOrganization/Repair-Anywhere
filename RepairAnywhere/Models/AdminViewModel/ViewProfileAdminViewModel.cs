using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairAnywhere.Models.AdminViewModel
{
    public class ViewProfileAdminViewModel : AdminModel
    {
        public Admin OtherAdmin { get; set; }
    }
}