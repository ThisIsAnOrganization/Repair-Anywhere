using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Service.Interfaces
{
    public interface IRequestService
    {
        IEnumerable<Request> GetAll();
        Request GetById(int RequestId);
        bool Insert(Request request);
        bool Update(Request request);
        bool Delete(int RequestId);

        IEnumerable<Request> GetPendingByCustomer(int CustomerID);
        IEnumerable<Request> GetActiveByCustomer(int CustomerID);
        IEnumerable<Request> GetCompletedByCustomer(int CustomerID);

        IEnumerable<Request> GetPendingByRepairman(int RepairmanID);
        IEnumerable<Request> GetActiveByRepairman(int RepairmanID);
        IEnumerable<Request> GetCompletedByRepairman(int RepairmanID);

        
    }
}
