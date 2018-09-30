using RepairAnywhere.Core.Entities;
using RepairAnywhere.Core.Service;
using RepairAnywhere.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class DeleteDb
    {
        public void DeleteFromTable()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();

            //Insert Admin

            AdminService AS = new AdminService(radb);

            IEnumerable<Admin> admins = AS.GetAll();

            foreach (var item in admins)
            {
                Console.WriteLine("\nDeleted "+AS.Delete(item.AdminID));
            }
            
        }
    }
}
