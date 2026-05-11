using Masroofy;
using Masroofy.Business.Services;
using Masroofy.Data.Database;
using Masroofy.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace Masroofy.UI
{
    public partial class Dashbourd : Form
    {
        private string _loggedInUserId;
        private string _loggedInUserType;

        private readonly IServiceProvider _serviceProvider;
        private readonly BudgetService _budgetService;
        private readonly IBudgetCycleRepository _cycleRepo;

        public static Dashbourd _instance;
        public static Dashbourd Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Dashbourd();
                }
                return _instance;
            }
        }


        public Dashbourd(IServiceProvider serviceProvider, IBudgetCycleRepository cycleRepo)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _budgetService = _serviceProvider.GetRequiredService<BudgetService>();
            _instance = this;
             _cycleRepo= cycleRepo;
            ShowNotify();
        }

        public Dashbourd()
        {
            InitializeComponent();
            _instance = this;

        }

        public async void RefreshData()
        {
            try
            {

                int currentCycleId = 1;

                var (newLimit, totalSpent, remaining) = await _budgetService.RecalculateAfterExpenseAsync(currentCycleId);



                Console.WriteLine($"Dashboard Refreshed: New Limit is {newLimit}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing data: " + ex.Message);
            }
        }

        private void Dashbourd_Load(object sender, EventArgs e)
        {
        }

        private void ReminderCheckTimer_Tick(object sender, EventArgs e) { }
        private void UpdateReminderCountDisplay() { }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void basic_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("هل انت متاكد من الخروج من البرنامج", "تاكيد الخروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) e.Cancel = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            var form = _serviceProvider.GetRequiredService<BudgetCycleForm>();
            form.Show();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            var expenseScreen = _serviceProvider.GetRequiredService<ExpenseEntryScreen>();
            if (expenseScreen.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }
        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {


            var TransactionsScreen = _serviceProvider.GetRequiredService<Transactions>();
            if (TransactionsScreen.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }

        }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {


            Setting dBConfig = new Setting();
            dBConfig.Show();

        }

        private void button10_Click(object sender, EventArgs e) { toolStripMenuItem33_Click(sender, e); }

        private void button7_Click(object sender, EventArgs e)
        {
            var expenseScreen = _serviceProvider.GetRequiredService<ExpenseEntryScreen>();
            if (expenseScreen.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }

        private void button11_Click(object sender, EventArgs e) { toolStripMenuItem2_Click_1(sender, e); }
        private void button8_Click(object sender, EventArgs e) { toolStripMenuItem7_Click(sender, e); }

        private void الالهالحاسبةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\Windows\\System32\\calc.exe");
        }

        private void اعداداتToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Setting dBConfig = new Setting();
            dBConfig.Show();
        }

        private void نسخاحطياتيToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void timer5_Tick(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
        private void نسخToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void button12_Click(object sender, EventArgs e)
        {
            var stats = _serviceProvider.GetRequiredService<StatisticsDashbourd>();
            stats.Show();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            var trans = _serviceProvider.GetRequiredService<Transactions>();

            trans.Show();
        }
        public async void ShowNotify()
        {


            var activeCycle = await _cycleRepo.GetActiveCycleAsync();

            if (activeCycle == null)
            {
                Refresh();
                return;
            }
            var (remainingBalance, remainingDays) =
                await CalculateRemainingBalance(activeCycle.Id);

            decimal totalBudget = activeCycle.TotalAllowance;

            decimal remainingPercentage = totalBudget > 0
                ? (Convert.ToDecimal(remainingBalance) / totalBudget) * 100
                : 0;
            if (activeCycle == null)
            {
                MessageBox.Show("No active budget cycle found.\nPlease create a cycle first.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (remainingPercentage < 80)
            {
            }
           

            if (remainingPercentage <= 80)
            {
                ShowNotification("Low Budget Alert", $"Your remaining budget is critically low at {remainingPercentage:F2}%!");
                panel1.Visible = true;
                panel1.BackColor = Color.Red;
                Dashbourd._instance.massagee.Text = $"Remaining Balance: {remainingBalance:C}\n Remaining Days: {remainingDays}\nRemaining Percentage: {remainingPercentage:F2}%";
            }
            else
            {
                panel1.Visible = false;

            }


        }
        private async Task<(decimal remainingBalance, int remainingDays)>
        CalculateRemainingBalance(int cycleId)
        {
            return await _budgetService.FindAllAndCalculateRemainingAsync(cycleId);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        public void ShowNotification(string title, string message)
        {

            notifyIcon1.BalloonTipTitle = title;
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(50000);

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void setPINToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}