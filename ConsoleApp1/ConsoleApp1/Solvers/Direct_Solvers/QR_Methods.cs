using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class QR_Methods
    {
        public static void QR_GramDecompose(Matrix A, Matrix Q, Matrix R)
        {
            Matrix Q_ = new Matrix(Q.M, Q.N);

            for (int j = 0; j < A.M; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    //rij=(q_j,Qi)
                    R.Elem[i][j] = A.GetCol(j) * Q.GetCol(i);
                }
                
                double[] sum = new double[A.M];
                //Erijqi
                for (int i = 0; i < j; i++)
                    for (int k = 0; k < A.M; k++)
                        sum[k] += R.Elem[i][j] * Q.Elem[k][i];
                
                //q_j-=xj-Erijqi
                for (int k = 0; k < A.M; k++)
                    Q_.Elem[k][j] = A.GetCol(j).Elem[k] - sum[k];


                //rjj=|q_j|
                R.Elem[j][j] = Q_.GetCol(j).Norma();

                if (R.Elem[j][j] == 0)
                    break;

                //qj=q_j/rjj
                for (int k = 0; k < A.M; k++)
                    Q.Elem[k][j] = Q_.Elem[k][j] / R.Elem[j][j];

            }
        }

        public static void QR_GramDecomposeMOD(Matrix A, Matrix Q, Matrix R)
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

        public static void QR_RotationGivens(Matrix A, Matrix Q, Matrix R)
        {
            A.Copy(R);
            Q.SetI();
            
            double sinO = 0;
            double cosO = 1;
            double div = 1;

            //buffers
            double b1 = 0;
            double b2 = 0;

            for (int j = 0; j < A.N - 1; j++)
            {
                for (int i = j + 1; i < A.M; i++)
                {
                    //Xij != 0
                    if (Math.Abs(R.Elem[i][j]) > CONST.Eps)
                    {
                        div = Math.Sqrt(R.Elem[i][j] * R.Elem[i][j] + R.Elem[j][j] * R.Elem[j][j]);
                        sinO = R.Elem[i][j] / div;
                        cosO = R.Elem[j][j] / div;


                        for (int k = j; k < A.N; k++)
                        {
                            b1 = cosO * R.Elem[j][k] + sinO * R.Elem[i][k];
                            b2 = -sinO * R.Elem[j][k] + cosO * R.Elem[i][k];

                            R.Elem[j][k] = b1;
                            R.Elem[i][k] = b2;
                        }

                        for (int k = 0; k < Q.M; k++)
                        {
                            b1 = cosO * Q.Elem[k][j] + sinO * Q.Elem[k][i];
                            b2 = -sinO * Q.Elem[k][j] + cosO * Q.Elem[k][i];
                            Q.Elem[k][j] = b1;
                            Q.Elem[k][i] = b2;
                        }

                    }
                }
            }
        }
        
        public static void QR_ReflectionHouseholder(Matrix A, Matrix Q, Matrix R)
        {
            Vector P = new Vector(A.M);

            double summ = 0;

            double beta = 0;
            double mu = 0;

            A.Copy(R);
            Q.SetI();

            for (int j = 0; j < A.N-1; j++)
            {
                //|X|
                summ = 0;
                for (int i = j + 1; i < A.M; i++)
                    summ += R.Elem[i][j] * R.Elem[i][j];

                if (Math.Sqrt(summ) > CONST.Eps)
                {
                    //beta = sign(-Rjj)*|P|
                    beta = Utils.Sign(-R.Elem[j][j]) * Math.Sqrt(summ + R.Elem[j][j] * R.Elem[j][j]);

                    //mu = 2/|P|^2 = 1/(beta*(beta - Rjj))
                    mu = 1 / beta / (beta - R.Elem[j][j]);

                    //P=(x1-beta,x2...,xn)
                    for (int i = 0; i < A.M; i++)
                        P.Elem[i] = (i >= j)? R.Elem[i][j] : 0;

                    P.Elem[j] -= beta;

                    //R = R - 2*p*(At*p)t/|p|^2
                    for (int m = j; m < A.N; m++)
                    {
                        summ = 0;

                        for (int n = j; n < A.M; n++)
                            summ += R.Elem[n][m] * P.Elem[n];

                        summ *= mu;

                        for (int n = j; n < A.M; n++)
                            R.Elem[n][m] -= summ * P.Elem[n];
                    }

                    //Q = Q - p*(Q*p)t
                    for (int m = 0; m < A.N; m++)
                    {
                        summ = 0;

                        for (int n = j; n < A.M; n++)
                            summ += Q.Elem[m][n] * P.Elem[n];

                        summ *= mu;

                        for (int n = j; n < A.M; n++)
                            Q.Elem[m][n] -= summ * P.Elem[n];
                    }
                }
            }
        }

        public static Vector Start_Solver(Matrix A, Vector F, int mod = MODE.QR_GRAMM)
        {
            Vector X = new Vector(F.N);
            Vector Y;
            Matrix Q = new Matrix(F.N, F.N);
            Matrix R = new Matrix(F.N, F.N);


            switch (mod)
            {
                case MODE.QR_GRAMM:
                    QR_GramDecompose(A, Q, R);
                    break;
                case MODE.QR_GRAMM_MOD:
                    QR_GramDecomposeMOD(A, Q, R);
                    break;
                case MODE.QR_GIVENS:
                    QR_RotationGivens(A, Q, R);
                    break;
                case MODE.QR_HOUSEHOLDER:
                    QR_ReflectionHouseholder(A, Q, R);
                    break;

            }
            //1. y=Q'F
            Y = F * Q;
            //2. Rx=y
            Substitution_Methods.Back_Row_Substitution(R, X, Y);

            return X;
        }
    }
}
