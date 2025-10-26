using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.DataAccess;
public class MyRecipeBookDbContext : DbContext
{
    // o construtor recebe as opções de configuração e passa para a classe base DbContext
    public MyRecipeBookDbContext(DbContextOptions options) : base(options){}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas as configurações de entidade encontradas neste assembly (as configurações acima, dentro dessa propria classe)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
    }
}
