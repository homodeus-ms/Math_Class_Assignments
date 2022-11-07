using System;

namespace Lab8
{
    public static class Matrix
    {
        public static int DotProduct(int[] v1, int[] v2)
        {
            if (v1 == null || v2 == null)
            {
                return int.MaxValue;
            }
            
            int result = 0;

            for (int i = 0; i < v1.Length; ++i)
            {
                result += v1[i] * v2[i];
            }

            return result;
        }
        public static int[,] Transpose(int[,] matrix)
        {
            if (matrix == null)
            {
                return null;
            }

            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);

            int[,] result = new int[col, row];

            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }
        public static int[,] GetIdentityMatrix(int size)
        {
            int[,] result = new int[size, size];

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (i == j)
                    {
                        result[i, j] = 1;
                    }
                }
            }
            
            return result;
        }
        public static int[] GetRowOrNull(int[,] matrix, int row)
        {
            if (matrix == null || row < 0 || row >= matrix.GetLength(0))
            {
                return null;
            }

            int[] result = new int[matrix.GetLength(1)];

            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = matrix[row, i];
            }

            return result;
        }
        public static int[] GetColumnOrNull(int[,] matrix, int col)
        {
            if (matrix == null || col < 0 || col >= matrix.GetLength(1))
            {
                return null;
            }

            int[] result = new int[matrix.GetLength(0)];

            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = matrix[i, col];
            }

            return result;
        }
        public static int[] MultiplyMatrixVectorOrNull(int[,] matrix, int[] vector)
        {
            if (matrix == null || vector == null || matrix.GetLength(1) != vector.Length)
            {
                return null;
            }

            int[] result = new int[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    result[i] += matrix[i, j] * vector[j];  
                }
            }

            return result;
        }
        public static int[] MultiplyVectorMatrixOrNull(int[] vector, int[,] matrix)
        {
            if (matrix == null || vector == null || vector.Length != matrix.GetLength(0))
            {
                return null;
            }

            int[] result = new int[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(1); ++i)
            {
                for (int j = 0; j < matrix.GetLength(0); ++j)
                {
                    result[i] += vector[j] * matrix[j, i];
                }
            }
            return result;
        }
        public static int[,] MultiplyOrNull(int[,] multiplicandMatrix, int[,] multiplierMatrix)
        {
            if (multiplicandMatrix == null || multiplierMatrix == null)
            {
                return null;
            }
            
            if (multiplierMatrix.GetLength(0) == 1)
            {
                multiplierMatrix = Transpose(multiplierMatrix);
            }

            if (multiplicandMatrix.GetLength(1) != multiplierMatrix.GetLength(0))
            {
                return null;
            }

            int row = multiplicandMatrix.GetLength(0);
            int col = multiplierMatrix.GetLength(1);

            int[,] result = new int[row, col];

            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < col; ++j)
                {
                    for (int k = 0; k < multiplierMatrix.GetLength(0); ++k)
                    {
                        result[i, j] += multiplicandMatrix[i, k] * multiplierMatrix[k, j];
                    }
                }
            }

            return result;
        }
    }
}
