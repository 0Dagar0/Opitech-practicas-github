using Microsoft.EntityFrameworkCore;
using OpiSupport.Domain.Entities;
using System.Net.Sockets;

namespace OpiSupport.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets representan las tablas en la base de datos
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<StatusHistory> StatusHistories { get; set; }
        public DbSet<TechnicianSpecialty> TechnicianSpecialties { get; set; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación: Un Usuario (Colaborador) crea muchos Tickets
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.CreatedTickets)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación: Un Usuario (Técnico) tiene muchos Tickets asignados
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación: Un Ticket tiene muchos Comentarios
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación: Un Usuario escribe muchos Comentarios
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación: Un Ticket tiene muchos registros de StatusHistory
            modelBuilder.Entity<StatusHistory>()
                .HasOne(sh => sh.Ticket)
                .WithMany(t => t.StatusHistories)
                .HasForeignKey(sh => sh.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación: Un Usuario hace muchos cambios de estado
            modelBuilder.Entity<StatusHistory>()
                .HasOne(sh => sh.ChangedByUser)
                .WithMany(u => u.StatusChanges)
                .HasForeignKey(sh => sh.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación: Un Técnico tiene muchas especialidades
            modelBuilder.Entity<TechnicianSpecialty>()
                .HasOne(ts => ts.Technician)
                .WithMany(u => u.Specialties)
                .HasForeignKey(ts => ts.TechnicianId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación: Una Categoría está asociada a muchos Técnicos (a través de TechnicianSpecialty)
            modelBuilder.Entity<TechnicianSpecialty>()
                .HasOne(ts => ts.Category)
                .WithMany(c => c.TechnicianSpecialties)
                .HasForeignKey(ts => ts.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de la relación: Un Ticket pertenece a una Categoría
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación: Un Ticket pertenece a un Área
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Area)
                .WithMany(a => a.Tickets)
                .HasForeignKey(t => t.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación: Un Ticket tiene muchas Alertas
            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Ticket)
                .WithMany(t => t.Alerts) 
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    
    }

}

