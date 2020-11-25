using System;
using System.Diagnostics;

namespace Lab2
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Ведите число n (1 < n <= 100)");
                int n = int.Parse(Console.ReadLine());

                if (n > 1 && n <= 100)
                    N = n;
                else
                    Main(new string[0]);

                Console.WriteLine("Ведите число m (1 < m <= 50)");
                int m = int.Parse(Console.ReadLine());

                if (m > 1 && m <= 50)
                    M = m;
                else
                    Main(new string[0]);

                Console.WriteLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Данные введены не верно!");
                Main(new string[0]);
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }


            Matrix = new int[N, M];
            Fill();
            ShowMatrix();


            Console.WriteLine();

            for (int row = 0; row < N; row++)
            {
                if (IsZeroRow(row))
                {
                    Tuple<int[,], int[,]> segments = GetSegments(row);
                    Combine(segments.Item1, GetNewRow(), segments.Item2);
                    row += 1;
                }
            }

            ShowMatrix();
            Console.ReadKey();
        }



        private static int N { get; set; }
        private static int M { get; set; }
        private static int[,] Matrix { get; set; }



        private static void Fill()
        {
            Random random = new Random();

            for (int row = 0; row < N; row++)
            {
                for (int column = 0; column < M; column++)
                {
                    double randDouble = random.NextDouble();
                    if (randDouble < 0.8)
                        Matrix[row, column] = 0;
                    else
                        Matrix[row, column] = 1;
                }
            }
        }



        private static void ShowMatrix()
        {
            for (int row = 0; row < N; row++)
            {
                for (int column = 0; column < M; column++)
                {
                    Console.Write(Matrix[row, column] + " ");
                }
                Console.WriteLine();
            }
        }



        private static bool IsZeroRow(int row)
        {
            for (int column = 0; column < M; column++)
            {
                if (Matrix[row, column] != 0)
                    return false;
            }

            return true;
        }



        private static Tuple<int[,], int[,]> GetSegments(int row)
        {
            int[,] item1 = new int[row + 1, M];
            int[,] item2 = new int[N - row - 1, M];


            for (int j = 0; j < M; j++)
            {
                int a = 0;

                for (int i = 0; i < row + 1; i++)
                    item1[i, j] = Matrix[i, j];

                for (int i = row + 1; i < N; i++)
                {
                    item2[a, j] = Matrix[i, j];
                    a++;
                }
            }

            return new Tuple<int[,], int[,]>(item1, item2);
        }



        private static int[] GetNewRow()
        {
            int[] newRow = new int[M];

            for (int column = 0; column < M; column++)
            {
                newRow[column] = 5;
            }

            return newRow;
        }



        private static void Combine(params Array[] arrays)
        {
            int finalRowsCount = 0;

            for (int i = 0; i < arrays.Length; i++)
            {
                Array array = arrays[i] as Array;

                if (arrays[i] is int[,])
                    finalRowsCount += array.GetLength(0);
                else if (arrays[i] is int[])
                    finalRowsCount += 1;
            }

            N = finalRowsCount;
            Matrix = new int[N, M];


            int arrayPosition = 0;
            int rowOffset = 0;

            for (int row = 0; row < N; row++)
            {
                if (arrayPosition == arrays.Length)
                    return;

                if (arrays[arrayPosition] is int[])
                {
                    int[] currentArray = arrays[arrayPosition] as int[];
                    for (int column = 0; column < M; column++)
                    {
                        Matrix[row, column] = currentArray[column];
                    }
                    arrayPosition++;
                }
                else if (arrays[arrayPosition] is int[,])
                {
                    int[,] currentArray = arrays[arrayPosition] as int[,];

                    for (int column = 0; column < M; column++)
                    {
                        Matrix[row, column] = currentArray[rowOffset, column];
                    }
                    if (rowOffset < currentArray.GetLength(0) - 1)
                    {
                        rowOffset++;
                    }
                    else
                    {
                        rowOffset = 0;
                        arrayPosition++;
                    }
                }
            }
        }
    }
}
