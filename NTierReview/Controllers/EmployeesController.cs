using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NTierReview.BLL;
using NTierReview.DAL;
using NTierReview.Data;
using NTierReview.Models;

namespace NTierReview.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeBusinessLogic employeeBusinessLogic;

        public EmployeesController(NTierReviewContext context)
        {
            employeeBusinessLogic = new EmployeeBusinessLogic(new EmployeeRepository(context));
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(employeeBusinessLogic.Repository.GetAll());
        }

        public IActionResult TopThreeSalaried()
        {
            return View(employeeBusinessLogic.GetTopThreeSalaried());
        }

           
    }
}
