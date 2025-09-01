using WMS.Models;

namespace WMS.DataAccess.Repository.IRepository
{
    public interface IUnitOfMeasureRepository : IRepository<UnitOfMeasure>
    {
        void Update(UnitOfMeasure obj);
    }
}
