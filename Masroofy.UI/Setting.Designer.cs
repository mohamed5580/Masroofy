namespace Masroofy.UI
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            btnConfirm = new Button();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.DarkRed;
            btnConfirm.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConfirm.ForeColor = Color.Snow;
            btnConfirm.Location = new Point(163, 327);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(107, 62);
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "Reset All";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonFace;
            label2.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            label2.Location = new Point(26, 243);
            label2.Name = "label2";
            label2.Size = new Size(427, 81);
            label2.TabIndex = 5;
            label2.Text = "Reset All Data";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(82, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(236, 227);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 401);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(btnConfirm);
            Name = "Setting";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Setting";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConfirm;
        private Label label2;
        private PictureBox pictureBox1;
    }
}