interface Book 
{
    title   :   string;
    author  :   string;
    year    :   number;
    available   :   boolean;
}

class library {
    books :Book[] = [];

    addBook(book: Book): void{
        this.books.push(book);
    }

    findBook(title: String) : Book | undefined{
        return this.books.find(book => book.title === title );
    }
    borrowBook(title: string ): void {
        const book = this.findBook(title);

        if (!book) {
            console.log("libro no encopntrado");
            return
        }

        if (!book.available) {
            console.log("Libro no disponible");
            return;
        }

        book.available = false;
        console.log ("Libro prestado: ", book.title)
    }

    returnBook(title: string): void {
  const book = this.findBook(title);

  if (!book) {
    console.log("Libro no encontrado");
    return;
  }

  if (book.available) {
    console.log("El libro ya está disponible");
    return;
  }

  book.available = true;
  console.log("Libro devuelto:", book.title);
}


}
let biblioteca = new library();

let libro1: Book = {
  title: "1984",
  author: "George Orwell",
  year: 1949,
  available: true
};

let libro2: Book = {
  title: "El principito",
  author: "Antoine de Saint-Exupéry",
  year: 1943,
  available: true
};

let libro3: Book = {
  title: "Don Quijote",
  author: "Miguel de Cervantes",
  year: 1605,
  available: true
};

biblioteca.addBook(libro1);
biblioteca.addBook(libro2);
biblioteca.addBook(libro3);

// pruebas
biblioteca.borrowBook("1984");
biblioteca.borrowBook("1984"); // ya prestado
biblioteca.returnBook("1984");
biblioteca.returnBook("1984"); // ya disponible

console.log(biblioteca);