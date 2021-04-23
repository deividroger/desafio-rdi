using desafio_rdi.domain.Models; 
using Microsoft.EntityFrameworkCore;

namespace desafio_rdi.domain.Repositories
{
    public  class DesafioDbContext: DbContext
    {
        public DesafioDbContext(DbContextOptions<DesafioDbContext> options)
         : base(options)
        { }
        
        public DbSet<CustomerCard> CustomerCard { get; set; }

    }
}
