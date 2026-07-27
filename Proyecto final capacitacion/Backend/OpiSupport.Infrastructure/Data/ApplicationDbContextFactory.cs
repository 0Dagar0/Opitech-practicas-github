using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OpiSupport.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            //  Configuración directa para las herramientas de migración
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            //  Aquí va la cadena de conexión que usaremos para crear las migraciones
            var connectionString = "Host=localhost;Port=5432;Database=OpiSupportDB;Username=postgres;Password=Postgres01";

            optionsBuilder.UseNpgsql(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

