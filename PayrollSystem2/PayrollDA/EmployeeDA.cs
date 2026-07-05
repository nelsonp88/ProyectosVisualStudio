using PayrollEntities;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PayrollDA
{
    public class EmployeeDA
    {
        public EmployeeDA() { }

        private readonly string connection = ConfigurationManager.ConnectionStrings["sqlDB"].ConnectionString;

        public List<IEmployee> GetAllEmployees()
        {
            return new List<IEmployee>
            {
                new SalariedEmployee { Id = 1, Name = "Nelson", MonthlySalary = 5000m },
                new HourlyEmployee   { Id = 2, Name = "Mercedes", HourlyRate = 30m, HoursWorked = 160m }, // 4800
                new SalariedEmployee { Id = 3, Name = "Sonia", MonthlySalary = 3000m },
                new HourlyEmployee   { Id = 4, Name = "Mateo", HourlyRate = 25m, HoursWorked = 200m }, // 5000
                new HourlyEmployee   { Id = 5, Name = "Valeria", HourlyRate = 15m, HoursWorked = 100m }  // 1500
            };
        }

        public List<IEmployee> GetAllEmployeesDB()
        {
            List<IEmployee> employees = new List<IEmployee>();

            SqlConnection conn = new SqlConnection(connection);
            conn.Open();

            SqlCommand cmdSalariedEmployee = new SqlCommand("SELECT Id, Name, MonthlySalary FROM SalariedEmployee", conn);
            SqlCommand cmdHourlyEmployee = new SqlCommand("SELECT Id, Name, HourlyRate, HoursWorked FROM HourlyEmployee", conn);

            SqlDataReader readerSalariedEmployee = cmdSalariedEmployee.ExecuteReader();

            while (readerSalariedEmployee.Read())
            {
                SalariedEmployee salariedEmployee = new SalariedEmployee
                {
                    /*Id = readerSalariedEmployee.GetInt32(0),
                    Name = readerSalariedEmployee.GetString(1),
                    MonthlySalary = readerSalariedEmployee.GetDecimal(2)*/

                    Id = int.Parse(readerSalariedEmployee["Id"].ToString()),
                    Name = readerSalariedEmployee["Name"].ToString(),
                    MonthlySalary = decimal.Parse(readerSalariedEmployee["MonthlySalary"].ToString())
                };
                employees.Add(salariedEmployee);
            }
            readerSalariedEmployee.Close();

            SqlDataReader readerHourlyEmployee = cmdHourlyEmployee.ExecuteReader();

            while (readerHourlyEmployee.Read())
            {
                HourlyEmployee hourlyEmployee = new HourlyEmployee
                {
                    /*Id = readerHourlyEmployee.GetInt32(0),
                    Name = readerHourlyEmployee.GetString(1),
                    HourlyRate = readerHourlyEmployee.GetDecimal(2),
                    HoursWorked = readerHourlyEmployee.GetDecimal(3)*/

                    Id = int.Parse(readerHourlyEmployee["Id"].ToString()),
                    Name = readerHourlyEmployee["Name"].ToString(),
                    HourlyRate = decimal.Parse(readerHourlyEmployee["HourlyRate"].ToString()),
                    HoursWorked = decimal.Parse(readerHourlyEmployee["HoursWorked"].ToString())
                };
                employees.Add(hourlyEmployee);
            }
            readerHourlyEmployee.Close();

            conn.Close();

            return employees.OrderBy(x => x.Id).ToList();
        }
    }
}
