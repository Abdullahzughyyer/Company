using Company.Models;
using System.Text.Json.Serialization;

namespace Company.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public bool IsManger { get; set; }

        public bool IsMember { get; set; }

        public string Name { get; set; }
        public int DepartmentId { get; set; }

        [JsonIgnore]
        public Department Department { get; set; }
    }
}
