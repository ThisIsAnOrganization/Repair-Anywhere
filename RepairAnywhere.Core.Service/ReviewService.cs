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
    public class ReviewService : IReviewService
    {
        DbContext _context;

        public ReviewService(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> GetAll()
        {
            return _context.Set<Review>().ToList();
        }


        public Review GetById(int ReviewID)
        {
            return _context.Set<Review>().Where(i => i.ReviewID == ReviewID).SingleOrDefault();
        }

        public bool Insert(Review review)
        {
            try
            {
                _context.Entry(review).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Review review)
        {
            if (_context.Set<Review>().Any(e => e.ReviewID == review.ReviewID))
            {
                _context.Set<Review>().Attach(review);
                _context.Entry(review).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Delete(int ReviewID)
        {
            var DeleteReview = _context.Set<Review>().Where(i => i.ReviewID == ReviewID).SingleOrDefault();
            /// 

            if (DeleteReview != null)
            {
                _context.Set<Review>().Remove(DeleteReview);
                _context.SaveChanges();
            }
            return true;
        }

        public IEnumerable<Review> GetByCustomerId(int CustomerID)
        {
            return _context.Set<Review>().Where(i => i.CustomerID == CustomerID).ToList();
        }

        public IEnumerable<Review> GetByRepairmanId(int RepairmanId)
        {
            return _context.Set<Review>().Where(i => i.RepairmanID == RepairmanId).ToList();
        }
    }
}
