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
    public class GoodsReceivedNoteRepository : Repository<GoodsReceivedNote>, IGoodsReceivedNoteRepository
    {
        private ApplicationDbContext _db;
        public GoodsReceivedNoteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(GoodsReceivedNote obj)
        {
            _db.GoodsReceivedNote.Update(obj);
        }
    }
}
