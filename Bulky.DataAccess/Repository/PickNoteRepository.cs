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
    public class PickNoteRepository : Repository<PickNote>, IPickNoteRepository
    {
        private ApplicationDbContext _db;
        public PickNoteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PickNote obj)
        {
            _db.PickNote.Update(obj);
        }

        public IEnumerable<PickNote> GetWithIncludes(Expression<Func<PickNote, bool>> predicate, params Expression<Func<PickNote, object>>[] includes)
        {
            IQueryable<PickNote> query = _db.PickNote;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(predicate).ToList();
        }
    }
}
