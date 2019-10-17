using System;
using System.Collections.Generic;
using System.Text;

namespace Operations_Research
{
    class Frac
    {

    }

    class Jordan
    {
        static public Frac[][] oneJordan(Frac[] matrix, int i, int j)
        {

        }

        static public Frac[][] fullJordan(Frac[] matrix)
        {

        }
    }

    class Simplex
    {
        static public Frac dotProduct(Frac[] f1, Frac[] f2)
        {
            Frac result = new Frac;
            try
            {
                for (int i = 0; i < f1.Length; i++)
                {
                    result += f1[i] * f2[i];
                }
                return result;
            }
            catch (Exception e)
            {
                if (f1.Length != f2.Length)
                {
                    Console.WriteLine("Dot product of the vectors with different lengths!");
                    throw;
                }
            }
        }

        static public Frac[] oneSimplex(Frac[] matrix, Frac[] target)
        {

        }

        static public Frac[] fullSimplex(Frac[] matrix, Frac[] target)
        {

        }
    }

    class Utils
    {
        static void PrintMatrix(Frac[] matrix)
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
