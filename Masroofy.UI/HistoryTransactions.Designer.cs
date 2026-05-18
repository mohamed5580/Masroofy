
namespace Masroofy.UI
{
    partial class HistoryTransactions
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            panel1 = new Panel();
            dateTimePicker1 = new DateTimePicker();
            Date = new Label();
            CategoryTxt = new TextBox();
            label2 = new Label();
            btnDel = new Button();
            btnEdit = new Button();
            label1 = new Label();
            dgw = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            CycleId = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgw).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(Date);
            panel1.Controls.Add(CategoryTxt);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnDel);
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dgw);
            panel1.Location = new Point(13, 17);
            panel1.Name = "panel1";
            panel1.Size = new Size(907, 481);
            panel1.TabIndex = 7;
            panel1.Paint += panel1_Paint;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(699, 37);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(182, 27);
            dateTimePicker1.TabIndex = 13;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // Date
            // 
            Date.AutoSize = true;
            Date.BackColor = Color.BlueViolet;
            Date.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Date.ForeColor = Color.White;
            Date.Location = new Point(634, 29);
            Date.Name = "Date";
            Date.Padding = new Padding(5);
            Date.Size = new Size(63, 35);
            Date.TabIndex = 12;
            Date.Text = "Date";
            Date.Click += Date_Click;
            // 
            // CategoryTxt
            // 
            CategoryTxt.Location = new Point(476, 37);
            CategoryTxt.Name = "CategoryTxt";
            CategoryTxt.Size = new Size(152, 27);
            CategoryTxt.TabIndex = 11;
            CategoryTxt.TextChanged += CategoryTxt_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.BlueViolet;
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(374, 29);
            label2.Name = "label2";
            label2.Padding = new Padding(5);
            label2.Size = new Size(102, 35);
            label2.TabIndex = 10;
            label2.Text = "Category";
            label2.Click += label2_Click;
            // 
            // btnDel
            // 
            btnDel.BackColor = Color.OrangeRed;
            btnDel.Location = new Point(729, 447);
            btnDel.Margin = new Padding(3, 4, 3, 4);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(86, 31);
            btnDel.TabIndex = 9;
            btnDel.Text = "Delete";
            btnDel.UseVisualStyleBackColor = false;
            btnDel.Click += btnDel_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.Green;
            btnEdit.Location = new Point(621, 447);
            btnEdit.Margin = new Padding(3, 4, 3, 4);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(86, 31);
            btnEdit.TabIndex = 8;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.BlueViolet;
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(27, 29);
            label1.Name = "label1";
            label1.Padding = new Padding(5);
            label1.Size = new Size(190, 35);
            label1.TabIndex = 6;
            label1.Text = "Transaction History";
            label1.Click += label1_Click;
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
            dgw.Columns.AddRange(new DataGridViewColumn[] { ID, Column1, Column5, Column3, CycleId });
            dgw.Cursor = Cursors.Hand;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.MediumOrchid;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgw.DefaultCellStyle = dataGridViewCellStyle3;
            dgw.EnableHeadersVisualStyles = false;
            dgw.GridColor = Color.Gray;
            dgw.Location = new Point(27, 72);
            dgw.Margin = new Padding(5);
            dgw.MultiSelect = false;
            dgw.Name = "dgw";
            dgw.ReadOnly = true;
            dgw.RightToLeft = RightToLeft.No;
            dgw.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.Fuchsia;
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = Color.Magenta;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgw.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgw.RowHeadersWidth = 25;
            dgw.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.SelectionBackColor = Color.Violet;
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dgw.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgw.RowTemplate.Height = 30;
            dgw.RowTemplate.Resizable = DataGridViewTriState.False;
            dgw.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgw.Size = new Size(854, 365);
            dgw.TabIndex = 5;
            dgw.CellContentClick += dgw_CellContentClick;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.MinimumWidth = 6;
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.HeaderText = "Amount";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.HeaderText = "Category";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "Date";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // CycleId
            // 
            CycleId.HeaderText = "Cycle";
            CycleId.MinimumWidth = 6;
            CycleId.Name = "CycleId";
            CycleId.ReadOnly = true;
            CycleId.Visible = false;
            // 
            // Transactions
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkViolet;
            ClientSize = new Size(934, 515);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Transactions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Transactions";
            Load += Transactions_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgw).EndInit();
            ResumeLayout(false);
        }

        private void Date_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        internal DataGridView dgw;
        private Button btnDel;
        private Button btnEdit;
        private Label label2;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn CycleId;
        private TextBox CategoryTxt;
        private Label Date;
        private DateTimePicker dateTimePicker1;
    }
}