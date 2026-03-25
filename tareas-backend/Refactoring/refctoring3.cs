using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring
{
    internal class refctoring3
    {

        /* 
         Dictionary<string, object> producto = new Dictionary<string, object>
            {
                {"nombre", "Laptop"},
                 {"precio", 1000}
            };
         */

    public class Producto
        {
            public string Nombre { get; set; }
            public double Precio { get; set; }

            public double AplicarDescuento() => Precio + 0.19;
            // usar clases mejora seguridad de tipos y claridad.
        }




    }
}
