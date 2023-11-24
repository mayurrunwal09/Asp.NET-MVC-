using System.ComponentModel.DataAnnotations;

namespace Cascading_In_MVC_Using_Stored_Procedure.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<State> states { get; set; }
    }
}
