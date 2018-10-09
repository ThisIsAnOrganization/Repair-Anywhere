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

        public IAdminService _AdminService;
        public ICustomerService _CustomerService;
        public ILoginService _LoginService;
        public IRepairmanService _RepairmanService;
        public IReportService _ReportService;
        public IRequestService _RequestService;
        public IReviewService _ReviewService;

        public CustomerController()
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

        public ActionResult dashboard()
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            DashboardViewModel DVM = new DashboardViewModel();

            DVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            DVM.ActiveRequests = _RequestService.GetActiveByCustomer(Convert.ToInt32(Session["userId"]));
            DVM.PendingRequests = _RequestService.GetPendingByCustomer(Convert.ToInt32(Session["userId"]));
            int count=0;
            foreach (var item in DVM.ActiveRequests)
            {
                DVM.ActiveRepairman[count] = _RepairmanService.GetById(item.RepairmanID);
                count++;
            }



            return View(DVM);
        }

        public ActionResult changePassword()
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            ChangePassowrdViewModel CPVM = new ChangePassowrdViewModel();

            CPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));
            CPVM.flag = 100;

            return View(CPVM);
        }

        [HttpPost]
        public ActionResult changePassword(string oldpass, string newpass, string repass)
        {
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
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            CompletedRepairsViewModel CRVM = new CompletedRepairsViewModel();

            CRVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(CRVM);
        }

        public ActionResult editProfile()
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            EditProfileViewModel EPVM = new EditProfileViewModel();

            EPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(EPVM);
        }

        [HttpPost]
        public ActionResult editProfile(string name,string address,string phone)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

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
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            MyServicesViewModel MSVM = new MyServicesViewModel();

            MSVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(MSVM);
        }

        public ActionResult RequestService()
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            RequestServiceViewModel RSVM = new RequestServiceViewModel();

            RSVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));
            RSVM.flag = 0;
            return View(RSVM);
        }


        [HttpPost]
        public ActionResult RequestService(string category, string prob, string address)
        {
            RequestServiceViewModel RSVM = new RequestServiceViewModel();

            RSVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));
            RSVM.flag = 0;
            if (category == "none")
                RSVM.flag = 1;

            else if (Equals(prob, ""))
                RSVM.flag = 2;

            else if (Equals(address, ""))
                RSVM.flag = 3;

            if (RSVM.flag != 0)
            {
                RSVM.category = category;
                RSVM.prob = prob;
                RSVM.address = address;
                return View(RSVM);
            }

            else
            {
                Request r = new Request();

                r.Category = category;
                r.RequestDescription = prob;
                r.CustomerID = RSVM.customer.CustomerID;
                r.Status = "Pending";
                r.RequestDate = DateTime.Now;

                _RequestService.Insert(r);



                return RedirectToAction("dashboard", "Customer");
            }
        }

        public ActionResult tripsdetails()
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            TripsDeailsViewModel TDVM = new TripsDeailsViewModel();

            TDVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(TDVM);
        }

        public ActionResult viewProfile()
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            ViewProfileViewModel VPVM = new ViewProfileViewModel();
            VPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(VPVM);
        }

        public ActionResult viewserviceprovider()
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Common");

            ViewServiceProviderViewModel VSVM = new ViewServiceProviderViewModel();

            VSVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(VSVM);
        }

        public ActionResult logout()
        {
            Session["userId"] = null;
            return RedirectToAction("index", "Common");
        }


    }
}
