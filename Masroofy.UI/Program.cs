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


            var providerStr = Masroofy.Data.Properties.Settings.Default.Provider;

            var provider = providerStr switch
            {
                "SQLite" => DatabaseProvider.SQLite,
                "MySQL" => DatabaseProvider.MySQL,
                _ => DatabaseProvider.SqlServer
            };

            DataAccessLayer.Configure(provider, "");

            var services = new ServiceCollection();

            services.AddTransient<IBudgetCycleRepository, BudgetCycleRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<RolloverEngine>();
            services.AddTransient<BudgetService>();
            services.AddTransient<ValidationService>();

            
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
                sp.GetRequiredService<IBudgetCycleRepository>(),
                sp.GetRequiredService<BudgetService>()
            ));
            services.AddTransient<Transactions>(sp => new Transactions(
            sp.GetRequiredService<ITransactionRepository>(),
            sp.GetRequiredService<BudgetService>(),
            sp.GetRequiredService<StatisticsDashbourd>(),
            sp.GetRequiredService<IBudgetCycleRepository>()

             ));
            services.AddSingleton<StatisticsDashbourd>(sp => new StatisticsDashbourd(sp));

            services.AddSingleton<Dashbourd>(sp => new Dashbourd(sp, sp.GetRequiredService<IBudgetCycleRepository>()));

            services.AddSingleton<Setting>(sp => new Setting(sp));

            var serviceProvider = services.BuildServiceProvider();


            if (Masroofy.Data.Properties.Settings.Default.PINCheck)
            {

                using (var pinForm = new PIN()) 
                {
                    if (pinForm.ShowDialog() != DialogResult.OK)
                    {
                        return;  
                    }
                }
            }

            Application.Run(serviceProvider.GetRequiredService<Dashbourd>());

        }
    }
}