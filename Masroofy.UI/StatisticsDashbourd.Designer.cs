namespace Masroofy
{
    partial class StatisticsDashbourd
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel4 = new Panel();
            pnlLimitCircle = new Panel();
            button1 = new Button();
            panel3 = new Panel();
            pnlPieChart = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            panel5 = new Panel();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Location = new Point(-1, -3);
            panel1.Name = "panel1";
            panel1.Size = new Size(491, 208);
            panel1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.BackColor = Color.DarkViolet;
            panel4.Controls.Add(pnlLimitCircle);
            panel4.Controls.Add(button1);
            panel4.Location = new Point(-11, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(502, 234);
            panel4.TabIndex = 3;
            // 
            // pnlLimitCircle
            // 
            pnlLimitCircle.BackColor = Color.Transparent;
            pnlLimitCircle.Location = new Point(171, 20);
            pnlLimitCircle.Name = "pnlLimitCircle";
            pnlLimitCircle.Size = new Size(160, 160);
            pnlLimitCircle.TabIndex = 0;
            pnlLimitCircle.Paint += pnlLimitCircle_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(367, 96);
            button1.Name = "button1";
            button1.Size = new Size(120, 29);
            button1.TabIndex = 2;
            button1.Text = "Log Expense";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Black;
            panel3.Location = new Point(-14, 197);
            panel3.Name = "panel3";
            panel3.Size = new Size(505, 11);
            panel3.TabIndex = 1;
            // 
            // pnlPieChart
            // 
            pnlPieChart.Location = new Point(-1, 197);
            pnlPieChart.Name = "pnlPieChart";
            pnlPieChart.Size = new Size(491, 341);
            pnlPieChart.TabIndex = 1;
            pnlPieChart.Paint += pnlPieChart_Paint;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.38F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.62F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.Controls.Add(button2, 0, 0);
            tableLayoutPanel1.Controls.Add(button3, 1, 0);
            tableLayoutPanel1.Controls.Add(button4, 2, 0);
            tableLayoutPanel1.Controls.Add(button5, 3, 0);
            tableLayoutPanel1.Location = new Point(47, 541);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(386, 52);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(3, 3);
            button2.Name = "button2";
            button2.Size = new Size(102, 46);
            button2.TabIndex = 0;
            button2.Text = "Dashbourd";
            // 
            // button3
            // 
            button3.Dock = DockStyle.Fill;
            button3.Location = new Point(111, 3);
            button3.Name = "button3";
            button3.Size = new Size(81, 46);
            button3.TabIndex = 1;
            button3.Text = "Stats";
            // 
            // button4
            // 
            button4.Dock = DockStyle.Fill;
            button4.Location = new Point(198, 3);
            button4.Name = "button4";
            button4.Size = new Size(104, 46);
            button4.TabIndex = 2;
            button4.Text = "History";
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Dock = DockStyle.Fill;
            button5.Location = new Point(308, 3);
            button5.Name = "button5";
            button5.Size = new Size(75, 46);
            button5.TabIndex = 3;
            button5.Text = "Settings";
            // 
            // panel5
            // 
            panel5.BackColor = Color.DarkViolet;
            panel5.Location = new Point(-1, 647);
            panel5.Name = "panel5";
            panel5.Size = new Size(495, 137);
            panel5.TabIndex = 3;
            // 
            // StatisticsDashbourd
            // 
            ClientSize = new Size(489, 785);
            Controls.Add(panel5);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pnlPieChart);
            Controls.Add(panel1);
            Name = "StatisticsDashbourd";
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel           panel1;
        private Panel           panel3;
        private Panel           panel4;
        private Panel           panel5;
        private Panel           pnlLimitCircle;   // custom-painted double circle
        private Panel           pnlPieChart;
        private Button          button1;
        private Button          button2;
        private Button          button3;
        private Button          button4;
        private Button          button5;
        private TableLayoutPanel tableLayoutPanel1;

        // Removed: panel2, lblSafeDailyLimit, label1
        // These are replaced by pnlLimitCircle which paints everything itself.
    }
}