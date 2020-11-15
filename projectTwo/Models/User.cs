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
        public int Age { get; set; }
        public string Attrition { get; set; }
        public int DailyRate { get; set; }
        public int DistanceFromHome { get; set; }
        public int Education { get; set; }
        public int EmployeeCount { get; set; }
        public int EnvironmentSatisfaction { get; set; }
        public string Gender { get; set; }
        public int HourlyRate { get; set; }
        public int JobInvolvement { get; set; }
        public string MaritalStatus { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyRate { get; set; }
        public int NumCompaniesWorked { get; set; }
        public char Over18 { get; set; }
        public string OverTime { get; set; }
        public int PercentSalaryHike { get; set; }
        public int PerformanceRating { get; set; }
        public int RelationshipSatisfaction { get; set; }
        public int StandardHours { get; set; }
        public int StockOptionLevel { get; set; }
        public int TotalWorkingYears { get; set; }
        public int TrainingTimesLastYear { get; set; }
        public int WorkLifeBalance { get; set; }
        public int YearsAtCompany { get; set; }
        public int YearsInCurrentRole { get; set; }
        public int YearsSinceLastPromotion { get; set; }
        public int YearsWithCurrManager { get; set; }



    }
}
