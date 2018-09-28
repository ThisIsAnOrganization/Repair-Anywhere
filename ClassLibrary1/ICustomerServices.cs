using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface ICustomerServices
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int CustomerID);
        bool Insert(Customer customer);
        Customer Insert(Customer customer, bool returnCustomer);
        bool Update(Customer customer);
        bool Delete(int CustomerID);
    }
}
