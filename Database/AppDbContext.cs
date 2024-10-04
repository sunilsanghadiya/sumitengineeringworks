using Microsoft.EntityFrameworkCore;

namespace sew.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) 
    {

    }

}
