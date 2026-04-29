namespace Masroofy
{
    partial class ExpenseEntryScreen
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

        private void InitializeComponent()
        {
            txtAmountInput = new TextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnFood = new Button();
            btnTransport = new Button();
            btnEntertainment = new Button();
            btnUtilities = new Button();
            btnOther = new Button();
            btnConfirm = new Button();
            btnCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            panel4 = new Panel();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtAmountInput
            // 
            txtAmountInput.BackColor = Color.White;
            txtAmountInput.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtAmountInput.ForeColor = Color.LimeGreen;
            txtAmountInput.Location = new Point(205, 53);
            txtAmountInput.Name = "txtAmountInput";
            txtAmountInput.Size = new Size(246, 61);
            txtAmountInput.TabIndex = 0;
            txtAmountInput.TextAlign = HorizontalAlignment.Center;
            txtAmountInput.KeyPress += txtAmountInput_KeyPress;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.DarkOrchid;
            flowLayoutPanel1.Controls.Add(btnFood);
            flowLayoutPanel1.Controls.Add(btnTransport);
            flowLayoutPanel1.Controls.Add(btnEntertainment);
            flowLayoutPanel1.Controls.Add(btnUtilities);
            flowLayoutPanel1.Controls.Add(btnOther);
            flowLayoutPanel1.ForeColor = Color.ForestGreen;
            flowLayoutPanel1.Location = new Point(58, 189);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(539, 120);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // btnFood
            // 
            btnFood.Location = new Point(3, 3);
            btnFood.Name = "btnFood";
            btnFood.Size = new Size(94, 75);
            btnFood.TabIndex = 0;
            btnFood.Text = "Food";
            btnFood.UseVisualStyleBackColor = true;
            btnFood.Click += CategoryButton_Click;
            // 
            // btnTransport
            // 
            btnTransport.Location = new Point(103, 3);
            btnTransport.Name = "btnTransport";
            btnTransport.Size = new Size(94, 75);
            btnTransport.TabIndex = 1;
            btnTransport.Text = "Transport";
            btnTransport.UseVisualStyleBackColor = true;
            btnTransport.Click += CategoryButton_Click;
            // 
            // btnEntertainment
            // 
            btnEntertainment.Location = new Point(203, 3);
            btnEntertainment.Name = "btnEntertainment";
            btnEntertainment.Size = new Size(123, 75);
            btnEntertainment.TabIndex = 2;
            btnEntertainment.Text = "Entertainment";
            btnEntertainment.UseVisualStyleBackColor = true;
            btnEntertainment.Click += CategoryButton_Click;
            // 
            // btnUtilities
            // 
            btnUtilities.Location = new Point(332, 3);
            btnUtilities.Name = "btnUtilities";
            btnUtilities.Size = new Size(94, 75);
            btnUtilities.TabIndex = 3;
            btnUtilities.Text = "Utilities";
            btnUtilities.UseVisualStyleBackColor = true;
            btnUtilities.Click += CategoryButton_Click;
            // 
            // btnOther
            // 
            btnOther.Location = new Point(432, 3);
            btnOther.Name = "btnOther";
            btnOther.Size = new Size(94, 75);
            btnOther.TabIndex = 4;
            btnOther.Text = "Other";
            btnOther.UseVisualStyleBackColor = true;
            btnOther.Click += CategoryButton_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.Lime;
            btnConfirm.Location = new Point(490, 386);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(107, 62);
            btnConfirm.TabIndex = 2;
            btnConfirm.Text = "Confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.ForeColor = Color.Red;
            btnCancel.Location = new Point(61, 386);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(107, 62);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonFace;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(276, 21);
            label2.Name = "label2";
            label2.Size = new Size(108, 20);
            label2.TabIndex = 4;
            label2.Text = "Enter Amount";
            // 
            // panel1
            // 
            panel1.BackColor = Color.BlueViolet;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(659, 10);
            panel1.TabIndex = 5;
            // 
            // panel4
            // 
            panel4.BackColor = Color.DarkViolet;
            panel4.Location = new Point(0, 491);
            panel4.Name = "panel4";
            panel4.Size = new Size(659, 10);
            panel4.TabIndex = 8;
            // 
            // ExpenseEntryScreen
            // 
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(659, 500);
            Controls.Add(panel4);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnConfirm);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(txtAmountInput);
            Name = "ExpenseEntryScreen";
            Text = "Add Expense";
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        private TextBox txtAmountInput;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnFood;
        private Button btnTransport;
        private Button btnEntertainment;
        private Button btnUtilities;
        private Button btnOther;
        private Button btnConfirm;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel panel4;
    }
}