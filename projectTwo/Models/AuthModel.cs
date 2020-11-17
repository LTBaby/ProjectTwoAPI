using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
    public class AuthModel
    {
        [Required]
        public int EmployeeNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
