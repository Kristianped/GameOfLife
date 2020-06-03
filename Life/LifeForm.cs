using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Life
{
    public partial class LifeForm : Form
    {
        #region Variables
        LifeGrid life;
        int sqsize;
        bool isNullified, mouseDown;
        #endregion

        #region Constructor
        /// <summary>
        /// Makes a new life.
        /// </summary>
        /// <param name="n">The size of the cells.</param>
        public LifeForm(int n)
        {
            sqsize = n;
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Builds the grid.
        /// </summary>
        /// <param name="squareSize">Size of the cells.</param>
        private void buildLifeGrid(int squareSize)
        {
            Random random = new Random();
            int h = pictureBox1.Height/squareSize;
            int w = pictureBox1.Width/squareSize;
            int[,] grid=new int[h,w];

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    grid[y, x] = random.Next(0, 2);
                }
            }
            life = new LifeGrid(grid);
        }
        /// <summary>
        /// Clears all the cells in the grid.
        /// </summary>
        private void nullifyGrid()
        {
            isNullified = true;
            for (int y = 0; y < life.Height; y++)
            {
                for (int x = 0; x < life.Width; x++)
                {
                    life.Cells[y, x] = 0;
                }
            }
        }
        /// <summary>
        /// Rebuilds the grid.
        /// </summary>
        private void rebuildGrid()
        {
            isNullified = false;
            Random random = new Random();
            for (int y = 0; y < life.Height; y++)
            {
                for (int x = 0; x < life.Width; x++)
                {
                    life.Cells[y, x] = random.Next(0, 2);
                }
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            buildLifeGrid(sqsize);
            isNullified = false;
            mouseDown = false;
            button2_Click(sender, e);
        }

        /// <summary>
        /// Handles the click event of the start/stop-button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            startButton.Text = timer1.Enabled ? "Stop" : "Start";
        }

        /// <summary>
        /// Handles the tick event of the timer.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            life.processGeneration();
            pictureBox1.Invalidate();
            activeLabel.Text = life.aliveCells().ToString();
        }

        /// <summary>
        /// Handles the paint event of the picturebox that draws the cells.
        /// </summary>
        /// <param name="sender">The soure of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            double temp = sqsize * 0.8;
            int n = Convert.ToInt32(Math.Round(temp, 0));
            for (int y = 0; y < life.Height; y++)
            {
                for (int x = 0; x < life.Width; x++)
                {
                    if (life.Cells[y, x] > 0)
                        g.FillRectangle(new SolidBrush(Color.Black), x*sqsize, y*sqsize, n, n);
                    else
                        g.FillRectangle(new SolidBrush(Color.White), x*sqsize, y*sqsize, n, n);
                }
            }
        }

        /// <summary>
        /// Handles the event of the Clear button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Stop")
            {
                MessageBox.Show("Please stop the life first");
            }
            else if (startButton.Text == "Start")
            {
                nullifyGrid();
                timer1.Enabled = false;
                pictureBox1.Invalidate();
                activeLabel.Text = "";
            }
        }

        /// <summary>
        /// Handles the mouseDown event of the picturebox containing the life.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            mouseDown = true;
            life.Cells[e.Y/sqsize, e.X/sqsize] = 1;
        }

        /// <summary>
        /// Handles the mouseMove event of the picturebox containing the life.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0) return;
            if (e.Y < 0) return;
            if (e.X > pictureBox1.Width) return;
            if (e.Y > pictureBox1.Height) return;

            if (mouseDown)
            {
                try
                {
                    life.Cells[e.Y / sqsize, e.X / sqsize] = 1;
                    pictureBox1.Invalidate();
                }
                catch (IndexOutOfRangeException er1)
                { }
            }
        }

        /// <summary>
        /// Handles the mouseUp event of the picturebox containing the life.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;

            if (startButton.Text == "Stop")
                timer1.Enabled = true;
        }

        /// <summary>
        /// Handles the click event of the Random button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void randomButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Stop")
                MessageBox.Show("Please stop the life first");
            
            else if (startButton.Text == "Start")
            {
                timer1.Enabled = false;
                rebuildGrid();
                pictureBox1.Invalidate();
                activeLabel.Text = life.aliveCells().ToString();
            }
        }

        /// <summary>
        /// Handles the event of the radiobutton Slow.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Interval = 800;
        }

        /// <summary>
        /// Handles the event of the radiobutton Medium.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Interval = 400;
        }

        /// <summary>
        /// Handles the event of the radiobutton Fast.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Interval = 100;
        }

        /// <summary>
        /// Handles the event of the radiobutton Medium Slow.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Interval = 600;
        }

        /// <summary>
        /// Handles the event of the radiobutton Medium Fast.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Interval = 200;
        }

        /// <summary>
        /// Handles the click event of the Exit button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
