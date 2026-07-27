using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpiSupport.Domain.Entities;
using OpiSupport.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpiSupport.Infrastructure.Services
{
    public class SlaMonitorService : BackgroundService
    {
        private readonly ILogger<SlaMonitorService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public SlaMonitorService(ILogger<SlaMonitorService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Iniciando verificación de SLA a las {time}", DateTime.UtcNow);

                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // 🔹 Obtener el ID del usuario "Sistema" (una sola vez por ciclo)
                    int systemUserId;
                    try
                    {
                        systemUserId = await GetSystemUserIdAsync(context);
                    }
                    catch (InvalidOperationException)
                    {
                        _logger.LogError("Usuario 'Sistema' no encontrado. Asegúrate de que el seed se haya ejecutado.");
                        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Esperar 5 min y reintentar
                        continue;
                    }

                    // 1. Obtener tickets vencidos
                    var overdueTickets = await context.Tickets
                        .Where(t => t.SLA_Deadline < DateTime.UtcNow
                                    && !t.IsOverdue
                                    && new[] { "Abierto", "Asignado", "En Proceso" }.Contains(t.Status))
                        .ToListAsync(stoppingToken);

                    if (overdueTickets.Any())
                    {
                        _logger.LogInformation("Se encontraron {count} tickets vencidos", overdueTickets.Count);

                        foreach (var ticket in overdueTickets)
                        {
                            // 2. Marcar como vencido
                            ticket.IsOverdue = true;

                            // 3. Registrar en historial (usando systemUserId)
                            var history = new StatusHistory
                            {
                                TicketId = ticket.Id,
                                ChangedByUserId = systemUserId,   // Ahora dinámico
                                PreviousStatus = ticket.Status,
                                NewStatus = ticket.Status,        // No cambia el estado
                                ChangedAt = DateTime.UtcNow
                            };
                            context.StatusHistories.Add(history);

                            // 4. Generar alerta para el supervisor
                            var alert = new Alert
                            {
                                TicketId = ticket.Id,
                                Message = $"El ticket #{ticket.Id} ha superado el SLA (estado: {ticket.Status}, prioridad: {ticket.Priority}). Se requiere atención.",
                                IsRead = false,
                                CreatedAt = DateTime.UtcNow
                            };
                            context.Alerts.Add(alert);

                            _logger.LogInformation("Ticket {id} marcado como vencido", ticket.Id);
                        }

                        await context.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation("Tickets vencidos procesados correctamente");
                    }
                    else
                    {
                        _logger.LogInformation("No se encontraron tickets vencidos");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al ejecutar el monitor de SLA");
                }

                // 5. Esperar 1 hora antes de la siguiente ejecución
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task<int> GetSystemUserIdAsync(ApplicationDbContext context)
        {
            var systemUser = await context.Users
                .FirstOrDefaultAsync(u => u.EmployeeCode == "SYS-000" && u.Role == "Sistema");

            if (systemUser == null)
                throw new InvalidOperationException("Usuario 'Sistema' no encontrado. Ejecuta el seed.");

            return systemUser.Id;
        }
    }
}