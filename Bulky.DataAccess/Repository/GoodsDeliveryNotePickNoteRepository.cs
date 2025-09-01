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
    public class GoodsDeliveryNotePickNoteRepository : Repository<GoodsDeliveryNotePickNote>, IGoodsDeliveryNotePickNoteRepository
    {
        private ApplicationDbContext _db;
        public GoodsDeliveryNotePickNoteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GoodsDeliveryNotePickNote obj)
        {
            _db.GoodsDeliveryNotePickNote.Update(obj);
        }
    }
}
