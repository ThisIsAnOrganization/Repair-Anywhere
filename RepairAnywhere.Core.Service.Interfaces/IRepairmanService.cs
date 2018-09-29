using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Service.Interfaces
{
    public interface IRepairmanService
    {
        IEnumerable<Repairman> GetAll();
        Repairman GetById(int RepairmanId);
        bool Insert(Repairman repairman);
        bool Update(Repairman repairman);
        bool Delete(int RepairmanId);
    }
}
