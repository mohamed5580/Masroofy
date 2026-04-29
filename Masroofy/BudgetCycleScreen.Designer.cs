namespace Masroofy
{
    partial class BudgetCycleScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dgw = new DataGridView();
            panel1 = new Panel();
            amount = new Label();
            label1 = new Label();
            ID = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            IsActive = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgw).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgw
            // 
            dgw.AllowUserToAddRows = false;
            dgw.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FloralWhite;
            dgw.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgw.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgw.BackgroundColor = Color.White;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.DarkViolet;
            dataGridViewCellStyle2.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 178);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.MediumOrchid;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgw.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgw.ColumnHeadersHeight = 40;
            dgw.Columns.AddRange(new DataGridViewColumn[] { ID, Column5, Column3, Column4, IsActive });
            dgw.Cursor = Cursors.Hand;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = Color.MediumOrchid;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgw.DefaultCellStyle = dataGridViewCellStyle4;
            dgw.EnableHeadersVisualStyles = false;
            dgw.GridColor = Color.Gray;
            dgw.Location = new Point(28, 92);
            dgw.Margin = new Padding(4, 5, 4, 5);
            dgw.MultiSelect = false;
            dgw.Name = "dgw";
            dgw.ReadOnly = true;
            dgw.RightToLeft = RightToLeft.No;
            dgw.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.Fuchsia;
            dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = Color.Magenta;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgw.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgw.RowHeadersWidth = 25;
            dgw.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.SelectionBackColor = Color.Violet;
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dgw.RowsDefaultCellStyle = dataGridViewCellStyle6;
            dgw.RowTemplate.Height = 30;
            dgw.RowTemplate.Resizable = DataGridViewTriState.False;
            dgw.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgw.Size = new Size(854, 365);
            dgw.TabIndex = 5;
            dgw.MouseDoubleClick += dgw_MouseDoubleClick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(amount);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dgw);
            panel1.Location = new Point(14, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(908, 481);
            panel1.TabIndex = 6;
            // 
            // amount
            // 
            amount.AutoSize = true;
            amount.BackColor = Color.MediumPurple;
            amount.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            amount.ForeColor = Color.White;
            amount.Location = new Point(270, 29);
            amount.Name = "amount";
            amount.Padding = new Padding(5);
            amount.Size = new Size(10, 33);
            amount.TabIndex = 7;
            amount.Click += amount_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.BlueViolet;
            label1.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(28, 29);
            label1.Name = "label1";
            label1.Padding = new Padding(5);
            label1.Size = new Size(221, 33);
            label1.TabIndex = 6;
            label1.Text = "Total Budget Cycle : ";
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.MinimumWidth = 6;
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.HeaderText = "Total Allowance";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            Column3.DefaultCellStyle = dataGridViewCellStyle3;
            Column3.HeaderText = "Start Date";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 166;
            // 
            // Column4
            // 
            Column4.HeaderText = "End Date";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // IsActive
            // 
            IsActive.HeaderText = "IsActive";
            IsActive.MinimumWidth = 6;
            IsActive.Name = "IsActive";
            IsActive.ReadOnly = true;
            // 
            // BudgetCycleScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkViolet;
            ClientSize = new Size(934, 515);
            Controls.Add(panel1);
            Name = "BudgetCycleScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Budget Cycle Screen";
            Load += BudgetCycleScreen_Load;
            ((System.ComponentModel.ISupportInitialize)dgw).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        internal DataGridView dgw;
        private Panel panel1;
        private Label label1;
        private Label amount;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn IsActive;
        private DataGridViewTextBoxColumn ID;
    }
}