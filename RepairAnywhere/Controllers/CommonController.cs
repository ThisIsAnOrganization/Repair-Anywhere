using RepairAnywhere.Core.Entities;
using RepairAnywhere.Core.Service;
using RepairAnywhere.Core.Service.Interfaces;
using RepairAnywhere.Infrastructure;
using RepairAnywhere.Models.CommonViewModel;
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
        public IAdminService _AdminService;
        public ICustomerService _CustomerService;
        public ILoginService _LoginService;
        public IRepairmanService _RepairmanService;
        public IReportService _ReportService;
        public IRequestService _RequestService;
        public IReviewService _ReviewService;

        public CommonController()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();

            _AdminService = new AdminService(radb);
            _CustomerService = new CustomerService(radb);
            _LoginService = new LoginService(radb);
            _RepairmanService = new RepairmanService(radb);
            _ReportService = new ReportService(radb);
            _RequestService = new RequestService(radb);
            _ReviewService = new ReviewService(radb);

        }

        


        public ActionResult index()
        {
            if (Session["userId"] != null)
                return RedirectToAction("dashboard", "Customer");
            
            return View();
        }

        public ActionResult login()
        {
            if (Session["userId"] != null)
                return RedirectToAction("dashboard", "Customer");

            LoginViewModel LVM = new LoginViewModel();
            LVM.flag = "";
            
            return View(LVM);
        }

        [HttpPost]
        public ActionResult login(string email, string password)
        {
            Login l = _LoginService.GetByUsername(email);
            LoginViewModel LVM = new LoginViewModel();
            if (l == null)
            {
                LVM.flag = "Invalid";
                return View(LVM);
            }
            else
            {
                if (l.Password != password)
                {
                    LVM.flag = "Invalid Password";
                    return View(LVM);
                }

                else
                {
                    Session["userId"] = l.UserID;
                    if (Equals(l.UserType, "admin"))
                        return RedirectToAction("index", "Common");

                    else
                        return RedirectToAction("dashboard", "Customer");

                }
                    
            }

        }

        public ActionResult forgetpass()
        {
            if (Session["userId"] != null)
                return RedirectToAction("dashboard", "Customer");

            return View();
        }

        public ActionResult register()
        {
            if (Session["userId"] != null)
                return RedirectToAction("dashboard", "Customer");

            return View();
        }

        [HttpPost]
        public ActionResult register(string Name, string Email, string PhoneNumber, string Password1, string Password)
        {
            if (Equals(Password, Password1))
            {
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
