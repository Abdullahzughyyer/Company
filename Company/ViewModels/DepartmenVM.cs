using Company.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.ViewModels
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        //   public IFormFile File { get; set; }

        //  public string ImageUrl { get; set; }
        public List<EmployeeVM> Employees { get; set; } = new List<EmployeeVM>();


    }
}
