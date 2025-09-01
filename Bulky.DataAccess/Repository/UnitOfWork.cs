using WMS.DataAccess.Data;
using WMS.DataAccess.Repository.IRepository;

namespace WMS.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IUnitOfMeasureRepository UnitOfMeasure { get; private set; }
        public ISubCategoryRepository SubCategory { get; private set; }
        public IWarehouseRepository Warehouse { get; private set; }
        public IRackRepository Rack { get; private set; }
        public IRackLocationRepository RackLocation { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IGoodsReceivedNoteRepository GoodsReceivedNote { get; private set; }
        public IGoodsReceivedNoteProductRepository GoodsReceivedNoteProduct { get; private set; }
        public IPickNoteRepository PickNote { get; private set; }
        public IPickNoteRackLocationRepository PickNoteRackLocation { get; private set; }
        public IGoodsDeliveryNoteRepository GoodsDeliveryNote { get; private set; }
        public IGoodsDeliveryNotePickNoteRepository GoodsDeliveryNotePickNote { get; private set; }
        public IInternalMoveRepository InternalMove { get; private set; }
        public ICourseRepository Course { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            UnitOfMeasure = new UnitOfMeasureRepository(_db);
            SubCategory = new SubCategoryRepository(_db);
            Warehouse = new WarehouseRepository(_db);
            Rack = new RackRepository(_db);
            RackLocation = new RackLocationRepository(_db);
            Customer = new CustomerRepository(_db);
            GoodsReceivedNote = new GoodsReceivedNoteRepository(_db);
            GoodsReceivedNoteProduct = new GoodsReceivedNoteProductRepository(_db);
            PickNote = new PickNoteRepository(_db);
            PickNoteRackLocation = new PickNoteRackLocationRepository(_db);
            GoodsDeliveryNote = new GoodsDeliveryNoteRepository(_db);
            GoodsDeliveryNotePickNote = new GoodsDeliveryNotePickNoteRepository(_db);
            InternalMove = new InternalMoveRepository(_db);
            Course = new CourseRepository(_db);
        }
        
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
