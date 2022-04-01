#nullable disable
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NextHome.Data
{
    public class NextHomeContext : IdentityDbContext
    {
        public NextHomeContext(DbContextOptions<NextHomeContext> options)
            : base(options)
        {
        }

        public DbSet<NextHome.Models.Estate> Estate { get; set; }
    }
}
