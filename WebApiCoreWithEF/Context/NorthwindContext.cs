
namespace WebApiCoreWithEF.Context
{
    using Microsoft.EntityFrameworkCore;
    using WebApiCoreWithEF.Models;

    public class NorthwindContext
        : DbContext
    {
        public NorthwindContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
    }
}
