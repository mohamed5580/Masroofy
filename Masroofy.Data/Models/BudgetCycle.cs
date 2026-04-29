using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masroofy.Data.Models
{
    public class BudgetCycle
    {
        public int Id { get; set; }
        public decimal TotalAllowance { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
