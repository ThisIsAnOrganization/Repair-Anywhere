using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public interface IReportServices
    {
        IEnumerable<Report> GetAll();
        Report GetById(int ReportID);
        bool Insert(Report report);
        Report Insert(Report report, bool returnReport);
        bool Update(Report report);
        bool Delete(int ReportID);
    }
}
