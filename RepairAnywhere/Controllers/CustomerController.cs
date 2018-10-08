using RepairAnywhere.Core.Entities;
using RepairAnywhere.Core.Service;
using RepairAnywhere.Core.Service.Interfaces;
using RepairAnywhere.Infrastructure;
using RepairAnywhere.Models.CustomerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepairAnywhere.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ICustomerService _CustomerService;
        public ILoginService _LoginService;

        public ActionResult dashboard()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();
            _CustomerService = new CustomerService(radb);

            DashboardViewModel DVM = new DashboardViewModel();

            DVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));



            return View(DVM);
        }

        public ActionResult changePassword()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();
            _CustomerService = new CustomerService(radb);

            ChangePassowrdViewModel CPVM = new ChangePassowrdViewModel();

            CPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));
            CPVM.flag = 100;

            return View(CPVM);
        }

        [HttpPost]
        public ActionResult changePassword(string oldpass, string newpass, string repass)
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();
            _CustomerService = new CustomerService(radb);
            _LoginService = new LoginService(radb);

            ChangePassowrdViewModel CPVM = new ChangePassowrdViewModel();

            CPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));


            if (Equals(newpass, repass))
            {
                if (Equals(oldpass, CPVM.customer.Password))
                {
                    CPVM.customer.Password = newpass;
                    _CustomerService.Update(CPVM.customer);
                    Login l=_LoginService.GetByUsername(CPVM.customer.Name);
                    l.Password = newpass;
                    _LoginService.Update(l);
                    CPVM.flag = 10;
                }
                else
                    CPVM.flag = 1;
            }
            else
                CPVM.flag = 2;
            return View(CPVM);
        }

        public ActionResult completedrepairs()
        {
            return View();
        }

        public ActionResult editProfile()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();
            _CustomerService = new CustomerService(radb);

            EditProfileViewModel EPVM = new EditProfileViewModel();

            EPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(EPVM);
        }

        [HttpPost]
        public ActionResult editProfile(string name,string address,string phone)
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();
            _CustomerService = new CustomerService(radb);

            EditProfileViewModel EPVM = new EditProfileViewModel();

            EPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            EPVM.customer.Name = name;
            EPVM.customer.Address = address;
            EPVM.customer.PhoneNumber = phone;

            _CustomerService.Update(EPVM.customer);

            return View(EPVM);
        }

        public ActionResult myServices()
        {
            return View();
        }

        public ActionResult RequestService()
        {
            return View();
        }

        public ActionResult tripsdetails()
        {
            return View();
        }

        public ActionResult viewProfile()
        {
            RepairAnywhereDbContext radb = new RepairAnywhereDbContext();
            _CustomerService = new CustomerService(radb);

            ViewProfileViewModel VPVM = new ViewProfileViewModel();
            VPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));



            return View(VPVM);
        }

        public ActionResult viewserviceprovider()
        {
            return View();
        }

        public ActionResult logout()
        {
            Session["userId"] = null;
            return RedirectToAction("index", "Common");
        }


    }
}
