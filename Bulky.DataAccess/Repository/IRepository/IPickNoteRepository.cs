using System.Linq.Expressions;
using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IPickNoteRepository : IRepository<PickNote>
    {
        void Update(PickNote obj);
        IEnumerable<PickNote> GetWithIncludes(Expression<Func<PickNote, bool>> predicate, params Expression<Func<PickNote, object>>[] includes);
    }
}
