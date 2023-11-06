﻿
class ExamContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne<MainCategory>(_ => _.MainCategory)
            .WithMany(mc => mc.SubCategories)
            .HasForeignKey(_ => _.CategoryID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>().HasKey(_ => new { _.CustomerID, _.FoodItemID });

        modelBuilder.Entity<Order>()
            .HasOne<FoodItem>(_ => _.FoodItem)
            .WithMany()
            .HasForeignKey(_ => _.FoodItemID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne<Customer>(_ => _.Customer)
            .WithMany()
            .HasForeignKey(_ => _.CustomerID)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<Order>()
            .HasOne<Employee>(_ => _.Employee)
            .WithMany()
            .HasForeignKey(_ => _.EmployeeID)
            .OnDelete(DeleteBehavior.Cascade); 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID = postgres; Password = a; Host = localhost; port = 5432; Database = ExamDB2324Model; Pooling = true");
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<MainCategory> MainCategories { get; set; } = null!;
    public DbSet<FoodItem> FoodItems { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Employee> Employees { get;  set; } = null!;
}


class Employee
{
    public int ID { get; set; }
    [DataType("varchar(50)")]
    public string Name { get; set; } = null!;

    public Employee(int id, string name) {
        ID = id;
        Name = name;
    }
}

class MainCategory
{
    public int ID { get; set; }  //Convention
    public string Name { get; set; } = null!;
    public List<Category> SubCategories { get; set; } = null!;
}

class Category
{
    public int ID { get; set; }  //Convention
    public string Name { get; set; } = null!;
    public MainCategory? MainCategory { get; set; }
    public int? CategoryID { get; set; }

    //Constructors
    public Category() { }
    public Category(int id, string name)  { Name = name; ID = id; }
    public Category(int id, string name, int categoryID) : this(id, name) => CategoryID = categoryID;
}

class FoodItem
{
    public int ID { get; set; }  //Convention
    public string Name { get; set; } = null!;
    public Category? category { get; set; }
    public int CategoryID { get; set; }
    public decimal Price { get; set; }
    public string? Unit { get; set; }

    //Constructors
    public FoodItem(int iD, string name, int categoryID, decimal price)
    {
        ID = iD;
        Name = name;
        CategoryID = categoryID;
        Price = price;
    }
    public FoodItem(int iD, string name, int categoryID, decimal price, string unit)
        : this(iD, name, categoryID, price) => Unit = unit;

}

class Customer
{
    public int ID { get; set; }   //Convention
    public string? Name { get; set; }
    public DateTime DateTime { get; set; }
    public int? TableNumber { get; set; }
}

class Order
{
    public Customer Customer { get; set; } = null!;
    public int CustomerID { get; set; }  

    public FoodItem? FoodItem { get; set; }
    public int FoodItemID { get; set; }

    public int Quantity { get; set; }

    public Employee? Employee { get; set; }
    public int EmployeeID { get; set; }
}

