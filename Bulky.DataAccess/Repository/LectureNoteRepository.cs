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
    public class LectureNoteRepository : Repository<LectureNote>, ILectureNoteRepository
    {
        private ApplicationDbContext _db;
        public LectureNoteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(LectureNote obj)
        {
            _db.LectureNote.Update(obj);
        }
    }
}
