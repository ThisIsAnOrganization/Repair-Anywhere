using RepairAnywhere.Core.Entities;
using RepairAnywhere.Core.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Service
{
    public class RequestService : IRequestService
    {
        DbContext _context;

        public RequestService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Request> GetAll()
        {
            return _context.Set<Request>().ToList();
        }


        public Request GetById(int RequestID)
        {
            return _context.Set<Request>().Where(i => i.RequestID == RequestID).SingleOrDefault();
        }

        public bool Insert(Request request)
        {
            try
            {
                _context.Entry(request).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Request request)
        {
            if (_context.Set<Request>().Any(e => e.RequestID == request.RequestID))
            {
                _context.Set<Request>().Attach(request);
                _context.Entry(request).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int RequestID)
        {
            var DeleteRequest = _context.Set<Request>().Where(i => i.RequestID == RequestID).SingleOrDefault();
            /// 

            if (DeleteRequest != null)
            {
                _context.Set<Request>().Remove(DeleteRequest);
                _context.SaveChanges();
            }
            return true;
        }


        public IEnumerable<Request> GetPendingByCustomer(int CustomerID)
        {
            return _context.Set<Request>().Where(i => i.CustomerID == CustomerID && i.Status=="Pending").ToList();
        }

        public IEnumerable<Request> GetActiveByCustomer(int CustomerID)
        {
            return _context.Set<Request>().Where(i => i.CustomerID == CustomerID && i.Status == "Active").ToList();
        }

        public IEnumerable<Request> GetCompletedByCustomer(int CustomerID)
        {
            return _context.Set<Request>().Where(i => i.CustomerID == CustomerID && i.Status == "Completed").ToList();
        }

        public IEnumerable<Request> GetPendingByRepairman(int RepairmanID)
        {
            return _context.Set<Request>().Where(i => i.RepairmanID == RepairmanID && i.Status == "Pending").ToList();
        }

        public IEnumerable<Request> GetActiveByRepairman(int RepairmanID)
        {
            return _context.Set<Request>().Where(i => i.RepairmanID == RepairmanID && i.Status == "Active").ToList();
        }

        public IEnumerable<Request> GetCompletedByRepairman(int RepairmanID)
        {
            return _context.Set<Request>().Where(i => i.RepairmanID == RepairmanID && i.Status == "Completed").ToList();
        }
    }
}
