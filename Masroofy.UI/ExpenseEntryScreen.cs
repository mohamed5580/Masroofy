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


        private int _selectedCategoryId = 0;
        private Button _activeButton = null;

        public ExpenseEntryScreen(
            ValidationService validationService,
            ITransactionRepository transactionRepo,
            BudgetService budgetService,
            IBudgetCycleRepository cycleRepo,
            StatisticsDashbourd dashboard)
        {
            InitializeComponent();
            _cycleRepo = cycleRepo;
            _budgetService = budgetService;
            _loggingController = new LoggingUIController(
                validationService,
                transactionRepo,
                budgetService,
                dashboard
            );
        }
        public ExpenseEntryScreen()
        {
            InitializeComponent();
        }
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            if (_activeButton != null)
                _activeButton.BackColor = SystemColors.Control;

            _activeButton = (Button)sender;
            _activeButton.BackColor = Color.LimeGreen;

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
            return await _budgetService.FindAllAndCalculateRemainingAsync(cycleId);
        }

        private float CalculateSafeDailyLimit(decimal remainingBalance, int remainingDays)
        {
            if (remainingDays <= 0) return (float)remainingBalance;
            return (float)Math.Round(remainingBalance / remainingDays, 2);
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (_selectedCategoryId == 0)
            {
                MessageBox.Show("Please select a category.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAmountInput.Text))
            {
                MessageBox.Show("Please enter an amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var activeCycle = await _cycleRepo.GetActiveCycleAsync();

            if (activeCycle == null)
            {
                Refresh();
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
                ? (Convert.ToDecimal(totalBudget - remainingBalance) / totalBudget) * 100
                : 0;
            if (activeCycle == null)
            {
                MessageBox.Show("No active budget cycle found.\nPlease create a cycle first.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"This expense exceeds your remaining budget! Your remaining budget will be = {remainingPercentage}% ", "Warning", MessageBoxButtons.YesNo,
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
                    Dashbourd._instance.ShowNotify();
                    return;
                }
                else
                {
                    MessageBox.Show("Invalid amount. Please enter a positive number.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void CategoryButton1_Click(object sender, EventArgs e)
        {

        }
    }
}