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
            MSVM.requests = _RequestService.GetCompletedandDisaprovedByCustomer(MSVM.customer.CustomerID);

            IEnumerable<Review> reviews = _ReviewService.GetAll();
            IEnumerable<Review> common;
            int c = 0;
            foreach (var item in MSVM.requests)
            {
                if (item.Status=="Completed")
                {
                    MSVM.repairmen[c] = _RepairmanService.GetById(item.RepairmanID);
                    MSVM.count[c] = 0;
                    MSVM.rating[c] = 0;
                    common = _ReviewService.GetByBothId(MSVM.repairmen[c].RepairmanID, MSVM.customer.CustomerID);

                    if ((common.Count() > 0) && (common.SingleOrDefault().Rating > 0))
                        MSVM.flagR[c] = 1;

                    else
                        MSVM.flagR[c] = 0;

                    
                }
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
                if (item != null)
                {
                    MSVM.completecount[c] = 0;
                    IEnumerable<Request> rs = _RequestService.GetAll();
                    if (item != null)
                    {
                        foreach (var item3 in rs)
                        {
                            if (((item3.Status == "Completed" && (item3.RepairmanID == item.RepairmanID)) || (item3.Status == "Disaproved")))
                                MSVM.completecount[c]++;
                        }
                    }
                }
                c++;
            }
            c = 0;
            foreach (var item in MSVM.requests)
            {
                MSVM.details[c] = "Details" + Convert.ToString(c + 1);
                if (item.Status == "Completed")
                MSVM.drivers[c] = "Drivers" + Convert.ToString(c + 1);
                c++;
            }
            //MSVM.requests = MSVM.requests.Reverse().AsEnumerable();
            //MSVM.repairmen = MSVM.repairmen.Reverse().ToArray();
            //MSVM.details = MSVM.details.Reverse().ToArray();
            //MSVM.drivers = MSVM.drivers.Reverse().ToArray();
            return View(MSVM);
        }

        public ActionResult RequestService(int flag)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            RequestServiceViewModel RSVM = new RequestServiceViewModel();

            RSVM.customer = _CustomerService.GetById(Convert.ToInt32(Session["userId"]));
            RSVM.flag = 0;
            RSVM.address = RSVM.customer.Address;
            RSVM.flag1 = flag;
            if (flag!=0)
            {
                RSVM.request = _RequestService.GetById(flag);
            }
            
            return View(RSVM);
        }


        [HttpPost]
        public ActionResult RequestService(string category, string prob, string address,int flag1)
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

                if (flag1>0)
                {
                    _RequestService.Delete(flag1);
                }



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

        public ActionResult deleteRequest(int id,string p)
        {
            if ((Session["userId"] == null) || (Convert.ToInt32(Session["type"])!=1))
                return RedirectToAction("login", "Common");

            _RequestService.Delete(id);

            if(p=="tripdetails")
                return RedirectToAction(p, "Customer", new { id=id});

            else
                return RedirectToAction(p, "Customer");
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

        public ActionResult changeRate(string star,string comment, int r, int c,string page)
        {
            IEnumerable<Review> reviews = _ReviewService.GetByBothId(r, c);
            if (Equals(star,""))
            {
                star = "0";
            }
            if (reviews.Count() > 0)
            {
                foreach (var item in reviews)
                {
                    item.Rating = Convert.ToDouble(star);
                    item.Comment = comment;
                    _ReviewService.Update(item);
                }

            }

            else
            {
                Review r1 = new Review();

                r1.CustomerID = c;
                r1.RepairmanID = r;
                r1.Rating = Convert.ToDouble(star);
                r1.Comment = comment;
                _ReviewService.Insert(r1);
            }
            Repairman rr = _RepairmanService.GetById(r);

            IEnumerable<Review> reviews1 = _ReviewService.GetByRepairmanId(r);
            double rat;
            if (reviews1.Count() != 0)
            {
                rat = (((reviews1.Count() - 1) * rr.Rating) + Convert.ToDouble(star)) / reviews1.Count();
                rr.Rating = rat;
            }
            _RepairmanService.Update(rr);

            if (Equals(page, "myServices"))
                return RedirectToAction(page, "Customer");
            
            else
                return RedirectToAction(page, "Customer", new { id = r });

        }

        public ActionResult logout()
        {
            Session["userId"] = null;
            Session["type"] = null;
            return RedirectToAction("index", "Common");
        }


    }
}
