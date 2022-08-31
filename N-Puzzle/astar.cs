using System;
using System.Collections.Generic;
using System.Text;

namespace N_Puzzle
{
    class astar
    {
        int size;
        int[] One_D;
        int[,] Goal;
        Priority_Queue pqueue;
        bool Is_Hamming_OR_Manhatten;
        bool Compare_With_Parient;
        HashSet<int> Visited = new HashSet<int>();
        public static int[] ONE_D_Matrix(int[,] Two_D_Puzzle, int size)
        {
            int length = (size) * (size);
            int[] arr1D = new int[length];
            //int indexofZero_X = -1, indexofZero_Y = -1;
            int x = 0, y = 0;
            for (int i = 0; i < length; i++)
            {
                arr1D[i] = Two_D_Puzzle[x, y];
                y++;
                if (y == size)
                {
                    x++;
                    y = 0;
                }
            }
            return arr1D;
        }
        private static bool is_Solvable_OR_Not(int[] Matrix, int length)
        {
            int Number_Of_inversions = 0;
            int spaceIndex = -1;
            for (int i = 0; i < Matrix.Length - 1; i++)
            {
                //find index of Zero
                if (Matrix[i] == 0)
                {
                    spaceIndex = i / length;
                    continue;
                }
                for (int j = i + 1; j < Matrix.Length; j++)
                {
                    //ignore Zero from adding
                    if (Matrix[j] == 0)
                    {
                        continue;
                    }
                    else if (Matrix[i] > Matrix[j])
                    {
                        Number_Of_inversions++;
                    }
                }
            }
            //if size is odd and num of inversions is even 
            if (length % 2 != 0 && Number_Of_inversions % 2 == 0)
            {
                return true;
            }
            //if size is even and num of inversions id odd and where zero place is in even place
            else if (length % 2 == 0 && Number_Of_inversions % 2 != 0 && spaceIndex % 2 == 0)
            {
                return true;
            }
            //if size is even and num of inversions id even and where zero place is in odd place
            else if (length % 2 == 0 && Number_Of_inversions % 2 == 0 && spaceIndex % 2 != 0)
            {
                return true;
            }
            return false;
        }
        public bool check_Parient_Step(State child)
        {
            bool check = false;
            if (child.parient.Old_X == child.New_x && child.parient.Old_y == child.New_y)
            {
                return true;
            }
            return check;
        }
        private void ShowSteps(State S)
        {
            int CounterOfSteps = 1;
            Stack<State> Save_Steps = new Stack<State>();
            while (S.parient != null)
            {
                Save_Steps.Push(S);
                S = S.parient;
            }
            // push initial state in stack
            Save_Steps.Push(S);
            // pop initial state
            State TempIteration = Save_Steps.Pop();

            while (Save_Steps.Count > 0)
            {
                // print Board Of Puzzle for each step
                for (int i = 0; i < TempIteration.size; i++)
                {
                    for (int j = 0; j < TempIteration.size; j++)
                    {
                        Console.Write(TempIteration.Matrix[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                //Console.WriteLine(InitialStep.Total_Cost() - InitialStep.G_Of_X);
                //Console.WriteLine(InitialStep.H_Of_X);
                Console.WriteLine();
                TempIteration = Save_Steps.Pop();
                Console.WriteLine("Step Number : " + CounterOfSteps);
                CounterOfSteps++;
            }
            for (int i = 0; i < S.size; i++)
            {
                for (int j = 0; j < S.size; j++)
                {
                    Console.Write(Goal[i, j] + " ");
                }
                Console.WriteLine();
            }
            return;
        }
        private int Manhattan(int[,] Matrix, int length)
        {
            int Reminder = 0, divide = 0, counter = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (Matrix[i, j] != 0 && Matrix[i, j] != ((i * length) + j + 1))
                    {
                        divide = (Matrix[i, j] - 1) / length;
                        Reminder = (Matrix[i, j] - 1) % length;
                        int check1 = divide - i;
                        int check2 = Reminder - j;
                        if (check1 < 0)
                        {
                            check1 *= -1;
                        }
                        if (check2 < 0)
                        {
                            check2 *= -1;
                        }

                        counter += check1 + check2;
                    }
                }
            }
            return counter;
        }
        private int Hamming(int[,] Matrix)
        {
            int counter = 0;
            for (int i = 0; i <this.size ; i++)
            {
                for (int j = 0; j < this.size; j++)
                {
                    if (Matrix[i, j] != Goal[i,j] && Matrix[i, j] != 0)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
        public int[,] Optimal_N_puzzel(int length)
        {
            int counter = 0;
            int[,] Matrix = new int[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    counter++;
                    Matrix[i, j] = counter;

                    if (i == (length - 1) && j == (length - 1))
                    {
                        Matrix[i, j] = 0;
                    }
                }
            }
            return Matrix;
        }
        public astar(int[,] start, int size, bool Is_Hamming_OR_Manhatten, int old_Of_X, int old_Of_Y)
        {
            this.Goal = Optimal_N_puzzel(size);
            this.size = size;
            this.Is_Hamming_OR_Manhatten = Is_Hamming_OR_Manhatten;
            pqueue = new Priority_Queue();
            //if true it is same with parient 
            initialState(start, ref old_Of_X, ref old_Of_Y, size);
        }
        public void initialState(int[,] Temp, ref int old_Of_X, ref int old_Of_Y, int size)
        {
            int x = 0;
            int y = 0;
            One_D = ONE_D_Matrix(Temp, size);
            x = Environment.TickCount;
            if (is_Solvable_OR_Not(One_D, size))
            {
                Console.WriteLine("Solvable");
                State initialMatrix = new State(Temp, old_Of_X, old_Of_Y, size);
                if (Is_Hamming_OR_Manhatten)
                {
                    initialMatrix.H_Of_X = Hamming(Temp);
                }
                else
                {
                    initialMatrix.H_Of_X = Manhattan(initialMatrix.Matrix, size);
                }
                if (initialMatrix.H_Of_X == 0)
                {
                    Console.Write("Number Of Movement = " + initialMatrix.G_Of_X);
                    Console.WriteLine();
                    return;
                }
                pqueue.Enqueue(new KeyValuePair<State, int>(initialMatrix, initialMatrix.Total_Cost()));
                Resolve();
                y = Environment.TickCount;
                Console.WriteLine("Time Taken Is : " + (y - x)+ " Ms ");
            }
            else
            {
                Console.WriteLine("Not Solvable!!!");
                return;
            }
        }
        public void Resolve()
        {
            KeyValuePair<State, int> Temp;
            while (!pqueue.IsEmpty())//pqueue.Count!=0
            {
                Temp = pqueue.Dequeue();
                Visited.Add(Temp.Key.path);
                if (Temp.Key.New_x < this.size - 1)//Temp.Key.Move.down
                {
                    State new_Down_State = new State(Temp.Key, Temp.Key.Matrix, Temp.Key.G_Of_X, Temp.Key.New_x, Temp.Key.New_y, Temp.Key.New_x + 1, Temp.Key.New_y, this.size);
                    Compare_With_Parient = check_Parient_Step(new_Down_State);
                    if (!Compare_With_Parient)
                    {
                        if (Is_Hamming_OR_Manhatten)
                        {
                            new_Down_State.H_Of_X = Hamming(new_Down_State.Matrix);
                        }
                        else
                        {
                            new_Down_State.H_Of_X = Manhattan(new_Down_State.Matrix, this.size);
                        }
                        if (new_Down_State.H_Of_X == 0)
                        {
                            ShowSteps(new_Down_State);
                            return;
                        }
                        if (!Visited.Contains(new_Down_State.path))
                        {
                            pqueue.Enqueue(new KeyValuePair<State, int>(new_Down_State, new_Down_State.Total_Cost()));
                        }
                    }
                }
                if (Temp.Key.New_y > 0)//Temp.Key.Move.left
                {
                    State new_Left_State = new State(Temp.Key, Temp.Key.Matrix, Temp.Key.G_Of_X, Temp.Key.New_x, Temp.Key.New_y, Temp.Key.New_x, Temp.Key.New_y - 1, this.size);
                    Compare_With_Parient = check_Parient_Step(new_Left_State);
                    if (!Compare_With_Parient)
                    {

                        if (Is_Hamming_OR_Manhatten)
                        {
                            new_Left_State.H_Of_X = Hamming(new_Left_State.Matrix);
                        }
                        else
                        {
                            new_Left_State.H_Of_X = Manhattan(new_Left_State.Matrix, this.size);
                        }
                        if (new_Left_State.H_Of_X == 0)
                        {
                            ShowSteps(new_Left_State);
                            return;
                        }
                        if (!Visited.Contains(new_Left_State.path))
                        {
                            pqueue.Enqueue(new KeyValuePair<State, int>(new_Left_State, new_Left_State.Total_Cost()));
                        }
                    }
                }
                if (Temp.Key.New_x > 0) //Temp.Key.Move.up
                {
                    State New_Up_State = new State(Temp.Key, Temp.Key.Matrix, Temp.Key.G_Of_X, Temp.Key.New_x, Temp.Key.New_y, Temp.Key.New_x - 1, Temp.Key.New_y, Temp.Key.size);
                    Compare_With_Parient = check_Parient_Step(New_Up_State);
                    if (!Compare_With_Parient)
                    {

                        if (Is_Hamming_OR_Manhatten)
                        {
                            New_Up_State.H_Of_X = Hamming(New_Up_State.Matrix);
                        }
                        else
                        {
                            New_Up_State.H_Of_X = Manhattan(New_Up_State.Matrix, this.size);
                        }
                        if (New_Up_State.H_Of_X == 0)
                        {
                            ShowSteps(New_Up_State);
                            return;
                        }
                        if (!Visited.Contains(New_Up_State.path))
                        {
                            pqueue.Enqueue(new KeyValuePair<State, int>(New_Up_State, New_Up_State.Total_Cost()));
                        }
                    }
                }
                if (Temp.Key.New_y < size - 1) //Temp.Key.Move.right
                {
                    State new_Right_State = new State(Temp.Key, Temp.Key.Matrix, Temp.Key.G_Of_X, Temp.Key.New_x, Temp.Key.New_y, Temp.Key.New_x, Temp.Key.New_y + 1, this.size);
                    Compare_With_Parient = check_Parient_Step(new_Right_State);
                    if (!Compare_With_Parient)
                    {
                        if (Is_Hamming_OR_Manhatten)
                        {
                            new_Right_State.H_Of_X = Hamming(new_Right_State.Matrix);
                        }
                        else
                        {
                            new_Right_State.H_Of_X = Manhattan(new_Right_State.Matrix, this.size);
                        }
                        if (new_Right_State.H_Of_X == 0)
                        {
                            ShowSteps(new_Right_State);
                            return;
                        }
                        if (!Visited.Contains(new_Right_State.path))
                        {
                            pqueue.Enqueue(new KeyValuePair<State, int>(new_Right_State, new_Right_State.Total_Cost()));
                        }
                    }
                }
            }
        }
    }
}
