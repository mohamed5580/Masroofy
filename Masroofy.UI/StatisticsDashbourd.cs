using Masroofy.Business.Services;
using Masroofy.Data.Repositories;
using Masroofy.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Masroofy
{
    public partial class StatisticsDashbourd : Form
    {
        // ── State ─────────────────────────────────────────────────────────────
        private Dictionary<string, decimal> _chartData = new();
        private decimal _totalSpent = 0;
        private float _safeDailyLimit = 0;
        private bool _isTightBudget = false;   // true = exceptional scenario

        private readonly IServiceProvider _serviceProvider;
        private Label _finalDayBadge;

        // ── Fixed colors per category (must match BudgetService.MapIdToCategoryName)
        private static readonly Dictionary<string, Color> CategoryColors = new()
        {
            { "Food",          Color.LimeGreen    },
            { "Transport",     Color.DodgerBlue   },
            { "Entertainment", Color.MediumPurple },
            { "Utilities",     Color.Orange       },
            { "Other",         Color.Coral        }
        };

        // ── Constructor ───────────────────────────────────────────────────────
        public StatisticsDashbourd(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            BuildFinalDayBadge();

            // Load dashboard data automatically when the form first opens
            this.Load += StatisticsDashbourd_Load;

            // Refresh automatically whenever a budget cycle is saved, updated, or deleted
            BudgetCycleForm.CycleChanged += (s, e) => RefreshDashboardData();

            // FIX: hide instead of dispose so the DI singleton stays alive
            this.FormClosing += (s, e) => { e.Cancel = true; this.Hide(); };
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Sequence-diagram methods called by DashboardUIController
        // ═════════════════════════════════════════════════════════════════════

        // DashboardScreen.refresh()
        public new void Refresh()
        {
            _safeDailyLimit = 0;
            _isTightBudget = false;
            _finalDayBadge.Visible = false;
            _chartData = new Dictionary<string, decimal>();
            _totalSpent = 0;
            pnlLimitCircle.Invalidate();
            pnlPieChart.Invalidate();
        }

        // DashboardScreen.display(safeDailyLimit, remainingBalance, categoryTotals)
        public void Display(float safeDailyLimit, float remainingBalance,
                            Dictionary<string, decimal> categoryTotals)
        {
            _safeDailyLimit = safeDailyLimit;

            // ── Exceptional scenario detection ────────────────────────────────
            // A "negative rollover" / deficit means the user overspent a previous
            // day, so the remaining balance divided by remaining days is noticeably
            // lower than it should be.  We signal this when remainingBalance < 0
            // OR when the daily limit has been pushed below a meaningful threshold
            // (i.e. the limit shrank because of yesterday's overspend).
            // The simplest reliable signal: remainingBalance went negative.
            _isTightBudget = remainingBalance < 0;

            // Feed pie chart
            _chartData = categoryTotals ?? new Dictionary<string, decimal>();
            _totalSpent = 0;
            foreach (var v in _chartData.Values) _totalSpent += v;

            // Repaint both custom panels
            pnlLimitCircle.Invalidate();
            pnlPieChart.Invalidate();
        }

        // DashboardScreen.showFinalDayBadge()  [opt – last day of cycle]
        public void ShowFinalDayBadge()
        {
            _finalDayBadge.Visible = true;
            _finalDayBadge.BringToFront();
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Double-circle painter  (pnlLimitCircle)
        // ═════════════════════════════════════════════════════════════════════
        private void pnlLimitCircle_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int w = pnlLimitCircle.Width;   // 160
            int h = pnlLimitCircle.Height;  // 160

            // ── Ring colours ──────────────────────────────────────────────────
            // Normal:      white fill,  white outer ring,  white inner ring
            // Tight budget (exceptional scenario): orange rings to signal deficit
            Color outerRingColor = _isTightBudget ? Color.OrangeRed : Color.White;
            Color innerRingColor = _isTightBudget ? Color.Orange : Color.White;
            Color fillColor = _isTightBudget ? Color.FromArgb(255, 255, 235, 205)
                                                  : Color.White;
            Color textColor = _isTightBudget ? Color.OrangeRed : Color.DarkViolet;

            int outerThickness = 6;
            int gap = 8;   // gap between outer and inner ring
            int innerThickness = 4;

            // Outer circle
            var outerRect = new Rectangle(1, 1, w - 3, h - 3);
            using (var outerPen = new Pen(outerRingColor, outerThickness))
                g.DrawEllipse(outerPen, outerRect);

            // Inner circle (inset by gap + thickness)
            int inset = outerThickness + gap;
            var innerRect = new Rectangle(inset, inset, w - inset * 2, h - inset * 2);

            // Fill the inner circle
            using (var fillBrush = new SolidBrush(fillColor))
                g.FillEllipse(fillBrush, innerRect);

            using (var innerPen = new Pen(innerRingColor, innerThickness))
                g.DrawEllipse(innerPen, innerRect);

            // ── Text inside inner circle ──────────────────────────────────────
            // Line 1: "Today's Limit"  (small)
            // Line 2: "50 EGP"         (large, bold)
            // Line 3: "⚠ Tight Budget" (only in exceptional scenario)

            var innerBounds = new RectangleF(
                innerRect.X, innerRect.Y,
                innerRect.Width, innerRect.Height);

            // "Today's Limit" label
            using var smallFont = new Font("Segoe UI", 7.5f, FontStyle.Regular);
            using var bigFont = new Font("Segoe UI", 16f, FontStyle.Bold);
            using var warnFont = new Font("Segoe UI", 7f, FontStyle.Bold);
            using var textBrush = new SolidBrush(textColor);
            using var grayBrush = new SolidBrush(Color.Gray);
            var centred = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Shift label upward, amount in centre, warning below
            float cy = innerRect.Y + innerRect.Height / 2f;

            // "Today's Limit"
            g.DrawString("Today's Limit", smallFont, grayBrush,
                new RectangleF(innerRect.X, innerRect.Y + 14,
                               innerRect.Width, 18), centred);

            // Amount
            string amountText = _safeDailyLimit == 0
                ? "-- EGP"
                : $"{_safeDailyLimit:N2} EGP";

            g.DrawString(amountText, bigFont, textBrush,
                new RectangleF(innerRect.X, cy - 16,
                               innerRect.Width, 36), centred);

            // Exceptional scenario warning line
            if (_isTightBudget)
            {
                using var warnBrush = new SolidBrush(Color.OrangeRed);
                g.DrawString("⚠ Tight Budget", warnFont, warnBrush,
                    new RectangleF(innerRect.X, innerRect.Bottom - 26,
                                   innerRect.Width, 20), centred);
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        //  Pie chart + legend painter  (pnlPieChart)
        // ═════════════════════════════════════════════════════════════════════
        private void pnlPieChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int panelW = pnlPieChart.Width;
            int panelH = pnlPieChart.Height;
            int pieSize = Math.Min(panelW - 20, (int)(panelH * 0.55));
            int pieX = (panelW - pieSize) / 2;
            int pieY = 10;
            var pieRect = new Rectangle(pieX, pieY, pieSize, pieSize);

            int legendY = pieY + pieSize + 14;
            int swatchSize = 14;
            int rowHeight = 22;
            using var legendFont = new Font("Segoe UI", 9f);

            // ── No data ───────────────────────────────────────────────────────
            if (_totalSpent <= 0 || _chartData.Count == 0)
            {
                g.FillEllipse(Brushes.LightGray, pieRect);
                g.DrawEllipse(Pens.Gray, pieRect);
                using var msgFont = new Font("Segoe UI", 10f, FontStyle.Italic);
                using var grayBrush = new SolidBrush(Color.Gray);
                var fmt = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString("No expenses logged yet", msgFont, grayBrush,
                    new RectangleF(0, legendY, panelW, 30), fmt);
                return;
            }

            // ── Slices ────────────────────────────────────────────────────────
            float startAngle = 0f;
            foreach (var entry in _chartData)
            {
                if (entry.Value <= 0) continue;
                Color c = GetCategoryColor(entry.Key);
                float sweep = (float)(entry.Value / _totalSpent) * 360f;
                using var brush = new SolidBrush(c);
                g.FillPie(brush, pieRect, startAngle, sweep);
                startAngle += sweep;
            }
            g.DrawEllipse(Pens.Black, pieRect);

            // ── Legend ────────────────────────────────────────────────────────
            int legendBlockW = 200;
            int legendX = (panelW - legendBlockW) / 2;
            int row = 0;

            foreach (var entry in _chartData)
            {
                if (entry.Value <= 0) continue;
                Color c = GetCategoryColor(entry.Key);
                int rowY = legendY + row * rowHeight;

                using var swatchBrush = new SolidBrush(c);
                g.FillRectangle(swatchBrush, legendX, rowY + 2, swatchSize, swatchSize);
                g.DrawRectangle(Pens.Black, legendX, rowY + 2, swatchSize, swatchSize);

                decimal pct = _totalSpent > 0
                    ? Math.Round(entry.Value / _totalSpent * 100, 1) : 0;
                string label = $"{entry.Key}  {entry.Value:N2} EGP ({pct}%)";

                using var textBrush = new SolidBrush(Color.Black);
                g.DrawString(label, legendFont, textBrush,
                    legendX + swatchSize + 6, rowY);
                row++;
            }
        }

        private static Color GetCategoryColor(string name) =>
            CategoryColors.TryGetValue(name, out var c) ? c : Color.LightGray;

        // ═════════════════════════════════════════════════════════════════════
        //  Data loading
        // ═════════════════════════════════════════════════════════════════════
        private void StatisticsDashbourd_Load(object sender, EventArgs e) =>
            RefreshDashboardData();

        public async void RefreshDashboardData()
        {
            try
            {
                var budgetService = _serviceProvider.GetRequiredService<BudgetService>();
                var cycleRepo = _serviceProvider.GetRequiredService<IBudgetCycleRepository>();

                var activeCycle = await cycleRepo.GetActiveCycleAsync();
                if (activeCycle == null)
                {
                    Refresh();
                    // Show "--" in the circle instead of "Loading..."
                    _safeDailyLimit = 0;
                    pnlLimitCircle.Invalidate();
                    return;
                }

                var controller = new DashboardUIController(budgetService, this);
                await controller.OnOpen(activeCycle.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dashboard refresh failed:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Log Expense button ────────────────────────────────────────────────
        private void button1_Click(object sender, EventArgs e)
        {
            var entryScreen = _serviceProvider.GetRequiredService<ExpenseEntryScreen>();
            entryScreen.ShowDialog();
            RefreshDashboardData();
        }

        // ── Final Day badge ───────────────────────────────────────────────────
        private void BuildFinalDayBadge()
        {
            _finalDayBadge = new Label
            {
                Text = "⚠  FINAL DAY",
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                AutoSize = true,
                Padding = new Padding(6, 4, 6, 4),
                Visible = false,
                Location = new Point(160, 190)
            };
            panel4.Controls.Add(_finalDayBadge);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var trans = _serviceProvider.GetRequiredService<Transactions>();
            trans.Show();
        }
    }
}