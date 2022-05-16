using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace N_Puzzle
{
    public class A_star
    {
        public State current;
        private bool hamming;
        private bool manhattan;

        public A_star(State initial, int flag)
        {
            current = (State)initial.Clone();
            hamming = false;
            manhattan = false;
            switch (flag)
            {
                case 1: hamming = true; 
                    break;
                case 2: manhattan = true;
                    break;
                case 3:
                    {
                        hamming = true;
                        manhattan = true;
                    }
                    break;
            }
        }

        public void solve()
        {
            PriorityQueue<State, int> pq = new PriorityQueue<State, int>();
           

            HashSet<string> visited = new HashSet<string>();
            visited.Add(State.getStringPuzzle(current.puzzle));
            var timer = new Stopwatch();
            timer.Start();
            while (!(current.isGoal()))
            {
                // Gettign the legal moves of the blank square
                List<char> moves = current.getMoves();

                // Each move is a child node 
                for (int i = 0; i < moves.Count; i++)
                {
                    // Generating a child 
                    int[,] newPuzzle = current.getnewPuzzle(moves[i]);
                    /*if (State.getStringPuzzle(current.puzzle) == State.getStringPuzzle(newPuzzle))
                        continue;*/
                    // Console.WriteLine("df");

                    // if the child is visited we skip adding it's heuristic values to the priority queue
                    if (visited.Contains(State.getStringPuzzle(newPuzzle)))
                        continue;

                    State newChild = new State(newPuzzle, current);
                    current.addChild(newChild);

                    if (hamming)
                        pq.Enqueue(newChild, newChild.getHammingDist());
                    if (manhattan)
                        pq.Enqueue(newChild, newChild.getManhattanDist());
                }
                current = pq.Dequeue(); // Assigning the node with the minimum heuristic score to the current node
                visited.Add(State.getStringPuzzle(current.puzzle)); // setting the current node as visited

            }
            timer.Stop();
            Console.WriteLine("Time in sceonds: " + timer.Elapsed + " s");
            Console.WriteLine("Time in Milliseconds: " + timer.ElapsedMilliseconds + " ms");
        }

        public void printNumOfSteps()
        {
            Console.WriteLine("# Steps: "+ current.getDepth());
            Console.WriteLine();
        }
        public void printSteps(State it)
        {
            if (it.getParent() == null)
                return;

            printSteps(it.getParent());
            it.display();
            Console.WriteLine();
        }


    }
}