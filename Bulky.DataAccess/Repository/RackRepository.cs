using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;

namespace WMS.DataAccess.Repository
{
    public class RackRepository : Repository<Rack>, IRackRepository
    {
        private ApplicationDbContext _db;
        public RackRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Rack obj)
        {
            _db.Rack.Update(obj);
        }
    }
}
