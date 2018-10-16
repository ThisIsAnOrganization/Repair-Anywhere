using RepairAnywhere.Core.Entities;
using RepairAnywhere.Core.Service;
using RepairAnywhere.Core.Service.Interfaces;
using RepairAnywhere.Infrastructure;
using RepairAnywhere.Models.RepairmanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepairAnywhere.Controllers
{
    public class RepairmanController : Controller
    {
        //
        // GET: /Repairman/

        public IAdminService _AdminService;
        public ICustomerService _CustomerService;
        public ILoginService _LoginService;
        public IRepairmanService _RepairmanService;
        public IReportService _ReportService;
        public IRequestService _RequestService;
        public IReviewService _ReviewService;

        public RepairmanController()
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
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            DashboardViewModel DVM = new DashboardViewModel();
            DVM.repairman = _RepairmanService.GetById(Convert.ToInt32(Session["userId"]));
            DVM.request = _RequestService.GetActiveByRepairman(DVM.repairman.RepairmanID);
            if (DVM.request!=null)
            {
                DVM.customer = _CustomerService.GetById(DVM.request.CustomerID);
            }
            
            return View(DVM);
        }

        public ActionResult viewProfile()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            ViewProfileViewModel VPVM = new ViewProfileViewModel();
            VPVM.repairman = _RepairmanService.GetById(Convert.ToInt32(Session["userId"]));

            return View(VPVM);
        }

        public ActionResult editProfile()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            EditProfileViewModel EPVM = new EditProfileViewModel();

            EPVM.repairman = _RepairmanService.GetById(Convert.ToInt32(Session["userId"]));

            return View(EPVM);
        }

        [HttpPost]
        public ActionResult editProfile(string name, string address, string phone)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            EditProfileViewModel EPVM = new EditProfileViewModel();

            EPVM.repairman = _RepairmanService.GetById(Convert.ToInt32(Session["userId"]));

            EPVM.repairman.Name = name;
            EPVM.repairman.Address = address;
            EPVM.repairman.PhoneNumber = phone;

            _RepairmanService.Update(EPVM.repairman);

            return View(EPVM);
        }


        public ActionResult changePassword()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            ChangePasswordViewModel CPVM = new ChangePasswordViewModel();

            CPVM.repairman = _RepairmanService.GetById(Convert.ToInt32(Session["userId"]));
            CPVM.flag = 100;

            return View(CPVM);
        }

        [HttpPost]
        public ActionResult changePassword(string oldpass, string newpass, string repass)
        {
            ChangePasswordViewModel CPVM = new ChangePasswordViewModel();

            CPVM.repairman = _RepairmanService.GetById(Convert.ToInt32(Session["userId"]));


            if (Equals(newpass, repass))
            {
                if (Equals(oldpass, CPVM.repairman.Password))
                {
                    CPVM.repairman.Password = newpass;
                    _RepairmanService.Update(CPVM.repairman);
                    Login l = _LoginService.GetByUsername(CPVM.repairman.Name);
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

        public ActionResult activerepair(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            ActiveRepairViewModel ARVM = new ActiveRepairViewModel();
            ARVM.repairman = _RepairmanService.GetById(Convert.ToInt32(Session["userId"]));
            ARVM.request = _RequestService.GetById(id);
            ARVM.customer = _CustomerService.GetById(ARVM.request.CustomerID);

            return View(ARVM);
        }

        [HttpPost]
        public ActionResult activerepair(int id,string cost)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            ActiveRepairViewModel ARVM = new ActiveRepairViewModel();
            ARVM.repairman = _RepairmanService.GetById(Convert.ToInt32(Session["userId"]));
            ARVM.request = _RequestService.GetById(id);
            ARVM.customer = _CustomerService.GetById(ARVM.request.CustomerID);

            ARVM.request.Cost = Convert.ToDouble(cost);

            _RequestService.Update(ARVM.request);

            return View(ARVM);
        }

        public ActionResult completedrepair()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            return View();
        }

        public ActionResult viewDriver()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            return View();
        }

        public ActionResult completeRequest(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"]) != 2))
                return RedirectToAction("login", "Common");

            Request request = _RequestService.GetById(id);
            request.Status = "Completed";
            _RequestService.Update(request);

            Repairman repairman = _RepairmanService.GetById(request.RepairmanID);
            repairman.Status = "Idle";
            _RepairmanService.Update(repairman);
            return RedirectToAction("dashboard", "Repairman");
        }


    }
}
