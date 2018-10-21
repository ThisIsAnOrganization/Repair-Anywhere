using RepairAnywhere.Core.Entities;
using RepairAnywhere.Core.Service;
using RepairAnywhere.Core.Service.Interfaces;
using RepairAnywhere.Infrastructure;
using RepairAnywhere.Models.AdminViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepairAnywhere.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public IAdminService _AdminService;
        public ICustomerService _CustomerService;
        public ILoginService _LoginService;
        public IRepairmanService _RepairmanService;
        public IReportService _ReportService;
        public IRequestService _RequestService;
        public IReviewService _ReviewService;

        public AdminController()
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
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            DashboardViewModel DVM = new DashboardViewModel();
            DVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            DVM.requests = _RequestService.GetAllPending();
            int count = 0;
            foreach (var item in DVM.requests)
            {
                DVM.customers[count] = _CustomerService.GetById(item.CustomerID);
                count++;
            }

            DVM.activeCount = _RequestService.GetAllActive().Count();
            DVM.completeCount = _RequestService.GetAllCompleted().Count();

            return View(DVM);
        }

        public ActionResult admin()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            AdminViewModel AVM = new AdminViewModel();
            AVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            AVM.admins = _AdminService.GetAll();

            return View(AVM);
        }

        [HttpPost]
        public ActionResult admin(string name)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            AdminViewModel AVM = new AdminViewModel();
            AVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            AVM.admins = _AdminService.GetByName(name);

            return View(AVM);
        }

        public ActionResult changepassword()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            ChangePasswordViewModel CPVM = new ChangePasswordViewModel();
            CPVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            return View(CPVM);
        }

        [HttpPost]
        public ActionResult changepassword(string oldpass, string newpass, string repass)
        {
            ChangePasswordViewModel CPVM = new ChangePasswordViewModel();
            CPVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));


            if (Equals(newpass, repass))
            {
                if (Equals(oldpass, CPVM.admin.Password))
                {
                    CPVM.admin.Password = newpass;
                    _AdminService.Update(CPVM.admin);
                    Login l = _LoginService.GetByUsername(CPVM.admin.Name);
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

        public ActionResult completerepairs()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            CompleteRepairsViewModel CRVM = new CompleteRepairsViewModel();
            CRVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            return View(CRVM);
        }

        public ActionResult currentrepairs()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            CurrentRepairsViewModel CRVM = new CurrentRepairsViewModel();
            CRVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            return View(CRVM);
        }

        public ActionResult customer()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            CustomerViewModel CVM = new CustomerViewModel();
            CVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            CVM.customers = _CustomerService.GetAll();

            return View(CVM);
        }

        [HttpPost]
        public ActionResult customer(string name)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            CustomerViewModel CVM = new CustomerViewModel();
            CVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            CVM.customers = _CustomerService.GetByName(name);

            return View(CVM);
        }

        public ActionResult editprofile()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            EditProfileViewModel EPVM = new EditProfileViewModel();
            EPVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            return View(EPVM);
        }

        [HttpPost]
        public ActionResult editprofile(string Name,string Phone)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            EditProfileViewModel EPVM = new EditProfileViewModel();
            EPVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            EPVM.admin.Name = Name;
            EPVM.admin.PhoneNumber = Convert.ToInt32(Phone);
            _AdminService.Update(EPVM.admin);

            return View(EPVM);
        }


        public ActionResult login()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            

            return View();
        }

        public ActionResult requests()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            RequestsViewModel RVM = new RequestsViewModel();
            RVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            return View(RVM);
        }

        public ActionResult serviceprovider()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            ServiceProviderViewModel SPVM = new ServiceProviderViewModel();
            SPVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            SPVM.repairmen = _RepairmanService.GetAll();

            return View(SPVM);
        }

        [HttpPost]
        public ActionResult serviceprovider(string name)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            ServiceProviderViewModel SPVM = new ServiceProviderViewModel();
            SPVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            SPVM.repairmen = _RepairmanService.GetByName(name);

            return View(SPVM);
        }

        public ActionResult viewprofileadmin(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            ViewProfileAdminViewModel VPAVM = new ViewProfileAdminViewModel();
            VPAVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            VPAVM.OtherAdmin = _AdminService.GetById(id);

            return View(VPAVM);
        }

        public ActionResult viewprofilecustomer(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            ViewProfileCustomerViewModel VPCVM = new ViewProfileCustomerViewModel();
            VPCVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            VPCVM.customer = _CustomerService.GetById(id);
            VPCVM.completed = _RequestService.GetCompletedByCustomer(id).Count();
            VPCVM.pending = _RequestService.GetPendingByCustomer(id).Count();
            VPCVM.requests = _RequestService.GetAllCustomer(id);
            int count = 0;
            foreach (var item in VPCVM.requests)
            {
                VPCVM.repairmen[count] = _RepairmanService.GetById(item.RepairmanID);
                count++;
            }

            return View(VPCVM);
        }

        public ActionResult viewprofileservice(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            ViewProfileServiceViewModel VPSVM = new ViewProfileServiceViewModel();
            VPSVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            VPSVM.repairman = _RepairmanService.GetById(id);
            VPSVM.completed = _RequestService.GetCompletedByRepairman(id).Count();
            VPSVM.pending = _RequestService.GetPendingByRepairman(id).Count();
            VPSVM.requests = _RequestService.GetAllByRepairman(id);
            int count = 0;
            foreach (var item in VPSVM.requests)
            {
                VPSVM.customers[count] = _CustomerService.GetById(item.CustomerID);
                count++;
            }

            return View(VPSVM);
        }

        public ActionResult viewpendingservice(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            ViewPendingServiceViewModel VPSVM = new ViewPendingServiceViewModel();
            VPSVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            VPSVM.request = _RequestService.GetById(id);
            VPSVM.customer = _CustomerService.GetById(VPSVM.request.CustomerID);
            VPSVM.repairmen = _RepairmanService.GetAllIdle();
            if (VPSVM.request.RepairmanID != 0)
                VPSVM.repairmanActive = _RepairmanService.GetById(VPSVM.request.RepairmanID);
            else
                VPSVM.repairmanActive = null;

            return View(VPSVM);
        }

        public ActionResult deleteRequest(int id)
        {
            _RequestService.Delete(id);

            return RedirectToAction("dashboard");
        }

        public ActionResult assignRepairman(int id, int rid)
        {
            Request r = _RequestService.GetById(id);
            r.RepairmanID = rid;
            r.Status = "Active";
            _RequestService.Update(r);

            Repairman r1 = _RepairmanService.GetById(rid);
            r1.Status = "Active";
            _RepairmanService.Update(r1);
            

            return RedirectToAction("viewpendingservice", new { id=r.RequestID});

        }

        

        public ActionResult undoRepairman(int id, int rid)
        {
            Request r = _RequestService.GetById(id);
            r.RepairmanID = 0;
            r.Status = "Pending";
            _RequestService.Update(r);

            Repairman r1 = _RepairmanService.GetById(rid);
            r1.Status = "Idle";
            _RepairmanService.Update(r1);


            return RedirectToAction("viewpendingservice", new { id = r.RequestID });

        }

        public ActionResult viewactiveservices(string status)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 3))
                return RedirectToAction("login", "Common");

            ViewActiveServiceViewModel VASVM = new ViewActiveServiceViewModel();
            VASVM.admin = _AdminService.GetById(Convert.ToInt32(Session["userId"]));

            if (Equals("Completed", status))
            {
                VASVM.requests = _RequestService.GetAllCompleted();
            }

            else if (Equals("Active", status))
            {
                VASVM.requests = _RequestService.GetAllActive();
            }

            
            int count = 0;
            foreach (var item in VASVM.requests)
            {
                if (Equals(item.Status,status))
                {
                    VASVM.customers[count] = _CustomerService.GetById(item.CustomerID);
                    VASVM.repairmen[count] = _RepairmanService.GetById(item.RepairmanID);
                    count++;
                }
                
                
            }
            VASVM.status = status;
            
            return View(VASVM);
        }
    }
}
