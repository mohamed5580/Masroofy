using Masroofy.Business.Services;
using Masroofy.Data.Database;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Mysqlx.Notice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Masroofy.UI
{
    public partial class Setting : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public Setting()
        {
            InitializeComponent();
        }
        public Setting(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
           
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Are you sure you want to Reset All ? ", "Warning", MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DeleteAllTransactionsAsync();
                DeleteAllBudgetCycleAsync();
                MessageBox.Show("All Data Deleted Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public async Task DeleteAllTransactionsAsync()
        {
            const string sql = "DELETE FROM Transactions";

            await DataAccessLayer.ExecuteNonQueryAsync(
                sql,
                CommandType.Text
            );
        }
        public async Task DeleteAllBudgetCycleAsync()
        {
            const string sql = "DELETE FROM BudgetCycles";

            await DataAccessLayer.ExecuteNonQueryAsync(
                sql,
                CommandType.Text
            );
        }
    }
}
