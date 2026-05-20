using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task<Reservation?> GetReservationByIdAsync(Guid id);
        Task<Reservation> CreateReservationAsync(Guid bookCopyId, Guid userId);
        Task CancelReservationAsync(Guid id);
        Task<IEnumerable<Reservation>> GetActiveReservationsByCopyAsync(Guid bookCopyId);
        Task<PagedResult<Reservation>> GetPagedReservationsAsync(int page, int pageSize);
    }
}
