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
    public class CreateDb
    {
        public void InsertInTable()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();

            //Insert Admin

            AdminService AS = new AdminService(radb);

            Admin a1 = new Admin();
            a1.Name = "admin";
            a1.PhoneNumber = 01111111111;
            a1.Email = "admin@gmail.com";
            a1.Password = "1234";

            CustomerService CS = new CustomerService(radb);

            Customer c1 = new Customer();
            c1.Email = "mac@gmail.com";
            c1.Password = "123";
            c1.Name = "mac";
            c1.PhoneNumber = "2342";
            c1.Address = "Mirpur1";
            c1.Status = "Idle";
            c1.MemberSince = DateTime.Now;
            c1.LastLogin = DateTime.Now;


            RepairmanService RS = new RepairmanService(radb);

            Repairman r1 = new Repairman();
            r1.Name = "mac2";
            r1.Email = "mac2@gamil.com";
            r1.PhoneNumber = "7007";
            r1.Password = "123";
            r1.Address = "Mirpur2";
            r1.Status = "Idle";
            r1.Rating = 4.0;
            r1.MemberSince = DateTime.Now;
            r1.LastLogin = DateTime.Now;



            LoginService LS = new LoginService(radb);

            Login l1 = new Login();
            l1.Username = "admin";
            l1.Password = "123";
            l1.UserID = 1;
            l1.UserType = "Admin";

            Login l2 = new Login();
            l2.Username = "mac";
            l2.Password = "123";
            l2.UserID = 1;
            l2.UserType = "Customer";

            Login l3 = new Login();
            l3.Username = "mac2";
            l3.Password = "123";
            l3.UserID = 1;
            l3.UserType = "Repairman";




            Console.WriteLine("\n inseted " + AS.Insert(a1) + RS.Insert(r1) + CS.Insert(c1) + LS.Insert(l1) + LS.Insert(l2) + LS.Insert(l3));
        }
    }
}
