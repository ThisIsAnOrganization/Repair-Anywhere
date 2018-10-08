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
    public class LoginService : ILoginService
    {
        DbContext _context;

        public LoginService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Login> GetAll()
        {
            return _context.Set<Login>().ToList();
        }


        public Login GetByUsername(string Username)
        {
            return _context.Set<Login>().Where(i => Equals(i.Username, Username)).SingleOrDefault();
        }

        public bool Insert(Login login)
        {
            try
            {
                _context.Entry(login).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Login login)
        {
            if (_context.Set<Login>().Any(e => e.Username == login.Username))
            {
                _context.Set<Login>().Attach(login);
                _context.Entry(login).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Delete(string Username)
        {
            var DeleteLogin = _context.Set<Login>().Where(i => i.Username == Username).SingleOrDefault();
            /// 

            if (DeleteLogin != null)
            {
                _context.Set<Login>().Remove(DeleteLogin);
                _context.SaveChanges();
            }
            return true;
        }
    }
}
