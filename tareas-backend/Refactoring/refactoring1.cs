using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring
{
    internal class refactoring1
    {
        /*
        public double CalcularSalario(Empleado e)
    {
        if (e.Tipo == "fulltime")
            return e.Salario * 1.2;
        else if (e.Tipo == "freelance")
            return e.Horas * e.Tarifa;

        return 0;
    }
        */


    public abstract class Empleado
        {

            public abstract double CalcularSalario();

        }

    public class FullTime : Empleado
        {

            public double Salario;
            public override double CalcularSalario() => Salario * 1.2;

        }

    public class FreeLance : Empleado
        {

            public int Horas;
            public double Tarifa;

            public override double CalcularSalario() => Horas * Tarifa;              }

        //se elimina if/else usando polimorfismo (principio OCP).
    }
}

