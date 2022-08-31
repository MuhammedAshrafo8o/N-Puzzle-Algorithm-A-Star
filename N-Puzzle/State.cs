using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace N_Puzzle
{
    class State
    {
        public State parient;
        public int[,] Matrix;
        public int Old_X;
        public int Old_y;
        public int New_x;
        public int New_y;
        public int G_Of_X; // number of Movement or levels
        public int H_Of_X; // cost based on Manhatten or Hamming
        public int cost;
        public int size;
        public int path;

        public State(int[,] Temp,int old_Of_X,int old_Of_Y, int size)
        {
            this.size = size;
            this.Matrix = new int[size, size];
            for (int start = 0; start < size; start++)
            {
                for (int start2 = 0; start2 < size; start2++)
                {
                    this.Matrix[start, start2] = Temp[start, start2];
                }
            }
            //index of Zero
            this.Old_X = -1;
            this.Old_y = -1;
            this.New_x = old_Of_X;
            this.New_y = old_Of_Y;  
            //---------> Because it is the first initial state <---------
            this.G_Of_X = 0;
            MD5 md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(ConvertMatrixToString(Temp, size)));
            var ivalue = BitConverter.ToInt32(hashed, 0);
            this.path = ivalue;
            this.parient = null;
        }
        public State (State new_P,int[,]Temp,int Old_G_Of_X,int old_Of_X,int old_Of_Y,int New_Of_x,int New_Of_y,int size)
        {
            this.size = size;
            this.G_Of_X = Old_G_Of_X + 1;
            this.Matrix = new int[size, size];
            for (int start = 0; start < size; start++)
            {
                for (int start2 = 0; start2 < size; start2++)
                {
                    this.Matrix[start, start2] = Temp[start, start2];
                }
            }
            this.New_x = New_Of_x;
            this.New_y = New_Of_y;
            this.Old_X = old_Of_X;
            this.Old_y = old_Of_Y;
            int temp = this.Matrix[New_Of_x, New_Of_y];
            this.Matrix[New_Of_x, New_Of_y]=  this.Matrix[old_Of_X, old_Of_Y]; //0
            this.Matrix[old_Of_X, old_Of_Y] = temp;
            this.path = ConvertMatrixToString(Temp, size).GetHashCode();
            this.parient = new_P;
        }
        public int Total_Cost() {
            this.cost = G_Of_X + H_Of_X;
            return this.cost;
        }
        public string ConvertMatrixToString(int[,] arr, int length)
        {
            string StringMatrix = "";
            for (int i = 0; i < length; i++) { 
                for (int j = 0; j < length; j++) 
                {
                    StringMatrix += arr[i, j].ToString();
                }
            }
            return StringMatrix;
        }
    }
}
