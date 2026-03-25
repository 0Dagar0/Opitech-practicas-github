using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring
{
    internal class refactoring4
    {

        /*
         public class Pedido
        {
            public void Confirmar()
                {
                    var notificador = new EmailNotificador();
                    notificador.Enviar("Pedido confirmado");
                 }
        }
         */
        public interface INotificador
        {
            void Enviar(string mensaje);
        }

        public class EmailNotificador : INotificador
        {
            public void Enviar(string mensaje)
            {
                Console.WriteLine(mensaje);
            }
        }

        public class Pedido
        {
            private readonly INotificador _notificador;

            public Pedido(INotificador notificador)
            {
                _notificador = notificador;
            }

            public void Confirmar()
            {
                _notificador.Enviar("Pedido confirmado");
            }
        }   // se desacopla el código usando interfaces (principio DIP).

    }
}
