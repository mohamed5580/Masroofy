using Masroofy;
using Masroofy.Business.Services;
using Masroofy.Data.Database;
using Masroofy.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace Masroofy.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- Database Configuration ---
            var providerStr = Masroofy.Data.Properties.Settings.Default.Provider;

            var provider = providerStr switch
            {
                "SQLite" => DatabaseProvider.SQLite,
                "MySQL" => DatabaseProvider.MySQL,
                _ => DatabaseProvider.SqlServer   // default
            };

            DataAccessLayer.Configure(provider, "");
            Task.Run(async () => await DataAccessLayer.SeedCategoriesAsync()).GetAwaiter().GetResult();

            // --- Dependency Injection Registration ---
            var services = new ServiceCollection();

            // 1. Data Layer Registrations
            services.AddTransient<IBudgetCycleRepository, BudgetCycleRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            // 2. Business Layer Registrations
            services.AddTransient<RolloverEngine>();
            services.AddTransient<BudgetService>();
            services.AddTransient<ValidationService>(); // Added: Required for LoggingUIController

            // 3. UI Controller Registrations (The Bridge)
            // These allow your forms to talk to your services cleanly
            services.AddTransient<LoggingUIController>();   // Added: Required for ExpenseEntryScreen
            services.AddTransient<DashboardUIController>(); // Added: Required for Dashbourd refreshes

            // 4. UI Forms Registrations
            services.AddTransient<BudgetCycleForm>();
            services.AddTransient<Dashbourd>();
            services.AddTransient<ExpenseEntryScreen>();
            services.AddTransient<StatisticsDashbourd>();

            // Build the provider
            var serviceProvider = services.BuildServiceProvider();

            // Run the application starting with the Dashboard
            Application.Run(serviceProvider.GetRequiredService<Dashbourd>());
        }
    }
}