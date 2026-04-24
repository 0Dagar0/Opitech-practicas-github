using System;

namespace ServiciosEInyeccionDependencias.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Lógica del dominio: Prestar un libro
        public void MarkAsBorrowed()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("El libro ya está prestado");

            IsAvailable = false;
            UpdatedAt = DateTime.Now;
        }

        // Lógica del dominio: Devolver un libro
        public void MarkAsReturned()
        {
            if (IsAvailable)
                throw new InvalidOperationException("El libro ya está disponible");

            IsAvailable = true;
            UpdatedAt = DateTime.Now;
        }

        // Lógica del dominio: Actualizar datos del libro
        public void UpdateDetails(string title, string author, string isbn, int publicationYear)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("El título es requerido");

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("El autor es requerido");

            if (publicationYear <= 0 || publicationYear > DateTime.Now.Year)
                throw new ArgumentException("Año de publicación inválido");

            Title = title;
            Author = author;
            ISBN = isbn;
            PublicationYear = publicationYear;
            UpdatedAt = DateTime.Now;
        }
    }
}