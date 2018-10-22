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
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
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
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
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
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            CompletedRepairsViewModel CRVM = new CompletedRepairsViewModel();

            CRVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(CRVM);
        }

        public ActionResult editProfile()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            EditProfileViewModel EPVM = new EditProfileViewModel();

            EPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(EPVM);
        }

        [HttpPost]
        public ActionResult editProfile(string name,string address,string phone)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
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
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            MyServicesViewModel MSVM = new MyServicesViewModel();

            MSVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));
            MSVM.requests = _RequestService.GetCompletedByCustomer(MSVM.customer.CustomerID);

            IEnumerable<Review> reviews = _ReviewService.GetAll();
            int c = 0;
            foreach (var item in MSVM.requests)
            {
                MSVM.repairmen[c] = _RepairmanService.GetById(item.RepairmanID);
                MSVM.count[c]= 0;
                MSVM.rating[c]= 0;
                c++;
            }
            c = 0;
            foreach (var item in MSVM.repairmen)
            {
                //foreach (var item1 in reviews)
                //{
                //    if (item.RepairmanID == item1.RepairmanID)
                //    {
                //        MSVM.rating[c] += item1.Rating;
                //        MSVM.count[c]++;
                //    }
                //}
                //if (MSVM.count[c] != 0)
                //    MSVM.rating[c] = MSVM.rating[c] / MSVM.count[c];

                MSVM.completecount[c] = 0;
                IEnumerable<Request> rs = _RequestService.GetAll();
                if (item != null)
                {
                    foreach (var item3 in rs)
                    {
                        if ((item3.Status == "Completed") && (item3.RepairmanID == item.RepairmanID))
                            MSVM.completecount[c]++;
                    }
                }
                c++;
            }
            c = 0;
            foreach (var item in MSVM.requests)
            {
                MSVM.details[c] = "Details" + Convert.ToString(c + 1);
                MSVM.drivers[c] = "Drivers" + Convert.ToString(c + 1);
                c++;
            }

            return View(MSVM);
        }

        public ActionResult RequestService()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            RequestServiceViewModel RSVM = new RequestServiceViewModel();

            RSVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));
            RSVM.flag = 0;
            RSVM.address = RSVM.customer.Address;
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
                r.Description = prob;
                r.CustomerID = RSVM.customer.CustomerID;
                r.Status = "Pending";
                r.Date = DateTime.Now;
                r.Address = address;

                _RequestService.Insert(r);



                return RedirectToAction("dashboard", "Customer");
            }
        }

        public ActionResult tripsdetails(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            TripsDeailsViewModel TDVM = new TripsDeailsViewModel();

            TDVM.request = _RequestService.GetById(id);
            TDVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            if (TDVM.request.Status != "Pending")
            {

                TDVM.repairman = _RepairmanService.GetById(TDVM.request.RepairmanID);
                IEnumerable<Request> requests = _RequestService.GetAll();
                TDVM.count = 0;
                foreach (var item in requests)
                {
                    if ((item.RepairmanID == TDVM.repairman.RepairmanID) && (item.Status == "Completed"))
                        TDVM.count++;
                }

            
                IEnumerable<Review> reviews = _ReviewService.GetAll();
                TDVM.rating = 0;
                double count = 0;

                foreach (var item in reviews)
                {
                    if (item.RepairmanID == TDVM.repairman.RepairmanID)
                    {
                        TDVM.rating += item.Rating;
                        count++;
                    }
                }

                if (count != 0)
                    TDVM.rating = TDVM.rating / count;

            }
            return View(TDVM);
        }

        public ActionResult deleteRequest(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            _RequestService.Delete(id);

            return RedirectToAction("dashboard", "Customer");
        }

        public ActionResult viewProfile()
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            ViewProfileViewModel VPVM = new ViewProfileViewModel();
            VPVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            return View(VPVM);
        }

        public ActionResult viewserviceprovider(int id)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            ViewServiceProviderViewModel VSVM = new ViewServiceProviderViewModel();

            VSVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));

            VSVM.repairman = _RepairmanService.GetById(id);
            VSVM.reviews = _ReviewService.GetByRepairmanId(id);
            VSVM.flag = 0;

            DateTime endTime = DateTime.Now;

            TimeSpan span = endTime.Subtract(VSVM.repairman.MemberSince);
            VSVM.experience = "";
            if (span.Days > 0)
            {
                if (span.Days > 365)
                {
                    VSVM.experience += ((int)(span.Days / 365)) + " Years";
                }
                if ((span.Days % 365) > 30)
                {
                    VSVM.experience += ((int)((span.Days % 365) / 30)) + " Months";
                }
                if (span.Days % 30 > 0)
                {
                    VSVM.experience += (span.Days % 30) + " Days";
                }


            }
            else
                VSVM.experience = "New";

            foreach (var item in VSVM.reviews)
            {
                if (item.CustomerID==VSVM.customer.CustomerID)
                {
                    VSVM.flag = 1;
                    break;
                }
            }
            VSVM.completed = _RequestService.GetCompletedByRepairman(id).Count();
            int c=0;
            foreach (var item in VSVM.reviews)
            {
                VSVM.customers[c] = _CustomerService.GetById(item.CustomerID);
                c++;
            }


            return View(VSVM);
        }

        public ActionResult addReview(int rid,string desc)
        {

            Review r = new Review();
            r.CustomerID = Convert.ToInt32(Session["userId"]);
            r.RepairmanID = rid;
            r.Comment = desc;
            _ReviewService.Insert(r);
            
            return RedirectToAction("viewserviceprovider", "Customer", new { id=rid});
        }

        public ActionResult deleteReview(int rid, int revid)
        {

            
            _ReviewService.Delete(revid);

            return RedirectToAction("viewserviceprovider", "Customer", new { id = rid });
        }

        public ActionResult logout()
        {
            Session["userId"] = null;
            Session["type"] = null;
            return RedirectToAction("index", "Common");
        }


    }
}
