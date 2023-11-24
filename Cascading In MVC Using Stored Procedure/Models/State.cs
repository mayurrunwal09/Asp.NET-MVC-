using System.ComponentModel.DataAnnotations;

namespace Cascading_In_MVC_Using_Stored_Procedure.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }   

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public IEnumerable<City> City { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
