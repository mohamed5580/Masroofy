namespace Masroofy
{
    partial class StatisticsDashbourd
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel4 = new Panel();
            panel2 = new Panel();
            lblSafeDailyLimit = new Label();
            label1 = new Label();
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
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();

            // panel1[cite: 21]
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Location = new Point(-1, -3);
            panel1.Name = "panel1";
            panel1.Size = new Size(491, 208);
            panel1.TabIndex = 0;

            // panel4[cite: 21]
            panel4.BackColor = Color.DarkViolet;
            panel4.Controls.Add(panel2);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(button1);
            panel4.Location = new Point(-11, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(502, 234);
            panel4.TabIndex = 3;

            // panel2[cite: 21]
            panel2.BackColor = Color.GhostWhite;
            panel2.Controls.Add(lblSafeDailyLimit);
            panel2.Location = new Point(141, 37);
            panel2.Name = "panel2";
            panel2.Size = new Size(237, 105);
            panel2.TabIndex = 0;

            // lblSafeDailyLimit[cite: 21]
            lblSafeDailyLimit.AutoSize = true;
            lblSafeDailyLimit.Location = new Point(94, 43);
            lblSafeDailyLimit.Name = "lblSafeDailyLimit";
            lblSafeDailyLimit.Size = new Size(50, 20);
            lblSafeDailyLimit.TabIndex = 0;
            lblSafeDailyLimit.Text = "label2";

            // label1[cite: 21]
            label1.AutoSize = true;
            label1.BackColor = Color.GhostWhite;
            label1.Location = new Point(215, 14);
            label1.Name = "label1";
            label1.Size = new Size(92, 20);
            label1.TabIndex = 1;
            label1.Text = "Today's limit";

            // button1[cite: 21]
            button1.Location = new Point(202, 159);
            button1.Name = "button1";
            button1.Size = new Size(120, 29);
            button1.TabIndex = 2;
            button1.Text = "Log Expense";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;

            // panel3[cite: 21]
            panel3.BackColor = Color.Black;
            panel3.Location = new Point(-14, 197);
            panel3.Name = "panel3";
            panel3.Size = new Size(505, 11);
            panel3.TabIndex = 1;

            // pnlPieChart[cite: 21]
            pnlPieChart.Location = new Point(-1, 197);
            pnlPieChart.Name = "pnlPieChart";
            pnlPieChart.Size = new Size(491, 341);
            pnlPieChart.TabIndex = 1;
            pnlPieChart.Paint += pnlPieChart_Paint;

            // tableLayoutPanel1[cite: 21]
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.3846169F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.6153831F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.Controls.Add(button2, 0, 0);
            tableLayoutPanel1.Controls.Add(button3, 1, 0);
            tableLayoutPanel1.Controls.Add(button4, 2, 0);
            tableLayoutPanel1.Controls.Add(button5, 3, 0);
            tableLayoutPanel1.Location = new Point(47, 541);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.Size = new Size(386, 52);
            tableLayoutPanel1.TabIndex = 2;

            // Navigation Buttons[cite: 21]
            button2.Text = "Dashbourd";
            button3.Text = "Stats";
            button4.Text = "History";
            button5.Text = "Settings";

            // panel5[cite: 21]
            panel5.BackColor = Color.DarkViolet;
            panel5.Location = new Point(-1, 647);
            panel5.Name = "panel5";
            panel5.Size = new Size(495, 137);
            panel5.TabIndex = 3;

            // StatisticsDashbourd[cite: 21]
            ClientSize = new Size(489, 785);
            Controls.Add(panel5);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pnlPieChart);
            Controls.Add(panel1);
            Name = "StatisticsDashbourd";
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Button button1;
        private Label label1;
        private Panel panel2;
        private Label lblSafeDailyLimit;
        private Panel pnlPieChart;
        private TableLayoutPanel tableLayoutPanel1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Panel panel4;
        private Panel panel5;
    }
}