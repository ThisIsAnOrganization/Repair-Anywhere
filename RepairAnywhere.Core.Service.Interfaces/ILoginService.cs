using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Service.Interfaces
{
    public interface ILoginService
    {
        IEnumerable<Login> GetAll();
        Login GetByUsername(string Username);
        bool Insert(Login login);
        bool Update(Login login);
        bool Delete(string Username);
    }
}
