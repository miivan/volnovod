using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;



namespace Volnovod
{
    

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            

        }

        void pictureBox1_Click(object sender, EventArgs e)
        {
             
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            int a, b,c;                                       //Параметры волновода (ширина, высота)
            c=0;
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            pictureBox1.Width = a;
            pictureBox1.Height = b;
            pictureBox2.Width = a;
            pictureBox2.Height = b*3/2;
            pictureBox3.Width = a*3/2;
            pictureBox3.Height = b;

            Graphics g1 = pictureBox1.CreateGraphics();
            Graphics g2 = pictureBox2.CreateGraphics();
            Graphics g3 = pictureBox3.CreateGraphics();
            g1.Clear(Color.White);//очистка экрана перед построением
            g2.Clear(Color.White);
            g3.Clear(Color.White);
            
            //Зеленые магнитные , Красные Электрические

            if (Proverka(c) == 0)
            {
                ExEyTM();           //Электрические     xy
                HxHyTM();           //Магнитные         xy
                ExEzTM();           //Электрические     xz
                HxHzTM();           //Магнитные         xz
                EzEyTM();           //Электрические     zy
                HzHyTM();           //Магнитные         zy

            }
            else
                label12.Text = "Ошибка, волновое число меньше каппа квадрат";
        
        }//Кнопка для ТМ

        private void button2_Click(object sender, EventArgs e)
        {
            label12.Text = "";
            int a, b, c;                                       //Параметры волновода (ширина, высота)
            c = 0;
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            pictureBox1.Width = a;
            pictureBox1.Height = b;
            pictureBox2.Width = a;
            pictureBox2.Height = b*3/2;
            pictureBox3.Width = a*3/2;
            pictureBox3.Height = b;
            Graphics g1 = pictureBox1.CreateGraphics();
            Graphics g2 = pictureBox2.CreateGraphics();
            Graphics g3 = pictureBox3.CreateGraphics();
            g1.Clear(Color.White);//очистка экрана перед построением
            g2.Clear(Color.White);
            g3.Clear(Color.White);
            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            if (Proverka(c) == 0)
            {

                if (m != 0 && n != 0)
                {
                    HxHyTE();           //Магнитные Для Те xy
                    ExEyTE();           //Электрические Для ТЕ xy  
                    HzHyTE();
                    EzEyTE();
                    HxHzTE();
                    ExEzTE();
                }
                else if (n == 0 && m != 0)
                {
                    HxHyTE();
                    ExEyTE();
                    HzHyTE();
                    EzEyTE();
                    //HxHzTE();
                    //ExEzTE();
                }
                else if (m == 0 && n != 0)
                {
                    HxHyTE();           
                    ExEyTE();
                    HzHyTE();
                    EzEyTE();
                    HxHzTE();
                    ExEzTE();
                }
            }
            else
                label12.Text = "Ошибка, волновое число меньше каппа квадрат";
            
        }//Кнопка для ТЕ

        private void button3_Click(object sender, EventArgs e)
        {
            int a, b, c;                                       //Параметры волновода (ширина, высота)
            c = 0;
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            pictureBox1.Width = a;
            pictureBox1.Height = b;
            pictureBox2.Width = a;
            pictureBox2.Height = b * 3 / 2;
            pictureBox3.Width = a * 3 / 2;
            pictureBox3.Height = b;
        }//Кнопка для размеров



        public void HzHyTM()
        {

            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            //pictureBox1.Width = a;
            //pictureBox1.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float E0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Y1, Z1;
            float X, Y, Z;
            X = 0;
            double Hx, Hz, Hy;

            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];
            int Pz, Py;
            Pz = a / (n + 1) / 2;
            Py = b / (m + 1) / 2;
            int S = 0;
            int mm = m;
            if (m % 2 == 0)
                mm = m + 1;

            for (int q = 0; q < mm + 1; q++)                                  //Изменение по Х
                for (int w = 20; w < a * 3 / 2; w = w + 20)                       //Изменение по Y
                {

                    XX[S] = 1;
                    YY[S] = q * b / (mm + 1) + Py;
                    ZZ[S] = w;
                    S++;
                }

            for (int ii = 0; ii < S; ii++)
            {

                Z = ZZ[ii];
                Y = YY[ii];
                for (int i = 0; i <= 1600; i++)
                {
                    Hz = 0;
                    Hy = kapaX * k * E0 / kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);
                    X = XX[ii];
                    Hx = -kapaY * k * E0 / kapa2 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);

                    Z1 = Z + (float)Hz / 20;
                    Y1 = Y + (float)Hy / 20;
                    X1 = X + (float)Hx;

                    Graphics g = pictureBox3.CreateGraphics();
                    //g.DrawLine(new Pen(Brushes.Green, 1), new PointF(Z, Y), new PointF(Z1, Y1));

                    if (X < X1)
                    {
                        g.DrawEllipse(new Pen(Brushes.Green, 1), Z - 3, Y - 3, 6, 6);
                        break;
                    }
                    else
                    {

                        g.DrawLine(new Pen(Brushes.Green, 1), new PointF(Z - 3, Y - 3), new PointF(Z + 3, Y + 3));
                        g.DrawLine(new Pen(Brushes.Green, 1), new PointF(Z + 3, Y - 3), new PointF(Z - 3, Y + 3));
                        break;
                    }

                    Z = Z1;
                    Y = Y1;


                    //for (int v = 0; v < 1000000; v++) ;

                }
            }


        }//Норм !

        public void HxHzTM()
        {
            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text) * 3 / 2;
            //pictureBox2.Width = a;
            //pictureBox2.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float E0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Y1, Z1;
            float X, Y, Z;
            Y = 0;
            double Hx, Hz,Hy;


            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];
            int Px, Py;
            Px = a / (n + 1) / 2;
            Py = b / (m + 1) / 5;
            int S = 0;
            int nn=n;
            if(n%2==0)
                nn=n+1;

            for (int q = 0; q < nn+1; q++)                                  //Изменение по Х
                for (int w = 20; w < a*3/2; w=w+20)                       //Изменение по Y
                {

                    XX[S] = q * a / (nn + 1) + Px;
                    YY[S] = 1;
                    ZZ[S] = w;
                    S++;
                }

            
            for (int ii = 0; ii < S; ii++)
            {

                X = XX[ii];
                Z = ZZ[ii];
                
                for (int i = 0; i <= 600; i++)
                {
                    Hx = -kapaY * k * E0 / kapa2 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);
                    Hz = 0;
                    Y = YY[ii];
                    Hy = kapaX * k * E0 / kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);

                    X1 = X + (float)Hx;
                    Z1 = Z + (float)Hz;
                    Y1 = Y + (float)Hy;
                    Graphics g = pictureBox2.CreateGraphics();
                    //g.DrawLine(new Pen(Brushes.Green, 1), new PointF(X, Z), new PointF(X1, Z1));
                    if (Y < Y1)
                    {
                        g.DrawEllipse(new Pen(Brushes.Green, 1), X - 3, Z - 3, 6, 6);
                        break;
                    }
                    else
                    {
                        
                        g.DrawLine(new Pen(Brushes.Green, 1), new PointF(X - 3, Z - 3), new PointF(X + 3, Z + 3));
                        g.DrawLine(new Pen(Brushes.Green, 1), new PointF(X + 3, Z - 3), new PointF(X - 3, Z + 3));
                        break;
                    }
                    X = X1;
                    Z = Z1;
                    //for (int v = 0; v < 10000000; v++) ;

                }
            }




        }//Норм !

        public void ExEzTM()
        {
            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text)*3/2;
            //pictureBox2.Width = a;
            //pictureBox2.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float E0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Z1;
            float X, Y, Z;
            float X0,Z0;
            Y = 1;
            double Ex, Ez;


            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];
            double Px, Pz;
            Px = a / (n + 1) / 2;
            Pz = Math.PI/h;
            int S = 0;
            

            for (int q = 1; q <= n + 1; q++)                                  //Изменение по Х
                for (int w = 0; w * Pz < b * 3 / 2; w++)                       //Изменение по Y
                {

                    XX[S] = q * a / (n + 1) - (float)Px; YY[S] = 1; ZZ[S] = (float)(w * Pz) + (float)(0.9 * Math.PI / h); S++;
                    XX[S] = q * a / (n + 1) - (float)Px; YY[S] = 1; ZZ[S] = (float)(w * Pz) + (float)(0.7 * Math.PI / h); S++;
                }

            double xx = 0;
            double zz = 0;
            for (int ii = 0; ii < S; ii++)
            {

                X = XX[ii];
                Z = ZZ[ii];
                X0=X;
                Z0=Z;
                for (int i = 0; i <= 90000; i++)
                {
                    Ex = h * kapaX * E0 / kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);
                    Ez = E0 * Math.Sin(kapaX * X) * Math.Sin(kapaY * Y) * Math.Sin(h * Z);


                    X1 = X + (float)Ex ;
                    Z1 = Z + (float)Ez ;

                    Graphics g = pictureBox2.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Red, 1), new PointF(X, Z), new PointF(X1, Z1));

                    X = X1;
                    Z = Z1;
                    if (i == 495)
                    {
                        xx = (double)X;
                        zz = (double)Z;

                    }
                    if (i == 500)
                    {
                        double ugol = Math.Atan2(xx - X1, zz - Z1);

                        g.DrawLine(new Pen(Brushes.Red, 1), X1, Z1, Convert.ToInt32(X1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Z1 + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Red, 1), X1, Z1, Convert.ToInt32(X1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Z1 + 10 * Math.Cos(ugol - 0.3)));
                    }




                    if (i > 100)
                        if (Math.Abs(X - X0) < 0.2 && Math.Abs(Z - Z0) < 0.2)
                            break;
                }
            }




        }//Норм !

        public void EzEyTM()
        {

            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text)*3/2;
            b = Convert.ToInt32(textBox2.Text);
            //pictureBox3.Width = a;
            //pictureBox3.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float E0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float Y1, Z1;
            float X, Y, Z;
            float Z0, Y0;
            X = 1;
            double Ez, Ey;

            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];

            double Pz, Py;
            Pz = Math.PI / h;
            Py = b / (m + 1) / 2;
            int S = 0;

            for (int q = 1; q <= m + 1; q++)                                  //Изменение по Х
                for (int w = 0; w * Pz < a * 3 / 2; w++)                       //Изменение по Y
                {

                    YY[S] = q * b / (m + 1) - (float)Py; XX[S] = 1; ZZ[S] = (float)(w * Pz) + (float)(0.9 * Math.PI / h); S++;
                    YY[S] = q * b / (m + 1) - (float)Py; XX[S] = 1; ZZ[S] = (float)(w * Pz) + (float)(0.7 * Math.PI / h); S++;
                }


            double zz = 0;
            double yy = 0;

            for (int ii = 0; ii < S; ii++)
            {

                Z = ZZ[ii];
                Y = YY[ii];
                Z0 = Z;
                Y0 = Y;
                for (int i = 0; i <= 600000; i++)
                {
                    Ez = E0 * Math.Sin(kapaX * X) * Math.Sin(kapaY * Y) * Math.Sin(h * Z);
                    Ey = h * kapaY * E0 / kapa2 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z)*10;


                    Z1 = Z +(float)Ez ;
                    Y1 = Y + (float)Ey /20;

                    Graphics g = pictureBox3.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Z, Y), new PointF(Z1, Y1));

                    Z = Z1;
                    Y = Y1;

                    if (i == 1095)
                    {
                        zz = (double)Z;
                        yy = (double)Y;

                    }
                    if (i == 1100)
                    {
                        double ugol = Math.Atan2(zz - Z1, yy - Y1);

                        g.DrawLine(new Pen(Brushes.Red, 1), Z1, Y1, Convert.ToInt32(Z1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Red, 1), Z1, Y1, Convert.ToInt32(Z1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                    }


                    if (i > 100)
                        if (Math.Abs(Z - Z0) < 0.1 && Math.Abs(Y - Y0) < 0.1)
                            break;

                    //for (int v = 0; v < 10000000; v++) ;

                }
            }


        }//Норм !
        
        public void ExEyTM()
        {

            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            //pictureBox1.Width = a;
            //pictureBox1.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float E0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 3;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Y1, X2, Y2, X3, Y3;
            float X, Y, Z;
            float Xs, Ys;
            Z = 0;
            double Ex, Ey, Ex1, Ey1;


            float[] XX = new float[1000];
            float[] YY = new float[1000];
            int Px, Py;
            Px = a / n / 4;
            Py = b / m / 4;
            int S = 0;
            int x1, y1;

            for (int q = 0; q < n; q++)                                  //Изменение по Х
                for (int w = 0; w < m; w++)                       //Изменение по Y
                {
                    x1 = q * a / n;
                    y1 = w * b / m;
                    
                        XX[S] = x1 + (float)4; YY[S] = y1 + Py; S++;
                        XX[S] = x1 + (float)4; YY[S] = y1 + 3 * Py; S++;
                        XX[S] = x1 + Px; YY[S] = y1 + (float)4; S++;
                        XX[S] = x1 + Px; YY[S] = y1 + 4 * Py - (float)4; S++;
                        XX[S] = x1 + 3 * Px; YY[S] = y1 + (float)4; S++;
                        XX[S] = x1 + 3 * Px; YY[S] = y1 + 4 * Py - (float)4; S++;
                        XX[S] = x1 + 4 * Px - (float)4; YY[S] = y1 + Py; S++;
                        XX[S] = x1 + 4 * Px - (float)4; YY[S] = y1 + 3 * Py; S++;
                }


            double xx=0;
            double yy = 0;
            //
            for (int ii = 0; ii <= S; ii++)
            {
                X = XX[ii];
                Y = YY[ii];
                X2 = XX[ii];
                Y2 = YY[ii];
                Xs = 0;
                Ys = 0;
                for (int i = 0; i <= 60000; i++)
                {
                    Ex = h * kapaX * E0 / kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);
                    Ey = h * kapaY * E0 / kapa2 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);
                    X1 = X + (float)Ex / 15;
                    Y1 = Y + (float)Ey / 15;
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Red, 1), new PointF(X, Y), new PointF(X1, Y1));
                    X = X1;
                    Y = Y1;
                    if (i == 400)
                    {
                        xx = (double)X;
                        yy = (double)Y;

                    }
                    if (i == 500)
                    {
                        double ugol = Math.Atan2(xx - X1, yy - Y1);

                        g.DrawLine(new Pen(Brushes.Red, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Red, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                    }

                    

                    if (i % 10000 == 0)
                    {
                        if (Xs == X && Ys == Y) break;
                        Xs = X;
                        Ys = Y;
                    }
                }
            }

            for (int ii = 0; ii <= S; ii++)
            {
                X = XX[ii];
                Y = YY[ii];
                X2 = XX[ii];
                Y2 = YY[ii];
                Xs = 0;
                Ys = 0;
                for (int i = 0; i <= 60000; i++)
                {
                    Ex = h * kapaX * E0 / kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);
                    Ey = h * kapaY * E0 / kapa2 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);
                    X1 = X - (float)Ex / 20;
                    Y1 = Y - (float)Ey / 20;
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Red, 1), new PointF(X, Y), new PointF(X1, Y1));
                    X = X1;
                    Y = Y1;

                    if (i == 400)
                    {
                        xx = (double)X;
                        yy = (double)Y;

                    }
                    if (i == 500)
                    {
                        double ugol = Math.Atan2(X1 - xx, Y1 - yy);

                        g.DrawLine(new Pen(Brushes.Red, 1), (float)xx, (float)yy, Convert.ToInt32(xx + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(yy + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Red, 1), (float)xx, (float)yy, Convert.ToInt32(xx + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(yy + 10 * Math.Cos(ugol - 0.3)));
                    }
                    
                    if (i % 10000 == 0)
                    {
                        if (Xs == X && Ys == Y) break;
                        Xs = X;
                        Ys = Y;
                    }
                }
            }

        }//норм !

        public void HxHyTM()
        {

            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            //pictureBox1.Width = a;
            //pictureBox1.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float E0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.5;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Y1;
            float X, Y, Z;
            float X0, Y0;
            Z = 0;
            double Hx, Hy;

            float[] XX = new float[1000];
            float[] YY = new float[1000];
            int Px, Py;
            Px = a / (n + 1) / 5;
            Py = b / (m + 1) / 5;
            int S = 0;

            for(int q=0;q<n;q++)                                  //Изменение по Х
                for (int w = 0; w < m; w++)                       //Изменение по Y
                {
                    for (int e = 1; e < 5; e++)                    //Количество приращений
                    {
                        XX[S] = q * a / n + +Px * e;
                        YY[S] = w * b / m + Py * e;
                        S++;
                    }
                }
            


            for (int ii = 0; ii < S; ii++)
            {

                X = XX[ii];
                Y = YY[ii];
                X0 = X;
                Y0 = Y;
                for (int i = 0; i <= 60000; i++)
                {
                    Hx = -kapaY*k*E0/kapa2* Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);
                    Hy = kapaX*k*E0/kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);


                    X1 = X + (float)Hx / 20;
                    Y1 = Y + (float)Hy / 20;

                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Green, 1), new PointF(X, Y), new PointF(X1, Y1));

                    if (i == 100)
                    {
                        double ugol = Math.Atan2(X - X1, Y - Y1);

                        g.DrawLine(new Pen(Brushes.Green, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Green, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                    }


                    X = X1;
                    Y = Y1;
                    if(i>100)
                        if (i > 100)
                            if (Math.Abs(X - X0) < 0.3 && Math.Abs(Y - Y0) < 0.3)
                                break;


                    //for (int v = 0; v < 1000000; v++) ;

                }
            }


        }//норм !





        public void ExEyTE()
        {

            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            pictureBox1.Width = a;
            pictureBox1.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float H0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.5;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Y1;
            float X, Y, Z;
            float X0, Y0;
            Z = 0;
            double Ex, Ey,xx,yy;
            xx = 0;
            yy = 0;

            int S = 0;
            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];

            if (n != 0 && m!=0)
            {
                for (int q = 0; q < n; q++)                                  //Изменение по Х
                    for (int w = 0; w < m; w++)                       //Изменение по Y
                    {
                        XX[S] = q * (a / n) + a / n / 2 - a / n / 4; YY[S] = w * (b / m) + b / m / 2 - b / m / 4; S++;

                        XX[S] = q * (a / n) + a / n / 2 + a / n / 4; YY[S] = w * (b / m) + b / m / 2 + b / m / 4; S++;

                        XX[S] = q * (a / n) + a / n / 2 + a / n / 4; YY[S] = w * (b / m) + b / m / 2 - b / m / 4; S++;

                        XX[S] = q * (a / n) + a / n / 2 - a / n / 4; YY[S] = w * (b / m) + b / m / 2 + b / m / 4; S++;
                    }
            }
            else if(n==0)
            {
                for (int r = 1; r < 4 * (n + 1); r++)
                {
                    XX[S] = 1; YY[S] = r * b / 4 * (n + 1); S++;
                    XX[S] = a; YY[S] = r * b / 4 * (n + 1); S++;
                }
            }
            else if (m == 0)
            {
                for (int r = 1; r < 4 * (m + 1); r++)
                {
                    XX[S] = r * a / 4 * (m + 1); YY[S] = b; S++;
                    XX[S] = r * a / 4 * (m + 1); YY[S] = 0; S++;
                }
            }


            for (int ii = 0; ii < S; ii++)
            {

                X = XX[ii];
                Y = YY[ii];
                X0 = X;
                Y0 = Y;
                for (int i = 0; i <= 60000; i++)
                {
                    Ex = -k *kapaY*H0/kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);
                    Ey = k * kapaX * H0 / kapa2 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);

                    if (n != 0 && m!=0)
                    {
                        X1 = X + (float)Ex / 20;
                        Y1 = Y + (float)Ey / 20;
                    }
                    else if(n==0)
                    {
                        X1 = X - (float)Ex / 20;
                        Y1 = Y - (float)Ey / 20;
                    }
                    else
                    {
                        X1 = X - (float)Ex / 20;
                        Y1 = Y - (float)Ey / 20;
                    }

                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Red, 1), new PointF(X, Y), new PointF(X1, Y1));

                    X = X1;
                    Y = Y1;
                    if (i == 495)
                    {
                        xx = (double)X;
                        yy = (double)Y;

                    }
                    if (i == 500)
                    {
                        double ugol = Math.Atan2(xx - X1, yy - Y1);

                        g.DrawLine(new Pen(Brushes.Red, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Red, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                    }
                    if (i > 100)
                        if (i > 100)
                            if (Math.Abs(X - X0) < 0.3 && Math.Abs(Y - Y0) < 0.3)
                                break;

                    
                }
            }
        }//норм !

        public void HxHyTE()
        {

            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            pictureBox1.Width = a;
            pictureBox1.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float H0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Y1;
            float X, Y, Z;
            Z = 0;
            double Hx, Hy;



            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];

            
            int S = 0;
            if (n != 0 && m!=0)
            {
                for (int q = 0; q < n; q++)                                  //Изменение по Х
                    for (int w = 0; w < m; w++)                             //Изменение по Y
                    {
                        XX[S] = q * (a / n) + a / n / 2 - a / n / 4; YY[S] = w * (b / m) + b / m / 2; S++;

                        XX[S] = q * (a / n) + a / n / 2 + a / n / 4; YY[S] = w * (b / m) + b / m / 2; S++;

                        XX[S] = q * (a / n) + a / n / 2; YY[S] = w * (b / m) + b / m / 2 - b / m / 4; S++;

                        XX[S] = q * (a / n) + a / n / 2; YY[S] = w * (b / m) + b / m / 2 + b / m / 4; S++;
                    }
            }
            else if (n == 0)
            {
                for (int r = 1; r < 4*(n+1); r++)
                {
                    XX[S] = r * a / 4 * (n + 1); YY[S] = b - 1; S++;
                }
            }
            else if(m==0)
            {
                for (int r = 1; r < 4 * (m + 1); r++)
                {
                    XX[S] = a - 1; YY[S] = r * b / 4 * (m + 1); S++;
                }

            }
            float Xs = 0; 
            float Ys = 0;

            for (int ii = 0; ii < S; ii++)
            {

                X = XX[ii];
                Y = YY[ii];
                for (int i = 0; i <= 16000; i++)
                {
                    Hx = -h * kapaX / kapa2 * H0 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);
                    Hy = -h * kapaY / kapa2 * H0 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);


                    X1 = X + (float)Hx / 20;
                    Y1 = Y + (float)Hy / 20;

                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Green, 1), new PointF(X, Y), new PointF(X1, Y1));
                    X = X1;
                    Y = Y1;
                    if (n == 0)
                    {
                        Xs = X;
                        Ys = Y;
                        if (i == 2000)
                        {
                            double ugol = Math.Atan2(Xs - X1, Ys - Y1);

                            g.DrawLine(new Pen(Brushes.Green, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                            g.DrawLine(new Pen(Brushes.Green, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                        }
                    }
                    if (m == 0)
                    {
                        Xs = XX[ii];
                        Ys = YY[ii];
                        if (i == 2000)
                        {
                            double ugol = Math.Atan2(Xs - X1, Ys - Y1);

                            g.DrawLine(new Pen(Brushes.Green, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                            g.DrawLine(new Pen(Brushes.Green, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                        }
                    }
                    if (i == 100 && n != 0 && m!=0)
                    {
                        Xs = X;
                        Ys = Y;
                    }
                    if (i == 200)
                    {
                        double ugol = Math.Atan2(Xs - X1, Ys - Y1);

                        g.DrawLine(new Pen(Brushes.Green, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Green, 1), X1, Y1, Convert.ToInt32(X1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                    }

                    if (X < 0 || X > a || Y < 0 || Y > b) break;

                    //for (int v = 0; v < 1000000; v++) ;

                }
            }

            for (int ii = 0; ii <= S; ii++)
            {
                X = XX[ii];
                Y = YY[ii];
                for (int i = 0; i <= 16000; i++)
                {
                    Hx = -h * kapaX / kapa2 * H0 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);
                    Hy = -h * kapaY / kapa2 * H0 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);
                    X1 = X - (float)Hx / 20;
                    Y1 = Y - (float)Hy / 20;
                    Graphics g = pictureBox1.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Green, 1), new PointF(X, Y), new PointF(X1, Y1));
                    X = X1;
                    Y = Y1;
                    if (X < 0 || X > a || Y < 0 || Y > b) break;

                }
            }


        }//норм !

        public void HzHyTE()
        {

            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            //pictureBox1.Width = a;
            //pictureBox1.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float H0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float Y1, Z1,X1;
            float X, Y, Z;
            float Z0, Y0;
            X = 0;
            double Hz, Hy,Hx;

            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];

            double Pz, Py;
            Pz = Math.PI / h;
            Py = b / (m + 1 ) / 2;
            int S = 0;
            if(m!=0)
                for (int q = 1; q <= m + 1; q++)                                  //Изменение по Х
                    for (int w = 0; w * Pz < a * 3 / 2; w++)                       //Изменение по Y
                    {

                        YY[S] = q * b / (m ) - (float)Py; XX[S] = 1; ZZ[S] = (float)(w * Pz) + (float)(0.9 * Math.PI / h); S++;
                        YY[S] = q * b / (m ) - (float)Py; XX[S] = 1; ZZ[S] = (float)(w * Pz) + (float)(0.7 * Math.PI / h); S++;
                    }
            
            if(m==0)
                for (int q = 0; q < m + 1; q++)                                  //Изменение по Х
                    for (int w = 20; w < a * 3 / 2; w = w + 20)                       //Изменение по Y
                    {

                        XX[S] = 0;
                        YY[S] = q * b / (m + 1) + (float)Py;
                        ZZ[S] = w;
                        S++;
                    }

            float Zs = 0;
            float Ys = 0;

            for (int ii = 0; ii <= S; ii++)
            {

                Z = ZZ[ii];
                Y = YY[ii];
                Z0 = Z;
                Y0 = Y;
                for (int i = 0; i <= 60000; i++)
                {
                    Hz = H0 * Math.Cos(kapaX * X) * Math.Cos(kapaY * Y) * Math.Sin(h * Z);
                    Hy = -h * kapaY / kapa2 * H0 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);

                    Z1 = Z + (float)Hz / 20;
                    Y1 = Y + (float)Hy / 20;


                    Graphics g = pictureBox3.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Green, 1), new PointF(Z, Y), new PointF(Z1, Y1));

                    if(m!=0)
                    if (i == 100)
                    {
                        Zs = Z;
                        Ys = Y;
                    }

                    if (m != 0)
                    if (i == 200)
                    {
                        double ugol = Math.Atan2(Zs - Z1, Ys - Y1);

                        g.DrawLine(new Pen(Brushes.Green, 1), Z1, Y1, Convert.ToInt32(Z1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Green, 1), Z1, Y1, Convert.ToInt32(Z1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                    }
                    if(m==0)
                        if (Z < Z1)
                        {
                            g.DrawEllipse(new Pen(Brushes.Green, 1), Z - 3, Y - 3, 7, 7);
                            break;
                        }
                        else
                        {

                            g.DrawLine(new Pen(Brushes.Green, 1), new PointF(Z - 4, Y - 4), new PointF(Z + 4, Y + 4));
                            g.DrawLine(new Pen(Brushes.Green, 1), new PointF(Z + 4, Y - 4), new PointF(Z - 4, Y + 4));
                            break;
                        }


                    if (i > 100)
                        if (Math.Abs(Z - Z0) < 1 && Math.Abs(Y - Y0) < 1)
                            break;
                    Z = Z1;
                    Y = Y1;


                    //for (int v = 0; v < 1000000; v++) ;

                }
            }


        }//Норм !

        public void HxHzTE()
        {
            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text) * 3 / 2;
            //pictureBox2.Width = a;
            //pictureBox2.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float H0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Z1;
            float X, Y, Z;
            float X0, Z0;
            Y = 0;
            double Hx, Hz;


            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];
            double Px, Pz;
            Px = a / (n + 1) / 2;
            Pz = Math.PI / h;
            int S = 0;


            for (int q = 1; q < n + 1; q++)                                  //Изменение по Х
                for (int w = 0; w * Pz < b * 3 / 2; w++)                       //Изменение по Y
                {

                    XX[S] = q * a / (n ) - (float)Px; YY[S] = 1; ZZ[S] = (float)(w * Pz) + (float)(0.9 * Math.PI / h); S++;
                    XX[S] = q * a / (n ) - (float)Px; YY[S] = 1; ZZ[S] = (float)(w * Pz) + (float)(0.7 * Math.PI / h); S++;
                }

            double xx = 0;
            double zz = 0;
            for (int ii = 0; ii <= S; ii++)
            {

                X = XX[ii];
                Z = ZZ[ii];
                X0 = X;
                Z0 = Z;
                for (int i = 0; i <= 60000; i++)
                {
                    Hx = -h * kapaX / kapa2 * H0 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);
                    Hz = H0 * Math.Cos(kapaX * X) * Math.Cos(kapaY * Y) * Math.Sin(h * Z);

                    X1 = X + (float)Hx / 10;
                    Z1 = Z + (float)Hz / 10;
                    Graphics g = pictureBox2.CreateGraphics();
                    g.DrawLine(new Pen(Brushes.Green, 1), new PointF(X, Z), new PointF(X1, Z1));

                    if (i > 100)
                        if (Math.Abs(X - X0) < 1 && Math.Abs(Z - Z0) < 1)
                            break;

                    X = X1;
                    Z = Z1;

                    if (i == 495)
                    {
                        xx = (double)X;
                        zz = (double)Z;

                    }
                    if (i == 500)
                    {
                        double ugol = Math.Atan2(xx - X1, zz - Z1);

                        g.DrawLine(new Pen(Brushes.Green, 1), X1, Z1, Convert.ToInt32(X1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Z1 + 10 * Math.Cos(0.3 + ugol)));
                        g.DrawLine(new Pen(Brushes.Green, 1), X1, Z1, Convert.ToInt32(X1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Z1 + 10 * Math.Cos(ugol - 0.3)));
                    }

                    //for (int v = 0; v < 10000000; v++) ;

                }
            }
        }//Норм !

        public void EzEyTE()
        {

            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text) * 3 / 2;
            b = Convert.ToInt32(textBox2.Text);
            //pictureBox3.Width = a;
            //pictureBox3.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float E0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Y1, Z1;
            float X, Y, Z;
            float Z0, Y0;
            X = 1;
            double Ez, Ey, Ex;

            double t = Math.PI / h;

            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];
            int Pz, Py;
            Pz = a / (n + 1) / 2;
            Py = b / (m + 1) / 2;
            int S = 0;
            int mm = m;
            if (m % 2 == 0)
            {
                mm = m + 1;
                Py = Py / 2;
            }
            
            if(m!=0)
            for (int q = 0; q < mm + 1; q++)                                  //Изменение по Х
                for (int w = 20; w < a * 3 / 2; w = w + 20)                       //Изменение по Y
                {


                    YY[S] = q * b / (mm + 1) + Py;
                    ZZ[S] = w;
                    S++;
                }
            if (m == 0)
            {
                for (int q = 0; q < mm + 1; q++)                                  //Изменение по Х
                    for (int w = 20; w < a * 3 / 2; w = w + 20)                       //Изменение по Y
                    {
                        XX[S] = 0;
                        YY[S] = q * b ;
                        ZZ[S] = w;
                        S++;
                    }

            }

            for (int ii = 0; ii < S; ii++)
            {

                Z = ZZ[ii];
                Y = YY[ii];
                Z0 = Z;
                Y0 = Y;
                for (int i = 0; i <= 600000; i++)
                {
                    Ez = 0;
                    Ey = k * kapaX * E0 / kapa2 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);
                    if (m != 0)
                        X = XX[0];
                    Ex = -k * kapaY * E0 / kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);

                    Z1 = Z + (float)Ez;
                    Y1 = Y + (float)Ey;
                    X1 = X + (float)Ex;

                    Graphics g = pictureBox3.CreateGraphics();
                    

                    
                    if (m != 0)
                    {
                        Z = Z1;
                        Y = Y1;
                        if (X < X1)
                        {
                            g.DrawEllipse(new Pen(Brushes.Red, 1), Z - 3, Y - 3, 7, 7);
                            break;
                        }
                        else
                        {

                            g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Z - 4, Y - 4), new PointF(Z + 4, Y + 4));
                            g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Z + 4, Y - 4), new PointF(Z - 4, Y + 4));
                            break;
                        }
                    }
                    else if(m==0)
                    {
                        g.DrawLine(new Pen(Brushes.Red, 1), new PointF(Z, Y), new PointF(Z1, Y1));
                        if ((int)Y==b/2)
                        {
                            double ugol = Math.Atan2(Z - Z1, Y - Y1);

                            g.DrawLine(new Pen(Brushes.Red, 1), Z1, Y1, Convert.ToInt32(Z1 + 10 * Math.Sin(0.3 + ugol)), Convert.ToInt32(Y1 + 10 * Math.Cos(0.3 + ugol)));
                            g.DrawLine(new Pen(Brushes.Red, 1), Z1, Y1, Convert.ToInt32(Z1 + 10 * Math.Sin(ugol - 0.3)), Convert.ToInt32(Y1 + 10 * Math.Cos(ugol - 0.3)));
                        }
                    }
                    Z = Z1;
                    Y = Y1;
                    if (m == 0)
                        if (Y < 0 || Y > b)
                            break;

                    //for (int v = 0; v < 10000000; v++) ;

                }
            }


        }//норм !

        public void ExEzTE()
        {
            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text) * 3 / 2;
            //pictureBox2.Width = a;
            //pictureBox2.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            float H0 = 1;
            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            float c = 300000000;
            double k = 0.05;
            k = Convert.ToDouble(textBox5.Text);
            double h = Math.Sqrt(k * k - kapa2);


            float X1, Y1, Z1;
            float X, Y, Z;
            float X0, Z0;
            Y = 1;
            double Ex, Ey, Ez;


            float[] XX = new float[1000];
            float[] YY = new float[1000];
            float[] ZZ = new float[1000];
            int Px, Py;
            Px = a / (n + 1) / 2;
            Py = b / (m + 1) / 5;
            int S = 0;
            int nn = n;
            if (n % 2 == 0)
                nn = n + 1;

            for (int q = 0; q < nn + 1; q++)                                  //Изменение по Х
                for (int w = 20; w < a * 3 / 2; w = w + 20)                       //Изменение по Y
                {

                    XX[S] = q * a / (nn + 1) + Px;
                    YY[S] = 1;
                    ZZ[S] = w;
                    S++;
                }

            for (int ii = 0; ii < S; ii++)
            {

                X = XX[ii];
                Z = ZZ[ii];
                X0 = X;
                Z0 = Z;
                for (int i = 0; i <= 90000; i++)
                {
                    Ex = -k * kapaY * H0 / kapa2 * Math.Cos(kapaX * X) * Math.Sin(kapaY * Y) * Math.Cos(h * Z);
                    Ez = 0;
                    Y = YY[0];
                    Ey = k * kapaX * H0 / kapa2 * Math.Sin(kapaX * X) * Math.Cos(kapaY * Y) * Math.Cos(h * Z);


                    X1 = X + (float)Ex;
                    Y1 = Y + (float)Ey;
                    Z1 = Z + (float)Ez;

                    Graphics g = pictureBox2.CreateGraphics();
                    //g.DrawLine(new Pen(Brushes.Red, 1), new PointF(X, Z), new PointF(X1, Z1));

                    if (Y > Y1)
                    {
                        g.DrawEllipse(new Pen(Brushes.Red, 1), X - 3, Z - 3, 7, 7);
                        break;
                    }
                    else
                    {

                        g.DrawLine(new Pen(Brushes.Red, 1), new PointF(X - 4, Z - 4), new PointF(X + 4, Z + 4));
                        g.DrawLine(new Pen(Brushes.Red, 1), new PointF(X + 4, Z - 4), new PointF(X - 4, Z + 4));
                        break;
                    }

                    X = X1;
                    Z = Z1;
                    if (i > 100)
                        if (Math.Abs(X - X0) < 0.1 && Math.Abs(Z - Z0) < 0.1)
                            break;

                    //for (int v = 0; v < 10000000; v++) ;

                }
            }




        }//Норм !

        public int Proverka(int c)
        {
            int a, b;                                       //Параметры волновода (ширина, высота)
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            //pictureBox1.Width = a;
            //pictureBox1.Height = b;

            int m, n;
            m = Convert.ToInt32(textBox3.Text); //Мода горизонтали
            n = Convert.ToInt32(textBox4.Text); //Мода вертикали

            double kapaX = Math.PI * n / a;
            double kapaY = Math.PI * m / b;
            double kapa2 = kapaX * kapaX + kapaY * kapaY;
            double k;
            k = Convert.ToDouble(textBox5.Text);
            if (k * k - kapa2 < 0) c = 1;

            return c;
        }//Норм

    }
}