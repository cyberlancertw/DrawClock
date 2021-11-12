using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ClockExercise2
{
    public partial class Form1 : Form
    {
        int padding;
        int widthCenter, heightCenter, clockWidth;
        double minRad, minCos, minSin;
        int startR, endR, start5R;
        int nowHr, nowMin = 45, nowSec, nowMilliSec;



        //public delegate void MyInvoke(Object obj, Pen pen);


        public Form1()
        {
            InitializeComponent();

            padding = 30;
            clockWidth = this.Size.Width - padding * 2;
            
            widthCenter = this.Size.Width / 2;
            heightCenter = this.Size.Height / 2;
            startR = clockWidth/2 - 30;
            endR = clockWidth/2 - 15;
            start5R = clockWidth/2 - 40;
            minRad = Math.PI * 6 / 180;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            clockWidth = this.Size.Width - padding * 2;
            widthCenter = this.Size.Width / 2;
            heightCenter = this.Size.Height / 2;
            nowHr = DateTime.Now.Hour;
            nowMin = DateTime.Now.Minute;
            nowSec = DateTime.Now.Second;
            nowMilliSec = DateTime.Now.Millisecond;
            label1.Location = new Point(widthCenter - label1.Size.Width/2 , this.Size.Height - label1.Size.Height/2 - 100);
            label1.Text=$"{nowHr}時{nowMin}分{nowSec}秒";
            this.Refresh();
            //panel1.Refresh();
            //Thread thread = new Thread(new ThreadStart(drawMinuteTikToc));
            //thread.Start();
            //this.Invalidate();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            drawClockDisc(e.Graphics, new Pen(Color.Black, 1));
            drawHourTikToc(e.Graphics, new Pen(Color.Brown, 9));
            drawMinuteTikToc(e.Graphics, new Pen(Color.Orange, 8));
            drawSecondTikToc(e.Graphics, new Pen(Color.DarkSlateBlue, 4));

        }

        void drawClockDisc(Graphics g, Pen blackPen)
        {
            
            g.DrawEllipse(blackPen, padding, padding, clockWidth, clockWidth);
            g.DrawEllipse(blackPen, padding + 5, padding + 5, this.Size.Width - padding * 2 - 10, this.Size.Width - padding * 2 - 10);
            g.DrawEllipse(blackPen, widthCenter - 5, widthCenter - 5, 10, 10);
            for (int i = 0; i < 60; i++)
            {
                minCos = Math.Cos(minRad * i);
                minSin = Math.Sin(minRad * i);

                if (i % 5 == 0)
                {
                    g.DrawLine(blackPen, 
                        (int)(start5R * minCos) + widthCenter,
                        (int)(start5R * minSin) + widthCenter,
                        (int)(endR * minCos) + widthCenter,
                        (int)(endR * minSin) + widthCenter);
                }
                else
                {
                    g.DrawLine(blackPen, 
                        (int)(startR * minCos) + widthCenter,
                        (int)(startR * minSin) + widthCenter,
                        (int)(endR * minCos) + widthCenter,
                        (int)(endR * minSin) + widthCenter);
                }
            }
        }
        void drawHourTikToc(Graphics g, Pen hourPen)
        {
            
            double hourRad = (nowHr + nowMin / 60f) * 30 * Math.PI / 180f - Math.PI / 2;
            double hourCos = Math.Cos(hourRad);
            double hourSin = Math.Sin(hourRad);
            g.DrawLine(hourPen, 
                (int)(5 * hourCos) + widthCenter,
                (int)(5 * hourSin) + widthCenter,
                (int)(120 * hourCos) + widthCenter,
                (int)(120 * hourSin) + widthCenter);
        }

        void drawMinuteTikToc(Graphics g, Pen minutePen)
        {
            
            double minuteRad = (nowMin + nowSec / 60f) * 6 * Math.PI / 180f - Math.PI / 2;
            double minuteCos = Math.Cos(minuteRad);
            double minuteSin = Math.Sin(minuteRad);
            g.DrawLine(minutePen, 
                (int)(5 * minuteCos) + widthCenter,
                (int)(5 * minuteSin) + widthCenter,
                (int)(180 * minuteCos) + widthCenter,
                (int)(180 * minuteSin) + widthCenter);

            //MyInvoke mi = new MyInvoke(g, minutePen);
            //this.BeginInvoke(mi);
        }

        void drawSecondTikToc(Graphics g, Pen secondPen)
        {
            
            double secondRad = (nowSec + nowMilliSec / 1000f) * 6 * Math.PI / 180f - Math.PI / 2;
            double secondCos = Math.Cos(secondRad);
            double secondSin = Math.Sin(secondRad);
            g.DrawLine(secondPen, 
                (int)(5 * secondCos) + widthCenter,
                (int)(5 * secondSin) + widthCenter,
                (int)(200 * secondCos) + widthCenter,
                (int)(200 * secondSin) + widthCenter);
            g.DrawLine(secondPen, 
                (int)(5 * secondCos) + widthCenter,
                (int)(5 * secondSin) + widthCenter,
                (int)(200 * secondCos) + widthCenter,
                (int)(200 * secondSin) + widthCenter);

            
            double minuteFullRad = nowSec * 6 * Math.PI / 180f - Math.PI / 2;
            double minuteFullCos = Math.Cos(minuteFullRad);
            double minuteFullSin = Math.Sin(minuteFullRad);
            if (nowSec % 5 == 0)
            {
                g.DrawLine(new Pen(Color.DarkGreen, 3),
                    (int)(start5R * minuteFullCos) + widthCenter,
                    (int)(start5R * minuteFullSin) + widthCenter,
                    (int)(endR * minuteFullCos) + widthCenter,
                    (int)(endR * minuteFullSin) + widthCenter);
            }
            else
            {
                g.DrawLine(new Pen(Color.DarkGreen, 3),
                    (int)(startR * minuteFullCos) + widthCenter,
                    (int)(startR * minuteFullSin) + widthCenter,
                    (int)(endR * minuteFullCos) + widthCenter,
                    (int)(endR * minuteFullSin) + widthCenter);
            }
        }
    }
}
