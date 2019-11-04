using System;
using System.Collections.Generic;
using System.Text;

namespace Operations_Research
{
    class Utils
    {
        static public int maxIdxSolution(Frac[] solution)
        {
            //the last one excluded
            int max = 0;
            for (int i = 0; i < solution.Length - 1; i++)
                if (solution[i] > solution[max]) max = i;
            return max;
        }

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
            Frac[] solution = null,
            int k = -1,
            int l = -1)
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
                    if (k != -1 && l != -1 && i == k && j == l)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(matrix[i, j].ToString() + "\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.Write(matrix[i, j].ToString() + "\t");
                }
                Console.WriteLine(b[i].ToString());
            }
            if (solution != null)
            {
                for (int i = 0; i < matrix.GetLength(1) + 4; i++) Console.Write("_______");
                Console.Write("\n  \tz \t");

                for (int i = 0; i < solution.GetLength(0); i++)
                    Console.Write(solution[i].ToString() + "\t");
                Console.WriteLine();
            }

        }

        static public Frac[,] SimplexForJordanConvertion(Frac[,] matrix, Frac[] b, Frac[] solution)
        {
            Frac[,] result = new Frac[matrix.GetLength(0) + 1, matrix.GetLength(1) + 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = matrix[i, j];
                }
            }
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                result[i, result.GetLength(1) - 1] = b[i];
            }
            for (int j = 0; j < result.GetLength(1); j++)
            {
                result[result.GetLength(0) - 1, j] = solution[j];
            }
            return result;
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
            Frac f3 = new Frac();
            f3.numerator = f1.numerator * f2.numerator;
            f3.denominator = f1.denominator * f2.denominator;
            return f3;
        }
        public static Frac operator /(Frac f1, Frac f2)
        {
            Frac f3 = new Frac();
            f3.numerator = f1.numerator * f2.denominator;
            f3.denominator = f1.denominator * f2.numerator;
            return f3;
        }
        public static Frac operator +(Frac f1, Frac f2)
        {
            Frac f3 = new Frac();
            f3.denominator = f1.denominator * f2.denominator;
            f3.numerator = (f1.numerator * f2.denominator) + (f2.numerator * f1.denominator);
            return f3;
        }
        public static Frac operator -(Frac f1, Frac f2)
        {
            Frac f3 = new Frac(0);
            f3.denominator = f1.denominator * f2.denominator;
            f3.numerator = (f1.numerator * f2.denominator) - (f2.numerator * f1.denominator);
            return f3;
        }
        public static bool operator >(Frac f1, Frac f2)
        {
            return (f1.numerator / (float)f1.denominator) > (f2.numerator / f2.denominator);
        }
        public static bool operator <(Frac f1, Frac f2)
        {
            return (f1.numerator / (float)f1.denominator) < (f2.numerator / f2.denominator);
        }
        public static bool operator >(Frac f1, int number)
        {
            return f1.numerator / (float)f1.denominator > number;
        }
        public static bool operator <(Frac f1, int number)
        {
            return (f1.numerator / (float)f1.denominator) < number;
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
                if ((numerator % i == 0) && (denominator % i == 0))
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
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                if (stat(arr[i]))
                    return i;
            }
            return -1;
        }
        public static int findElem(Frac[,] matrix, int i)
        {
            Statement stat = x => x == 1;
            int result = firstEntry(Utils.getRow(matrix, i), stat);
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
                        resultMtx[g, k] = (leadingElem * matrix[g, k] - matrix[i, k] * matrix[g, j]) / leadingElem;
                    }
                }
            }

            return resultMtx;
        }

        //for simplex tables
        public static void JordanForSimplex(Frac[,] matrix, Frac[] b, Frac[] solution, int i, int j)
        {
            Frac[,] result = Utils.SimplexForJordanConvertion(matrix, b, solution);
            result = oneJordan(result, i, j);
            
            for (int k = 0; k < matrix.GetLength(0); k++)
            {
                for (int l = 0; l < matrix.GetLength(1); l++)
                {
                    matrix[k, l] = result[k, l];
                }
            }
            for (int k = 0; k < matrix.GetLength(0); k++)
            {
                b[k] = result[k, result.GetLength(1) - 1];
            }
            for (int l = 0; l < result.GetLength(1); l++)
            {
                solution[l] = result[result.GetLength(0) - 1, l];
            }
        }

        //public static Frac[][] fullJordan(Frac[][] matrix)
        //{
        //    for (int i = 0; i < matrix.GetLength(0); i++)
        //    {

        //        oneJordan(matrix, i,);
        //    }
        //    return matrix;
        //}

        public static void interactiveMode(Frac[,] matrix)
        {
            Utils.PrintMatrix(matrix);
            while (true)
            {
                int row, col;

                Console.Write("row:");
                row = int.Parse(Console.ReadLine());
                if (row == -1)
                {
                    Console.ReadKey();
                    return;
                }

                Console.Write("col:");
                col = int.Parse(Console.ReadLine());
                if (col == -1)
                {
                    Console.ReadKey();
                    return;
                }

                matrix = Jordan.oneJordan(matrix, row, col);
                Console.Write("\n");
                Utils.PrintMatrix(matrix);
            }
        }
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
                    if (matrix[i, j] == 1)
                    {
                        int k = 0;
                        while ((k < matrix.GetLength(0) && matrix[k, j] == 0) || k == i) k++;

                        if (k == matrix.GetLength(0))
                            basis[i] = j;
                    }
                }
            }
            return basis;
        }

        static public Frac[] computeSolution(Frac[,] matrix, Frac[] b, Frac[] target)
        {
            Frac[] solution = new Frac[target.GetLength(0) + 1];

            int[] basisIdx = obtainBasisIdx(matrix);
            Frac[] c = Utils.getValues(target, basisIdx);
            for (int i = 0; i < target.Length; i++)
            {
                Frac[] a = new Frac[matrix.GetLength(0)];
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    a[j] = matrix[j, i];
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
                if (solution[i] > 0)
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

        static private bool hasLimits(Frac[,] matrix, Frac[] solution)
        {
            for (int i = 0; i < solution.Length - 1; i++)
            {
                if(solution[i] > 0)
                {
                    for(int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[j, i] > 0)
                            return true;
                    }
                }
            }
            return false;
        }

        static private int[] chooseKeyElem(Frac[,] matrix, Frac[] b, Frac[] target, Frac[] solution)
        {
            int col = Utils.maxIdxSolution(solution);
            Dictionary<int, Frac> keyRelations = new Dictionary<int, Frac>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, col] > 0)
                {
                    keyRelations.Add(i, b[i] / matrix[i, col]);
                }
            }
            //find min from key relations
            int[] keys = new int[keyRelations.Keys.Count];
            keyRelations.Keys.CopyTo(keys, 0);
            int row = keys[0];
            foreach(int key in keys)
            {
                if (keyRelations[key] < keyRelations[row])
                    row = key;
            }
            return new int[] { row, col };
        }

        public static Frac fullSimplex(Frac[,] matrix, Frac[] b, Frac[] target)
        {
            Frac[] solution = computeSolution(matrix, b, target);
            Utils.PrintSimplexTable(matrix, b, target, solution);
            int iterNum = 1;
            while (!isSolutionCorrect(solution))
            {
                if (!hasLimits(matrix, solution))
                    return null;
                int[] coords = chooseKeyElem(matrix, b, target, solution);
                Jordan.JordanForSimplex(matrix, b, solution, coords[0], coords[1]);
                Console.WriteLine("\nIteration number:" + iterNum.ToString());
                Utils.PrintSimplexTable(matrix, b, target, solution, coords[0], coords[1]);
                iterNum++;
            }
            return solution[solution.Length - 1];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Frac[] target = new Frac[] { new Frac(0), new Frac(2), new Frac(0), new Frac(1), new Frac(-3), new Frac(0), new Frac(0), new Frac(0) };
            Frac[] b = new Frac[] { new Frac(6), new Frac(1), new Frac(24) };

            Frac[,] matrix = new Frac[,]
            {
                {new Frac(4), new Frac(1), new Frac(1),  new Frac(0),  new Frac(1),   new Frac(1), new Frac(0), new Frac(0)},
                {new Frac(-1), new Frac(3), new Frac(-1),   new Frac(0), new Frac(3),new Frac(0), new Frac(1), new Frac(0)},
                {new Frac(8), new Frac(4), new Frac(12),  new Frac(4), new Frac(12), new Frac(0), new Frac(0), new Frac(1)}
            };

            Frac result = SimplexMethod.fullSimplex(matrix, b, target);
            Console.Read();
                           
        }
    }
}