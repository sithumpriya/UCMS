using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;
using WMS.Models;

namespace WMS.DataAccess.Repository
{
    public class UnitOfMeasureRepository : Repository<UnitOfMeasure>, IUnitOfMeasureRepository
    {
        private ApplicationDbContext _db;
        public UnitOfMeasureRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UnitOfMeasure obj)
        {
            _db.UnitOfMeasure.Update(obj);
        }
    }
}
