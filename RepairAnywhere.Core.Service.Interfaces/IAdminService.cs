using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Service.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<Admin> GetAll();
        Admin GetById(int AdminId);
        bool Insert(Admin admin);
        bool Update(Admin admin);
        bool Delete(int AdminId);
    }
}
