using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IGoodsReceivedNoteRepository : IRepository<GoodsReceivedNote>
    {
        void Update(GoodsReceivedNote obj);
    }
}
