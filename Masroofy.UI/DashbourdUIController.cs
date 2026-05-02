using System;
using System.Windows.Forms;

namespace Masroofy.UI
{
    public class DashboardUIController
    {
        private readonly Dashbourd _dashboardForm;

        /// <summary>
        /// Constructor that takes the actual Dashboard form instance.
        /// </summary>
        public DashboardUIController(Dashbourd dashboardForm)
        {
            _dashboardForm = dashboardForm;
        }

        /// <summary>
        /// This method fulfills the 'updateDashboard()' requirement in the US #2 sequence diagram.
        /// </summary>
        public void UpdateDashboard()
        {
            // We check InvokeRequired to ensure that if the save happened on a background thread,
            // we switch back to the UI thread before trying to update labels.
            if (_dashboardForm.InvokeRequired)
            {
                _dashboardForm.Invoke(new Action(() => _dashboardForm.RefreshData()));
            }
            else
            {
                // This calls the RefreshData() method we just added to your Dashbourd.cs
                _dashboardForm.RefreshData();
            }
        }
    }
}