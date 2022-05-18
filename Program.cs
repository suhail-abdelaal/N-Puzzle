using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using N_Puzzle;

namespace N_Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            //  string[] folder = Directory.GetFiles(@"C:\Users\Suhail Mahmoud\Desktop\Testcases\Sample\Sample Test\Solvable Puzzles");

            var time = new Stopwatch();
            string filePath = @"C:\Users\Suhail Mahmoud\Desktop\Puzzle1.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();

            // replacing whitespaces strings with empty strings, i.e: "      " -> ""
            for (int i = 0; i < lines.Count; i++)
                lines[i] = lines[i].Trim();

            // removing empty lines
            lines.RemoveAll(s => s == "");

            int size = int.Parse(lines[0]);
            lines.RemoveAt(0);

            KeyValuePair<int, int> zeroPos = new KeyValuePair<int, int>(0, 0);
            int[,] puzzle = new int[size, size];
            for (int i = 0; i < lines.Count; i++)
            {
                var arr = lines[i].Split(new[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < arr.Length; j++)
                {
                    puzzle[i, j] = int.Parse(arr[j]);
                    if (puzzle[i, j] == 0)
                        zeroPos = new KeyValuePair<int, int>(i, j);
                }
            }


            State.setSize(size);
            State s = new State(puzzle, null);
            s.setZeroPos(zeroPos.Key, zeroPos.Value);
            if (s.isSolvable())
            {
                Console.WriteLine("Choose a method:");
                Console.WriteLine("1: Hamming \n2: Manhattan \n3: Hamming & Manhattan");
                Console.Write("> ");

                int flag = Convert.ToInt32(Console.ReadLine());
                switch (flag)
                {
                    case 1:
                        State.isHamming = true;
                        break;
                    case 2:
                        State.isManhattan = true;
                        break;
                    case 3:
                        {
                            State.isHamming = true;
                            State.isManhattan = true;
                        }
                        break;
                }
                A_star algo;
                algo = new A_star(s);

                algo.solve();
                algo.printNumOfSteps();
                //if (size == 3)
                //    algo.printSteps(algo.current);

            }
            else
                Console.WriteLine("Not Solvable");


        }
    }
}

