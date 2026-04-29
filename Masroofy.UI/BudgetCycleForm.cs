using Masroofy.Business.Services;
using Masroofy.Data.Database;
using Masroofy.Data.Models;
using Masroofy.Data.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Masroofy
{
    public partial class BudgetCycleForm : Form
    {
        private readonly IBudgetCycleRepository _budgetCycleRepository;
        private readonly BudgetService _budgetService;
        private bool _isEditMode;
        private static BudgetCycleForm _instance;
        public static BudgetCycleForm Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BudgetCycleForm();
                }
                return _instance;
            }
        }

        private BudgetCycleForm()
        {
            InitializeComponent();
            _instance = this;
        }

        public BudgetCycleForm(IBudgetCycleRepository cycleRepository, BudgetService budgetService) : this()
        {
            _budgetCycleRepository = cycleRepository;
            _budgetService = budgetService;
            ResetForm();
        }

        public async void LoadCycleForEdit(int cycleId)
        {
            var cycle = await _budgetCycleRepository.GetByIdAsync(cycleId);
            if (cycle != null)
            {

                txtAmount.Text = cycle.TotalAllowance.ToString();
                StartDate.Value = cycle.StartDate;
                EndDate.Value = cycle.EndDate;
                _isEditMode = true;
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void ResetForm()
        {
            txtAmount.Clear();
            StartDate.Value = DateTime.Today;
            EndDate.Value = DateTime.Today;
            _isEditMode = false;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ValidateInput()
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("المبلغ غير صحيح", "تحقق", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (EndDate.Value <= StartDate.Value)
            {
                MessageBox.Show("تاريخ النهاية يجب أن يكون بعد تاريخ البداية", "تحقق", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // CREATE
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                await _budgetCycleRepository.DeactivateCurrentCycleAsync();

                var cycle = new BudgetCycle
                {
                    TotalAllowance = decimal.Parse(txtAmount.Text),
                    StartDate = StartDate.Value,
                    EndDate = EndDate.Value,
                    IsActive = true
                };


                int newId = await _budgetService.CreateCycleAsync(cycle);


                MessageBox.Show($"تم حفظ الميزانية",
                    "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // UPDATE
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (!ValidateInput()) return;

            try
            {

                BudgetCycle newbudget = new BudgetCycle
                {
                    Id = Convert.ToInt32(txtID.Text),
                    TotalAllowance = decimal.Parse(txtAmount.Text),
                    StartDate = StartDate.Value,
                    EndDate = EndDate.Value,
                    IsActive = true
                };

                await _budgetCycleRepository.UpdateAsync(newbudget);
                MessageBox.Show("تم التحديث", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DELETE
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtID == null)
            {
                MessageBox.Show("لا توجد ميزانية محددة للحذف", "تنبيه");
                return;
            }

            if (MessageBox.Show("هل أنت متأكد من الحذف؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    await _budgetCycleRepository.DeleteAsync(Convert.ToInt32(txtID.Text));
                    MessageBox.Show("تم الحذف", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e) => ResetForm();

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && txtAmount.Text.Contains('.'))
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BudgetCycleScreen trackingIncomeScreen = new BudgetCycleScreen();

            trackingIncomeScreen.Show();

        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BudgetCycleForm_Load(object sender, EventArgs e)
        {

        }
    }
}