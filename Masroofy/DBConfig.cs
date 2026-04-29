using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Data.SqlClient;

namespace Masroofy.UI
{
    public partial class DBConfig : MetroFramework.Forms.MetroForm
    {
        public DBConfig()
        {
            InitializeComponent();
            // Example of accessing a setting called "ConnectionString" in Properties.Settings

            if (Masroofy.Data.Properties.Settings.Default.Mode == true)
                rbWindows.Checked = true;
            else
                rdSQL.Checked = true;

            tbServer.Text = Masroofy.Data.Properties.Settings.Default.Server;
            tbDb.Text = Masroofy.Data.Properties.Settings.Default.Database;
            tbUser.Text = Masroofy.Data.Properties.Settings.Default.Name;
            tbPass.Text = Masroofy.Data.Properties.Settings.Default.Pass;


        }

        private void rbWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWindows.Checked == true)
                tbUser.Enabled = tbPass.Enabled = false;
            else
                tbUser.Enabled = tbPass.Enabled = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Masroofy.Data.Properties.Settings.Default.Mode = rbWindows.Checked;
            Masroofy.Data.Properties.Settings.Default.Server = tbServer.Text;
            Masroofy.Data.Properties.Settings.Default.Database = tbDb.Text;
            Masroofy.Data.Properties.Settings.Default.Name = tbUser.Text;
            Masroofy.Data.Properties.Settings.Default.Pass = tbPass.Text;
            // ── حفظ نوع الداتابيز ──
            if (rbSqlServer.Checked) Masroofy.Data.Properties.Settings.Default.Provider = "SqlServer";
            else if (rbSQLite.Checked) Masroofy.Data.Properties.Settings.Default.Provider = "SQLite";
            else if (rbMySQL.Checked) Masroofy.Data.Properties.Settings.Default.Provider = "MySQL";
            Masroofy.Data.Properties.Settings.Default.Save();

            MessageBox.Show(this, "تم الحفظ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
        }

        private void rdSQL_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DBConfig_Load(object sender, EventArgs e)
        {

        }

        private void tbServer_TextChanged(object sender, EventArgs e)
        {

        }

     

        private void rbSQLite_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbSQLite.Checked) return;
            rbSQLite.Checked = true;
            rbSqlServer.Checked = false;
            rbMySQL.Checked = false;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "اختر ملف SQLite";
                ofd.Filter = "SQLite Files (*.db;*.sqlite;*.sqlite3)|*.db;*.sqlite;*.sqlite3|All Files (*.*)|*.*";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tbDb.Text = ofd.FileName;

                }
                else
                {
                    // لو ما اختارش ملف، ارجع للـ SQL Server
                    return;
                }
            }
        
        }

        private void rbSqlServer_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbSqlServer.Checked) return;
            rbSqlServer.Checked = true;
            rbMySQL.Checked = false;
            rbSQLite.Checked = false;


        }

        private void rbMySQL_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbMySQL.Checked) return;

            rbSqlServer.Checked = false;
            rbMySQL.Checked = true;
            rbSQLite.Checked = false;
        }
    }
}