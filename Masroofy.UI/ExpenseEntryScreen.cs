using Masroofy.Business.Services;
using Masroofy.Data.Repositories;
using Masroofy.UI;
using Microsoft.Extensions.DependencyInjection;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Masroofy
{
    public partial class ExpenseEntryScreen : Form
    {
        private readonly BudgetService _budgetService;

        private readonly LoggingUIController _loggingController;
        private readonly StatisticsDashbourd _StatisticsDashbourdController;
        private readonly IBudgetCycleRepository _cycleRepo;
        private readonly IServiceProvider _serviceProvider;
        private float _safeDailyLimit = 0;

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
            _budgetService = budgetService;
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
        private async Task<(decimal remainingBalance, int remainingDays)>
          CalculateRemainingBalance(int cycleId)
        {
            // Calls BudgetService.FindAllAndCalculateRemainingAsync()
            // which hits TransactionRepository → SQLite → List<Transaction>
            return await _budgetService.FindAllAndCalculateRemainingAsync(cycleId);
        }

        // ── calculateSafeDailyLimit() ─────────────────────────────────────────
        private float CalculateSafeDailyLimit(decimal remainingBalance, int remainingDays)
        {
            if (remainingDays <= 0) return (float)remainingBalance;
            return (float)Math.Round(remainingBalance / remainingDays, 2);
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
                Refresh();
                // Show "--" in the circle instead of "Loading..."
                _safeDailyLimit = 0;
                return;
            }
            var (remainingBalance, remainingDays) =
                await CalculateRemainingBalance(activeCycle.Id);

        
            if (activeCycle == null)
            {
                MessageBox.Show("No active budget cycle found.\nPlease create a cycle first.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var worning = remainingBalance - Convert.ToDecimal(txtAmountInput.Text);
     

            decimal totalBudget = activeCycle.TotalAllowance;

            decimal remainingPercentage = totalBudget > 0
                ? (Convert.ToDecimal(remainingBalance) / totalBudget) * 100
                : 0;

            decimal remainingPercentage1 = totalBudget > 0
             ? (Convert.ToDecimal(worning) / totalBudget) * 100
             : 0;

            if (worning <= 0 )
            {
                var result = MessageBox.Show($"This expense exceeds your remaining budget! Your remaining budget will be = {worning} ", "Warning", MessageBoxButtons.YesNo,
         MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool success = await _loggingController.OnSaveTapped(
                txtAmountInput.Text,
                _selectedCategoryId,
                activeCycle.Id);

                    if (success)
                    {
                        MessageBox.Show("Expense saved successfully! ", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                        Dashbourd.Instance.ShowNotification("Worning", $"You used %{remainingPercentage} of your budget");
                    }
                    else
                    {
                        MessageBox.Show("Invalid amount. Please enter a positive number.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                

            }
          

            if (remainingPercentage <= 80)
            {

                var result = MessageBox.Show(
          $"You used %{remainingPercentage} of your budget {totalBudget}. Do you want to continue?",
         "Confirmation",
         MessageBoxButtons.YesNo,
         MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool success = await _loggingController.OnSaveTapped(
                txtAmountInput.Text,
                _selectedCategoryId,
                activeCycle.Id);

                    if (success)
                    {
                        MessageBox.Show("Expense saved successfully! ", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                        Dashbourd.Instance.ShowNotification("Worning", $"You used %{remainingPercentage} of your budget");
                    }
                    else
                    {
                        MessageBox.Show("Invalid amount. Please enter a positive number.",
                            "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
            else
            {

                bool success = await _loggingController.OnSaveTapped(
                txtAmountInput.Text,
                _selectedCategoryId,
                activeCycle.Id);

                if (success)
                {
                    MessageBox.Show("Expense saved successfully! ", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("Invalid amount. Please enter a positive number.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }


        }
        void Reset()
        {
            _selectedCategoryId = 0;
            if (_activeButton != null)
                _activeButton.BackColor = SystemColors.Control;
            _activeButton = null;
            txtAmountInput.Text = "";
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

        private void ExpenseEntryScreen_Load(object sender, EventArgs e)
        {

        }
    }
}