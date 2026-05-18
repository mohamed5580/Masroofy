using System;
using System.ComponentModel;
namespace Masroofy.UI
{
    partial class PIN
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PIN));
            label2 = new Label();
            pictureBox1 = new PictureBox();
            txtPIN = new TextBox();
            LoginBtn = new Button();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((ISupportInitialize)pictureBox1).BeginInit();
            ((ISupportInitialize)pictureBox2).BeginInit();
            ((ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(195, 145);
            label2.Name = "label2";
            label2.Size = new Size(159, 37);
            label2.TabIndex = 5000;
            label2.Text = "Enter PIN : ";
            label2.Click += label2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1015, 533);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // txtPIN
            // 
            txtPIN.BackColor = Color.White;
            txtPIN.Cursor = Cursors.IBeam;
            txtPIN.Font = new Font("Segoe UI", 16F);
            txtPIN.Location = new Point(94, 217);
            txtPIN.Margin = new Padding(4, 3, 4, 3);
            txtPIN.Name = "txtPIN";
            txtPIN.PasswordChar = '*';
            txtPIN.RightToLeft = RightToLeft.Yes;
            txtPIN.Size = new Size(384, 43);
            txtPIN.TabIndex = 5001;
            txtPIN.TextAlign = HorizontalAlignment.Right;
            // 
            // LoginBtn
            // 
            LoginBtn.BackColor = Color.FromArgb(100, 88, 255);
            LoginBtn.Font = new Font("Segoe UI", 14F);
            LoginBtn.ForeColor = Color.White;
            LoginBtn.Location = new Point(62, 281);
            LoginBtn.Margin = new Padding(4, 3, 4, 3);
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Size = new Size(440, 76);
            LoginBtn.TabIndex = 5002;
            LoginBtn.Text = "Login";
            LoginBtn.UseVisualStyleBackColor = false;
            LoginBtn.Click += LoginBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(195, 360);
            label1.Name = "label1";
            label1.Size = new Size(153, 28);
            label1.TabIndex = 5003;
            label1.Text = "You Forgit PIN?";
            label1.Click += label1_ClickAsync;
            // 
            // pictureBox2
            // 
            pictureBox2.AccessibleRole = AccessibleRole.None;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.ErrorImage = (Image)resources.GetObject("pictureBox2.ErrorImage");
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(549, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(466, 533);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 5004;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(12, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(45, 46);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 5005;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // PIN
            // 
            AcceptButton = LoginBtn;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1015, 533);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Controls.Add(LoginBtn);
            Controls.Add(txtPIN);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PIN";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Enter PIN";
            FormClosing += PIN_FormClosing;
            FormClosed += PIN_FormClosed;
            Load += PIN_Load;
            ((ISupportInitialize)pictureBox1).EndInit();
            ((ISupportInitialize)pictureBox2).EndInit();
            ((ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private PictureBox pictureBox1;
        public TextBox txtPIN;
        private Button LoginBtn;
        private Label label1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}