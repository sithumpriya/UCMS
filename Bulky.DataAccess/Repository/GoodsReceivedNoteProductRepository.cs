using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;

namespace WMS.DataAccess.Repository
{
    public class GoodsReceivedNoteProductRepository : Repository<GoodsReceivedNoteProduct>, IGoodsReceivedNoteProductRepository
    {
        private ApplicationDbContext _db;
        public GoodsReceivedNoteProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GoodsReceivedNoteProduct obj)
        {
            _db.GoodsReceivedNoteProduct.Update(obj);
        }

        public IEnumerable<GoodsReceivedNoteProduct> GetWithIncludes(Expression<Func<GoodsReceivedNoteProduct, bool>> predicate, params Expression<Func<GoodsReceivedNoteProduct, object>>[] includes)
        {
            IQueryable<GoodsReceivedNoteProduct> query = _db.GoodsReceivedNoteProduct;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(predicate).ToList();
        }

    }
}
