using System;
using System.Collections.Generic;
using System.Text;

namespace Operations_Research
{
    class Frac
    {
        public double numerator;
        public double denominator;

        public Frac(double numerator, double denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }
        public Frac(double numerator) { this.numerator = numerator; denominator = 1; }

        public static Frac operator *(Frac f1, Frac f2)
        {
            Frac f3 = new Frac(0, 0);
            f3.numerator = f1.numerator * f2.numerator;
            f3.denominator = f1.denominator * f2.denominator;
            return f3;
        }
        public static Frac operator /(Frac f1, Frac f2)
        {
            Frac f3 = new Frac(0, 0);
            f3.numerator = f1.numerator * f2.denominator;
            f3.denominator = f1.denominator * f2.numerator;
            return f3;
        }
        public static Frac operator +(Frac f1, Frac f2)
        {
            Frac f3 = new Frac(0, 0);
            f3.denominator = f1.denominator * f2.denominator;
            f3.numerator = (f1.numerator * f2.denominator) + (f2.numerator * f1.denominator);
            return f3;
        }
        public static Frac operator -(Frac f1, Frac f2)
        {
            Frac f3 = new Frac(0, 0);
            f3.denominator = f1.denominator * f2.denominator;
            f3.numerator = (f1.numerator * f2.denominator) - (f2.numerator * f1.denominator);
            return f3;
        }
        public static bool operator >(Frac f1, Frac f2)
        {
            return (f1.numerator / f1.denominator) > (f2.numerator / f2.denominator);
        }
        public static bool operator <(Frac f1, Frac f2)
        {
            return (f1.numerator / f1.denominator) < (f2.numerator / f2.denominator);
        }
        public static bool operator >(Frac f1, int number)
        {
            return (f1.numerator / f1.denominator) > number;
        }
        public static bool operator <(Frac f1, int number)
        {
            return (f1.numerator / f1.denominator) < number;
        }
        public static bool operator ==(Frac f1, int number)
        {
            return (f1.numerator / f1.denominator) == number;
        }
        public static bool operator !=(Frac f1, int number)
        {
            return (f1.numerator / f1.denominator) != number;
        }
        public string toString()
        {
            
            if(this.denominator == 1)
            {
                return this.numerator.ToString();
            }
            return this.numerator.ToString() + "/" + this.denominator.ToString();
        }
    }

    class Jordan
    {
        public delegate bool Statement(Frac frac);

        public static int firstEntry(Frac[] arr, Statement stat)
        {
            for (int i = 0; i < arr.GetLength(0);i++)
            {
                if (stat(arr[i]))
                    return i;
            }
            return -1;
        }
        public static int  findElem(Frac[,] matrix, int  i)
        {
            Statement stat = x=>x==1;
            int result = firstEntry(Utils.getRow(matrix,i), stat);
            if (result != -1)
                return result;
            return 0;
        }
        public Frac[,] oneJordan(Frac[,] matrix, int i, int j)
        { 
            Frac zero = new Frac(0);
            Frac[,] tmpMatrix = new Frac[matrix.GetLength(0), matrix.GetLength(1)];
            for (int k = 0; k <= tmpMatrix.GetLength(0) + 1; k++)
            {
                matrix[i, k] /= matrix[i, j];
                tmpMatrix[i, k] = matrix[i, k];
            }
            for (int k = 0; k < tmpMatrix.GetLength(0); k++)
            {
                if (k != i)
                {
                    tmpMatrix[k, j] = zero;
                }
            }
            for (int g = 0; g < tmpMatrix.GetLength(0); g++)
            {
                for (int k = 0; k < tmpMatrix.GetLength(1); k++)
                {
                    if (g == i || k == j)
                        continue;
                    else
                    {
                        tmpMatrix[g, k] = matrix[i, j] * matrix[g, k] - matrix[i, k] * matrix[g, j];
                    }
                }
            }

            return tmpMatrix;
        }

        //static public Frac[][] fullJordan(Frac[][] matrix)
        //{
        //    for (int i = 0; i < matrix.GetLength(0); i++)
        //    {

        //        oneJordan(matrix, i,);
        //    }
        //    return matrix;
        //}
    }

    class SimplexMethod
    {
        //static public Frac dotProduct(Frac[] f1, Frac[] f2)
        //{
        //    Frac result = new Frac();
        //    try
        //    {
        //        for (int i = 0; i < f1.Length; i++)
        //        {
        //            result += f1[i] * f2[i];
        //        }
        //        return result;
        //    }
        //    catch (IndexOutOfRangeException e)
        //    {
        //        if (f1.Length != f2.Length)
        //        {
        //            Console.WriteLine(e.Message);
        //            Console.WriteLine("Dot product of the vectors with different lengths!");
        //            throw;
        //        }
        //    }
        //}

        static private int[] obtainBasisIdx(Frac[][] matrix)
        {
            int[] basis = new int[matrix.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 1)
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
        static private Frac[] computeSolution(Frac[][] matrix, Frac[] target)
        {
            Frac[] solution = new Frac[target.Length];

            int[] basisIdx = obtainBasisIdx(matrix);
            Frac[] c = Utils.getValues(target, basisIdx);
            for (int i = 0; i < target.Length - 1; i++)
            {
                Frac[] a = new Frac[matrix.Length];
                for (int j = 0; j < matrix.Length; j++)
                {
                    a[j] = matrix[j][i];
                }
                //solution[i] = dotProduct(c, a) - target[i];
            }
            //solution[target.Length - 1] = dotProduct(c, getValues(matrix, matrix.Length));
            return solution;
        }

        static private bool checkSolution(Frac[] solution)
        {
            for (int i = 0; i < solution.Length; i++)
            {
                if (solution[i] < 0)
                    return false;
            }
            return true;
        }

        static private bool checkSolution(Frac[][] simplexTable)
        {
            int length = simplexTable[0].Length;
            for (int i = 0; i < length; i++)
            {
                if (simplexTable[length - 1][i] < 0)
                    return false;
            }
            return true;
        }

        static private Frac[][] SimplexForJordan(Frac[][] matrix, Frac[] target)
        {
            Frac[][] result = new Frac[matrix.Length + 1][];
            int i = 0, j = 0;
            foreach (Frac[] line in matrix)
            {
                foreach (Frac frac in line)
                {
                    result[i][j] = frac;
                    j++;
                }
                i++;
            }
            return result;
        }

        static public Frac[][] firstStepSimplex(Frac[][] matrix, Frac[] target)
        {
            return SimplexForJordan(matrix, computeSolution(matrix, target));
        }
        /*
        static public Frac[] fullsimplex(frac[][] matrix, frac[] target)
        {

        }
        */
    }


    class Utils
    {
        static public Frac[] getValues(Frac[][] matrix, int row, int[] idxs)
        {
            Frac[] result = new Frac[idxs.GetLength(1)];
            for (int i = 0; i < idxs.GetLength(1); i++)
            {
                result[i] = matrix[row][idxs[i]];
            }
            return result;
        }

        static public Frac[] getCol(Frac[,] matrix, int col)
        {
            Frac[] result = new Frac[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                result[i] = matrix[i, col];
            }
            return result;
        }

        static public Frac[] getRow(Frac[,] matrix, int row)
        {
            Frac[] result = new Frac[matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                result[i] = matrix[row, i];
            }
            return result;
        }

        static public Frac[] getValues(Frac[] vector, int[] idxs)
        {
            Frac[] result = new Frac[idxs.GetLength(0)];
            for (int i = 0; i < idxs.GetLength(0); i++)
            {
                result[i] = vector[idxs[i]];
            }
            return result;
        }

        public static void PrintMatrix(Frac[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j].toString() + "\t");
                }
                Console.WriteLine();
            }
        }

        //static void PrintSimplexTable(Frac[][] matrix, Frac[] target)
        //{

        //}

    }

    class Program
    {
        static void Main(string[] args)
        {
            Jordan j = new Jordan();

            Frac[,] matrix = new Frac[,]
            {
                {new Frac(2), new Frac(-1), new Frac(1), new Frac(-3),new Frac(5)},
                {new Frac(1), new Frac(1), new Frac(0), new Frac(-1),new Frac(2)},
                {new Frac(1), new Frac(4), new Frac(-1), new Frac(0),new Frac(2)}
            };
            Utils.PrintMatrix(matrix);
            Console.WriteLine("\n" + Jordan.findElem(matrix, 0));
            Console.ReadKey();

        }
    }
}