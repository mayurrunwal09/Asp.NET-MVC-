using Cascading_In_MVC_Using_Stored_Procedure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Cascading_In_MVC_Using_Stored_Procedure.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly MainDBContext _dbContext;
        public DepartmentController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var dep = await _dbContext.Departments.ToListAsync();
            return View(dep);
        }

        public async Task<IActionResult>AddOrEdit(int Id = 0)
        {
            if(Id== 0)
            {
                return View(new Department());
            }
            var emp = await _dbContext.Departments.FindAsync(Id);
            if(emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult>AddOrEdit(int id,Department department)
        {
            if(id== 0)
            {
                var dep = await _dbContext.CreateOrUpdateDepartment(department.DepId, department.DepName);
                if (dep != null)
                {
                    await _dbContext.SaveChangesAsync();
                }

            }
            else
            {
                try
                {
                    var dep = await _dbContext.CreateOrUpdateDepartment(department.DepId, department.DepName);
                    if (dep != null)
                    {
                        await _dbContext.SaveChangesAsync();
                    }
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!ExployeeExist(department .DepId ))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Departments .ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", department ) });

        }

        private bool ExployeeExist(int depId)
        {
            throw new NotImplementedException();
        }

        /* public async Task<IActionResult>DeleteDepartment(int Id)
         {
             if(Id  == 0)
             {
                 return NotFound();
             }
             var dep = await _dbContext.Departments.FindAsync(Id);
             if(dep == null)
             {
                 return NotFound();
             }
             return View(dep);
         }
         [HttpPost]
         public async Task<IActionResult>DeleteDepartment(int id,Department department)
         {
             if (id != null)
             {
                 *//* _dbContext.DeleteDepartment(department.DepId);
                  await _dbContext.SaveChangesAsync();*//*
                 var dep = await _dbContext.DeleteDepartment(department.DepId);
                 await _dbContext.SaveChangesAsync();
                 if(dep == null )
                 {
                     return NotFound();
                 }

             }
             return RedirectToAction("Index");

         }*/


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var emp = await _dbContext.Departments.FirstOrDefaultAsync(x => x.DepId  == id);
            _dbContext.Departments .Remove(emp);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", emp) });


        }
    }
}
