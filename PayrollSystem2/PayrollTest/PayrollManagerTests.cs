using PayrollBL;
using PayrollEntities;
using System.Configuration;

namespace PayrollTest
{
    [TestClass]
    public class PayrollManagerTests
    {
        private readonly string connection = ConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString;

        [TestMethod]
        public void GetHighEarners_FiltersAndSortsCorrectly()
        {
            var objPayrollManager = new PayrollManager();
            decimal threshold = 4000m;

            var employees = objPayrollManager.GetAllEmployees();

            /*var employees = new List<IEmployee>
            {
                new SalariedEmployee { Id = 1, Name = "Nelson", MonthlySalary = 5000m },
                new HourlyEmployee   { Id = 2, Name = "Mercedes", HourlyRate = 30m, HoursWorked = 160m }, // 4800
                new SalariedEmployee { Id = 3, Name = "Sonia", MonthlySalary = 3000m },
                new HourlyEmployee   { Id = 4, Name = "Mateo", HourlyRate = 25m, HoursWorked = 200m }, // 5000
                new HourlyEmployee   { Id = 5, Name = "Valeria", HourlyRate = 15m, HoursWorked = 100m }  // 1500
            };*/

            var highEarners = objPayrollManager.GetHighEarners(employees, threshold);

            // Expect 3 employees above 4000: Nelson (5000), Mateo (5000), Mercedes (4800)
            Assert.AreEqual(3, highEarners.Count);

            // Ensure sorted descending by pay
            Assert.IsTrue(highEarners[0].CalculatePay() >= highEarners[1].CalculatePay());
            Assert.IsTrue(highEarners[1].CalculatePay() >= highEarners[2].CalculatePay());

            // Ensure returned set contains expected Ids
            CollectionAssert.AreEquivalent(new List<int> { 1, 4, 2 }, highEarners.Select(e => e.Id).ToList());
        }
    }
}
