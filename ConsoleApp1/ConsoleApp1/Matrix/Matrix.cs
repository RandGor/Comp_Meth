using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    public interface IMatrix
    {
        int M { set; get; } //stroki
        int N { set; get; }
    }
    public class Matrix: IMatrix
    {
        public int M { set; get; } //stroki
        public int N { set; get; }
        public double[][] Elem { set; get; }
        public Matrix() { }
        public Matrix(int m, int n) { 
            M = m; N = n; Elem = new double[m][];
            for (int i = 0; i < n; i++)
                Elem[i] = new double[n];
        }

        public void SetI()
        {
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    Elem[i][j] = i==j?1:0;
        }

        public double NormRow(int i)
        {
            return GetRow(i).Norma();
        }

        public double NormCol(int j)
        {
            return GetCol(j).Norma();
        }

        public Vector GetRow(int i) 
        {
            Vector row = new Vector(N);
            for (int j = 0; j < N; j++)
                row.Elem[i] = Elem[i][j];

            return row;
        }

        public Vector GetCol(int j)
        {
            Vector col = new Vector(N);
            for (int i = 0; i < N; i++)
                col.Elem[i] = Elem[i][j];

            return col;
        }


        public void Copy(Matrix Out)
        {
            if (N != Out.N || M != Out.M) throw new Exception("Copy: dim(matrix1) != dim(matrix2)...");

            
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                    Out.Elem[i][j] = Elem[i][j];
        }

        public void Add(Matrix T2)
        {
            if (M != T2.M || N != T2.N) throw new Exception("dim(Matrix1) != dim(Matrix2)...");

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < T2.N; j++)
                {
                    Elem[i][j] += T2.Elem[i][j];
                }
            }
        }


        public static Vector operator *(Matrix m1, Vector v1)
        {
            if (m1.N != v1.N) throw new Exception("dim(Matrix1) != dim(vector1)...");

            Vector RES = new Vector(m1.N);

            double sum;
            for (int i = 0; i < m1.M; i++)
            {
                sum = 0;
                for (int j = 0; j < m1.N; j++)
                {
                    sum += m1.Elem[i][j] * v1.Elem[j];
                }
                RES.Elem[i] = sum;
            }

            return RES;
        }

        public static Vector operator *(Vector v1, Matrix m1)
        {
            if (m1.M != v1.N) throw new Exception("dim(Matrix1) != dim(vector1)...");

            Vector RES = new Vector(m1.N);

            double sum;
            for (int i = 0; i < m1.M; i++)
            {
                sum = 0;
                for (int j = 0; j < m1.N; j++)
                {
                    sum += v1.Elem[j] * m1.Elem[j][i];
                }
                RES.Elem[i] = sum;
            }

            return RES;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.N != m2.N || m1.M != m2.M) throw new Exception("Copy: dim(matrix1) != dim(matrix2)...");

            Matrix RES = new Matrix(m1.M, m2.N);
            for (int i = 0; i < m1.M; i++)
                for (int j = 0; j < m1.N; j++)
                    RES.Elem[i][j] = m1.Elem[i][j] + m2.Elem[i][j];

            return RES;
        }

        public static Matrix operator *(Matrix T1, Matrix T2)
        {
            if (T1.N != T2.M) throw new Exception("M * M: dim(Matrix1) != dim(Matrix2)...");

            Matrix RES = new Matrix(T1.M, T2.N);

            for (int i = 0; i < T1.M; i++)
            {
                for (int j = 0; j < T2.N; j++)
                {
                    for (int k = 0; k < T1.N; k++)
                    {
                        RES.Elem[i][j] += T1.Elem[i][k] * T2.Elem[k][j];
                    }
                }
            }
            return RES;
        }

    }
}
