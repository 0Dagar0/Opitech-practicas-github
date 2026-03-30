

    public class Book : Material, ILoanable
    {
        public int NumberPages { get; set; }

        private bool _available = true;
        public override string GetDescription()
            {
                return $"Libro : {Title}, Autor: {Author}, Año: {PublicationYear}, Páginas: {NumberPages}";
            }

        public void Lend(User user)
        {      
            if (user ==  null)
            {
                Console.WriteLine(" Usuario invalido");
                return;
            }
               
            if  (!_available)
            {
                Console.WriteLine("El libro ya esta prestado");
                return;
            }
                _available = false;
                Console.WriteLine($"Libro prestado a {user.Name}");
        }

        public void Reincorporate()
            {
                _available = true;
                Console.WriteLine("Libro devuelto");
            }

        public bool IsAvaileable()
            {
                return _available;
            }   

    }


