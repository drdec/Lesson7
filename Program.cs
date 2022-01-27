using System;

namespace Laba5
{
    class Program
    {
        static void Main(string[] args)
        {
            double a = 0.6;
            double b = 1.724;
            double c = 0;
            double d = 1.9;
            double h = 1e-7;
            double n = (b - a) / h;

            Console.WriteLine(n);

            Console.WriteLine("Метод трапеции - " + TrapezoidMethod(a, b, h, n));
            Console.WriteLine("Метод Симсона - " + SimsonsMethod(a, b, h, n));
            Console.WriteLine("Кубаторный метод Симпсона - " + MethodOfCubeSimpson(a, b, c, d));
        }

        static double GetFunction(double x)
        {
            return Math.Pow(x + Math.Pow(x, 3), 0.5);
        }

        static double GetFunction(double x, double y)
        {
            return Math.Pow(x, 2) + (2 * y);
        }

        static double TrapezoidMethod(double a, double b, double h, double n)
        {
            double integral = h * (GetFunction(a) + GetFunction(b)) / 2.0;

            for (int i = 1; i <= n - 1; i++)
            {
                integral += h * GetFunction(a + h * i);
            }

            return integral;
        }

        static double SimsonsMethod(double a, double b, double h, double n)
        {
            double integral = h * (GetFunction(a) + GetFunction(b)) / 6.0;

            for (int i = 1; i <= n; i++)
            {
                integral += 4.0 / 6.0 * h * GetFunction(a + h * (i - 0.5));
            }
            for (int i = 1; i <= n - 1; i++)
            {
                integral += 2.0 / 6.0 * h * GetFunction(a + h * i);
            }

            return integral;
        }

        static double MethodOfCubeSimpson(double a, double b, double c, double d)
        {
            int m = 2;
            int n = 2 * m;
            double hx = (b - a) / (2 * n);
            double hy = (d - c) / n;
            double integral = 0.0;
            double sum = 0.0;
            double xi = a;
            double yi = c;

            double[] xI = new double[2 * n + 1];
            xI[0] = xi;

            for (int i = 1; i <= 2 * n; i++)
                xI[i] = xI[i - 1] + hx;

            double[] yI = new double[2 * m + 1];
            yI[0] = yi;

            for (int i = 1; i <= 2 * m; i++)
            {
                yI[i] = yI[i - 1] + hy;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    sum += GetFunction(xI[2 * i], yI[2 * j]);
                    sum += 4 * GetFunction(xI[2 * i + 1], yI[2 * j]);
                    sum += GetFunction(xI[2 * i + 2], yI[2 * j]);
                    sum += 4 * GetFunction(xI[2 * i], yI[2 * j + 1]);
                    sum += 16 * GetFunction(xI[2 * i + 1], yI[2 * j + 1]);
                    sum += 4 * GetFunction(xI[2 * i + 2], yI[2 * j + 1]);
                    sum += GetFunction(xI[2 * i], yI[2 * j + 2]);
                    sum += 4 * GetFunction(xI[2 * i + 1], yI[2 * j + 2]);
                    sum += GetFunction(xI[2 * i + 2], yI[2 * j + 2]);
                }
            }

            integral += sum;
            integral *= (hx * hy / 9);

            return integral;
        }
    }
}