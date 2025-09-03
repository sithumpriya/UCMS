namespace WMS.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IUnitOfMeasureRepository UnitOfMeasure { get; }
        ISubCategoryRepository SubCategory { get; }
        IWarehouseRepository Warehouse { get; }
        IRackRepository Rack { get; }
        IRackLocationRepository RackLocation { get; }
        ICustomerRepository Customer { get; }
        IGoodsReceivedNoteRepository GoodsReceivedNote { get; }
        IGoodsReceivedNoteProductRepository GoodsReceivedNoteProduct { get; }
        IPickNoteRepository PickNote { get; }
        IPickNoteRackLocationRepository PickNoteRackLocation { get; }
        IGoodsDeliveryNoteRepository GoodsDeliveryNote { get; }
        IGoodsDeliveryNotePickNoteRepository GoodsDeliveryNotePickNote { get; }
        IInternalMoveRepository InternalMove { get; }
        ICourseRepository Course { get; }
        ILectureNoteRepository LectureNote { get; }
        ICourseResultRepository CourseResult { get; }

        void Save();
    }
}
