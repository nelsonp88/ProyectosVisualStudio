using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollBL
{
    public class HourlyEmployee : IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }
        public decimal HoursWorked { get; set; }


        public decimal CalculatePay()
        {
            return HourlyRate * HoursWorked;
        }
    }
}
