using System.Linq.Expressions;
using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IInternalMoveRepository : IRepository<InternalMove>
    {
        void Update(InternalMove obj);
        IEnumerable<InternalMove> GetWithIncludes(Expression<Func<InternalMove, bool>> predicate, params Expression<Func<InternalMove, object>>[] includes);
    }
}
