﻿using RepairAnywhere.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairAnywhere.Core.Service.Interfaces
{
    public interface IReviewService
    {
        IEnumerable<Review> GetAll();
        Review GetById(int ReviewId);
        bool Insert(Review review);
        bool Update(Review review);
        bool Delete(int ReviewId);
    }
}