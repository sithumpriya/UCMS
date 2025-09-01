using System.Linq.Expressions;
using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IGoodsReceivedNoteProductRepository : IRepository<GoodsReceivedNoteProduct>
    {
        void Update(GoodsReceivedNoteProduct obj);
        IEnumerable<GoodsReceivedNoteProduct> GetWithIncludes(Expression<Func<GoodsReceivedNoteProduct, bool>> predicate, params Expression<Func<GoodsReceivedNoteProduct, object>>[] includes);

    }
}
