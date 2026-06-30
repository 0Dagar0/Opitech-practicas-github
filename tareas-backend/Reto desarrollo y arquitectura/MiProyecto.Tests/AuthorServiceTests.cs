using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Moq;
using Xunit;

namespace MiProyecto.Tests
{
    public class AuthorServiceTests
    {
        [Fact]
        public async Task CreateAuthorAsync_CuandoElAutorEsValido_DebeGuardarloYRetornarlo()
        {
            // 1. Arrange (Preparar el escenario con las propiedades reales)
            var nuevoAutor = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "Gabriel",
                LastName = "García Márquez"
            };

            // Creamos el simulador del repositorio para la entidad Author
            var mockAuthorRepository = new Mock<IRepository<Author>>();

            // Configuramos los métodos del repositorio que usa CreateAuthorAsync
            mockAuthorRepository
                .Setup(repo => repo.AddAsync(nuevoAutor))
                .Returns(Task.CompletedTask);

            mockAuthorRepository
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Inyectamos el repositorio simulado en el servicio de autores real
            var servicio = new AuthorService(mockAuthorRepository.Object);

            // 2. Act (Ejecutar la acción)
            var resultado = await servicio.CreateAuthorAsync(nuevoAutor);

            // 3. Assert (Verificar el resultado con las propiedades reales)
            Assert.NotNull(resultado);
            Assert.Equal("Gabriel", resultado.FirstName);
            Assert.Equal("García Márquez", resultado.LastName);

            // Verificación de Moq: Asegura que el servicio realmente llamó a los métodos de guardar
            mockAuthorRepository.Verify(repo => repo.AddAsync(nuevoAutor), Times.Once);
            mockAuthorRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}

