using Masroofy;
using Masroofy.Business.Services;
using Masroofy.Data.Database;
using Masroofy.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;

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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var providerStr = Masroofy.Data.Properties.Settings.Default.Provider;

            var provider = providerStr switch
            {
                "SQLite" => DatabaseProvider.SQLite,
                "MySQL" => DatabaseProvider.MySQL,
                _ => DatabaseProvider.SqlServer   // default
            };
                
            DataAccessLayer.Configure(provider, "");


            var services = new ServiceCollection();

            services.AddTransient<IBudgetCycleRepository, BudgetCycleRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<RolloverEngine>();
            services.AddTransient<BudgetService>();

            services.AddTransient<BudgetCycleForm>();
            services.AddTransient<Dashbourd>();

            var serviceProvider = services.BuildServiceProvider();

            Application.Run(serviceProvider.GetRequiredService<Dashbourd>());
        }
    }
}