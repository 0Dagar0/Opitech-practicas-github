using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;

namespace LibraryManagement.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<BookCopy> _copyRepository;
        private readonly IRepository<User> _userRepository;

        public ReservationService(
            IRepository<Reservation> reservationRepository,
            IRepository<BookCopy> copyRepository,
            IRepository<User> userRepository)
        {
            _reservationRepository = reservationRepository;
            _copyRepository = copyRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _reservationRepository.GetAllAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(Guid id)
        {
            return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task<Reservation> CreateReservationAsync(Guid bookCopyId, Guid userId)
        {
            // 1. Validar que la copia exista
            var copy = await _copyRepository.GetByIdAsync(bookCopyId);
            if (copy == null)
                throw new KeyNotFoundException($"No existe la copia con Id {bookCopyId}.");

            // 2. Validar que el usuario exista
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"No existe el usuario con Id {userId}.");

            // 3. Solo se puede reservar si la copia está prestada (Lent)
            if (copy.Status != CopyStatus.Lent)
                throw new InvalidOperationException($"Solo se puede reservar una copia que esté prestada. Estado actual: {copy.Status}.");

            // 4. Verificar que el usuario no tenga ya una reserva activa para esta copia
            var existing = await _reservationRepository.FindAsync(
                r => r.BookCopyId == bookCopyId && r.UserId == userId && r.Status == ReservationStatus.Active);
            if (existing.Any())
                throw new InvalidOperationException("Ya tienes una reserva activa para esta copia.");

            // 5. Crear la reserva
            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                BookCopyId = bookCopyId,
                UserId = userId,
                ReservationDate = DateTime.UtcNow,
                Status = ReservationStatus.Active
            };

            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.SaveChangesAsync();
            return reservation;
        }

        public async Task CancelReservationAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetByIdAsync(id);
            if (reservation == null)
                throw new KeyNotFoundException($"No existe la reserva con Id {id}.");

            if (reservation.Status != ReservationStatus.Active)
                throw new InvalidOperationException($"La reserva no está activa. Estado actual: {reservation.Status}.");

            reservation.Status = ReservationStatus.Cancelled;
            _reservationRepository.Update(reservation);
            await _reservationRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetActiveReservationsByCopyAsync(Guid bookCopyId)
        {
            return await _reservationRepository.FindAsync(
                r => r.BookCopyId == bookCopyId && r.Status == ReservationStatus.Active);
        }

        public async Task<PagedResult<Reservation>> GetPagedReservationsAsync(int page, int pageSize)
        {
            return await _reservationRepository.GetPagedAsync(page, pageSize);
        }


    }
}
