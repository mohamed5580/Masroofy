


namespace Masroofy
{
    partial class BudgetCycleForm
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
            components = new System.ComponentModel.Container();
            Panel3 = new Panel();
            button1 = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnSave = new Button();
            btnNew = new Button();
            EndDate = new DateTimePicker();
            txtID = new TextBox();
            txtCID = new TextBox();
            Label1 = new Label();
            Panel2 = new Panel();
            ToolTip1 = new ToolTip(components);
            Panel1 = new Panel();
            GroupBox4 = new GroupBox();
            StartDate = new DateTimePicker();
            txtAmount = new TextBox();
            Label12 = new Label();
            Label10 = new Label();
            Label6 = new Label();
            Timer1 = new System.Windows.Forms.Timer(components);
            Panel3.SuspendLayout();
            Panel2.SuspendLayout();
            Panel1.SuspendLayout();
            GroupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // Panel3
            // 
            Panel3.BorderStyle = BorderStyle.FixedSingle;
            Panel3.Controls.Add(button1);
            Panel3.Controls.Add(btnDelete);
            Panel3.Controls.Add(btnUpdate);
            Panel3.Controls.Add(btnSave);
            Panel3.Controls.Add(btnNew);
            Panel3.Location = new Point(625, 105);
            Panel3.Margin = new Padding(4, 5, 4, 5);
            Panel3.Name = "Panel3";
            Panel3.Size = new Size(162, 282);
            Panel3.TabIndex = 3;
            // 
            // button1
            // 
            button1.BackColor = Color.Gold;
            button1.Cursor = Cursors.Hand;
            button1.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(4, 214);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(140, 42);
            button1.TabIndex = 5;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Firebrick;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(4, 162);
            btnDelete.Margin = new Padding(4, 5, 4, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(140, 42);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.SkyBlue;
            btnUpdate.Cursor = Cursors.Hand;
            btnUpdate.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnUpdate.Location = new Point(4, 114);
            btnUpdate.Margin = new Padding(4, 5, 4, 5);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(140, 42);
            btnUpdate.TabIndex = 2;
            btnUpdate.Text = "Edit";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnSave
            // 
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnSave.Location = new Point(4, 62);
            btnSave.Margin = new Padding(4, 5, 4, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 42);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnNew
            // 
            btnNew.BackColor = Color.ForestGreen;
            btnNew.Cursor = Cursors.Hand;
            btnNew.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            btnNew.ForeColor = Color.White;
            btnNew.Location = new Point(4, 10);
            btnNew.Margin = new Padding(4, 5, 4, 5);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(140, 42);
            btnNew.TabIndex = 0;
            btnNew.Text = "New";
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            // 
            // EndDate
            // 
            EndDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            EndDate.CustomFormat = "dd/MM/yyyy";
            EndDate.DropDownAlign = LeftRightAlignment.Right;
            EndDate.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            EndDate.Format = DateTimePickerFormat.Custom;
            EndDate.Location = new Point(236, 190);
            EndDate.Margin = new Padding(3, 2, 3, 2);
            EndDate.Name = "EndDate";
            EndDate.Size = new Size(337, 30);
            EndDate.TabIndex = 7;
            EndDate.ValueChanged += EndDate_ValueChanged;
            // 
            // txtID
            // 
            txtID.BackColor = SystemColors.Control;
            txtID.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtID.Location = new Point(11, 41);
            txtID.Margin = new Padding(4, 5, 4, 5);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(92, 24);
            txtID.TabIndex = 12;
            txtID.Visible = false;
            txtID.TextChanged += txtID_TextChanged;
            // 
            // txtCID
            // 
            txtCID.BackColor = SystemColors.Control;
            txtCID.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCID.Location = new Point(11, 10);
            txtCID.Margin = new Padding(4, 5, 4, 5);
            txtCID.Name = "txtCID";
            txtCID.ReadOnly = true;
            txtCID.Size = new Size(92, 24);
            txtCID.TabIndex = 7;
            txtCID.Visible = false;
            // 
            // Label1
            // 
            Label1.AutoSize = true;
            Label1.BackColor = Color.Transparent;
            Label1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            Label1.ForeColor = Color.White;
            Label1.Location = new Point(193, 20);
            Label1.Margin = new Padding(4, 0, 4, 0);
            Label1.Name = "Label1";
            Label1.Size = new Size(331, 36);
            Label1.TabIndex = 0;
            Label1.Text = "Initialize Budget Cycle";
            // 
            // Panel2
            // 
            Panel2.BackColor = Color.DarkViolet;
            Panel2.BackgroundImageLayout = ImageLayout.Stretch;
            Panel2.Controls.Add(txtID);
            Panel2.Controls.Add(txtCID);
            Panel2.Controls.Add(Label1);
            Panel2.Location = new Point(12, 11);
            Panel2.Margin = new Padding(4, 5, 4, 5);
            Panel2.Name = "Panel2";
            Panel2.Size = new Size(781, 73);
            Panel2.TabIndex = 0;
            // 
            // Panel1
            // 
            Panel1.BackColor = Color.White;
            Panel1.BorderStyle = BorderStyle.FixedSingle;
            Panel1.Controls.Add(GroupBox4);
            Panel1.Controls.Add(Panel3);
            Panel1.Controls.Add(Panel2);
            Panel1.Location = new Point(5, 8);
            Panel1.Margin = new Padding(4, 5, 4, 5);
            Panel1.Name = "Panel1";
            Panel1.Size = new Size(814, 440);
            Panel1.TabIndex = 3;
            // 
            // GroupBox4
            // 
            GroupBox4.BackColor = Color.WhiteSmoke;
            GroupBox4.Controls.Add(StartDate);
            GroupBox4.Controls.Add(txtAmount);
            GroupBox4.Controls.Add(EndDate);
            GroupBox4.Controls.Add(Label12);
            GroupBox4.Controls.Add(Label10);
            GroupBox4.Controls.Add(Label6);
            GroupBox4.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            GroupBox4.Location = new Point(12, 94);
            GroupBox4.Margin = new Padding(4, 5, 4, 5);
            GroupBox4.Name = "GroupBox4";
            GroupBox4.Padding = new Padding(4, 5, 4, 5);
            GroupBox4.RightToLeft = RightToLeft.No;
            GroupBox4.Size = new Size(592, 286);
            GroupBox4.TabIndex = 0;
            GroupBox4.TabStop = false;
            GroupBox4.Text = "Detiles";
            // 
            // StartDate
            // 
            StartDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            StartDate.CustomFormat = "dd/MM/yyyy";
            StartDate.DropDownAlign = LeftRightAlignment.Right;
            StartDate.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            StartDate.Format = DateTimePickerFormat.Custom;
            StartDate.Location = new Point(236, 133);
            StartDate.Margin = new Padding(3, 2, 3, 2);
            StartDate.Name = "StartDate";
            StartDate.Size = new Size(337, 30);
            StartDate.TabIndex = 343;
            // 
            // txtAmount
            // 
            txtAmount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtAmount.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            txtAmount.Location = new Point(236, 86);
            txtAmount.Margin = new Padding(4, 5, 4, 5);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(337, 30);
            txtAmount.TabIndex = 342;
            // 
            // Label12
            // 
            Label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label12.BackColor = Color.DarkViolet;
            Label12.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            Label12.ForeColor = Color.White;
            Label12.Location = new Point(11, 190);
            Label12.Name = "Label12";
            Label12.Size = new Size(225, 30);
            Label12.TabIndex = 341;
            Label12.Text = "End Date :";
            Label12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label10
            // 
            Label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label10.BackColor = Color.DarkViolet;
            Label10.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            Label10.ForeColor = Color.White;
            Label10.Location = new Point(11, 133);
            Label10.Name = "Label10";
            Label10.Size = new Size(225, 30);
            Label10.TabIndex = 339;
            Label10.Text = "Start Date :";
            Label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Label6
            // 
            Label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Label6.BackColor = Color.DarkViolet;
            Label6.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            Label6.ForeColor = Color.White;
            Label6.Location = new Point(12, 86);
            Label6.Name = "Label6";
            Label6.Size = new Size(225, 30);
            Label6.TabIndex = 336;
            Label6.Text = "Total Allowance : ";
            Label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BudgetCycleForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkViolet;
            ClientSize = new Size(828, 455);
            Controls.Add(Panel1);
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "BudgetCycleForm";
            RightToLeft = RightToLeft.Yes;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += BudgetCycleForm_Load;
            Panel3.ResumeLayout(false);
            Panel2.ResumeLayout(false);
            Panel2.PerformLayout();
            Panel1.ResumeLayout(false);
            GroupBox4.ResumeLayout(false);
            GroupBox4.PerformLayout();
            ResumeLayout(false);

        }

        private void txtIncomeCode_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
        }

        #endregion

        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.Button btnUpdate;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnNew;
        internal System.Windows.Forms.DateTimePicker EndDate;
        internal System.Windows.Forms.TextBox txtID;
        internal System.Windows.Forms.TextBox txtCID;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Timer Timer1;
        internal Label Label12;
        internal TextBox txtAmount;
        internal Button button1;
        internal DateTimePicker StartDate;
    }
}