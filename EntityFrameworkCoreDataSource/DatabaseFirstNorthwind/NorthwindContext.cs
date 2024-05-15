using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseFirstNorthwind
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlphabeticalListOfProducts> AlphabeticalListOfProducts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; }
        public virtual DbSet<CurrentProductList> CurrentProductList { get; set; }
        public virtual DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCity { get; set; }
        public virtual DbSet<CustomerCustomerDemo> CustomerCustomerDemo { get; set; }
        public virtual DbSet<CustomerDemographics> CustomerDemographics { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderDetailsExtended> OrderDetailsExtended { get; set; }
        public virtual DbSet<OrderSubtotals> OrderSubtotals { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersQry> OrdersQry { get; set; }
        public virtual DbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrice { get; set; }
        public virtual DbSet<ProductsByCategory> ProductsByCategory { get; set; }
        public virtual DbSet<QuarterlyOrders> QuarterlyOrders { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SalesByCategory> SalesByCategory { get; set; }
        public virtual DbSet<SalesTotalsByAmount> SalesTotalsByAmount { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarter { get; set; }
        public virtual DbSet<SummaryOfSalesByYear> SummaryOfSalesByYear { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Northwind;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlphabeticalListOfProducts>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Alphabetical list of products");
            });

            modelBuilder.Entity<CategorySalesFor1997>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Category Sales for 1997");
            });

            modelBuilder.Entity<CurrentProductList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Current Product List");

                entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CustomerAndSuppliersByCity>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Customer and Suppliers by City");

                entity.Property(e => e.Relationship).IsUnicode(false);
            });

            modelBuilder.Entity<CustomerCustomerDemo>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.CustomerTypeId })
                    .IsClustered(false);

                entity.Property(e => e.CustomerId).IsFixedLength();

                entity.Property(e => e.CustomerTypeId).IsFixedLength();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerCustomerDemo)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCustomerDemo_Customers");

                entity.HasOne(d => d.CustomerType)
                    .WithMany(p => p.CustomerCustomerDemo)
                    .HasForeignKey(d => d.CustomerTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerCustomerDemo");
            });

            modelBuilder.Entity<CustomerDemographics>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeId)
                    .IsClustered(false);

                entity.Property(e => e.CustomerTypeId).IsFixedLength();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).IsFixedLength();
            });

            modelBuilder.Entity<EmployeeTerritory>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TerritoryId })
                    .IsClustered(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTerritories_Employees");

                entity.HasOne(d => d.Territory)
                    .WithMany(p => p.EmployeeTerritories)
                    .HasForeignKey(d => d.TerritoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeTerritories_Territories");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.DirectReports)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_Employees_Employees");
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Invoices");

                entity.Property(e => e.CustomerId).IsFixedLength();
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_Order_Details");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Products");
            });

            modelBuilder.Entity<OrderDetailsExtended>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Order Details Extended");
            });

            modelBuilder.Entity<OrderSubtotals>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Order Subtotals");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CustomerId).IsFixedLength();

                entity.Property(e => e.Freight).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.Shipper)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_Orders_Shippers");
            });

            modelBuilder.Entity<OrdersQry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Orders Qry");

                entity.Property(e => e.CustomerId).IsFixedLength();
            });

            modelBuilder.Entity<ProductSalesFor1997>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Product Sales for 1997");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<ProductsAboveAveragePrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Products Above Average Price");
            });

            modelBuilder.Entity<ProductsByCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Products by Category");
            });

            modelBuilder.Entity<QuarterlyOrders>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Quarterly Orders");

                entity.Property(e => e.CustomerId).IsFixedLength();
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .IsClustered(false);

                entity.Property(e => e.RegionId).ValueGeneratedNever();

                entity.Property(e => e.RegionDescription).IsFixedLength();
            });

            modelBuilder.Entity<SalesByCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Sales by Category");
            });

            modelBuilder.Entity<SalesTotalsByAmount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Sales Totals by Amount");
            });

            modelBuilder.Entity<SummaryOfSalesByQuarter>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Summary of Sales by Quarter");
            });

            modelBuilder.Entity<SummaryOfSalesByYear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Summary of Sales by Year");
            });

            modelBuilder.Entity<Territory>(entity =>
            {
                entity.HasKey(e => e.TerritoryId)
                    .IsClustered(false);

                entity.Property(e => e.TerritoryDescription).IsFixedLength();

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Territories)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Territories_Region");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
