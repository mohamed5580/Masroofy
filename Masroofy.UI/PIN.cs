using Masroofy.Business.Services;
using Masroofy.Data.Database;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Masroofy.UI
{
    public partial class PIN : Form
    {
        private readonly SecurityService _authenticationService = new SecurityService();
        public PIN()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(formclosed);

        }
        private void formclosed(object sender, FormClosedEventArgs e)
        {
        }
        private void PIN_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string enteredPin = txtPIN.Text.Trim();

            if (!new ValidationService().IsValidPin(enteredPin))
            {
                MessageBox.Show("PIN must be exactly 4 digits.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPIN.Text = "";
                txtPIN.Focus();
                return;

            }

            bool isValid = await _authenticationService.VerifyPinAsync(enteredPin);

            if (isValid)
            {
                MessageBox.Show("Sucsess PIN", "Sucsess",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPIN.Text = "";
                txtPIN.Focus();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect PIN. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPIN.Text = "";
                txtPIN.Focus();
            }
        }

        private void PIN_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void PIN_FormClosing(object sender, FormClosingEventArgs e)
        {


        }

        private async void label1_ClickAsync(object sender, EventArgs e)
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

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
