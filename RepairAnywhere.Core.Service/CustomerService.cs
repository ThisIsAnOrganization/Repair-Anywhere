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
    public class CustomerService : ICustomerService
    {
        DbContext _context;

        public CustomerService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Set<Customer>().ToList();
        }


        public Customer GetById(int CustomerID)
        {
            return _context.Set<Customer>().Where(i => i.CustomerID == CustomerID).SingleOrDefault();
        }

        public bool Insert(Customer customer)
        {
            try
            {
                _context.Entry(customer).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Customer customer)
        {
            if (_context.Set<Customer>().Any(e => e.CustomerID == customer.CustomerID))
            {
                _context.Set<Customer>().Attach(customer);
                _context.Entry(customer).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int CustomerID)
        {
            var DeleteCustomer = _context.Set<Customer>().Where(i => i.CustomerID == CustomerID).SingleOrDefault();
            /// 

            if (DeleteCustomer != null)
            {
                _context.Set<Customer>().Remove(DeleteCustomer);
                _context.SaveChanges();
            }
            return true;
        }

        public IEnumerable<Customer> GetByName(string name)
        {
            return _context.Set<Customer>().Where(i => i.Name.Contains(name));
        }
    }
}
