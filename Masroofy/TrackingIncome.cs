using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Data.SqlClient;


namespace Masroofy
{
    public partial class TrackingIncome : Form
    {
        
        private static TrackingIncome _instance;
        public static TrackingIncome Instance // Keep if used by Screen form
        {
            get
            {
     
                if (_instance == null || _instance.IsDisposed)
                {
                    return new TrackingIncome();
                }
                return _instance;
            }
        }

        

        
        public TrackingIncome() 
        {
            InitializeComponent();
            _instance = this; // Assign instance if keeping Singleton

            Reset(); 
        }
       
     

        private void TrackingIncome_Load(object sender, EventArgs e)
        {
            
        }


        
        private void Auto()
        {
            try
            {
                // Get the next code from the service
                string nextCode = GetMaxId().ToString();
                txtID.Clear(); // Clear ID for new record
                txtIncomeCode.Text = nextCode; // Set the generated code

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error generating Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIncomeCode.Text = "Error";
            }
        }
        public int GetMaxId()
        {
            const string query = @"SELECT IFNULL(MAX(IncomeId), 0) FROM Incomes";
            object result = DataAccessLayer.ExecuteScalar(query, CommandType.Text);
            return Convert.ToInt32(result) + 1;
        }
        // Reset form controls (UI logic, stays here)
        private void Reset()
        {
            txtAmount.Clear();
            txtCID.Clear(); 
            txtIncomeSource.Clear();
            dtpIncomeDate.Value = DateTime.Today;

            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;

            Auto(); 
        }

        
        private void DeleteRecord()
        {
           
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            // ── Validation ──
            if (string.IsNullOrWhiteSpace(txtIncomeCode.Text) ||
                string.IsNullOrWhiteSpace(txtIncomeSource.Text) ||
                !decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please fill in all fields correctly.",
                                "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"INSERT INTO Incomes (IncomeCode, IncomeSource, Amount, IncomeDate)
                         VALUES (@IncomeCode, @IncomeSource, @Amount, @IncomeDate)";

                DataAccessLayer.ExecuteNonQuery(query, CommandType.Text,
                     DataAccessLayer.CreateParameter("@IncomeCode", DbType.String, txtIncomeCode.Text.Trim()),
                     DataAccessLayer.CreateParameter("@IncomeSource", DbType.String, txtIncomeSource.Text.Trim()),
                     DataAccessLayer.CreateParameter("@Amount", DbType.Double, (double)amount),
                     DataAccessLayer.CreateParameter("@IncomeDate", DbType.String, dtpIncomeDate.Value.Date.ToString("yyyy-MM-dd"))
                 );

                MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ── Clear fields ──
                txtIncomeCode.Clear();
                txtIncomeSource.Clear();
                txtAmount.Clear();
                dtpIncomeDate.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data:\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
           
        }

        
        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        
        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void IncomeAmount_KeyPress(object sender, KeyPressEventArgs e) // Or new name if renamed
        {
            
            char keyChar = e.KeyChar;

            if (char.IsControl(keyChar))
            {
                
            }
            else if (char.IsDigit(keyChar) || keyChar == '.')
            {
                string text = this.txtAmount.Text; // Use correct control name
                int selectionStart = this.txtAmount.SelectionStart; // Use correct control name
                int selectionLength = this.txtAmount.SelectionLength; // Use correct control name

                text = text.Substring(0, selectionStart) + keyChar + text.Substring(selectionStart + selectionLength);

                
                if (text.Count(c => c == '.') > 1) // Prevent multiple dots
                {
                    e.Handled = true;
                }
                

            }
            else
            {
                
                e.Handled = true;
            }
        }


        
        private void button1_Click(object sender, EventArgs e)
        {
            TrackingIncomeScreen trackingIncomeScreen = new TrackingIncomeScreen();
           
            trackingIncomeScreen.Show(); // Show modally

        }


    }
}