using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircleAnimationApp
{
    public partial class Form1 : Form
    {
        private int smallCircleDiameter = 80;
        private const int LargeCircleDiameter = 160;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.DoubleBuffered = true;
            this.ClientSize = new Size(300, 300);
            this.BackColor = Color.Pink;
            this.Paint += new PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new MouseEventHandler(this.Form1_MouseDown);

            animationTimer = new Timer();
            animationTimer.Interval = 1000; // 1 second
            animationTimer.Tick += new EventHandler(this.AnimationTimer_Tick);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int centerX = 150;
            int centerY = 150;

            // Draw large circle
            g.FillEllipse(Brushes.Blue, centerX - LargeCircleDiameter / 2, centerY - LargeCircleDiameter / 2, LargeCircleDiameter, LargeCircleDiameter);

            // Draw small circle
            g.FillEllipse(Brushes.Red, centerX - smallCircleDiameter / 2, centerY - smallCircleDiameter / 2, smallCircleDiameter, smallCircleDiameter);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Start the animation
                animationTimer.Start();
            }
            else if (e.Button == MouseButtons.Left && ModifierKeys.HasFlag(Keys.Shift))
            {
                // Stop the animation and reset the figure
                animationTimer.Stop();
                smallCircleDiameter = 80;
                this.Invalidate(); // Force a repaint
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            smallCircleDiameter += 5;
            if (smallCircleDiameter >= LargeCircleDiameter)
            {
                animationTimer.Stop();
            }
            this.Invalidate(); // Force a repaint
        }
    }
}