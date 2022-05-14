using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using N_Puzzle;

namespace N_Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            //  string[] folder = Directory.GetFiles(@"C:\Users\Suhail Mahmoud\Desktop\Testcases\Sample\Sample Test\Solvable Puzzles");


            //  foreach (string file in folder)
            string fileName = "Puzzle1.txt";
            string[] textFile = File.ReadAllLines(fileName);
            int size = int.Parse(textFile[0]);
            string[,] withoutSpaces = new string[textFile.Length, size];
            int[,] puzzle = new int[size, size];

            for (int i = 0; i < textFile.Length; i++)
            {
                string[] arr = textFile[i].Split(' ');
                for (int j = 0; j < arr.Length; j++)
                {
                    withoutSpaces[i, j] = arr[j];
                }
            }


            int index = 0;
            for (int i = 2; i < withoutSpaces.GetLength(0); i++)
            {
                for (int j = 0; j < withoutSpaces.GetLength(1); j++)
                {
                    if (withoutSpaces[i, j] != " ")
                        puzzle[index, j] = int.Parse(withoutSpaces[i, j]);
                }
                index++;
            }

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
                algo.printSteps(algo.current);
            }
            else
                Console.WriteLine("Not Solvable");


        }
    }
}

