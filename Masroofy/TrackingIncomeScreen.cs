
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Masroofy
{

    public partial class TrackingIncomeScreen : Form
    {
        

        public TrackingIncomeScreen()
        {
            InitializeComponent();

        }


        private void TrackingIncomeScreen_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT IncomeId, IncomeCode, Amount, IncomeSource, IncomeDate " +
                               "FROM Incomes " +
                               "ORDER BY IncomeDate DESC";

                DataTable dt = DataAccessLayer.ExecuteTable(query, CommandType.Text);

                dgvIncomes.DataSource = dt;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public decimal GetTotalIncome()
        {
            return 0;
        }
        private void dgw_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void amount_Click(object sender, EventArgs e)
        {

        }

    }
}

