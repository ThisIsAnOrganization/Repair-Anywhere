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
    public class AdminService : IAdminService
    {
        DbContext _context;

        public AdminService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Admin> GetAll()
        {
            return _context.Set<Admin>().ToList();
        }


        public Admin GetById(int AdminID)
        {
            return _context.Set<Admin>().Where(i => i.AdminID == AdminID).SingleOrDefault();
        }

        public bool Insert(Admin admin)
        {
            try
            {
                _context.Entry(admin).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Admin admin)
        {
            if (_context.Set<Admin>().Any(e => e.AdminID == admin.AdminID))
            {
                _context.Set<Admin>().Attach(admin);
                _context.Entry(admin).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int AdminID)
        {
            var DeleteAdmin = _context.Set<Admin>().Where(i => i.AdminID == AdminID).SingleOrDefault();
            /// 

            if (DeleteAdmin != null)
            {
                _context.Set<Admin>().Remove(DeleteAdmin);
                _context.SaveChanges();
            }
            return true;
        }

    }
}