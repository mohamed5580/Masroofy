using Masroofy;
using Masroofy.Business.Services;
using Masroofy.Data.Database;
using Masroofy.Data.Repositories;
using Masroofy.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Masroofy.UI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var providerStr = Masroofy.Data.Properties.Settings.Default.Provider;
            var provider = providerStr switch
            {
                "SQLite" => DatabaseProvider.SQLite,
                "MySQL" => DatabaseProvider.MySQL,
                _ => DatabaseProvider.SqlServer
            };

            DataAccessLayer.Configure(provider, "");
            Task.Run(async () => await DataAccessLayer.SeedCategoriesAsync()).GetAwaiter().GetResult();

            var services = new ServiceCollection();

            // ── Core layers ────────────────────────────────────────────────────
            services.AddTransient<IBudgetCycleRepository, BudgetCycleRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<RolloverEngine>();
            services.AddTransient<BudgetService>();
            services.AddTransient<ValidationService>();

            // ── Forms ──────────────────────────────────────────────────────────
            // FIX: StatisticsDashbourd MUST be Singleton.
            // If it is Transient, every GetRequiredService<StatisticsDashbourd>()
            // returns a NEW blank form — the user never sees the one with data,
            // and LoggingUIController cannot reference the live instance.
            services.AddSingleton<StatisticsDashbourd>(sp => new StatisticsDashbourd(sp));

            // FIX: ExpenseEntryScreen gets the singleton StatisticsDashbourd injected
            // so its internal LoggingUIController can call RefreshDashboardData()
            // on the exact form the user is currently looking at.
            services.AddTransient<ExpenseEntryScreen>(sp => new ExpenseEntryScreen(
                sp.GetRequiredService<ValidationService>(),
                sp.GetRequiredService<ITransactionRepository>(),
                sp.GetRequiredService<BudgetService>(),
                sp.GetRequiredService<IBudgetCycleRepository>(),
                sp.GetRequiredService<StatisticsDashbourd>()   // <-- live dashboard reference
            ));

            services.AddTransient<BudgetCycleForm>(sp => new BudgetCycleForm(
                sp.GetRequiredService<IBudgetCycleRepository>(),
                sp.GetRequiredService<BudgetService>()
            ));
            services.AddTransient<BudgetCycleScreen>(sp => new BudgetCycleScreen(
                sp.GetRequiredService<IBudgetCycleRepository>()
            ));

            services.AddSingleton<Dashbourd>(sp => new Dashbourd(sp));

            var serviceProvider = services.BuildServiceProvider();

            Application.Run(serviceProvider.GetRequiredService<Dashbourd>());
        }
    }
}