using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class Substitution_Methods
    {
        public static void Direct_Row_Substitution(Matrix A, Vector X, Vector F)
        {
            F.Copy(X);

            for (int i = 0; i < F.N; i++)
            {
                if (Math.Abs(A.Elem[i][i]) < CONST.Eps)
                    throw new Exception("Direct Row Substitution: division by 0...");
                for (int j = 0; j < i; j++)
                {
                    X.Elem[i] -= A.Elem[i][j] * X.Elem[j];
                }
                X.Elem[i] /= A.Elem[i][i];
            }
        }
        public static void Direct_Column_Substitution(Matrix A, Vector X, Vector F)
        {
            F.Copy(X);

            for (int j = 0; j < F.N; j++)
            {
                if (Math.Abs(A.Elem[j][j]) < CONST.Eps)
                    throw new Exception("Direct Column Substitution: division by 0...");

                X.Elem[j] /= A.Elem[j][j];

                for (int i = j + 1; i < F.N; i++)
                {
                    X.Elem[i] -= A.Elem[i][j] * X.Elem[j];
                }
            }
        }

        public static void Back_Row_Substitution(Matrix A, Vector X, Vector F)
        {
            F.Copy(X);

            for (int i = F.N - 1; i >= 0; i--)
            {
                if (Math.Abs(A.Elem[i][i]) < CONST.Eps)
                    throw new Exception("Back Row Substitution: division by 0... ");

                for (int j = i + 1; j < F.N; j++)
                {
                    X.Elem[i] -= A.Elem[i][j] * X.Elem[j];
                }
                X.Elem[i] /= A.Elem[i][i];
            }

        }

        public static void Back_Column_Substitution(Matrix A, Vector X, Vector F)
        {
            F.Copy(X);

            for (int j = F.N - 1; j >= 0; j--)
            {
                if (Math.Abs(A.Elem[j][j]) < CONST.Eps)
                    throw new Exception("Back Column Substitution: division by 0...");
                X.Elem[j] /= A.Elem[j][j];

                for (int i = j - 1; i >= 0; i--)
                {
                    X.Elem[i] -= A.Elem[i][j] * X.Elem[j];
                }
            }
        }
    }
}
