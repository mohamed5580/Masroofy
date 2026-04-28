namespace Masroofy
{
    partial class TrackingIncomeScreen
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            dgvIncomes = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            amount = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvIncomes).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvIncomes
            // 
            dgvIncomes.AllowUserToAddRows = false;
            dgvIncomes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = Color.FloralWhite;
            dgvIncomes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dgvIncomes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvIncomes.BackgroundColor = Color.White;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = Color.DarkViolet;
            dataGridViewCellStyle8.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, 178);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = Color.MediumOrchid;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dgvIncomes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dgvIncomes.ColumnHeadersHeight = 40;
            dgvIncomes.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column5, Column3, Column4 });
            dgvIncomes.Cursor = Cursors.Hand;
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = SystemColors.Window;
            dataGridViewCellStyle10.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle10.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = Color.MediumOrchid;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            dgvIncomes.DefaultCellStyle = dataGridViewCellStyle10;
            dgvIncomes.EnableHeadersVisualStyles = false;
            dgvIncomes.GridColor = Color.Gray;
            dgvIncomes.Location = new Point(28, 92);
            dgvIncomes.Margin = new Padding(4, 5, 4, 5);
            dgvIncomes.MultiSelect = false;
            dgvIncomes.Name = "dgvIncomes";
            dgvIncomes.ReadOnly = true;
            dgvIncomes.RightToLeft = RightToLeft.No;
            dgvIncomes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = Color.Fuchsia;
            dataGridViewCellStyle11.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle11.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = Color.Magenta;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
            dgvIncomes.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            dgvIncomes.RowHeadersWidth = 25;
            dgvIncomes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.BackColor = Color.White;
            dataGridViewCellStyle12.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle12.SelectionBackColor = Color.Violet;
            dataGridViewCellStyle12.SelectionForeColor = Color.White;
            dgvIncomes.RowsDefaultCellStyle = dataGridViewCellStyle12;
            dgvIncomes.RowTemplate.Height = 30;
            dgvIncomes.RowTemplate.Resizable = DataGridViewTriState.False;
            dgvIncomes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIncomes.Size = new Size(854, 365);
            dgvIncomes.TabIndex = 5;
            dgvIncomes.MouseDoubleClick += dgw_MouseDoubleClick;
            // 
            // Column1
            // 
            Column1.HeaderText = "ID";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Visible = false;
            // 
            // Column2
            // 
            Column2.HeaderText = "Income Code";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.HeaderText = "Income Source";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            Column3.DefaultCellStyle = dataGridViewCellStyle9;
            Column3.HeaderText = "Income Amount";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "Income Date";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(amount);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dgvIncomes);
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
            label1.Size = new Size(242, 33);
            label1.TabIndex = 6;
            label1.Text = "Total Income Amount : ";
            // 
            // TrackingIncomeScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkViolet;
            ClientSize = new Size(934, 515);
            Controls.Add(panel1);
            Name = "TrackingIncomeScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TrackingIncomeScreen";
            Load += TrackingIncomeScreen_Load;
            ((System.ComponentModel.ISupportInitialize)dgvIncomes).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        internal DataGridView dgvIncomes;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private Panel panel1;
        private Label label1;
        private Label amount;
    }
}