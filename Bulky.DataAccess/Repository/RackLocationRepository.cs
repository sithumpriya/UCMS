using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;

namespace WMS.DataAccess.Repository
{
    public class RackLocationRepository : Repository<RackLocation>, IRackLocationRepository
    {
        private ApplicationDbContext _db;
        public RackLocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(RackLocation obj)
        {
            _db.RackLocation.Update(obj);
        }
    }
}
