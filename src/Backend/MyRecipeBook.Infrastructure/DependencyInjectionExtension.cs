using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddDbContext_SqlServer(services);
        AddRepositories(services);
    }

    private static void AddDbContext_SqlServer(IServiceCollection services)
    {
        var connectionString = "Data Source=ANAQUEIROZ\\SQLEXPRESS;Initial Catalog=meulivrodereceitas;User ID=ana_sqlserver;Password=@1250sqlserver;TrustServerCertificate=True;";
        services.AddDbContext<MyRecipeBookDbContext>(options => {
            options.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }
}
