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
    public class RetrieveFromDb
    {
        public void Retieve()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();

            //Insert Admin

            AdminService Aser = new AdminService(radb);

            IEnumerable<Admin> admins = Aser.GetAll();

            foreach (var item in admins)
            {
                Console.WriteLine(item.Name + "\n" + item.PhoneNumber + "\n" + item.Password + "\n\n");
            }
        }


    }
}
