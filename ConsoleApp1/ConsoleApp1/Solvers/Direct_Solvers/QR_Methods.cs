using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class QR_Methods
    {
        public static void QR_Decompose(Matrix A, Matrix Q, Matrix R)
        {
            Matrix Q_ = new Matrix(Q.M, Q.N);

            for (int j = 0; j < A.M; j++)
            {
                //q_j=xj
                for (int i = 0; i < A.M; i++)
                    Q_.Elem[i][j] = A.Elem[i][j];

                for (int i = 0; i < j; i++)
                {
                    //rij=(q_j,Qi)
                    R.Elem[i][j] = Q_.GetCol(j) * Q.GetCol(i);
                    
                    //q_j-=rij*qi
                    for (int k = 0; k < A.M; k++) 
                        Q_.Elem[k][j] -= Q.Elem[k][i] * R.Elem[i][j];
                }

                //rjj=|q_j|
                R.Elem[j][j] = Q_.GetCol(j).Norma();

                if (R.Elem[j][j] == 0)
                    break;

                //qj=q_j/rjj
                for (int k = 0; k < A.M; k++)
                    Q.Elem[k][j] = Q_.Elem[k][j] / R.Elem[j][j];

            }
        }

        public static Vector Start_Solver(Matrix A, Vector F)
        {
            Vector X = new Vector(F.N);
            Vector Y;
            Matrix Q = new Matrix(F.N, F.N);
            Matrix R = new Matrix(F.N, F.N);

            //0. A=QR
            QR_Decompose(A, Q, R);
            //1. y=Q'F
            Y = F * Q;
            //2. Rx=y
            Substitution_Methods.Back_Row_Substitution(R, X, Y);

            return X;
        }
    }
}
