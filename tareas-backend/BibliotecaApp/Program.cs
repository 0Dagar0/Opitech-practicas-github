using System;
using System.Timers;

class program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        // creamos usuarios 

        User user1 = new User
        {
            Name = "Pepito",
            DocumentType = "CC",
            Documentnumber = "123456789"
        };

        User user2 = new User
        {
            Name = "Ana",
            DocumentType = "TI",
            Documentnumber = "987654321"
        };

        // creamos materiales 

        Book book1 = new Book
        {
            Title = "El señor de los Anillos",
            Author = "J.R.R. Tolkien",
            PublicationYear = "1954",
            NumberPages = 1178
        };

        Book book2 = new Book
        {
            Title = "Juego de tronos",
            Author = "George R.R. Martin",
            PublicationYear = "1996",
            NumberPages = 795
        };


        Magazine magazine1 = new Magazine
        {
            Title = "Ntional Geograpic",
            Author = "Varios",
            PublicationYear = "2023",
            EditionNumber = 199
        };

        // agregamos a la biblioteca
        library.AddMaterial(book1);
        library.AddMaterial(book2);
        library.AddMaterial(magazine1);

        // Moatrar disponibles
        Console.WriteLine("Materiales disponibles: ");
        library.ShowAvaileable();

        // prestamos un libro
        Console.WriteLine("\nPrestando libro...");
        library.LendMaterial("Juego de tronos", user1);

        // intento de prestra el mismo libro 
        Console.WriteLine("Intento de prestar el mismo libro");
        library.LendMaterial("Juego de tronos", user2);

        // devolver el libro
        Console.WriteLine("\nDevolviendo libro...");
        library.ReturnMaterial("Juego de tronos");

        /// mostrar disponibles otra vez
        Console.WriteLine("\nMateriales disponibles despues de devolución: ");
        library.ShowAvaileable();



    }
}
