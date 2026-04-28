using Personal_Budgeting;
using System.Data.SqlClient;

namespace Masroofy
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

            var providerStr = Masroofy.Properties.Settings.Default.Provider;

            var provider = providerStr switch
            {
                "SQLite" => DatabaseProvider.SQLite,
                "MySQL" => DatabaseProvider.MySQL,
                _ => DatabaseProvider.SqlServer   // default
            };

            DataAccessLayer.Configure(provider, "");
            Application.Run(new Dashbourd());
        }
    }
}