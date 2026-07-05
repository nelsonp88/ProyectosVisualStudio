using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollBL
{
    public interface IEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        decimal CalculatePay();
    }
}
