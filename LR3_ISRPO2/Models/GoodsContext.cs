using Microsoft.EntityFrameworkCore;
namespace LR3_ISRPO2.Models
{
    public class GoodsContext : DbContext
    {
        public DbSet<Goods> Goods { get; set; }
        public GoodsContext(DbContextOptions<GoodsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
