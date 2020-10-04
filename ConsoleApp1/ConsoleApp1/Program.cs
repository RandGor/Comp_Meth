using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class CONST
    {
        public const double Eps = 1e-10;
    }
    class MODE
    {
        public const int QR_GRAMM = 0;
        public const int QR_GRAMM_MOD = 1;
        public const int QR_GIVENS = 2;
        public const int QR_HOUSEHOLDER = 3;
    }
    class Program
    {
        public static double MeasureTime(Matrix A, Vector F, int mode) {
            Stopwatch sw = new Stopwatch();
            Vector X;
            sw.Start();

            if (mode== -2)
                X = Gaussian_Methods.Start_Solver(A, F);
            else if (mode == -1)
                X = LU_Methods.Start_Solver(A, F);
            else
                X = QR_Methods.Start_Solver(A, F, mode);


            sw.Stop();

            return sw.Elapsed.TotalSeconds;
        }

        static void Main(string[] args)
        {
            int N = 3;
            var A = new Matrix(N, N);
            var F = new Vector(N);

            string[] names = new string[] { "Gauss", "LU", "QR Gramm", "QR GrammMod", "QR Givens", "QR Householder" };

            Vector X_GAUSS;
            Vector X_LU;
            Vector X_QR_Gramm;
            Vector X_QR_GrammMod;
            Vector X_QR_Givens;
            Vector X_QR_Householder;

            var Elem = new double[][]
            {
                new double[]{-2,-2,-1}, 
                new double[]{ 1, 0,-2}, 
                new double[]{ 0, 1, 2}
            };

            A.Elem = Elem;

            var ElemV = new double[] { -5, -1, 3 };
            
            F.Elem = ElemV;

            X_GAUSS = Gaussian_Methods.Start_Solver(A, F);
            X_LU = LU_Methods.Start_Solver(A, F);
            X_QR_Gramm = QR_Methods.Start_Solver(A, F, MODE.QR_GRAMM);
            X_QR_GrammMod = QR_Methods.Start_Solver(A, F, MODE.QR_GRAMM_MOD);
            X_QR_Givens = QR_Methods.Start_Solver(A, F, MODE.QR_GIVENS);
            X_QR_Householder = QR_Methods.Start_Solver(A, F, MODE.QR_HOUSEHOLDER);

            Console.WriteLine("==================SOLVE=================");
            Console.WriteLine(names[0] + "\t\t" + X_GAUSS.ToString());
            Console.WriteLine(names[1] + "\t\t" + X_LU.ToString());
            Console.WriteLine(names[2] + "\t" + X_QR_Gramm.ToString());
            Console.WriteLine(names[3] + "\t" + X_QR_GrammMod.ToString());
            Console.WriteLine(names[4] + "\t" + X_QR_Givens.ToString());
            Console.WriteLine(names[5] + "\t" + X_QR_Householder.ToString());
            Console.WriteLine("==================TIME==================");

            var rand = new Random();

            for (int c = 100; c <= 500; c+=100)
            {
                Matrix At = new Matrix(c, c);
                Vector Vt = new Vector(c);

                At.SetI();
                Vt.SetI();
                for (int i = 0; i < c; i++)
                    for (int I = 0; I < c; I++)
                    {
                        double rt = rand.NextDouble() * 100;
                        Vt.Elem[I] += Vt.Elem[i] * rt;
                        At.Elem[I][i] += At.Elem[i][i] * rt;
                    }





                Console.WriteLine(c + "x" + c);
                for (int mode = MODE.QR_GRAMM-2; mode <= MODE.QR_HOUSEHOLDER; mode++)
                {
                    double t = 0;
                    for (int time = 0; time < 10; time++)
                    {
                        t += MeasureTime(At, Vt, mode);
                    }
                    Console.WriteLine(names[mode+2] + "\t"+(t/10));
                }

            }


            Console.ReadKey();
        }
    }
}
