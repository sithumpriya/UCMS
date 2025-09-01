using System.Linq.Expressions;
using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IPickNoteRackLocationRepository : IRepository<PickNoteRackLocation>
    {
        void Update(PickNoteRackLocation obj);
        IEnumerable<PickNoteRackLocation> GetWithIncludes(Expression<Func<PickNoteRackLocation, bool>> predicate, params Expression<Func<PickNoteRackLocation, object>>[] includes);
    }
}
