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

        private static Dashbourd _instance;
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


        public Dashbourd(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _budgetService = _serviceProvider.GetRequiredService<BudgetService>();
            _instance = this;


       
           
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
            RefreshData();
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

        private void toolStripMenuItem19_Click(object sender, EventArgs e) { }
        private void toolStripMenuItem2_Click_1(object sender, EventArgs e) { }
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

            var expenseScreen = _serviceProvider.GetRequiredService<Setting>();
            expenseScreen.ShowDialog();

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
            lblDateTime.Text = DateTime.Today.ToString("dd/MM/yyyy");
            lblTime.Text = Microsoft.VisualBasic.DateAndTime.TimeOfDay.ToString("h:mm:ss tt");
        }

        private void button9_Click(object sender, EventArgs e)
        {

            ShowNotification("تذكير", "لا تنسى تحديث ميزانيتك اليوم!");
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
    }
}