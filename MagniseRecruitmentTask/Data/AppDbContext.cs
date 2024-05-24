using MagniseRecruitmentTask.Models;
using Microsoft.EntityFrameworkCore;

namespace MagniseRecruitmentTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {

        }

        internal DbSet<Coin> Coins { get; set; }
    }
}
