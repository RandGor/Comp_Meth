using System;
using System.IO;

namespace Com_Methods
{
    public interface IVector
    {
        int N { set; get; }
    }

    public class Vector : IVector
    {
        public int N { set; get; }
        public double[] Elem { set; get; }

        public Vector()
        {
        }

        public Vector(int n)
        {
            N = n;
            Elem = new double[n];
        }


        public static Vector operator *(Vector T, double Scal)
        {
            Vector RES = new Vector(T.N);

            for (int i = 0; i < T.N; i++)
                RES.Elem[i] = T.Elem[i] * Scal;
            return RES;
        }

        public void DotScal (double Scal)
        {
            for (int i = 0; i < N; i++)
                Elem[i] = Elem[i] * Scal;
        }

        public static double operator *(Vector V1, Vector V2)
        {
            if (V1.N != V2.N) throw new Exception("V1 * V2: dim(vector1) != dim(vector2)...");

            double RES = 0.0;

            for (int i = 0; i < V1.N; i++)
                RES += V1.Elem[i] * V2.Elem[i];

            return RES;
        }

        public static Vector operator +(Vector V1, Vector V2)
        {
            if (V1.N != V2.N) throw new Exception("V1 + V2: dim(vector1) != dim(vector2)...");
            Vector RES = new Vector(V1.N);

            for (int i = 0; i < V1.N; i++)
                RES.Elem[i] = V1.Elem[i] + V2.Elem[i];
            return RES;
        }

        public void Add (Vector V2)
        {
            if (N != V2.N) throw new Exception("V1 + V2: dim(vector1) != dim(vector2)...");

            for (int i = 0; i < N; i++)
                Elem[i] += V2.Elem[i];
        }

        public void Copy (Vector Out)
        {
            if (N != Out.N) throw new Exception("Copy: dim(vector1) != dim(vector2)...");
            for (int i = 0; i < N; i++) 
                Out.Elem[i] = Elem[i];
        }

        public double Norma()
        {
            return Math.Sqrt(this*this);
        }

        public void ConsoleWriteVector ()
        {
            for (int i = 0; i < N; i++) 
                Console.WriteLine(Elem[i]);
        }

        public override string ToString()
        {
            string s = "";

            for (int i = 0; i < N - 1; i++)
                s += Elem[i] + "\t";

            return s + Elem[N-1];
        }
        public void SetI()
        {
            for (int i = 0; i < N; i++)
                Elem[i] = 1;
        }
    }
}