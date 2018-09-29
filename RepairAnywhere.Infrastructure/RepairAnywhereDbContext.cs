using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Infrastructure
{
    public class RepairAnywhereDbContext : DbContext
    {
        public RepairAnywhereDbContext() : base("RepairAnywhereDbContext") { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Repairman> Repairmans { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Request> Requests { get; set; }

    }
}
