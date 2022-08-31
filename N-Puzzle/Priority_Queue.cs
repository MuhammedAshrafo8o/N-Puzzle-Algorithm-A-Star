using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class Priority_Queue
    {
        // enqueue , dequeue , empty , count
        // State and cost
        List<KeyValuePair<State, int>> Tree;
        public Priority_Queue()
        {
            Tree = new List<KeyValuePair<State, int>>();
        }
        public bool IsEmpty()
        {
            if (Tree.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Mini_Heapify(int x)
        {
            // Because it is zero Index
            int Right_term = (2 * x) + 2;
            int Parent_of_Node = x / 2;
            int left_term = (2 * x) + 1;
            int Lowest = x;
            if (left_term < Tree.Count && Tree[left_term].Value < Tree[Lowest].Value)
            {
                Lowest = left_term;
            }
            if (Right_term < Tree.Count && Tree[Right_term].Value < Tree[Lowest].Value)
            {
                Lowest = Right_term;
            }
            if (Lowest != x)
            {
                KeyValuePair<State, int> Temp = Tree[x];
                Tree[x] = Tree[Lowest];
                Tree[Lowest] = Temp;
                Mini_Heapify(Lowest);
            }
        }
        public void Mini_Heap_Insert(KeyValuePair<State, int> Key)
        {
            Tree.Add(Key);
            int Length = Tree.Count - 1;
            while (Length > 0)
            {
                int len = (Length - 1) / 2;
                if (Tree[len].Value > Tree[Length].Value)
                {
                    KeyValuePair<State, int> Temp = Tree[Length];
                    Tree[Length] = Tree[len];
                    Tree[len] = Temp;
                    Length = len; //parent = i/2
                }
                else
                {
                    break;
                }
            }
        }
        private KeyValuePair<State, int> Heap_Extract_Mini()
        {
            if (Tree.Count==0) {
                KeyValuePair<State, int> newp = new KeyValuePair<State, int>(null,-1);
                return newp;

            }
            KeyValuePair<State, int> mini_Value = Tree[0];
            Tree[0] = Tree[Tree.Count - 1];
            Tree.RemoveAt(Tree.Count - 1);
            Mini_Heapify(0);
            return mini_Value;
        }
        public void Enqueue(KeyValuePair<State, int> new_node)
        {
            Mini_Heap_Insert(new_node);
        }
        public KeyValuePair<State, int> Dequeue()
        {
            return Heap_Extract_Mini();
        }
        /*******************************************************************************************************************************/

    }
}
