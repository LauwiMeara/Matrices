using System;

namespace Matrices
{
    class Program
    {
        static void Main()
        {
            // Write a program that creates the following N by N multidimensional arrays (matrices) and then prints them on the console.
            // All examples are for n=4.
            // a) 1  5  9 13   b)  1  8  9 16    c)  7 11 14 16    d)   1  2  3  4
            //    2  6 10 14       2  7 10 15        4  8 12 15        12 13 14  5
            //    3  7 11 15       3  6 11 14        2  5  9 13        11 16 15  6
            //    4  8 12 16       4  5 12 13        1  3  6 10        10  9  8  7

            bool wantsMatrix = true;

            while (wantsMatrix)
            {
                Console.Clear();
                Console.WriteLine(@"
  __  __       _        _               
 |  \/  | __ _| |_ _ __(_) ___ ___  ___ 
 | |\/| |/ _` | __| '__| |/ __/ _ \/ __|
 | |  | | (_| | |_| |  | | (_|  __/\__ \
 |_|  |_|\__,_|\__|_|  |_|\___\___||___/
                                        
");

                Console.WriteLine("Let's print a matrix!");
                Console.WriteLine();

                int minValue = 1;
                int maxValue = 20;

                Console.Write("Give a number between {0} and {1}: ", minValue, maxValue);

                int n = GetIntInput(minValue, maxValue);

                n = GetInputBetween(n, minValue, maxValue);
                Console.WriteLine();

                string printOption = GetPrintOption();
                Console.WriteLine();

                if (printOption == "A")
                {
                    PrintMatrix(n, CreateMatrixPerColumn(n), printOption);
                }
                else if (printOption == "B")
                {
                    PrintMatrix(n, CreateMatrixSnakePattern(n), printOption);
                }
                else if (printOption == "C")
                {
                    PrintMatrix(n, CreateMatrixDiagonallyFromBottomLeft(n), printOption);
                }
                else if (printOption == "D")
                {
                    PrintMatrix(n, CreateMatrixSpiral(n), printOption);
                }

                Console.WriteLine();

                wantsMatrix = RequestOtherMatrix(wantsMatrix);
            }
        }

        static bool RequestOtherMatrix(bool wantsMatrix)
        {
            Console.Write("Do you want to see another matrix? (Y/N) ");

            string yesOrNo = Console.ReadLine().Trim().ToUpper();

            string[] validAnswers = { "Y", "N", "YES", "NO"};

            while (Array.IndexOf(validAnswers, yesOrNo) == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This input is invalid.");
                Console.ResetColor();
                Console.Beep();
                Console.WriteLine();
                Console.Write("Please enter Yes (Y) or No (N). ");
                yesOrNo = Console.ReadLine().Trim().ToUpper();
            }

            if (yesOrNo == "N" || yesOrNo == "No")
            {
                wantsMatrix = false;
            }

            return wantsMatrix;
        }

        static int GetIntInput(int minValue, int maxValue)
        {
            int n;

            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have to enter a number.");
                Console.ResetColor();
                Console.Beep();
                Console.WriteLine();
                Console.Write("Please give a number between {0} and {1}: ", minValue, maxValue);
             }

            return n;
        }

        static int GetInputBetween(int n, int minValue, int maxValue)
        {
            while (n < minValue || n > maxValue)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This number isn't between the requested values.");
                Console.ResetColor();
                Console.Beep();
                Console.WriteLine();
                Console.Write("Please give a number between {0} and {1}: ", minValue, maxValue);
                n = GetIntInput(minValue, maxValue);
            }

            return n;
        }

        static string GetPrintOption()
        {
            Console.WriteLine("Do you want to print the matrix:");
            Console.WriteLine("A. per column;");
            Console.WriteLine("B. in a snake pattern;");
            Console.WriteLine("C. diagonally, starting from the bottom left corner;");
            Console.WriteLine("D. as a spiral.");
            Console.WriteLine();
            Console.Write("Please enter A, B, C or D: ");
            string printOption = Console.ReadLine().Trim().ToUpper();

            string[] validPrintOptions =  { "A", "B", "C", "D" };

            while (Array.IndexOf(validPrintOptions, printOption) == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This input is invalid.");
                Console.ResetColor();
                Console.Beep();
                Console.WriteLine();
                Console.Write("Please enter A, B, C or D: ");
                printOption = Console.ReadLine().Trim().ToUpper();
            }

            return printOption;
        }

        static int[,] CreateMatrixPerColumn(int n)
        {
            int[,] matrix = new int[n, n];

            int outputNumber = 1;
            int cycle = 0;

            while (outputNumber <= n * n)
            {
                for (int row = 0; row < n; row++)
                {
                    matrix[row, cycle] = outputNumber++;
                }
                cycle++;
            }

            return matrix;
        }

        static int[,] CreateMatrixSnakePattern(int n)
        {
            int[,] matrix = new int[n, n];

            int outputNumber = 1;
            int cycle = 0;

            while (outputNumber <= n * n)
            {
                // Down
                for (int row = 0; row < n; row++)
                {
                    matrix[row, cycle] = outputNumber++;
                }

                cycle++;

                // Up
                for (int row = n - 1; row >= 0 && cycle < n; row--)
                {
                    matrix[row, cycle] = outputNumber++;
                }

                cycle++;
            }

            return matrix;
        }

        static int[,] CreateMatrixDiagonallyFromBottomLeft(int n)
        {
            int[,] matrix = new int[n, n];

            int outputNumber = 1;
            int cycle = 1;
            int x = n - 1;
            int y = 0;

            matrix[x, y] = outputNumber++;

            // First half of the matrix (including diagonal line top left - bottom right)
            while (cycle < n)
            {
                x = x - cycle;
                y = y - cycle + 1;
                matrix[x, y] = outputNumber++;

                for (int i = 0; i < cycle; i++)
                {
                    x++;
                    y++;
                    matrix[x, y] = outputNumber++;
                }

                cycle++;
            }

            cycle--;

            // Second half of the matrix
            while (cycle > 0)
            {
                x = x - cycle;
                y = y - cycle + 1;
                matrix[x, y] = outputNumber++;

                for (int i = 1; i < cycle; i++)
                {
                    x++;
                    y++;
                    matrix[x, y] = outputNumber++;
                }

                cycle--;
            }

            return matrix;
        }

        static int[,] CreateMatrixSpiral(int n)
        {
            int[,] matrix = new int[n, n];

            int i = 1;
            int j = 0;

            while (i <= n * n)
            {
                // First row
                for (int y = j; y < n - j; y++)
                {
                    matrix[j, y] = i;
                    i++;
                }

                // Last column
                for (int x = 1 + j; x < n - j; x++)
                {
                    matrix[x, n - 1 - j] = i;
                    i++;
                }

                // Last row
                for (int y = n - 2 - j; y >= j; y--)
                {
                    matrix[n - 1 - j, y] = i;
                    i++;
                }

                // First column
                for (int x = n - 2 - j; x > j; x--)
                {
                    matrix[x, j] = i;
                    i++;
                }

                j++;
            }

            return matrix;
        }

        static void PrintMatrix(int n, int[,] matrix, string printOption)
        {
            Console.WriteLine("This is matrix {0}:", printOption);

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    Console.Write("{0}\t", matrix[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}
