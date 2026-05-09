using Masroofy.Data.Repositories;
using Masroofy.Data.Models;
using Masroofy.Business.Services; // for validation
using Microsoft.VisualBasic; //for inputBox
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Masroofy.UI
{
    public partial class Transactions : Form
    {
        private readonly ITransactionRepository _repo;
        private readonly BudgetService _budgetService;
        private readonly ValidationService _validationService = new ValidationService();
        private readonly StatisticsDashbourd _statsDashboard;
        private int _cycleId = 1;
        private int _selectedCategoryId = 0;
        public Transactions(ITransactionRepository repo, BudgetService budgetService, StatisticsDashbourd statsDashboard)
        {
            InitializeComponent();
            _repo = repo;
            _budgetService = budgetService;
            this.Load += Transactions_Load;
            _statsDashboard = statsDashboard;
        }

        private async void Transactions_Load(object sender, EventArgs e)
        {
            await LoadHistory();
        }

        private async Task LoadHistory()
        {
            try
            {
                dgw.Rows.Clear();
                var transactions = await _repo.GetHistoryAsync(_cycleId);
                ShowTransactions(transactions);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}");
            }
        }

        private void ShowTransactions(List<Transaction> transactions)
        {
            dgw.Rows.Clear();

            foreach (var t in transactions)
            {
                dgw.Rows.Add(
                    t.Id,
                    t.Amount,
                    t.CategoryName,
                    t.Timestamp.ToString("yyyy-MM-dd HH:mm"),
                    t.BudgetCycleId
                );
            }
        }
        //edit btn
        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgw.SelectedRows.Count > 0)
            {
                int transactionId = Convert.ToInt32(dgw.SelectedRows[0].Cells[0].Value);
                decimal currentAmount = Convert.ToDecimal(dgw.SelectedRows[0].Cells[1].Value);

                string input = Interaction.InputBox("Enter the new amount:", "Edit Transaction", currentAmount.ToString());

                if (decimal.TryParse(input, out decimal newAmount))
                {
                    if (_validationService.IsValidAmount(newAmount))
                    {
                        var t = new Transaction { Id = transactionId, Amount = newAmount };
                        await _repo.UpdateAsync(t);

                        await _budgetService.RecalculateAfterExpenseAsync(_cycleId);

                        MessageBox.Show("Transaction Updated Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _statsDashboard.RefreshDashboardData(); 
                        await LoadHistory();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid amount greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a transaction to edit first.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnDel_Click(object sender, EventArgs e)
        {
            if (dgw.SelectedRows.Count > 0)
            {
                int transactionId = Convert.ToInt32(dgw.SelectedRows[0].Cells[0].Value);

                var result = MessageBox.Show(
                    "Are you sure you want to delete this transaction? This will update your daily limit.",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    await _repo.DeleteAsync(transactionId);

                    await _budgetService.RecalculateAfterExpenseAsync(_cycleId);
                    _statsDashboard.RefreshDashboardData();
                    await LoadHistory();
                }
            }
            else
            {
                MessageBox.Show("Please select a transaction to delete first.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgw_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private async void CategoryTxt_TextChanged(object sender, EventArgs e)
        {
            string search = CategoryTxt.Text.Trim();

            dgw.Rows.Clear();

            var transactions = await _repo.GetHistoryAsync(_cycleId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                transactions = transactions
                    .Where(t => t.CategoryName != null &&
                                t.CategoryName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ShowTransactions(transactions);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value.Date;

            dgw.Rows.Clear();

            var transactions = await _repo.GetHistoryAsync(_cycleId);

            transactions = transactions
                .Where(t => t.Timestamp.Date == selectedDate)
                .ToList();

            ShowTransactions(transactions);
        }

        
    }
}