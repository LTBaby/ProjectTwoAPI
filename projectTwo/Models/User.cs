using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
    public class User
    {
        [Key]
        public int EmployeeNumber { get; set; }
        public int BusinessTravelId { get; set; }
        public int DepartmentId { get; set; }
        public int EducationFieldId { get; set; }
        public int JobRoleId { get; set; }
    }
}
