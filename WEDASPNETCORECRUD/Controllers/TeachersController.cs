using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEDASPNETCORECRUD.Data;
using WEDASPNETCORECRUD.Models;
using WEDASPNETCORECRUD.Models.Domail;

namespace WEDASPNETCORECRUD.Controllers
{
    public class TeachersController : Controller
    {
        private readonly MVCDemoDbContext mvcDemoDbContext;

        public TeachersController(MVCDemoDbContext mvcDemoDbContext)
        {
            this.mvcDemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var teachers = await mvcDemoDbContext.Teachers.ToListAsync();
            return View(teachers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTeacherViewModel addTeacherRequest)
        {
            var teacher = new Teacher()
            {
                Id = Guid.NewGuid(),
                Name = addTeacherRequest.Name,
                Email = addTeacherRequest.Email,
                Salary = addTeacherRequest.Salary,
                DateOfBirth = addTeacherRequest.DateOfBirth,
                Deparment = addTeacherRequest.Deparment,
            };

            await mvcDemoDbContext.Teachers.AddAsync(teacher);
            await mvcDemoDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id) 
        {
            var teacher = await mvcDemoDbContext.Teachers.FirstOrDefaultAsync(x => x.Id == id);

            if (teacher != null)
            {
                var viewModel = new UpdateTeacherViewModel()
                {
                    Id = teacher.Id, 
                    Name = teacher.Name,
                    Email = teacher.Email,
                    Salary = teacher.Salary,
                    DateOfBirth= teacher.DateOfBirth,
                    Deparment = teacher.Deparment,
                };

                return await Task.Run(() => View("View",viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateTeacherViewModel model)
        {
            var teacher = await mvcDemoDbContext.Teachers.FindAsync(model.Id);

            if (teacher != null)
            {
                teacher.Name = model.Name;
                teacher.Email = model.Email;
                teacher.Salary = model.Salary;
                teacher.DateOfBirth = model.DateOfBirth;
                teacher.Deparment = model.Deparment;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateTeacherViewModel model)
        {
            var teacher = await mvcDemoDbContext.Teachers.FindAsync(model.Id);

            if(teacher != null)
            {
                mvcDemoDbContext.Teachers.Remove(teacher);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
    }
}
