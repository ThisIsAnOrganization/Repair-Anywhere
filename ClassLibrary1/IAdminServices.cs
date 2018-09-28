using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IAdminServices
    {
        IEnumerable<Admin> GetAll();
        Admin GetById(int AdminID);
        bool Insert(Admin admin);
        Admin Insert(Admin admin, bool returnAdmin);
        bool Update(Admin admin);
        bool Delete(int AdminID);
    }
}
