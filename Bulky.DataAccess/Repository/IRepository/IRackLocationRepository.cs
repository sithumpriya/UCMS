using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IRackLocationRepository : IRepository<RackLocation>
    {
        void Update(RackLocation obj);
    }
}
