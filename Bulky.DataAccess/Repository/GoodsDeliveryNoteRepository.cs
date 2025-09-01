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
    public class GoodsDeliveryNoteRepository : Repository<GoodsDeliveryNote>, IGoodsDeliveryNoteRepository
    {
        private ApplicationDbContext _db;
        public GoodsDeliveryNoteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GoodsDeliveryNote obj)
        {
            _db.GoodsDeliveryNote.Update(obj);
        }
    }
}
