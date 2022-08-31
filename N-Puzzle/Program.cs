using System;
using System.Collections.Generic;
using System.IO;

namespace N_Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            int file_path =15;
            Go:
            string[] Array_Of_Path = {
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Solvable Puzzles\8 Puzzle (1).txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Solvable Puzzles\8 Puzzle (2).txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Solvable Puzzles\8 Puzzle (3).txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Solvable Puzzles\15 Puzzle - 1.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Solvable Puzzles\24 Puzzle 1.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Solvable Puzzles\24 Puzzle 2.txt",

                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan & Hamming\50 Puzzle.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan & Hamming\99 Puzzle - 1.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan & Hamming\99 Puzzle - 2.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan & Hamming\9999 Puzzle.txt",

                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 1.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 3.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 4.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Solvable puzzles\Manhattan Only\15 Puzzle 5.txt",

                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\V. Large test case\TEST.txt",

                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Unsolvable Puzzles\8 Puzzle - Case 1.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Unsolvable Puzzles\8 Puzzle(2) - Case 1.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Unsolvable Puzzles\8 Puzzle(3) - Case 1.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Unsolvable Puzzles\15 Puzzle - Case 2.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Sample\Sample Test\Unsolvable Puzzles\15 Puzzle - Case 3.txt",

                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Unsolvable puzzles\15 Puzzle 1 - Unsolvable.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Unsolvable puzzles\99 Puzzle - Unsolvable Case 1.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Unsolvable puzzles\99 Puzzle - Unsolvable Case 2.txt",
                @"F:\3-2-Projects\Ahmed Osama\Alghorithms\Project\Testcases\Complete\Complete Test\Unsolvable puzzles\9999 Puzzle.txt" };
            // @ reduce double //
            FileStream f = new FileStream(Array_Of_Path[file_path], FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(f);
            int size = int.Parse(sr.ReadLine());
            int[,] Two_D_Puzzle = new int[size, size];
            int indexofZero_X = -1, indexofZero_Y = -1;
            string firstline = sr.ReadLine();
            for (int i = 0; i < size; i++)
            {
                string[] s;
                if (firstline != "")
                {
                    s = firstline.Split(' ');
                    firstline = "";
                }
                else
                {
                    s = sr.ReadLine().Split(' ');
                }
                for (int j = 0; j < size; j++)
                {
                    Two_D_Puzzle[i, j] = int.Parse(s[j]);
                    if (Two_D_Puzzle[i, j] == 0)
                    {
                        indexofZero_X = i;
                        indexofZero_Y = j;
                    }
                }
            }
            sr.Close();
            f.Close();
            Console.WriteLine("\t\t\t\t\t  -----------------------------------");
            Console.WriteLine("\t\t\t\t\t  |                                 |");
            Console.WriteLine("\t\t\t\t\t  | <<       N-Puzzle Game       >> |");
            Console.WriteLine("\t\t\t\t\t  |                                 |");
            Console.WriteLine("\t\t\t\t\t  |============= Names =============|");
            Console.WriteLine("\t\t\t\t\t  |                                 |");
            Console.WriteLine("\t\t\t\t\t  |1- Ahmed Osama Mansour           |");
            Console.WriteLine("\t\t\t\t\t  |2- Mohammed Ashraf Said          |");
            Console.WriteLine("\t\t\t\t\t  |3- Yara Elsayed Ali              |");
            Console.WriteLine("\t\t\t\t\t  |                                 |");
            Console.WriteLine("\t\t\t\t\t  -----------------------------------");
            Console.WriteLine();
        again:
            Console.WriteLine();
            Console.WriteLine("1 - Solve Puzzle Using Hamming");
                Console.WriteLine("2 - Solve Puzzle Using Manhatten");
                Console.WriteLine("3 - Choose Another Puzzle");
                Console.WriteLine("4 - If You Want To Close");
                Console.WriteLine("************************************************************************************************************************");
                Console.WriteLine("Enter Option That You Want:");
                int Option = int.Parse(Console.ReadLine());
                switch (Option)
                {
                    case 1:
                    //Hamming
                    // Is_Hamming_OR_Manhatten True
                    new astar(Two_D_Puzzle, size, true, indexofZero_X, indexofZero_Y);
                    goto again;
                    break;
                    case 2:
                    //Manhatten
                    // Is_Hamming_OR_Manhatten false
                    new astar(Two_D_Puzzle, size, false, indexofZero_X, indexofZero_Y);
                    goto again;
                    break;
                    case 3:
                        file_path++;
                        goto Go;
                        //Console.WriteLine(file_path);
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Enter Vaild Number Between 1 , 2 , 3 and 4");
                        goto again;
                        //break;
                }
        }
    }
}
