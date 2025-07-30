using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Company.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


public class DepartmentController : Controller
{
    private readonly CompanyDbContext _context;

    public DepartmentController(CompanyDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await _context.Departments.Include(d => d.Employees).ToListAsync();
        return View(departments);
    }

    public ActionResult Delete(int id)
    {
        Department department = _context.Departments.Find(id);
        _context.Departments.Remove(department);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult Create()
    {
        var model = new DepartmentVM
        {
            Employees = new List<EmployeeVM>() //  ÷„«‰ ⁄œ„ ÊÃÊœ null
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentVM model)
    {

        var department = new Department
        {
            Description = model.Description,
            Name = model.Name,
        };
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();


        foreach (var item in model.Employees)
        {
            var employee = new Employee
            {
                Name = item.Name,
                IsManger = item.IsManger,
                IsMember = item.IsMember,
                DepartmentId = department.Id
            };

            _context.Employees.Add(employee);
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));


        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var department = await _context.Departments
            .Include(d => d.Employees) // Include employees in the query
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(DepartmentVM department)
    {

        var existingDepartment = await _context.Departments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.Id == department.Id);

        if (existingDepartment == null)
        {
            return NotFound();
        }

        existingDepartment.Name = department.Name;
        existingDepartment.Description = department.Description;
        foreach (var item in department.Employees)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.Id == item.Id);

            if (existingEmployee != null)
            {
                existingEmployee.Name = item.Name;
                existingEmployee.IsManger = item.IsManger;
                existingEmployee.IsMember = item.IsMember;

            }

        }


        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> GetById(int id)
    {
        var department = await _context.Departments
        .Include(d => d.Employees) //  Õ„Ì· «·„ÊŸ›Ì‰ „⁄ «·ﬁ”„
        .Where(d => d.Id == id)
        .Select(x => new DepartmentVM
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Employees = x.Employees.Select(y => new EmployeeVM()
            {
                Id = y.Id,
                Name = y.Name,
                IsManger = y.IsManger,
                IsMember = y.IsMember,
            }).ToList()
        })
        .FirstOrDefaultAsync();

        if (department == null)
        {
            return NotFound();
        }

        return Ok(department); // ≈—Ã«⁄ «·»Ì«‰«  »’Ì€… JSON

    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var departments = _context.Departments
            .Include(d => d.Employees) //  Õ„Ì· «·„ÊŸ›Ì‰ „⁄ «·√ﬁ”«„
            .Select(d => new DepartmentVM
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Employees = d.Employees.Select(e => new EmployeeVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    IsManger = e.IsManger,
                    IsMember = e.IsMember,
                }).ToList()
            }).ToList();

        return Ok(departments);
    }








}
