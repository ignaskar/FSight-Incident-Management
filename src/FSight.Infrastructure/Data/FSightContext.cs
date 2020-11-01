using Microsoft.EntityFrameworkCore;

namespace FSight.Infrastructure.Data
{
    public class FSightContext : DbContext
    {
        public FSightContext(DbContextOptions<FSightContext> options) : base(options)
        {
            
        }
    }
}