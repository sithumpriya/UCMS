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
    public class InternalMoveRepository : Repository<InternalMove>, IInternalMoveRepository
    {
        private ApplicationDbContext _db;
        public InternalMoveRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InternalMove obj)
        {
            _db.InternalMove.Update(obj);
        }

        public IEnumerable<InternalMove> GetWithIncludes(Expression<Func<InternalMove, bool>> predicate, params Expression<Func<InternalMove, object>>[] includes)
        {
            IQueryable<InternalMove> query = _db.InternalMove;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(predicate).ToList();
        }
    }
}
