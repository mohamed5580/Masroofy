using Masroofy.Business.Services;
using Masroofy.Data.Repositories;
using Masroofy.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Masroofy
{
    public partial class ExpenseEntryScreen : Form
    {
        private readonly LoggingUIController _loggingController;
        private readonly IBudgetCycleRepository _cycleRepo;

        // Tracks whichever category button the user last clicked.
        // Matches Designer buttons: btnFood=1, btnTransport=2,
        // btnEntertainment=3, btnUtilities=4, btnOther=5
        private int _selectedCategoryId = 0;
        private Button _activeButton = null;

        // ── Constructor (injected by Program.cs) ──────────────────────────────
        public ExpenseEntryScreen(
            ValidationService validationService,
            ITransactionRepository transactionRepo,
            BudgetService budgetService,
            IBudgetCycleRepository cycleRepo,
            StatisticsDashbourd dashboard)  // live singleton — enables refresh after save
        {
            InitializeComponent();
            _cycleRepo = cycleRepo;

            // Wire up LoggingUIController with the live dashboard reference
            // so RefreshDashboardData() is called after every successful save.
            _loggingController = new LoggingUIController(
                validationService,
                transactionRepo,
                budgetService,
                dashboard
            );
        }

        // ── Category buttons (all share one handler via Designer) ─────────────
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            // Reset previous selection highlight
            if (_activeButton != null)
                _activeButton.BackColor = SystemColors.Control;

            _activeButton = (Button)sender;
            _activeButton.BackColor = Color.LimeGreen;

            // Map button name to category ID (matches BudgetService.MapIdToCategoryName)
            _selectedCategoryId = _activeButton.Name switch
            {
                "btnFood" => 1,
                "btnTransport" => 2,
                "btnEntertainment" => 3,
                "btnUtilities" => 4,
                "btnOther" => 5,
                _ => 5
            };
        }

        // ── Confirm button (Designer: btnConfirm_Click) ───────────────────────
        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            // Validate category selection
            if (_selectedCategoryId == 0)
            {
                MessageBox.Show("Please select a category.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Resolve the active cycle from DB (never hardcoded)
            var activeCycle = await _cycleRepo.GetActiveCycleAsync();
            if (activeCycle == null)
            {
                MessageBox.Show("No active budget cycle found.\nPlease create a cycle first.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // OnSaveTapped:
            //   1. Saves transaction to SQLite
            //   2. Calls dashboard.RefreshDashboardData()
            //      → DashboardUIController.OnOpen()
            //        → calculateRemainingBalance() → calculateSafeDailyLimit()
            //        → Refresh() → Display() → [opt] ShowFinalDayBadge()
            bool success = await _loggingController.OnSaveTapped(
                txtAmountInput.Text,
                _selectedCategoryId,
                activeCycle.Id);

            if (success)
            {
                MessageBox.Show("Expense saved successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid amount. Please enter a positive number.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ── Cancel button (Designer: btnCancel_Click) ─────────────────────────
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ── Only allow digits and one decimal point (Designer: txtAmountInput_KeyPress)
        private void txtAmountInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && txtAmountInput.Text.Contains('.'))
                e.Handled = true;
        }
    }
}