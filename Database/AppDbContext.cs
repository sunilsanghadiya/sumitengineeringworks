using sew.Entities;
using static sew.Models.ViewModel;

namespace sew.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) 
    {

    }

    //ENTITIES
    public DbSet<Users> Users;
    public DbSet<Customers> Customers;
    public DbSet<Orders> Orders;

    //VIEWS
    public DbSet<UsersView> UsersViews;
    public DbSet<CustomersView> CustomersViews;
    public DbSet<OrdersView> OrdersViews;
    
    //STORE PRODCUERS


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
