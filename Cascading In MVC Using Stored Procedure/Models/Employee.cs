using System.ComponentModel.DataAnnotations;

namespace Cascading_In_MVC_Using_Stored_Procedure.Models
{
    public class Employee
    {
        [Key]
        public int EmpId {  get; set; }
        public string EmpName { get; set; }
        public int Age { get; set; }
       // public string   Salary { get; set; }
        public string MobileNo { get; set; }

        public int DepId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        public Department Department { get; set; }
        public Country  Country { get; set; }
        public State State { get; set; }
        public City City { get; set; }
    }
}
