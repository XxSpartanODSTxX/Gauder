using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DarcMonitor
{
   

    class Kinematik
    {

        static double L1 = 240.97;
        static double L2 = 140;
        static double ld = 259.2;// Basisbreit MHT


        static double hm = ld / (2 * Math.Sqrt(3));//% Mittelpunktslage MHT

        static double oy = 25.0;            // Lage x Drehpunkt MHT
        static double ox = 148.14 - hm;     // Lage y Drehpunkt MHT

        double theta1o = Math.Asin(ox / L1); // Nulloffset Theta1    
        double theta2o = 0; // Nulloffset Theta2
        double theta2o0 = (180 - 16.52) * Math.PI / 180;  // Nulloffset Eingefahrener Zustand




        static double ldr = ld / 2 * Math.Sqrt(3) - hm; //% halb Diagonale der Box
        static double lhr = 20;


        double[] po = { ox, oy }; // Offset von Box Mitte zu Drehpunkt
        double[] p1 = { -ldr, 0 };
        double[] p2 = { hm, -ld / 2.0f };
        double[] p3 = { hm, ld / 2.0f };
        double[] p4 = { hm + lhr, 0 };


        double x_old;
        double y_old;
        double phi_old;

        double[] theta = { 0, 0 }; // Position in Rad
        int[] nx = { 0, 0 };      // Position in inkrementen

        double[] dt = { 0, 0 };
        double[,] D12 = { {1,0, 0},{0, 1, 0 },{0, 0, 1 } };
        double[,] Ji = { {1,0},{0, 1} }; // Inverse Jacobimatrix

        int n0_mag = 1000; // offset Nullage Magnet Encoder
        double sc_mag = 2048 / Math.PI; // Skalierung Magnet Encoder

        int n0_mot = 0; // offset Nullage Motor
        double sc_mot = 40 * 1000 / Math.PI; // Skalierung Motor

        double[] xo = { 0, 0, 0 };// Last Position x,y,phi

  //      typedef double Array22[2][2];
  //  typedef double Array33[3][3];

  //  Array33* vkin(double theta1, double theta2);
   //     Array22* invJacobian(double theta1, double theta2);
   //     double* invkinstep(double dx, double dtheta);
     //   double* invkin(double x, double phi);

       // public:

    
       public  double[] jointWorld(double[] theta) { return jointWorld(theta[0], theta[1]); }


        public double[] worldJoint(double x, double phi) { return invkin(x, phi); }

        double[] inc2winkel(int n1x, int n2x)
        {
            nx[0] = n1x;
            nx[1] = n2x;
            theta[0] = (n1x - n0_mot) * sc_mot;
            theta[1] = (n2x - n0_mot) * sc_mot;
            return theta;
        }

        int[] winkel2inc(double theta1, double theta2)
        {

            nx[0] = (int)(theta1 / sc_mot + n0_mot);
            nx[1] = (int)(theta2 / sc_mot + n0_mot);
            return nx;

        }

        double[] magInc2winkel(int n1x, int n2x)
        {
            nx[0] = n1x;
            nx[1] = n2x;
            theta[0] = (n1x - n0_mag) * sc_mag;
            theta[1] = (n2x - n0_mag) * sc_mag;
            return theta;
        }

        int[] winkel2MagInc(double theta1, double theta2)
        {

            nx[0] = (int)(theta1 / sc_mag + n0_mag);
            nx[1] = (int)(theta2 / sc_mag + n0_mag);
            return nx;

        }


        public double norm2(double[] x, double[] y)
        {
            return Math.Sqrt((y[0] + x[0]) * (y[0] + x[0]) + (y[1] + x[1]) * (y[1] + x[1]));
        }

        public void setTCP(int num)
        {
            switch (num)
            {
                case 0:
                    L2 = Math.Sqrt(ox * ox + oy * oy);
                    theta2o = Math.Atan2(oy, ox);
                    break;
                case 1:
                    L2 = norm2(po, p1);
                    theta2o = Math.Atan2(po[1] + p1[1], po[0] + p1[0]);
                    break;
                case 2:
                    L2 = norm2(po, p2);
                    theta2o = Math.Atan2(po[1] + p2[1], po[0] + p2[0]);
                    break;
                case 3:
                    L2 = norm2(po, p3);
                    theta2o = Math.Atan2(po[1] + p3[1], po[0] + p3[0]);
                    break;
                case 4:
                    L2 = norm2(po, p4);
                    theta2o = Math.Atan2(po[1] + p4[1], po[0] + p4[0]);
                    break;


            }

        }


        double[,] invJacobian(double theta1, double theta2)
        {

            double c1 = Math.Cos(theta1);
            double s1 = Math.Sin(theta1);
            double c2 = Math.Cos(theta2);
            double s2 = Math.Sin(theta2);
            double lf = 1 / (L1 * s1);
            Ji[0,0] = -lf;
            Ji[0,1] = -(L2 * c1 * s2 + L2 * c2 * s1) * lf;
            Ji[1,0] = lf;
            Ji[1,1] = (L1 * s1 + L2 * c1 * s2 + L2 * c2 * s1) * lf;
            return Ji;
        }

        double[,] vkin(double theta1, double theta2)
        {
         //   double t12 = theta1 + theta1o + theta2 + theta2o + theta2o0;
            double t12 = theta1 +  theta2;
            double c = Math.Cos(t12);
            double s = Math.Sin(t12);
            D12[0,0] = c;
            D12[0,1] = -s;
            D12[0, 2] = L2 * c + L1 * Math.Cos(theta1);
            //D12[0, 2] = L2 * c + L1 * Math.Cos(theta1 + theta1o);

            D12[1,0] = s;
            D12[1,1] = c;
            D12[1, 2] = L2 * s + L1 * Math.Sin(theta1);
           // D12[1, 2] = L2 * s + L1 * Math.Sin(theta1 + theta1o);

            D12[2,0] = 0;
            D12[2,1] = 0;
            D12[2,2] = 1;
            return D12;
            //return { { c, -s, L2 * c + L1 * cos(theta1)},
            //    {s, c, L2* s + L1 * sin(theta1)},
            //    {0, 0, 1 }};
        }
        /*
        * dTheta = J-1 * dx 
        */
        double[] invkinstep(double dx, double dtheta)
        {
            dt[0] = Ji[0,0] * dx + Ji[0,1] * dtheta;
            dt[1] = Ji[1,0] * dx + Ji[1,1] * dtheta;
            return dt;
        }

        /*
        * invers Kinematik
        */
        double[] invkin(double x, double phi)
        {
            double[] xo = { x_old, y_old, phi_old };
            double[] thetax = { theta[0] + theta1o, theta[1] + theta2o + theta2o0 };
            double[,] D = vkin(thetax[0], thetax[1]);
            xo[0] = D[0,2];
            xo[1] = D[1,2];
            xo[2] = Math.Atan2(D[1,0], D[0,0]);

            for (int i = 0; i < 3; i++)
            {
                invJacobian(thetax[0], thetax[1]);
                invkinstep(x - xo[0], phi - xo[2]);
                if (dt[0] > 0.5) dt[0] = 0.5;
                if (dt[0] < -0.5) dt[0] = -0.5;
                if (dt[1] > 0.5) dt[1] = 0.5;
                if (dt[1] < -0.5) dt[1] = -0.5;
                thetax[0] += dt[0] * 0.9;
                thetax[1] += dt[1] * 0.9;
                D = vkin(thetax[0], thetax[1]);
                xo[0] = D[0,2];
                xo[1] = D[1,2];
                xo[2] = Math.Atan2(D[1,0], D[0,0]);
            }

            theta[0] = thetax[0] - theta1o;
            theta[1] = thetax[1] - (theta2o + theta2o0);
            return theta;
        }


        public double[] jointWorld(double theta1, double theta2)
        {

            theta[0] = theta1;
            theta[1] = theta2;
            double[,] D = vkin(theta1 + theta1o, theta2 + theta2o + theta2o0);
            xo[0] = D[0,2];
            xo[1] = D[1,2];
            xo[2] = Math.Atan2(D[1,0], D[0,0]);
            return xo;
        }




    }
}
