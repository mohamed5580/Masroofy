using System;

using System.Threading.Tasks;

using Masroofy.Business.Services;

using Masroofy.Data.Repositories;

using Masroofy.Data.Models;



namespace Masroofy.UI

{

    public class LoggingUIController

    {

        private readonly ValidationService _validationService;

        private readonly ITransactionRepository _repository;

        private readonly BudgetService _budgetService;

        private readonly DashboardUIController _dashboardController;



        public LoggingUIController(ValidationService vs, ITransactionRepository repo, BudgetService bs, DashboardUIController dc)

        {

            _validationService = vs;

            _repository = repo;

            _budgetService = bs;

            _dashboardController = dc;

        }



        public async Task<bool> OnSaveTapped(string amountText, int categoryId, int budgetCycleId)

        {

            if (decimal.TryParse(amountText, out decimal amount))

            {

                // Call your ValidationService.IsValidAmount

                if (_validationService.IsValidAmount(amount))

                {

                    var transaction = new Transaction

                    {

                        Amount = amount,

                        Timestamp = DateTime.Now,

                        CategoryId = categoryId,

                        BudgetCycleId = budgetCycleId

                    };



                    // This calls your Repository's AddAsync method

                    await _repository.AddAsync(transaction);



                    // This calls your BudgetService's Recalculate logic

                    await _budgetService.RecalculateAfterExpenseAsync(budgetCycleId);



                    // This triggers the Dashboard refresh

                    _dashboardController.UpdateDashboard();



                    return true;

                }

            }

            return false;

        }

    }

}