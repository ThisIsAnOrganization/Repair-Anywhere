using RepairAnywhere.Core.Entities;
using RepairAnywhere.Core.Service;
using RepairAnywhere.Core.Service.Interfaces;
using RepairAnywhere.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepairAnywhere.Controllers
{
    public class CommonController : Controller
    {
        public ILoginService _LoginService;
        public ICustomerService _CustomerService;


        public ActionResult index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public string login(string email, string password)
        {

            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();
            _LoginService = new LoginService(radb);

            Login l = _LoginService.GetByUsername(email);
            if (l == null)
            {
                return "Invalid";
            }
            else
            {
                if (l.Password != password)
                {
                    return "Invalid Password";
                }

                else
                {
                    Session["userId"] = l.UserID;
                    if (Equals(l.UserType, "admin"))
                        return "0";

                    else
                        return "1";

                }
                    
            }

        }

        public ActionResult forgetpass()
        {
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult register(string Name, string Email, string PhoneNumber, string Password1, string Password)
        {
            if(Equals(Password,Password1))
            {
                RepairAnywhereDbContext radb = new RepairAnywhereDbContext();
                _LoginService = new LoginService(radb);
                _CustomerService = new CustomerService(radb);

                Customer c = new Customer();
                c.Name = Name;
                c.Email = Email;
                c.Password = Password1;
                c.MemberSince = DateTime.Now;
                c.PhoneNumber = PhoneNumber;
                c.Address = "";
                c.LastLogin = DateTime.Now; 

                _CustomerService.Insert(c);

                IEnumerable<Customer> CA = _CustomerService.GetAll();

                foreach (var item in CA)
                {
                    c = item;
                }

                Login l = new Login();
                l.Username = Name;
                l.Password = Password1;
                l.UserType = "customer";
                l.UserID = c.CustomerID;

                _LoginService.Insert(l);


                return RedirectToAction("index", "Common");


            }
            return View();
        }
    }
}
