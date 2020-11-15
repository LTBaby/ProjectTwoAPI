using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeNumber { get; set; }
        public int Age { get; set; }
    }
}
