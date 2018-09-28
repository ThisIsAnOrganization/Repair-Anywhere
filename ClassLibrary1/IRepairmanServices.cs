using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IRepairmanServices
    {
        IEnumerable<Repairman> GetAll();
        Repairman GetById(int RepairmanID);
        bool Insert(Repairman report);
        Repairman Insert(Repairman repairman, bool returnRepairman);
        bool Update(Repairman repairman);
        bool Delete(int RepairmanID);
    }
}
