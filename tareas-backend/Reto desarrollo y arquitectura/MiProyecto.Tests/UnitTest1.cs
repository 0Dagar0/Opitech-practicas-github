using System;
using System.Threading.Tasks;
using LibraryManagement.Application.Services;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using Moq;
using Xunit;

namespace MiProyecto.Tests
{
    public class CategoryServiceTests
    {
        [Fact]
        public async Task GetCategoryByIdAsync_CuandoLaCategoriaExiste_DebeRetornarLaCategoria()
        {
            // 1. Arrange (Preparar el escenario)
            var categoriaId = Guid.NewGuid();
            var categoriaEsperada = new Category
            {
                Id = categoriaId,
                Name = "Ficción"
            };

            // Creamos el simulador del repositorio
            var mockRepository = new Mock<IRepository<Category>>();

            // Configuramos el simulador para que cuando llamen a GetByIdAsync con nuestro ID, devuelva la categoría esperada
            mockRepository
                .Setup(repo => repo.GetByIdAsync(categoriaId))
                .ReturnsAsync(categoriaEsperada);

            // Inyectamos el falso repositorio en el servicio real que vamos a probar
            var servicio = new CategoryService(mockRepository.Object);

            // 2. Act (Ejecutar la acción)
            var resultado = await servicio.GetCategoryByIdAsync(categoriaId);

            // 3. Assert (Verificar el resultado)
            Assert.NotNull(resultado);
            Assert.Equal(categoriaId, resultado.Id);
            Assert.Equal("Ficción", resultado.Name);
        }
    }
}
