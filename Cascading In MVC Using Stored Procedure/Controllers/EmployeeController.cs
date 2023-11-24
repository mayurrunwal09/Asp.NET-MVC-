using Cascading_In_MVC_Using_Stored_Procedure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cascading_In_MVC_Using_Stored_Procedure.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MainDBContext _dbContext;
        public EmployeeController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var dep = await _dbContext.Employees.Include(d=>d.Department).Include(d=>d.Country)
                .Include(d=>d.State)
                .Include(d=>d.City ).ToListAsync();
            return View(dep);
        }

        public async Task<IActionResult> AddOrEdit(int Id = 0)
        {
            if (Id == 0)
            {
                ViewBag.Departments = _dbContext.Departments.ToList();
                ViewBag.Countries = _dbContext.Countries.ToList();
                ViewBag.Statements = _dbContext.Statements.ToList();


                ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName");
                ViewData["StateId"] = new SelectList(_dbContext.Statements , "StateId", "StateName");
                ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");
                ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName");
                return View(new Employee());
            }
            var emp = await _dbContext.Employees.FindAsync(Id);
            if (emp == null)
            {
               
               
                return NotFound();
            }
            ViewBag.Departments = _dbContext.Departments.ToList();
            ViewBag.Countries = _dbContext.Countries.ToList();
            ViewBag.Statements = _dbContext.Statements.ToList();

            ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName");
            ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");
            ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");
            ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName");
            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("EmpId,EmpName,Age,MobileNo,DepId,CountryId,StateId,CityId")] Employee department)
        {
            if (id == 0)
            {
                var dep = await _dbContext.CreateOrUpdateEmployee(department.EmpId, department.EmpName, department.Age,  department.MobileNo, department.DepId, department.CountryId, department.StateId, department.CityId);
                if (dep != null)
                {
                    ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName", department.CountryId);
                    ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName", department.StateId);
                    ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName", department.CityId);
                    ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName", department.DepId);
                    await _dbContext.SaveChangesAsync();
                }

            }
            else
            {
                try
                {
                    var dep = await _dbContext.CreateOrUpdateEmployee(department.EmpId, department.EmpName, department.Age, department.MobileNo, department.DepId, department.CountryId, department.StateId, department.CityId);
                    if (dep != null)
                    {
                        ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName", department.CountryId);
                        ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName", department.StateId);
                        ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName", department.CityId);
                        ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName", department.DepId);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExployeeExist(department.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Employees.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", department) });

        }



        private bool ExployeeExist(int depId)
        {
            throw new NotImplementedException();
        }

       


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var emp = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmpId == id);
            _dbContext.Employees .Remove(emp);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", emp) });


        }

        [HttpGet]
        public JsonResult GetCitiesByState(int stateId)
        {
            var cities = _dbContext.Cities.Where(c => c.StateId == stateId).ToList();
            return Json(cities);
        }

        [HttpGet]
        public JsonResult GetStatesByCountry(int countryId)
        {
            var states = _dbContext.Statements .Where(s => s.CountryId == countryId).ToList();
            return Json(states);
        }
    }
}
