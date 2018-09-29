using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Service.Interfaces
{
    public interface IReportService
    {
        IEnumerable<Report> GetAll();
        Report GetById(int ReportId);
        bool Insert(Report report);
        bool Update(Report report);
        bool Delete(int ReportId);
    }
}
