using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Moq;

namespace LibraryManagement.Application.Tests.Services
{
    public class LoanServiceTests
    {
        [Fact]
        public async Task CreateLoanAsync_CopyAvailable_ShouldCreateLoan()
        {
                            //  Arrange (Preparar)
            var copyId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var copy = new BookCopy { Id = copyId, Status = CopyStatus.Available, BookId = Guid.NewGuid() };
            var user = new User { Id = userId, FirstName = "Test", LastName = "User" };

            var mockLoanRepo = new Mock<IRepository<Loan>>();
            var mockCopyRepo = new Mock<IRepository<BookCopy>>();
            var mockUserRepo = new Mock<IRepository<User>>();
            var mockBookRepo = new Mock<IBookRepository>(); // No se usa en CreateLoan, pero el constructor lo pide

            mockCopyRepo.Setup(r => r.GetByIdAsync(copyId)).ReturnsAsync(copy);
            mockUserRepo.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            var service = new LoanService(
                mockLoanRepo.Object,
                mockCopyRepo.Object,
                mockUserRepo.Object,
                mockBookRepo.Object
            );

                            //  Act (Actuar)
            var result = await service.CreateLoanAsync(copyId, userId);

                        //  Assert (Verificar)
            Assert.NotNull(result);
            Assert.Equal(copyId, result.BookCopyId);
            Assert.Equal(userId, result.UserId);
            Assert.Null(result.ReturnDate);
            Assert.Equal(0, result.FineAmount);
            Assert.True((result.DueDate - result.LoanedDate).Days == 14);

                            // Verificar que se llamó a AddAsync y SaveChangesAsync
            mockLoanRepo.Verify(r => r.AddAsync(It.IsAny<Loan>()), Times.Once);
            mockLoanRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
