using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollBL
{
    public class SalariedEmployee : IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MonthlySalary { get; set; }

        public decimal CalculatePay()
        {
            return MonthlySalary;
        }
    }
}
