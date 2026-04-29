
using Masroofy.Data.Database;
using Masroofy.Data.Models;
using Masroofy.Data.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Masroofy
{

    public partial class BudgetCycleScreen : Form
    {
        private readonly IBudgetCycleRepository _BudgetcycleRepository;
        private BudgetCycle? _BudgetcurrentCycle;


        public BudgetCycleScreen()
        {
            InitializeComponent();
            _BudgetcycleRepository = new BudgetCycleRepository();

        }


        private void BudgetCycleScreen_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            dgw.Rows.Clear(); // مهم جدًا

            var cycles = await _BudgetcycleRepository.GetAllCyclesAsync();

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
                BudgetCycleForm budgetingAnalysis = BudgetCycleForm.Instance;
                budgetingAnalysis.txtID.Text = row.Cells[0].Value.ToString();
                budgetingAnalysis.txtAmount.Text = row.Cells[1].Value.ToString();
                budgetingAnalysis.StartDate.Value = Convert.ToDateTime(row.Cells[2].Value);
                budgetingAnalysis.EndDate.Value = Convert.ToDateTime(row.Cells[3].Value);

                budgetingAnalysis.btnUpdate.Enabled = true;
                budgetingAnalysis.btnDelete.Enabled = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error retrieving record data: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public decimal GetTotalIncome()
        {
            return 0;
        }
      
        private void amount_Click(object sender, EventArgs e)
        {

        }

    }
}

