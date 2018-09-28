using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IRequestServices
    {
        IEnumerable<Request> GetAll();
        Request GetById(int RequestID);
        bool Insert(Request request);
        Request Insert(Request request, bool returnRequest);
        bool Update(Repairman request);
        bool Delete(int RequestID);
    }
}
