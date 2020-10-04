using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class LU_Methods
    {
        public static void LU_Decompose(Matrix A, Matrix L, Matrix U)
        {
            A.Copy(U);

            Vector F = new Vector(A.N);

            Gaussian_Methods.Direct_Way(U, F);


            for (int i = 1; i < A.M; i++)
                for (int j = 0; j < i; j++)
                {
                    double sum = 0;
                    for (int k = 1; k <= j - 1; k++)
                        sum += L.Elem[i][k] * U.Elem[k][j];

                    L.Elem[i][j] = (A.Elem[i][j] - sum) / U.Elem[j][j];
                }

            for (int i = 0; i < A.M; i++)
                L.Elem[i][i] = 1;

        }

        public static Vector Start_Solver(Matrix A, Vector F)
        {
            Vector X = new Vector(F.N);
            Vector Y = new Vector(F.N);
            Matrix L = new Matrix(F.N, F.N);
            Matrix U = new Matrix(F.N, F.N);

            //0. A=LU
            LU_Decompose(A, L, U);
            //1. Ly=f
            Substitution_Methods.Direct_Row_Substitution(L, Y, F);
            //2. Ux=y
            Substitution_Methods.Back_Row_Substitution(U, X, Y);

            return X;
        }
    }
}
