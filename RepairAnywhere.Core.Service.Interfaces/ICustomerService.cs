using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Service.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int CustomerId);
        bool Insert(Customer customer);
        bool Update(Customer customer);
        bool Delete(int CustomerId);
    }
}
