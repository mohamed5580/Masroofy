using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masroofy.Data.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int CategoryId { get; set; }
        public int BudgetCycleId { get; set; }

        public int TransactionId { get; set; }

        public string CategoryName { get; set; }

        public int CycleId { get; set; }

    }
}
