using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using NTierReview.DAL;
using NTierReview.Models;
using System.Text;

namespace NTierReview.BLL
{
    public class EmployeeBusinessLogic
    {
        public IRepository<Employee> Repository { get; set; }
        public EmployeeBusinessLogic(IRepository<Employee> repository)
        {
            this.Repository = repository;
        }

        // CalculateAnnualBonus_EmployeeFound_ReturnsSalaryTimesPercentage
        // CalculateAnnualBonus_EmployeeNotFound_ThrowsKeyNotFoundException
        public decimal CalculateAnnualBonus(int employeeId)
        {
            Employee employee;

            try
            {
                employee = Repository.Get(employeeId);
            } catch (Exception ex)
            {
                throw new KeyNotFoundException();
            }

            TimeSpan timeSpan = DateTime.Now - employee.HiringDate;
            int years = (int)Math.Floor((double)timeSpan.Days / 365);

            decimal percentage = years / 100M;

            return percentage * employee.Salary;
        }

        public List<Employee> GetTopThreeSalaried()
        {
            // get list of top three
            List<Employee> topThree = Repository.GetAll().OrderByDescending(e => e.Salary).Take(3).ToList();

            return topThree;
        }
    }
}
