using System;
using System.Collections.Generic;
using System.Text;

namespace Operations_Research
{
    class Utils
    {
        static public Frac[] getValues(Frac[,] matrix, int row, int[] idxs)
        {
            Frac[] result = new Frac[idxs.GetLength(1)];
            for (int i = 0; i < idxs.GetLength(1); i++)
            {
                result[i] = matrix[row, idxs[i]];
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
                    Console.Write(matrix[i, j].ToString() + "\t");
                }
                Console.WriteLine();
            }
        }

        public static void PrintSimplexTable(
            Frac[,] matrix,
            Frac[] b,
            Frac[] target,
            Frac[] solution = null)
        {
            Console.WriteLine();
            Console.Write("Cb\txb\t");
            for (int i = 1; i <= matrix.GetLength(1); i++)
            {
                Console.Write("x" + i.ToString() + "\t");
            }
            Console.WriteLine("  B  ");

            for (int i = 0; i < matrix.GetLength(1) + 4; i++) Console.Write("_______");
            Console.WriteLine();

            Console.Write("  \t  \t");
            for (int i = 0; i < target.GetLength(0); i++)
            {
                Console.Write(target[i].ToString() + "\t");
            }
            Console.WriteLine();

            int[] basisIdx = SimplexMethod.obtainBasisIdx(matrix);
            Frac[] basis = Utils.getValues(target, basisIdx);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write(basis[i].ToString() + "\tx" + (basisIdx[i] + 1).ToString() + "\t");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j].ToString() + "\t");
                }
                Console.WriteLine(b[i].ToString());
            }
            if (solution != null)
            {
                Console.Write("  \tz \t");
                for (int i = 0; i < matrix.GetLength(1) + 4; i++) Console.Write("_______");
                Console.WriteLine();

                for (int i = 0; i < solution.GetLength(0); i++)
                    Console.Write(solution[i].ToString() + "\t");
                Console.WriteLine();
            }

        }

    }

    class Frac
    {
        public int numerator;
        public int denominator;

        public Frac() { numerator = 0; denominator = 1; } //zero initialization if empty
        public Frac(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }
        public Frac(int numerator) { this.numerator = numerator; denominator = 1; }

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

        public void simplify()
        {
            // 2/2 => 1
            if (numerator == denominator)
                numerator = denominator = 1;

            //-2/-1 => 2/1
            if (numerator < 0 && denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }
            //2/-3 => -2/3
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            //simplification
            for (int i = denominator; i > 1; i--)
            {
                if((numerator % i == 0) && (denominator % i == 0))
                {
                    numerator /= i;
                    denominator /= i;
                }
            }
        }

        public override string ToString()
        {
            simplify();
            // 2/1 => 2
            if (denominator == 1 || numerator == 0)
            {
                return numerator.ToString();
            }
            
            return numerator.ToString() + "/" + denominator.ToString();
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
        public static Frac[,] oneJordan(Frac[,] matrix, int i, int j)
        { 
            Frac zeroFrac = new Frac(0);
            Frac leadingElem = matrix[i, j];
            Frac[,] resultMtx = new Frac[matrix.GetLength(0), matrix.GetLength(1)];

            for (int k = 0; k < resultMtx.GetLength(1); k++)
            {
                resultMtx[i, k] = matrix[i, k] / leadingElem;
            }

            for (int k = 0; k < resultMtx.GetLength(0); k++)
            {
                if (k != i)
                {
                    resultMtx[k, j] = zeroFrac;
                }
            }

            for (int g = 0; g < resultMtx.GetLength(0); g++)
            {
                for (int k = 0; k < resultMtx.GetLength(1); k++)
                {
                    if (g == i || k == j)
                        continue;
                    else
                    {
                        resultMtx[g, k] = (leadingElem * matrix[g, k] - matrix[i, k] * matrix[g, j])/leadingElem;
                    }
                }
            }

            return resultMtx;
        }

        //public static Frac[][] fullJordan(Frac[][] matrix)
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
        public static Frac dotProduct(Frac[] f1, Frac[] f2)
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
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Dot product of the vectors with different lengths!");
                }
                return null;
            }
        }

        static public int[] obtainBasisIdx(Frac[,] matrix)
        {
            int[] basis = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] == 1)
                    {
                        int k = 0;
                        while ((k < matrix.GetLength(0) && matrix[k,j] == 0) || k == i) k++;
                    
                        if (k == matrix.GetLength(0))
                            basis[i] = j;
                    }
                }
            }
            //Array.Sort(basis);
            return basis;
        }

        static public Frac[] computeSolution(Frac[,] matrix,Frac[] b, Frac[] target)
        {
            Frac[] solution = new Frac[target.GetLength(0) + 1];

            int[] basisIdx = obtainBasisIdx(matrix);
            Frac[] c = Utils.getValues(target, basisIdx);
            for (int i = 0; i < target.Length; i++)
            {
                Frac[] a = new Frac[matrix.GetLength(0)];
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    a[j] = matrix[j,i];
                }
                solution[i] = dotProduct(c, a) - target[i];
            }
            solution[target.Length] = dotProduct(c, b);
            return solution;
        }

        static private bool isSolutionCorrect(Frac[] solution)
        {
            //the last one excepted: it is solution for system 
            //and can be less than zero
            for (int i = 0; i < solution.Length - 1; i++)
            {
                if (solution[i] < 0)
                    return false;
            }
            return true;
        }

        static private bool isSolutionCorrect(Frac[,] simplexTable)
        {
            int length = simplexTable.GetLength(1);
            for (int i = 0; i < length - 1; i++)
            {
                if (simplexTable[length - 1, i] < 0)
                    return false;
            }
            return true;
        }
        
        //static private bool hasLimits(Frac[,] matrix, Frac[] solution)
        //{
            
        //}

        //static private Frac computeKeyRelation()
        //{

        //}

        //static private Frac[,] SimplexForJordanConvertion(Frac[,] matrix, Frac[] target)
        //{
        //    Frac[,] result = new Frac[matrix.GetLength(0), matrix.GetLength(1)];
        //    for (int i = 0; i < matrix.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < matrix.GetLength(1); j++)
        //        {
        //            result[i,j] = matrix[i,j];
        //        }
        //    }
        //    for (int i = 0; i < matrix.GetLength(1); i++)
        //    {
        //        result[matrix.GetLength(0), i] = target[i];
        //    }
        //        return result;
        //}

    //    public static Frac fullSimplex(Frac[,] matrix, Frac[] b, Frac[] target)
    //    {
    //        Utils.PrintSimplexTable(matrix, b, target);
    //        Frac[] solution = computeSolution(matrix, b, target);
    //        while(!isSolutionCorrect(solution))
    //        {
    //            if (!hasLimits(matrix, solution))
    //                return null;
    //            else
    //            {
                    
    //            }
    //        }
    //        Utils.PrintSimplexTable(matrix, b, target);
    //        return solution[solution.Length - 1];
    //    }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Frac[] target = new Frac[] { new Frac(2), new Frac(1), new Frac(-1), new Frac(2), new Frac(1) };
            Frac[] b = new Frac[] { new Frac(6), new Frac(5), new Frac(1) };
            Frac[,] matrix = new Frac[,]
            {
                {new Frac(0), new Frac(2), new Frac(0),  new Frac(-3), new Frac(1)},
                {new Frac(1), new Frac(-1), new Frac(0),  new Frac(4), new Frac(0)},
                {new Frac(0), new Frac(3), new Frac(1),   new Frac(2), new Frac(0)}
            };

            //Frac result = SimplexMethod.fullSimplex(matrix, b, target);

            //TODO: Throw this in a function of Jordan

            //Frac[,] matrix = new Frac[,]
            //{
            //    {new Frac(4), new Frac(1), new Frac(1),  new Frac(0),  new Frac(1), new Frac(6)},
            //    {new Frac(-1), new Frac(3), new Frac(-1),   new Frac(0), new Frac(3), new Frac(1)},
            //    {new Frac(8), new Frac(4), new Frac(12),  new Frac(4), new Frac(12), new Frac(24)}
            //};                            

            //Utils.PrintMatrix(matrix);
            //while(true)
            //{
            //    int row, col;

            //    Console.Write("row:");
            //    row = int.Parse(Console.ReadLine());
            //    if (row == -1)
            //    {
            //        Console.ReadKey();
            //        return;
            //    }

            //    Console.Write("col:");
            //    col = int.Parse(Console.ReadLine());
            //    if (col == -1)
            //    {
            //        Console.ReadKey();
            //        return;
            //    }

            //    matrix = Jordan.oneJordan(matrix, row, col);
            //    Console.Write("\n");
            //    Utils.PrintMatrix(matrix);
            //}
        }
    }
}