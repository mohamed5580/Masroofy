using Masroofy.Data.Repositories;
using Masroofy.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Masroofy.UI
{
    public partial class Transactions : Form
    {
        private readonly ITransactionRepository _repo;
        private int _cycleId;

        public Transactions(ITransactionRepository repo)
        {
            InitializeComponent();
            _repo = repo;
            this.Load += Transactions_Load;
        }

       
        private async void Transactions_Load(object sender, EventArgs e)
        {
            await LoadHistory();
        }

        // loadHistory()
        private async Task LoadHistory()
        {
            try
            {
                var transactions = await _repo.GetHistoryAsync(_cycleId);
                ShowTransactions(transactions);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transactions: {ex.Message}");
            }
        }

        // display()
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

        private void dgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ممكن تستخدمها بعدين لو عايز تفاصيل
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // optional
        }
    }
}