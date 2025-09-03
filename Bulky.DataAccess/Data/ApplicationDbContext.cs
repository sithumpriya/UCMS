using WMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WMS.Utility;

namespace WMS.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Rack> Rack { get; set; }
        public DbSet<RackLocation> RackLocation { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<GoodsReceivedNote> GoodsReceivedNote { get; set; }
        public DbSet<GoodsReceivedNoteProduct> GoodsReceivedNoteProduct { get; set; }
        public DbSet<PickNote> PickNote { get; set; }
        public DbSet<PickNoteRackLocation> PickNoteRackLocation { get; set; }
        public DbSet<GoodsDeliveryNote> GoodsDeliveryNote { get; set; }
        public DbSet<GoodsDeliveryNotePickNote> GoodsDeliveryNotePickNote { get; set; }
        public DbSet<InternalMove> InternalMove { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<LectureNote> LectureNote { get; set; }
        public DbSet<CourseResult> CourseResult { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var encryptConverter = new ValueConverter<string, string>(
            v => v == null ? null : EncryptionHelper.Encrypt(v),
            v => v == null ? null : EncryptionHelper.Decrypt(v)
        );

            modelBuilder.Entity<Customer>()
                .Property(c => c.MobileNumber)
                .HasConversion(encryptConverter);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Address)
                .HasConversion(encryptConverter);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    Id = 1, 
                    Name = "Product 1", 
                    Description = "TestDescription0001", 
                    OutBoundMethod = "FIFO",
                    UnitOfMeasure = "Kg",
                    CategoryId = 1
                },

                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Description = "TestDescription0002",
                    OutBoundMethod = "LIFO",
                    UnitOfMeasure = "Kg",
                    CategoryId = 1
                },

                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    Description = "TestDescription0003",
                    OutBoundMethod = "FIFO",
                    UnitOfMeasure = "Kg",
                    CategoryId = 2
                }

            );
        }
    }
}
