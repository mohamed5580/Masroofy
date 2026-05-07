using Masroofy.Business.Services;
using Masroofy.Data.Database;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Masroofy.UI
{
    public partial class Setting : MetroFramework.Forms.MetroForm
    {
        private readonly IServiceProvider _serviceProvider;

        public Setting()
        {
            InitializeComponent();

            if (Masroofy.Data.Properties.Settings.Default.Mode == true)
                rbWindows.Checked = true;
            else
                rdSQL.Checked = true;

            tbServer.Text = Masroofy.Data.Properties.Settings.Default.Server;
            tbDb.Text = Masroofy.Data.Properties.Settings.Default.Database;
            tbUser.Text = Masroofy.Data.Properties.Settings.Default.Name;
            tbPass.Text = Masroofy.Data.Properties.Settings.Default.Pass;


        }
        public Setting(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

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
            if (rbSqlServer.Checked) Masroofy.Data.Properties.Settings.Default.Provider = "SqlServer";
            else if (rbSQLite.Checked) Masroofy.Data.Properties.Settings.Default.Provider = "SQLite";
            else if (rbMySQL.Checked) Masroofy.Data.Properties.Settings.Default.Provider = "MySQL";

            Masroofy.Data.Properties.Settings.Default.Save();

            MessageBox.Show(this, "تم الحفظ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
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
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Are you sure you want to Reset All ? ", "Warning", MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DeleteAllTransactionsAsync();
                DeleteAllBudgetCycleAsync();
                MessageBox.Show("All Data Deleted Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnChange_Click(object sender, EventArgs e)
        {

            string input = Interaction.InputBox("Enter the new PIN:", "Change PIN", "");


            if (!new ValidationService().IsValidPin(input))
            {
                MessageBox.Show("Please enter a valid 4-digit PIN.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await ChangePinAsync(input);

            MessageBox.Show("PIN updated successfully.", "Confirmation",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Masroofy.Data.Properties.Settings.Default.PINCheck = true;
            Masroofy.Data.Properties.Settings.Default.Save();

        }
        public async Task<bool> HasPinAsync()
        {
            const string sql = "SELECT COUNT(*) FROM Authentication WHERE PIN IS NOT NULL AND PIN != ''";

            var result = await DataAccessLayer.ExecuteScalarAsync(sql, CommandType.Text);
            return Convert.ToInt32(result) > 0;
        }
        private async void btnSet_Click(object sender, EventArgs e)
        {
            
            if (Masroofy.Data.Properties.Settings.Default.PINCheck == false)
            {
                Masroofy.Data.Properties.Settings.Default.PINCheck = true;
            }
            string input = Interaction.InputBox("Enter a 4-digit PIN:", "Set PIN", "");

            if (!new ValidationService().IsValidPin(input))
            {
                MessageBox.Show("Please enter a valid 4-digit PIN.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool hasPin = await HasPinAsync();
            if (hasPin)
            {
                MessageBox.Show("A PIN already exists. Please use the Change PIN option.",
                    "PIN Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await SetPinAsync(input);

            MessageBox.Show("PIN saved successfully.", "Confirmation",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            Masroofy.Data.Properties.Settings.Default.PINCheck = true;
            Masroofy.Data.Properties.Settings.Default.Save();

        }


        public async Task SetPinAsync(string plainPin)
        {
            if (!new ValidationService().IsValidPin(plainPin))
            {
                MessageBox.Show("Please enter a valid 4-digit PIN.", "Validation Error",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var security = new SecurityService();
            string hashedPin = ComputeSha256Hash(plainPin);

            const string sql = @"
        INSERT INTO Authentication (PIN)
        VALUES (@PIN)";

            var paramPin = DataAccessLayer.CreateParameter(
                "@PIN",
                DbType.String,
                hashedPin
            );

            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, paramPin);

        }

        public async Task ChangePinAsync(string plainPin)
        {
            if (!new ValidationService().IsValidPin(plainPin))
                MessageBox.Show("PIN must be 4 digits.",
                  "PIN ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            string hashedPin = ComputeSha256Hash(plainPin);

            const string sql = @"
        UPDATE Authentication
        SET PIN = @PIN";

            var paramPin = DataAccessLayer.CreateParameter(
                "@PIN",
                DbType.String,
                hashedPin
            );

            await DataAccessLayer.ExecuteNonQueryAsync(sql, CommandType.Text, paramPin);
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes) builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
        public async Task DeleteAllTransactionsAsync()
        {
            const string sql = "DELETE FROM Transactions";

            await DataAccessLayer.ExecuteNonQueryAsync(
                sql,
                CommandType.Text
            );
        }
        public async Task DeleteAllBudgetCycleAsync()
        {
            const string sql = "DELETE FROM BudgetCycles";

            await DataAccessLayer.ExecuteNonQueryAsync(
                sql,
                CommandType.Text
            );
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Masroofy.Data.Properties.Settings.Default.PINCheck = false;
            Masroofy.Data.Properties.Settings.Default.Save();

            MessageBox.Show("PIN Removed successfully ", "Confirmation",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}