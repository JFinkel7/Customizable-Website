using MainActivity.Models;
using Microsoft.EntityFrameworkCore;

namespace MainActivity.Repository {
    public sealed class PrimeDbContext : DbContext {

        public PrimeDbContext(DbContextOptions<PrimeDbContext> options) : base(options) { }

        // [Table - ArticleContent] 
        public DbSet<ArticleContent> ArticleContents { get; set; }

        // [Table - Administrator] 
        public DbSet<Administrator> Administrators { get; set; }

    }// CLASS ENDS 
}
