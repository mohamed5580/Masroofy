using System;
using System.Drawing;
using System.Windows.Forms;
using Masroofy.Data.Repositories;
using Masroofy.Business.Services;

namespace Masroofy
{
    public partial class ExpenseEntryScreen : Form
    {
        public ExpenseEntryScreen()
        {
            InitializeComponent();
        }

        private void txtAmountInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits (0-9), backspace, and one decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true; // This blocks the character
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private string _selectedCategory = "";
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            foreach (Control c in btnFood.Parent.Controls)
            {
                if (c is Button b) b.BackColor = Color.WhiteSmoke;
            }

            Button clicked = (Button)sender;
            _selectedCategory = clicked.Text;
            clicked.BackColor = Color.LightGreen;
        }



        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAmountInput.Text) || string.IsNullOrEmpty(_selectedCategory))
            {
                MessageBox.Show("Please enter amount and select category!");
                return;
            }
            // Saving logic goes here...
            MessageBox.Show($"Saved {txtAmountInput.Text} for {_selectedCategory}");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}