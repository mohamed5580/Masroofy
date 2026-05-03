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

        // INJECTION FIX: Using the repository from the ServiceProvider ensures data consistency
        public BudgetCycleScreen(IBudgetCycleRepository repository)
        {
            InitializeComponent();
            _budgetcycleRepository = repository;
        }

        private void BudgetCycleScreen_Load(object sender, EventArgs e)
        {
            LoadData();
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
                // Passing the ID back to the parent Dashboard via the Tag property
                this.Tag = row.Cells[0].Value;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving record data: {ex.Message}");
            }
        }

        public decimal GetTotalIncome() => 0;
        private void amount_Click(object sender, EventArgs e) { }
    }
}