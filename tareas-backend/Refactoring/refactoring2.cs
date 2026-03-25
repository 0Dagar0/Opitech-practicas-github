using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring
{
    internal class refactoring2
    {

        /*
         public double TotalConIva(double precio)
            {
                return precio + (precio * 0.19);
            }

         public double CostoConIva(double costo)
            {
                return costo + (costo * 0.19);
            }
         */


        const double IVA = 0.19;

        public double AplicaIva(double valor)
        {

            return valor + valor * IVA;

            // se elimina código duplicado y se centraliza la lógica.
        }




    }
}
