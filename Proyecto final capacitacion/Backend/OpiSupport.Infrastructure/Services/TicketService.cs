using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpiSupport.Application.DTOs;
using OpiSupport.Application.Interfaces;
using OpiSupport.Domain.Entities;
using OpiSupport.Infrastructure.Data;


namespace OpiSupport.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;

        public TicketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TicketResponseDto> CreateTicketAsync(CreateTicketDto dto, int userId)
        {
            // Validar que la categoría existe
            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
                throw new ArgumentException("La categoría especificada no existe.");

            // Validar que el área existe
            var area = await _context.Areas.FindAsync(dto.AreaId);
            if (area == null)
                throw new ArgumentException("El área especificada no existe.");

            // Validar prioridad
            var validPriorities = new[] { "Baja", "Media", "Alta", "Critica" };
            if (!validPriorities.Contains(dto.Priority))
                throw new ArgumentException("La prioridad debe ser Baja, Media, Alta o Critica.");

            // Obtener el usuario creador
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            // Crear el ticket
            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                AreaId = dto.AreaId,
                Priority = dto.Priority,
                Status = "Abierto",
                CreatedById = userId,
                CreatedAt = DateTime.UtcNow,
                // Asignación automática: Buscar técnico con especialidad en esta categoría y menos de 5 tickets activos
                AssignedToId = await FindAvailableTechnicianAsync(dto.CategoryId)
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            // Registrar en el historial de estados
            var history = new StatusHistory
            {
                TicketId = ticket.Id,
                ChangedByUserId = userId,
                PreviousStatus = null,
                NewStatus = "Abierto",
                ChangedAt = DateTime.UtcNow
            };
            _context.StatusHistories.Add(history);
            await _context.SaveChangesAsync();

            // Cargar las relaciones para la respuesta
            await _context.Entry(ticket).Reference(t => t.Category).LoadAsync();
            await _context.Entry(ticket).Reference(t => t.Area).LoadAsync();
            await _context.Entry(ticket).Reference(t => t.CreatedByUser).LoadAsync();

            // Devolver la respuesta
            return new TicketResponseDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Status = ticket.Status,
                Priority = ticket.Priority,
                Category = ticket.Category.Name,
                Area = ticket.Area.Name,
                CreatedAt = ticket.CreatedAt,
                CreatedById = ticket.CreatedById,
                CreatedByFullName = ticket.CreatedByUser.FullName
            };
        }

        private async Task<int?> FindAvailableTechnicianAsync(int categoryId)
        {
            // Buscar técnicos con especialidad en esta categoría
            var technicians = await _context.Users
                .Where(u => u.Role == "Tecnico" && u.IsActive)
                .Join(_context.TechnicianSpecialties,
                      u => u.Id,
                      ts => ts.TechnicianId,
                      (u, ts) => new { User = u, ts.CategoryId })
                .Where(x => x.CategoryId == categoryId)
                .Select(x => x.User)
                .ToListAsync();

            // Ordenar por los que tengan menos tickets activos
            var availableTechnician = technicians
                .OrderBy(t => _context.Tickets
                    .Count(tk => tk.AssignedToId == t.Id &&
                                 new[] { "Abierto", "Asignado", "En Proceso" }.Contains(tk.Status)))
                .FirstOrDefault();

            // Verificar si el técnico tiene menos de 5 tickets activos
            if (availableTechnician != null)
            {
                var activeTicketsCount = await _context.Tickets
                    .CountAsync(tk => tk.AssignedToId == availableTechnician.Id &&
                                      new[] { "Abierto", "Asignado", "En Proceso" }.Contains(tk.Status));

                if (activeTicketsCount < 5) // Límite máximo configurable
                    return availableTechnician.Id;
            }

            return null; // No hay técnico disponible

        }

        public async Task<List<TicketListDto>> GetTicketsAsync(int userId, string role)
        {
            IQueryable<Ticket> query = _context.Tickets
                .Include(t => t.Category)
                .Include(t => t.Area)
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser);

            // Aplicar filtros según el rol del usuario autenticado
            if (role == "Colaborador")
            {
                query = query.Where(t => t.CreatedById == userId);
            }
            else if (role == "Tecnico")
            {
                query = query.Where(t => t.AssignedToId == userId);
            }
            // Supervisor: no se aplica ningún filtro (ve todos)

            var tickets = await query
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TicketListDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    Priority = t.Priority,
                    Category = t.Category != null ? t.Category.Name : "Sin categoría",
                    Area = t.Area != null ? t.Area.Name : "Sin área",
                    CreatedAt = t.CreatedAt,
                    AssignedToFullName = t.AssignedToUser != null ? t.AssignedToUser.FullName : null,
                    CreatedByFullName = t.CreatedByUser != null ? t.CreatedByUser.FullName : "Usuario desconocido",
                    IsOverdue = t.IsOverdue
                })
                .ToListAsync();

            return tickets;
        }

        public async Task<TicketDetailDto> GetTicketByIdAsync(int ticketId, int userId, string role)
        {
            // 1. Buscar el ticket con sus relaciones
            var ticket = await _context.Tickets
                .Include(t => t.Category)
                .Include(t => t.Area)
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
                throw new ArgumentException("Ticket no encontrado");

            // 2. Validar permisos según el rol
            if (role == "Colaborador" && ticket.CreatedById != userId)
                throw new UnauthorizedAccessException("No tienes permiso para ver este ticket");

            if (role == "Tecnico" && ticket.AssignedToId != userId)
                throw new UnauthorizedAccessException("No tienes permiso para ver este ticket");

            // 3. Supervisor: no tiene restricciones, pasa directamente

            // 4. Mapear a DTO
            return new TicketDetailDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority,
                Category = ticket.Category?.Name ?? "Sin categoría",
                Area = ticket.Area?.Name ?? "Sin área",
                CreatedAt = ticket.CreatedAt,
                SLA_Deadline = ticket.SLA_Deadline,
                AssignedToFullName = ticket.AssignedToUser?.FullName,
                CreatedByFullName = ticket.CreatedByUser?.FullName ?? "Usuario desconocido",
                IsOverdue = ticket.IsOverdue
            };
        }

        public async Task<TicketDetailDto> ChangeStatusAsync(int ticketId, string newStatus, string? comment, int userId, string role)
        {
            // 1. Obtener el ticket con relaciones
            var ticket = await _context.Tickets
                .Include(t => t.Category)
                .Include(t => t.Area)
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
                throw new ArgumentException("Ticket no encontrado");

            // 2. Validar permisos básicos
            if (role == "Colaborador" && ticket.CreatedById != userId)
                throw new UnauthorizedAccessException("No tienes permiso para modificar este ticket");

            if (role == "Tecnico" && ticket.AssignedToId != userId)
                throw new UnauthorizedAccessException("No tienes permiso para modificar este ticket");

            // 3. Validar que el nuevo estado sea válido
            var validStatuses = new[] { "En Proceso", "Resuelto", "Cerrado", "Reabierto" };
            if (!validStatuses.Contains(newStatus))
                throw new ArgumentException("Estado no válido");

            string previousStatus = ticket.Status;

           
            // 4. LÓGICA DE REAPERTURA CON CONTROL DE LÍMITE
        
            if (newStatus == "Reabierto")
            {
                // 4.1. Solo se puede reabrir desde Resuelto o Cerrado
                if (!(ticket.Status == "Resuelto" || ticket.Status == "Cerrado"))
                    throw new ArgumentException("Solo se pueden reabrir tickets resueltos o cerrados");

                // 4.2. Validar plazo de 48 horas (aplica a cualquier usuario)
                if (!ticket.ResolvedAt.HasValue || (DateTime.UtcNow - ticket.ResolvedAt.Value).TotalHours > 48)
                    throw new ArgumentException("El plazo para reabrir el ticket ha expirado (máximo 48 horas)");

                // 4.3. Si el ticket ya estaba asignado a un técnico
                if (ticket.AssignedToId.HasValue)
                {
                    // Contar cuántos tickets reabiertos tiene este técnico actualmente
                    var reopenedCount = await _context.Tickets
                        .CountAsync(t => t.AssignedToId == ticket.AssignedToId
                                         && t.Status == "Reabierto");

                    // Si ya tiene 2 o más tickets reabiertos
                    if (reopenedCount >= 2)
                    {
                        // NO reasignar: el ticket queda en Reabierto sin técnico
                        ticket.AssignedToId = null;

                        // Crear alerta para el supervisor
                        var alert = new Alert
                        {
                            TicketId = ticket.Id,
                            Message = $"El ticket #{ticket.Id} ha sido reabierto, pero el técnico {ticket.AssignedToUser?.FullName} ya tiene {reopenedCount} tickets reabiertos. Se requiere reasignación manual.",
                            IsRead = false,
                            CreatedAt = DateTime.UtcNow
                        };
                        _context.Alerts.Add(alert);
                    }
                    // Si el técnico tiene menos de 2 reabiertos, se mantiene asignado
                }
                // Si el ticket no tenía técnico asignado, queda sin asignar
            }

            
            // 5. VALIDACIONES PARA TÉCNICO (SOLO SI NO ES REABIERTO)
            
            if (role == "Tecnico" && newStatus != "Reabierto")
            {
                if (newStatus == "En Proceso" && ticket.Status != "Abierto" && ticket.Status != "Asignado")
                    throw new ArgumentException("Solo se puede pasar a En Proceso desde Abierto o Asignado");

                if (newStatus == "Resuelto" && ticket.Status != "En Proceso")
                    throw new ArgumentException("Solo se puede resolver un ticket que está En Proceso");

                if (newStatus == "Cerrado" && ticket.Status != "Resuelto")
                    throw new ArgumentException("Solo se puede cerrar un ticket que está Resuelto");

                if (newStatus == "Resuelto")
                {
                    var hasComment = await _context.Comments.AnyAsync(c => c.TicketId == ticketId);
                    if (!hasComment)
                        throw new ArgumentException("Debe haber al menos un comentario antes de resolver el ticket");
                }
            }

             // 6. ACTUALIZAR ESTADO Y CAMPOS ESPECIALES
            string oldStatus = ticket.Status;
            ticket.Status = newStatus;

            if (newStatus == "En Proceso" && !ticket.StartedAt.HasValue)
                ticket.StartedAt = DateTime.UtcNow;

            if (newStatus == "Resuelto" && !ticket.ResolvedAt.HasValue)
                ticket.ResolvedAt = DateTime.UtcNow;

            // 7. Si es Reabierto, incrementar contador
            if (newStatus == "Reabierto")
                ticket.ReopenCount = (ticket.ReopenCount ?? 0) + 1;

            
            // 8. GUARDAR HISTORIAL
            var history = new StatusHistory
            {
                TicketId = ticket.Id,
                ChangedByUserId = userId,
                PreviousStatus = oldStatus,
                NewStatus = newStatus,
                ChangedAt = DateTime.UtcNow
            };
            _context.StatusHistories.Add(history);

            await _context.SaveChangesAsync();

            // 9. DEVOLVER RESPUESTA
            return new TicketDetailDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority,
                Category = ticket.Category?.Name ?? "Sin categoría",
                Area = ticket.Area?.Name ?? "Sin área",
                CreatedAt = ticket.CreatedAt,
                SLA_Deadline = ticket.SLA_Deadline,
                AssignedToFullName = ticket.AssignedToUser?.FullName,
                CreatedByFullName = ticket.CreatedByUser?.FullName ?? "Usuario desconocido",
                IsOverdue = ticket.IsOverdue
            };
        }

        public async Task<TicketDetailDto> AssignTechnicianAsync(int ticketId, int technicianId, int userId)
        {
            // 1. Obtener el ticket con sus relaciones
            var ticket = await _context.Tickets
                .Include(t => t.Category)
                .Include(t => t.Area)
                .Include(t => t.CreatedByUser)
                .Include(t => t.AssignedToUser)
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
                throw new ArgumentException("Ticket no encontrado");

            // 2. Validar que el técnico exista y esté activo
            var technician = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == technicianId && u.Role == "Tecnico" && u.IsActive);

            if (technician == null)
                throw new ArgumentException("El técnico especificado no es válido o no está activo");

            // 3. Guardar el estado anterior para el historial
            string previousStatus = ticket.Status;

            // 4. Asignar el técnico y actualizar estado si corresponde
            ticket.AssignedToId = technicianId;

            // Si el ticket estaba en "Abierto" o "Reabierto", pasar a "Asignado"
            if (ticket.Status == "Abierto" || ticket.Status == "Reabierto")
                ticket.Status = "Asignado";

            // 5. Registrar en el historial de estados
            var history = new StatusHistory
            {
                TicketId = ticket.Id,
                ChangedByUserId = userId,
                PreviousStatus = previousStatus,
                NewStatus = ticket.Status,
                ChangedAt = DateTime.UtcNow
            };
            _context.StatusHistories.Add(history);

            await _context.SaveChangesAsync();

            // 6. Devolver el ticket actualizado
            return new TicketDetailDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority,
                Category = ticket.Category?.Name ?? "Sin categoría",
                Area = ticket.Area?.Name ?? "Sin área",
                CreatedAt = ticket.CreatedAt,
                SLA_Deadline = ticket.SLA_Deadline,
                AssignedToFullName = technician.FullName,
                CreatedByFullName = ticket.CreatedByUser?.FullName ?? "Usuario desconocido",
                IsOverdue = ticket.IsOverdue
            };
        }

        public async Task<SlaReportDto> GetSlaReportAsync()
        {
            // 1. Obtener todos los tickets resueltos (con ResolvedAt != null)
            var resolvedTickets = await _context.Tickets
                .Include(t => t.AssignedToUser)
                .Include(t => t.Category)
                .Where(t => t.ResolvedAt.HasValue)
                .ToListAsync();

            // 2. Calcular estadísticas globales
            int totalResolved = resolvedTickets.Count;
            int compliantGlobal = resolvedTickets.Count(t => t.ResolvedAt!.Value <= t.SLA_Deadline);
            int nonCompliantGlobal = totalResolved - compliantGlobal;
            double globalPercentage = totalResolved > 0 ? (double)compliantGlobal / totalResolved * 100 : 0;

            // 3. Agrupar por técnico (solo tickets con técnico asignado)
            var technicianGroups = resolvedTickets
                .Where(t => t.AssignedToId.HasValue)
                .GroupBy(t => new { t.AssignedToId!.Value, t.AssignedToUser!.FullName })
                .Select(g => new TechnicianSlaDto
                {
                    TechnicianId = g.Key.Value,
                    TechnicianName = g.Key.FullName,
                    TotalResolved = g.Count(),
                    Compliant = g.Count(t => t.ResolvedAt!.Value <= t.SLA_Deadline),
                    NonCompliant = g.Count(t => t.ResolvedAt!.Value > t.SLA_Deadline),
                    CompliancePercentage = g.Count() > 0
                        ? (double)g.Count(t => t.ResolvedAt!.Value <= t.SLA_Deadline) / g.Count() * 100
                        : 0
                })
                .OrderByDescending(d => d.CompliancePercentage)
                .ToList();

            // 4. Agrupar por categoría
            var categoryGroups = resolvedTickets
                .GroupBy(t => new { t.CategoryId, t.Category!.Name })
                .Select(g => new CategorySlaDto
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.Name,
                    TotalResolved = g.Count(),
                    Compliant = g.Count(t => t.ResolvedAt!.Value <= t.SLA_Deadline),
                    NonCompliant = g.Count(t => t.ResolvedAt!.Value > t.SLA_Deadline),
                    CompliancePercentage = g.Count() > 0
                        ? (double)g.Count(t => t.ResolvedAt!.Value <= t.SLA_Deadline) / g.Count() * 100
                        : 0
                })
                .OrderByDescending(d => d.CompliancePercentage)
                .ToList();

            // 5. Construir respuesta
            return new SlaReportDto
            {
                GlobalCompliancePercentage = globalPercentage,
                Technicians = technicianGroups,
                Categories = categoryGroups
            };
        }



    }
}

