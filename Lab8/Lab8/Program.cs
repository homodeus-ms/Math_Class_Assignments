using System;
using System.Diagnostics;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = new int[3, 5]
            {
                {1,1,1,1,1 },
                {1,1,1,1,1 },
                {1,1,1,1,1 }
            };

            int[,] b = new int[5, 3]
            {
                {1,1,1 },
                {1,1,1 },
                {1,1,1 },
                {1,1,1 },
                {1,1,1 }
            };

            int[,] result = Matrix.MultiplyOrNull(a, b);

            printMatrix(result);

        }

        private static bool areVectorsEqual(int[] expected, int[] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool areMatricesEqual(int[,] expected, int[,] actual)
        {
            if (expected.GetLength(0) != actual.GetLength(0)
                || expected.GetLength(1) != actual.GetLength(1))
            {
                return false;
            }

            int row = expected.GetLength(0);
            int column = expected.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (expected[i, j] != actual[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void printMatrix(int[,] matrix)
        {
            Console.WriteLine("---------------------------------");

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0, -6} ", matrix[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("---------------------------------");
        }
    }
}