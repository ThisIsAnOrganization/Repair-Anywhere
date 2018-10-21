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
    public class RepairmanService : IRepairmanService
    {
        DbContext _context;

        public RepairmanService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Repairman> GetAll()
        {
            return _context.Set<Repairman>().ToList();
        }


        public Repairman GetById(int RepairmanID)
        {
            return _context.Set<Repairman>().Where(i => i.RepairmanID == RepairmanID).SingleOrDefault();
        }

        public bool Insert(Repairman repairman)
        {
            try
            {
                _context.Entry(repairman).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Repairman repairman)
        {
            if (_context.Set<Repairman>().Any(e => e.RepairmanID == repairman.RepairmanID))
            {
                _context.Set<Repairman>().Attach(repairman);
                _context.Entry(repairman).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int RepairmanID)
        {
            var DeleteRepairman = _context.Set<Repairman>().Where(i => i.RepairmanID == RepairmanID).SingleOrDefault();
            /// 

            if (DeleteRepairman != null)
            {
                _context.Set<Repairman>().Remove(DeleteRepairman);
                _context.SaveChanges();
            }
            return true;
        }

        public IEnumerable<Repairman> GetAllIdle()
        {
            return _context.Set<Repairman>().Where(i => i.Status == "Idle");
        }

        public IEnumerable<Repairman> GetByName(string name)
        {
            return _context.Set<Repairman>().Where(i => i.Name.Contains(name));
        }
    }
}
