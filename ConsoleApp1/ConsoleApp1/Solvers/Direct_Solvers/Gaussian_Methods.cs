using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class Gaussian_Methods
    {
        public static int Find_Main_Element(Matrix A, int j)
        {
            int Index = j;
            for (int i = j + 1; i < A.M; i++)
                if (Math.Abs(A.Elem[i][j]) > Math.Abs(A.Elem[Index][j])) Index = i;
            if (Math.Abs(A.Elem[Index][j]) < CONST.Eps)
                throw new Exception("Degenerate matrix");
            return Index;
        }

        public static void Direct_Way(Matrix A, Vector F)
        {
            double help = 0;
            for (int i = 0; i < A.M-1; i++)
            {
                int I = Find_Main_Element(A, i);
                
                if (I != i)
                {
                    var Help = A.Elem[I];
                    A.Elem[I] = A.Elem[i];
                    A.Elem[i] = Help;

                    help = F.Elem[i];
                    F.Elem[i] = F.Elem[I];
                    F.Elem[I] = help;
                }

                for (int j = i + 1; j < A.M; j++)
                {
                    help = A.Elem[j][i] / A.Elem[i][i];
                    A.Elem[j][i] = 0;
                    for (int k = i + 1; k < A.N; k++)
                    {
                        A.Elem[j][k] -= A.Elem[i][k] * help;
                    }
                    F.Elem[j] -= F.Elem[i] * help;
                }
            }
        }

        public static Vector Start_Solver(Matrix A_, Vector F_) {
            Matrix A = new Matrix(A_.M, A_.N);
            Vector F = new Vector(F_.N);

            Vector X = new Vector(F_.N);

            A_.Copy(A);
            F_.Copy(F);

            Direct_Way(A, F);

            Substitution_Methods.Back_Row_Substitution(A, X, F);

            return X;
        }
    }
}
