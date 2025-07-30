using Company.Models;
using System.Text.Json.Serialization;

namespace Company.Models
{
    public class Department
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        // public IFormFile File { get; set; }

        // public string ImageUrl { get; set; }

        [JsonIgnore]
        public List<Employee> Employees { get; set; }


    }
}
