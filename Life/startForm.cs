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
    public partial class startForm : Form
    {
        #region Variables
        LifeForm main;
        int n;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the startForm that just initializes the components.
        /// </summary>
        public startForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Closes the startForm and starts the lifeForm.
        /// </summary>
        public void startLifeForm()
        {
            main = new LifeForm(n);
            main.StartPosition = FormStartPosition.CenterScreen;
            this.Hide();
            main.ShowDialog();
            this.Close();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the click event of the Small button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            n = 4;
            startLifeForm();
        }

        /// <summary>
        /// Handles the click event of the Medium Small button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            n = 6;
            startLifeForm();
        }

        /// <summary>
        /// Handles the click event of the Medium button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button5_Click(object sender, EventArgs e)
        {
            n = 8;
            startLifeForm();
        }

        /// <summary>
        /// Handles the click event of the Medium Big button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            n = 10;
            startLifeForm();
        }

        /// <summary>
        /// Handles the click event of the Big button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            n = 12;
            startLifeForm();
        }
        #endregion
    }
}
