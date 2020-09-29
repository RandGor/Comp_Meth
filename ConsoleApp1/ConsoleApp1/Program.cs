using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class CONST
    {
        public static double Eps = 1e-10;
    }
    class Program
    {
        static void Main(string[] args)
        {
            int N = 3;
            var A = new Matrix(N, N);
            var A_Gaussian = new Matrix(N, N);
            var F = new Vector(N);
            var F_Gaussian = new Vector(N);


            var L = new Matrix(N, N);
            var U = new Matrix(N, N);


            var Y = new Vector(N);

            var X_GAUSS = new Vector(N);
            var X_LU = new Vector(N);
            var X_QR = new Vector(N);

            var Elem = new double[][]
            {
                new double[]{-2,-2,-1}, new double[]{1,0,-2}, new double[]{0,1,2}
            };

            A.Elem = Elem;
            A.Copy(A_Gaussian);

            var ElemV = new double[] { -5, -1, 3 };
            
            F.Elem = ElemV;
            F.Copy(F_Gaussian);
            F.Copy(X_QR);

            //Гаусс меняет вектор и матрицу
            X_GAUSS.Elem = Gaussian_Methods.Start_Solver(A_Gaussian, F_Gaussian).Elem;
            X_LU.Elem = LU_Methods.Start_Solver(A, F).Elem;
            X_QR.Elem = QR_Methods.Start_Solver(A, F,true).Elem;

            Console.WriteLine("Gauss: " + X_GAUSS.ToString());
            Console.WriteLine("LU: " + X_LU.ToString());
            Console.WriteLine("QR: " + X_QR.ToString());

            Console.ReadKey();
        }
    }
}
