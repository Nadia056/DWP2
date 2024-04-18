using Microsoft.EntityFrameworkCore;
using DWP2.Models;


namespace DWP2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Customers> customers { get; set; }
        public DbSet<Countries> countries { get; set; }
        public DbSet<Regions> regions { get; set; }
        public DbSet<Locations> locations { get; set; }
        public DbSet<Employees> employees { get; set; }
        public DbSet<Orders> orders { get; set; }

        /*public DbSet<Contacts> contacts { get; set; }
        public DbSet<Countries> countries { get; set; }
        public DbSet<Employees> employees { get; set; }
        public DbSet<Inventories> inventories { get; set; }
        public DbSet<Locations> locations { get; set; }
        public DbSet<Order_Items> order_items { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Product_Categories> product_categories { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Regions> regions { get; set; }
        public DbSet<Warehouses> warehouses { get; set; }
        public DbSet<SqlProductosCategoria> sqlproductoscategoria { get; set; }

        [DbFunction(Schema = "dbo")]
       */
        public static int fn_PorductCategory_count(int pCategoryId)
        {
            throw new Exception();
        }
        public DbSet<DWP2.Models.Contacts> Contacts { get; set; } = default!;
        public DbSet<DWP2.Models.Products> Products { get; set; } = default!;
        public DbSet<DWP2.Models.Product_Categories> Product_Categories { get; set; } = default!;
        public DbSet<DWP2.Models.Order_Items> Order_Items { get; set; } = default!;
        public DbSet<DWP2.Models.Warehouses> Warehouses { get; set; } = default!;

        public DbSet<Inventories> Inventories { get; set; }
        // Agrega tus DbSet para Products, Warehouses y otras entidades aquí

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventories>()
                .HasKey(i => new { i.PRODUCT_ID, i.WAREHOUSE_ID });
            // Agrega cualquier otra configuración de entidad aquí
        }
    }
}
