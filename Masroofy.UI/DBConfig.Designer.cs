namespace Masroofy.UI
{
    partial class DBConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBConfig));
            label1 = new Label();
            tbServer = new TextBox();
            tbDb = new TextBox();
            label2 = new Label();
            label3 = new Label();
            rbWindows = new RadioButton();
            rdSQL = new RadioButton();
            tbPass = new TextBox();
            label4 = new Label();
            tbUser = new TextBox();
            label5 = new Label();
            btnSave = new Button();
            gunaButton1 = new Button();
            label6 = new Label();
            rbSqlServer = new CheckBox();
            rbSQLite = new CheckBox();
            rbMySQL = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(64, 64, 64);
            label1.Location = new Point(28, 90);
            label1.Name = "label1";
            label1.Size = new Size(62, 18);
            label1.TabIndex = 0;
            label1.Text = "Server:";
            // 
            // tbServer
            // 
            tbServer.Location = new Point(123, 89);
            tbServer.Name = "tbServer";
            tbServer.Size = new Size(255, 27);
            tbServer.TabIndex = 1;
            tbServer.TextChanged += tbServer_TextChanged;
            // 
            // tbDb
            // 
            tbDb.Location = new Point(123, 135);
            tbDb.Name = "tbDb";
            tbDb.Size = new Size(255, 27);
            tbDb.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(64, 64, 64);
            label2.Location = new Point(28, 136);
            label2.Name = "label2";
            label2.Size = new Size(84, 18);
            label2.TabIndex = 2;
            label2.Text = "Database:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(64, 64, 64);
            label3.Location = new Point(28, 347);
            label3.Name = "label3";
            label3.Size = new Size(92, 18);
            label3.TabIndex = 4;
            label3.Text = "Login Way:";
            // 
            // rbWindows
            // 
            rbWindows.AutoSize = true;
            rbWindows.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rbWindows.Location = new Point(123, 379);
            rbWindows.Name = "rbWindows";
            rbWindows.Size = new Size(209, 22);
            rbWindows.TabIndex = 5;
            rbWindows.Text = "Windows Authintecation";
            rbWindows.UseVisualStyleBackColor = true;
            rbWindows.CheckedChanged += rbWindows_CheckedChanged;
            // 
            // rdSQL
            // 
            rdSQL.AutoSize = true;
            rdSQL.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rdSQL.Location = new Point(123, 407);
            rdSQL.Name = "rdSQL";
            rdSQL.Size = new Size(227, 22);
            rdSQL.TabIndex = 6;
            rdSQL.Text = "SQL Server Authintecation";
            rdSQL.UseVisualStyleBackColor = true;
            rdSQL.CheckedChanged += rdSQL_CheckedChanged;
            // 
            // tbPass
            // 
            tbPass.Location = new Point(123, 494);
            tbPass.Name = "tbPass";
            tbPass.Size = new Size(255, 27);
            tbPass.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(64, 64, 64);
            label4.Location = new Point(28, 495);
            label4.Name = "label4";
            label4.Size = new Size(88, 18);
            label4.TabIndex = 9;
            label4.Text = "Password:";
            // 
            // tbUser
            // 
            tbUser.Location = new Point(123, 447);
            tbUser.Name = "tbUser";
            tbUser.Size = new Size(255, 27);
            tbUser.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(64, 64, 64);
            label5.Location = new Point(28, 449);
            label5.Name = "label5";
            label5.Size = new Size(49, 18);
            label5.TabIndex = 7;
            label5.Text = "User:";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.DodgerBlue;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Arial Rounded MT Bold", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.ForeColor = Color.WhiteSmoke;
            btnSave.Location = new Point(133, 555);
            btnSave.Margin = new Padding(5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(197, 42);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // gunaButton1
            // 
            gunaButton1.Font = new Font("Segoe UI", 9F);
            gunaButton1.ForeColor = Color.White;
            gunaButton1.Image = (Image)resources.GetObject("gunaButton1.Image");
            gunaButton1.Location = new Point(105, 609);
            gunaButton1.Margin = new Padding(4, 5, 4, 5);
            gunaButton1.Name = "gunaButton1";
            gunaButton1.Size = new Size(256, 65);
            gunaButton1.TabIndex = 12;
            gunaButton1.Text = "استعادة نسخه احتياطية";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(64, 64, 64);
            label6.Location = new Point(27, 190);
            label6.Name = "label6";
            label6.Size = new Size(135, 18);
            label6.TabIndex = 16;
            label6.Text = "Database Type : ";
            // 
            // rbSqlServer
            // 
            rbSqlServer.AutoSize = true;
            rbSqlServer.Location = new Point(45, 234);
            rbSqlServer.Name = "rbSqlServer";
            rbSqlServer.Size = new Size(93, 24);
            rbSqlServer.TabIndex = 20;
            rbSqlServer.Text = "SqlServer";
            rbSqlServer.UseVisualStyleBackColor = true;
            rbSqlServer.CheckedChanged += rbSqlServer_CheckedChanged;
            // 
            // rbSQLite
            // 
            rbSQLite.AutoSize = true;
            rbSQLite.Location = new Point(183, 234);
            rbSQLite.Name = "rbSQLite";
            rbSQLite.Size = new Size(90, 24);
            rbSQLite.TabIndex = 21;
            rbSQLite.Text = "SQLiteee";
            rbSQLite.UseVisualStyleBackColor = true;
            rbSQLite.CheckedChanged += rbSQLite_CheckedChanged;
            // 
            // rbMySQL
            // 
            rbMySQL.AutoSize = true;
            rbMySQL.Location = new Point(351, 234);
            rbMySQL.Name = "rbMySQL";
            rbMySQL.Size = new Size(77, 24);
            rbMySQL.TabIndex = 22;
            rbMySQL.Text = "MySQL";
            rbMySQL.UseVisualStyleBackColor = true;
            rbMySQL.CheckedChanged += rbMySQL_CheckedChanged;
            // 
            // DBConfig
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 695);
            Controls.Add(rbMySQL);
            Controls.Add(rbSQLite);
            Controls.Add(rbSqlServer);
            Controls.Add(label6);
            Controls.Add(gunaButton1);
            Controls.Add(btnSave);
            Controls.Add(tbPass);
            Controls.Add(label4);
            Controls.Add(tbUser);
            Controls.Add(label5);
            Controls.Add(rdSQL);
            Controls.Add(rbWindows);
            Controls.Add(label3);
            Controls.Add(tbDb);
            Controls.Add(label2);
            Controls.Add(tbServer);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "DBConfig";
            Padding = new Padding(20, 92, 20, 25);
            Resizable = false;
            Text = "DB Config";
            Load += DBConfig_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbDb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbWindows;
        private System.Windows.Forms.RadioButton rdSQL;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button gunaButton1;
        private Label label6;
        private CheckBox rbSqlServer;
        private CheckBox rbSQLite;
        private CheckBox rbMySQL;
    }
}