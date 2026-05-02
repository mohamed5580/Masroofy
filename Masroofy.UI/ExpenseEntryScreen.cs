using System;
using System.Drawing;
using System.Windows.Forms;
using Masroofy.UI; // Required for LoggingUIController access
using Masroofy.Business.Services;
using Masroofy.Data.Repositories;

namespace Masroofy
{
    public partial class ExpenseEntryScreen : Form
    {
        // Reference to the controller (The "Brain" per the Sequence Diagram)
        private readonly LoggingUIController _controller;

        // Tracking state for the logic
        private string _selectedCategory = "";
        private int _currentCycleId;

        /// <summary>
        /// Constructor injected with the controller and current cycle context.
        /// </summary>
        public ExpenseEntryScreen(LoggingUIController controller, int currentCycleId = 1)
        {
            InitializeComponent();
            _controller = controller;
            _currentCycleId = currentCycleId;
        }

        #region UI Event Handlers (Original Logic Preserved)

        private void txtAmountInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits (0-9), backspace, and one decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            // Reset all buttons in the flow layout to default color
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is Button b) b.BackColor = Color.WhiteSmoke;
            }

            // Highlight the clicked button
            Button clicked = (Button)sender;
            _selectedCategory = clicked.Text;
            clicked.BackColor = Color.LightGreen;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Sequence Diagram Logic (Integrated)

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            // 1. UI Validation: Ensure data is present before calling the controller
            if (string.IsNullOrEmpty(txtAmountInput.Text) || string.IsNullOrEmpty(_selectedCategory))
            {
                MessageBox.Show("Please enter amount and select category!", "Missing Information");
                return;
            }

            // 2. Map Category Name to ID (Logic required for your TransactionRepository)
            // Note: In a full system, this mapping would come from a CategoryService
            int categoryId = MapCategoryToId(_selectedCategory);

            // 3. Trigger the Controller (Flow: Screen -> Controller -> Service -> Repo)
            // This satisfies the 'onSaveTapped' trigger in your Sequence Diagram
            bool success = await _controller.OnSaveTapped(txtAmountInput.Text, categoryId, _currentCycleId);

            if (success)
            {
                // US#2: showConfirmation()
                MessageBox.Show($"Saved {txtAmountInput.Text} for {_selectedCategory}", "Success");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // US#2: showError()
                MessageBox.Show("Invalid input. Please enter a valid positive number.", "Error");
            }
        }

        /// <summary>
        /// Simple helper to map UI strings to Database Category IDs
        /// </summary>
        private int MapCategoryToId(string categoryName)
        {
            return categoryName switch
            {
                "Food" => 1,
                "Transport" => 2,
                "Entertainment" => 3,
                "Utilities" => 4,
                _ => 5 // "Other"
            };
        }

        #endregion
    }
}