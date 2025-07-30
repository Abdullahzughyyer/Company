using Company.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.ViewModels
{
    public class EmployeeVM
    {
        public string Name { get; set; }

        public bool IsManger { get; set; }

        public bool IsMember { get; set; }
        public int Id { get; set; }



    }
}
