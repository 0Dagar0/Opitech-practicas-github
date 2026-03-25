using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Refactoring
{
    internal class refactorin5
    {
        /*
         public string ObtenerIniciales(string nombre)
    {
        var partes = nombre.Split(" ");
        string resultado = "";

        foreach (var p in partes)
        {
            if (p != "")
            {
                resultado += p[0].ToString().ToUpper() + ".";
            }
        }

        return resultado;
    }
        */

public string ObtenerIniciales(string nombre)
    {
        var partes = nombre
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return string.Join(".", partes.Select(p => char.ToUpper(p[0]))) + ".";
    }

        /*
            Separación de responsabilidades: limpiar y procesar
            Uso de funciones del lenguaje: join, Split con opciones
            Código más declarativo: se entiende “qué hace” sin ver el detalle
            Mejor rendimiento: evita concatenaciones repetidas
        */

    }
}
