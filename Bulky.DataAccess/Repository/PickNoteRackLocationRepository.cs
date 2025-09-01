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
    public class PickNoteRackLocationRepository : Repository<PickNoteRackLocation>, IPickNoteRackLocationRepository
    {
        private ApplicationDbContext _db;
        public PickNoteRackLocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PickNoteRackLocation obj)
        {
            _db.PickNoteRackLocation.Update(obj);
        }

        public IEnumerable<PickNoteRackLocation> GetWithIncludes(Expression<Func<PickNoteRackLocation, bool>> predicate, params Expression<Func<PickNoteRackLocation, object>>[] includes)
        {
            IQueryable<PickNoteRackLocation> query = _db.PickNoteRackLocation;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(predicate).ToList();
        }
    }
}
