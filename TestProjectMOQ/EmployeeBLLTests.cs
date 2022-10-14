using Microsoft.EntityFrameworkCore;
using Moq;
using NTierReview.BLL;
using NTierReview.DAL;
using NTierReview.Data;
using NTierReview.Models;
using System;


namespace TestProjectMOQ
{
    [TestClass]
    public class EmployeeBLLTests
    {
        private EmployeeBusinessLogic EmployeeBusinessLogic;
        public EmployeeBLLTests()
        {
            var testData = new List<Employee> {
                new Employee{Id = 1, HiringDate = new DateTime(1990, 10, 12), FullName = "Long Timer", Salary = 300000 },

                new Employee{Id = 2, HiringDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), FullName = "New Person", Salary = 10000 },

                new Employee{Id = 3, HiringDate = new DateTime(2010, 5, 5), FullName = "Mid Timer", Salary = 30000 },

                new Employee{Id = 4, HiringDate = new DateTime(2010, 5, 5), FullName = "Other Mid Timer", Salary = 20000}
            }.AsQueryable();

            var mockEmployeeSet = new Mock<DbSet<Employee>>();

            mockEmployeeSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(testData.Provider);
            mockEmployeeSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(testData.Expression);
            mockEmployeeSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            mockEmployeeSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<NTierReviewContext>();
            mockContext.Setup(c => c.Employee).Returns(mockEmployeeSet.Object);

            EmployeeBusinessLogic = new EmployeeBusinessLogic(new EmployeeRepository(mockContext.Object));
        }

        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [TestMethod]
        public void CalculateAnnualBonus_WithYearsAndSalary_CalculatesYearTimesPercentage(int id)
        {
            decimal bonusValue = EmployeeBusinessLogic.CalculateAnnualBonus(id);

            Employee employee = EmployeeBusinessLogic.Repository.Get(id);
            
            TimeSpan timeSpan = DateTime.Now - employee.HiringDate;
            int years = (int)Math.Floor((double)timeSpan.Days / 365);
            decimal percentage = years / 100M;

            decimal expected = employee.Salary * percentage;

            Assert.AreEqual(expected, bonusValue);
        }

        [TestMethod]
        public void CalculateAnnualBonus_EmployeeNotFound_ThrowsException()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => EmployeeBusinessLogic.CalculateAnnualBonus(10));
        }


        [TestMethod]
        public void GetTopThreeSalaried_MethodCall_ReturnsThreeEmployeesSalaryDescending()
        {
            decimal expectedSalaryTotal = 350000M;

            Assert.AreEqual(expectedSalaryTotal, EmployeeBusinessLogic.GetTopThreeSalaried().Sum(e => e.Salary));
        }
    }
}