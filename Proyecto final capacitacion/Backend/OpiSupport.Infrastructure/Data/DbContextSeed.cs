using Microsoft.Extensions.DependencyInjection;
using OpiSupport.Domain.Entities;
using System;
using System.Linq;
using BCrypt.Net;

namespace OpiSupport.Infrastructure.Data
{
    public static class DbContextSeed
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // 1. Crear categorías si no existen
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Hardware" },
                    new Category { Name = "Software" },
                    new Category { Name = "Red" },
                    new Category { Name = "Infraestructura / Instalaciones" },
                    new Category { Name = "Otro" }
                );
            }

            // 2. Crear áreas de reporte si no existen
            if (!context.Areas.Any())
            {
                context.Areas.AddRange(
                    new Area { Name = "Sistemas / TI" },
                    new Area { Name = "Recursos Humanos" },
                    new Area { Name = "Contabilidad / Finanzas" },
                    new Area { Name = "Operaciones / Planta" },
                    new Area { Name = "Dirección / Gerencia" },
                    new Area { Name = "Ventas / Comercial" },
                    new Area { Name = "Logística / Bodega" },
                    new Area { Name = "Servicios Generales / Mantenimiento" }
                );
            }

            // 3. Crear configuración del sistema si no existe
            if (!context.SystemConfigs.Any())
            {
                context.SystemConfigs.Add(
                    new SystemConfig { Key = "MaxTicketsPerTechnician", Value = "5" }
                );
            }

            // 4. Crear usuario supervisor por defecto (si no existe)
            if (!context.Users.Any())
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin123");
                context.Users.Add(new User
                {
                    FullName = "Supervisor Principal",
                    EmployeeCode = "SUP-001",
                    Email = "supervisor@opitech.com",
                    PasswordHash = passwordHash,
                    Role = "Supervisor",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // Crear usuario "Sistema" (para procesos automáticos como SLA)
            if (!context.Users.Any(u => u.EmployeeCode == "SYS-000"))
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword("System123!"); // Contraseña segura, no la usarás para login
                context.Users.Add(new User
                {
                    FullName = "Sistema",
                    EmployeeCode = "SYS-000",
                    Email = "system@opitech.com",
                    PasswordHash = passwordHash,
                    Role = "Sistema",        // Nuevo rol (no tiene permisos de login)
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    ActiveReopenedCount = 0
                });
            }


            // 4. Guardar todos los cambios en la base de datos
            context.SaveChanges();
        }
    }
}