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
    public class ReportService : IReportService
    {
        DbContext _context;

        public ReportService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Report> GetAll()
        {
            return _context.Set<Report>().ToList();
        }


        public Report GetById(int ReportID)
        {
            return _context.Set<Report>().Where(i => i.ReportID == ReportID).SingleOrDefault();
        }

        public bool Insert(Report report)
        {
            try
            {
                _context.Entry(report).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Report report)
        {
            if (_context.Set<Report>().Any(e => e.ReportID == report.ReportID))
            {
                _context.Set<Report>().Attach(report);
                _context.Entry(report).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int ReportID)
        {
            var DeleteReport = _context.Set<Report>().Where(i => i.ReportID == ReportID).SingleOrDefault();
            /// 

            if (DeleteReport != null)
            {
                _context.Set<Report>().Remove(DeleteReport);
                _context.SaveChanges();
            }
            return true;
        }
    }
}
