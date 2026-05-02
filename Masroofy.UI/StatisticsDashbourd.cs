using System;
using System.Drawing;
using System.Windows.Forms;

namespace Masroofy
{
    public partial class StatisticsDashbourd : Form
    {
        public StatisticsDashbourd()
        {
            // This calls the code in the Designer file to build the UI
            InitializeComponent();
        }

        private void pnlPieChart_Paint(object sender, PaintEventArgs e)
        {
            // Use AntiAlias to make the circles look smooth
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Define the area for our pie chart[cite: 21]
            Rectangle rect = new Rectangle(10, 10, pnlPieChart.Width - 20, pnlPieChart.Height - 20);

            // Draw the slices (Start Angle, Sweep Angle)[cite: 21]
            e.Graphics.FillPie(Brushes.LimeGreen, rect, 0, 150); // Food[cite: 21]
            e.Graphics.FillPie(Brushes.DodgerBlue, rect, 150, 120); // Trans[cite: 21]
            e.Graphics.FillPie(Brushes.MediumPurple, rect, 270, 90); // Other[cite: 21]

            e.Graphics.DrawEllipse(Pens.Black, rect);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Opens the expense entry pop-up[cite: 21]
            ExpenseEntryScreen entryScreen = new ExpenseEntryScreen(null, 0);
            entryScreen.ShowDialog();
        }
    }
}