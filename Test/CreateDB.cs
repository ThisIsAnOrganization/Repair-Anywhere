﻿using RepairAnywhere.Core.Entities;
using RepairAnywhere.Core.Service;
using RepairAnywhere.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CreateDb
    {
        public void InsertInTable()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();

            //Insert Admin

            AdminService AS = new AdminService(radb);

            Admin a1 = new Admin();
            a1.Name = "Admin";
            a1.PhoneNumber = 01111111111;
            a1.Email = "admin@ra.com";
            a1.Password = "1234";

            Login l1 = new Login();
            l1.Username = "admin";
            l1.Password = "1234";
            l1.UserID = 3;
            l1.UserType = "admin";

            LoginService LS = new LoginService(radb);


            Console.WriteLine("\n inseted " + AS.Insert(a1) + LS.Insert(l1));
        }
    }
}