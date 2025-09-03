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
    public class CourseResultRepository : Repository<CourseResult>, ICourseResultRepository
    {
        private ApplicationDbContext _db;
        public CourseResultRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CourseResult obj)
        {
            _db.CourseResult.Update(obj);
        }
    }
}
