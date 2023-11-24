using System.ComponentModel.DataAnnotations;

namespace Cascading_In_MVC_Using_Stored_Procedure.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        public string CityName { get; set; }    

        public int StateId { get; set; }
        public State State { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
