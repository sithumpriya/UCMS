using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IGoodsDeliveryNoteRepository : IRepository<GoodsDeliveryNote>
    {
        void Update(GoodsDeliveryNote obj);
    }
}
