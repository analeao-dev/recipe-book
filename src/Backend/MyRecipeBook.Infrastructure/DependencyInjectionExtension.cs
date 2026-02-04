using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Se utilizar mais de um banco de dados, descomentar o código abaixo e fazer configurações necessárias
        //var databaseType = configuration.GetConnectionString("DatabaseType");

        //var databaseTypeEnum = (DatabaseType)Enum.Parse(typeof(DatabaseType), databaseType);

        //if (databaseTypeEnum == DatabaseType.SqlServer)
        //{
        //    AddDbContext_SqlServer(services, configuration);
        //}
        //else
        //{
        //    AddDbContext_MySqlServer(services, configuration);
        //}

        AddDbContext_SqlServer(services, configuration);
        AddRepositories(services);
    }

    private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionSqlServer");
        services.AddDbContext<MyRecipeBookDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    //private static void AddDbContext_MySqlServer(IServiceCollection services, IConfiguration configuration)
    //{
    //    var connectionString = "";
    //    services.AddDbContext<MyRecipeBookDbContext>(options =>
    //    {
    //        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    //    });
    //}

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }
}
