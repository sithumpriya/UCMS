using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IRackRepository : IRepository<Rack>
    {
        void Update(Rack obj);
    }
}
