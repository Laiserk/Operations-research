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
        static public Frac[] oneJordan(Frac[] matrix)
        {

        }

        static public Frac[] fullJordan(Frac[] matrix)
        {

        }
    }

    class Simplex
    {
        static public Frac dotProduct(Frac[] f1, Frac[] f2)
        {
            Frac result = new Frac();
            try
            {
                for (int i = 0; i < f1.Length; i++)
                {
                    result += f1[i] * f2[i];
                }
                return result;
            }
            catch (IndexOutOfRangeException e)
            {
                if (f1.Length != f2.Length)
                {
                    Console.WriteLine("Dot product of the vectors with different lengths!");
                    throw;
                }
            }
        }

        private int[] obtainBasisIdx(Frac[][] matrix)
        {
            int[] basis = new int[matrix.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if(matrix[i][j] == 1)
                    {
                        int k = 0;
                        while ((k < matrix.Length && matrix[k][j] == 0) || k == i)
                        {
                            k++;    
                        } 

                        if (k == matrix.Length)
                            basis[i] = j;
                    }
                }
            }
            return basis;
        }

        static public Frac[][] oneSimplex(Frac[][] matrix, Frac[] target)
        {

        }

        static public frac[] fullsimplex(frac[][] matrix, frac[] target)
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
