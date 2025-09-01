using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        void Update(Warehouse obj);
    }
}
