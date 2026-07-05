using PayrollDA;
using PayrollEntities;
using System.Collections.Generic;
using System.Linq;

namespace PayrollBL
{
    public class PayrollManager
    {
        public List<IEmployee> GetAllEmployees()
        {
            var employeeDA = new PayrollDA.EmployeeDA();
            return employeeDA.GetAllEmployeesDB();
        }

        public List<IEmployee> GetHighEarners(IEnumerable<IEmployee> employees, decimal threshold)
        {
            if (employees == null)
            {
                return new List<IEmployee>();
            }

            return employees
                .Where(e => e.CalculatePay() > threshold)
                .OrderByDescending(e => e.CalculatePay())
                .ToList();
        }

        /*public List<IEmployee> GetHighEarners(IEnumerable<IEmployee> employees, decimal threshold)
        {
            if (employees == null)
            {
                return new List<IEmployee>();
            }

            return employees
                .Where(e => e.CalculatePay() > threshold)
                .OrderByDescending(e => e.CalculatePay())
                .ToList();
        }*/
    }
}
