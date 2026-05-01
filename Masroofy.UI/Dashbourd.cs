 
using Masroofy;
using Masroofy.Data.Database;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System.Windows.Forms;
namespace Masroofy.UI
{
    public partial class Dashbourd : Form 
    {
        
        private string _loggedInUserId;
        private string _loggedInUserType;

        private readonly IServiceProvider _serviceProvider;

        public Dashbourd(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

        }


        public Dashbourd()
        {

        }


        private void Dashbourd_Load(object sender, EventArgs e)
        {
        }



        private void ReminderCheckTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void UpdateReminderCountDisplay()
        {

        }


        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _reminderCheckTimer?.Stop();
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
            var form = _serviceProvider.GetRequiredService<BudgetCycleForm>();
            form.Show();
        }


        private void button7_Click(object sender, EventArgs e) // Panel button "Budgeting Analysis"
        {
<<<<<<< Updated upstream
            // Use the ServiceProvider to get an instance of the screen
            // This ensures the ITransactionRepository and BudgetService are injected automatically
            var expenseScreen = _serviceProvider.GetRequiredService<ExpenseEntryScreen>();

            // Show the screen as a dialog so the user focuses on it
            expenseScreen.ShowDialog();

            toolStripMenuItem19_Click(sender, e);
=======

>>>>>>> Stashed changes
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
            lblTime.Text = Microsoft.VisualBasic.DateAndTime.TimeOfDay.ToString("h:mm:ss tt"); // Depends on VB namespace
        }
        private void button9_Click(object sender, EventArgs e)
        {
            
         
        }

        private void نسخToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }
    }
}