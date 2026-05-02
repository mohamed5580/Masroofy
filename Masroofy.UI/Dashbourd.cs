using Masroofy;
using Masroofy.Data.Database;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;
using Masroofy.Business.Services; // Added for BudgetService logic

namespace Masroofy.UI
{
    public partial class Dashbourd : Form
    {
        private string _loggedInUserId;
        private string _loggedInUserType;

        private readonly IServiceProvider _serviceProvider;
        // Added field to access the budget logic
        private readonly BudgetService _budgetService;

        public Dashbourd(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            // Get the BudgetService from the service provider
            _budgetService = _serviceProvider.GetRequiredService<BudgetService>();
        }

        // Keep the empty constructor for WinForms Designer support if needed
        public Dashbourd()
        {
        }

        #region Sequence Diagram Implementation (US #2 Refresh Logic)

        /// <summary>
        /// This method fulfills the 'updateDashboard()' requirement in the sequence diagram.
        /// It recalculates the budget and updates the UI labels.
        /// </summary>
        public async void RefreshData()
        {
            try
            {
                // In US #2, we assume we are working with the active cycle (ID 1 for now)
                int currentCycleId = 1;

                // 1. Logic: Call the business layer to get new numbers
                var (newLimit, totalSpent, percentage) = await _budgetService.RecalculateAfterExpenseAsync(currentCycleId);

                // 2. UI: Update your labels. 
                // Note: Update these names (e.g., lblLimit) to match your designer names
                // lblLimit.Text = newLimit.ToString("0.00");
                // lblSpent.Text = totalSpent.ToString("0.00");

                Console.WriteLine($"Dashboard Refreshed: New Limit is {newLimit}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing data: " + ex.Message);
            }
        }

        #endregion

        private void Dashbourd_Load(object sender, EventArgs e)
        {
            // Initial load of data
            RefreshData();
        }

        private void ReminderCheckTimer_Tick(object sender, EventArgs e)
        {
        }

        private void UpdateReminderCountDisplay()
        {
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Fixed variable name to match your class members if applicable
            Application.Exit();
        }

        private void basic_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("هل انت متاكد من الخروج من البرنامج", "تاكيد الخروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e) // Menu "Tracking Income"
        {
            var form = _serviceProvider.GetRequiredService<BudgetCycleForm>();
            form.Show();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e) // Panel button "Tracking Income"
        {
            toolStripMenuItem33_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e) // Panel button "Budgeting Analysis" (LOG EXPENSE)
        {
            // 1. Get the screen from ServiceProvider
            var expenseScreen = _serviceProvider.GetRequiredService<ExpenseEntryScreen>();

            // 2. Show as dialog
            var result = expenseScreen.ShowDialog();

            // 3. If the user saved successfully (DialogResult.OK), refresh the dashboard numbers
            if (result == DialogResult.OK)
            {
                RefreshData();
            }

            toolStripMenuItem19_Click(sender, e);
        }

        private void button11_Click(object sender, EventArgs e) // Panel button "Reminders"
        {
            toolStripMenuItem2_Click_1(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            toolStripMenuItem7_Click(sender, e);
        }

        private void الالهالحاسبةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\Windows\\System32\\calc.exe");
        }

        private void اعداداتToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DBConfig dBConfig = new DBConfig();
            dBConfig.Show();
        }

        private void نسخاحطياتيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBConfig dBConfig = new DBConfig();
            dBConfig.Show();
        }

        public void Backup()
        {
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblTime.Text = Microsoft.VisualBasic.DateAndTime.TimeOfDay.ToString("h:mm:ss tt");
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void نسخToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}