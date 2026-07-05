namespace PayrollBL
{
    public class PayrollManager
    {
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
    }
}
