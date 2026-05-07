using Masroofy.Business.Services;
using Masroofy.Data.Models;
using Masroofy.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Masroofy
{
    public partial class BudgetCycleScreen : Form
    {
        private readonly IBudgetCycleRepository _budgetcycleRepository;
        private readonly BudgetService _budgetService;

        public BudgetCycleScreen(IBudgetCycleRepository repository, BudgetService _budgetCycle)
        {
            InitializeComponent();
            _budgetcycleRepository = repository;
            _budgetService = _budgetCycle;
        }

        private async void BudgetCycleScreen_Load(object sender, EventArgs e)
        {
            LoadData();
            var total = await GetTotalIncomeAsync();
            amount.Text = total.ToString("0.##");
        }

        private async void LoadData()
        {
            dgw.Rows.Clear();

            var cycles = await _budgetcycleRepository.GetAllCyclesAsync();

            if (cycles != null)
            {
                foreach (var cycle in cycles)
                {
                    dgw.Rows.Add(
                        cycle.Id,
                        cycle.TotalAllowance,
                        cycle.StartDate,
                        cycle.EndDate,
                        cycle.IsActive
                    );
                }
            }
        }

        private void dgw_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgw.CurrentRow == null) return;

            var row = dgw.CurrentRow;
            try
            {
                BudgetCycleForm.Instance.txtID.Text = row.Cells[0].Value.ToString();
                BudgetCycleForm.Instance.txtAmount.Text = row.Cells[1].Value.ToString();
                BudgetCycleForm.Instance.StartDate.Value = Convert.ToDateTime(row.Cells[2].Value);
                BudgetCycleForm.Instance.EndDate.Value = Convert.ToDateTime(row.Cells[3].Value);
                BudgetCycleForm.Instance.btnSave.Enabled = false;
                BudgetCycleForm.Instance.btnUpdate.Enabled = true;
                BudgetCycleForm.Instance.btnDelete.Enabled = true;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving record data: {ex.Message}");
            }
        }

        private async Task<decimal> GetTotalIncomeAsync()
        {
            var totalCycle = await _budgetService.GetTotalCycleAsync();

            if (totalCycle == null)
                return 0;

            return totalCycle;
        }
        private void amount_Click(object sender, EventArgs e) { }

        private void dgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}