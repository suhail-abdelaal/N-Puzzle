using System;
using System.Collections.Generic;
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


            string filePath = @"C:\Users\Suhail Mahmoud\Desktop\Puzzle1.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();

            // replacing whitespaces strings with empty strings, i.e: "      " -> ""
            for (int i = 0; i < lines.Count; i++)
                lines[i] = lines[i].Trim();

            // removing empty lines
            lines.RemoveAll(s => s == "");

            int size = int.Parse(lines[0]);
            lines.RemoveAt(0);

            int[,] puzzle = new int[size, size];
            for (int i = 0; i < lines.Count; i++)
            {
                var arr = lines[i].Split(new[] { ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < arr.Length; j++)
                    puzzle[i, j] = int.Parse(arr[j]);
            }

            /*Console.WriteLine(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(puzzle[i, j] + " ");
                Console.WriteLine();
            }*/



            State s = new State(puzzle);
            if (s.isSolvable())
            {
                Console.WriteLine("Choose a method:");
                Console.WriteLine("1: Hamming \n2: Manhattan \n3: Hamming & Manhattan");
                Console.Write("> ");

                int n = Convert.ToInt32(Console.ReadLine());
                A_star algo;
                algo = new A_star(s, n);

                algo.solve();
                algo.printNumOfSteps();
            }
            else
                Console.WriteLine("Not Solvable");


        }
    }
}

