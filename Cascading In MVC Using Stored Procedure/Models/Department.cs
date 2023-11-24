using System.ComponentModel.DataAnnotations;

namespace Cascading_In_MVC_Using_Stored_Procedure.Models
{
    public class Department
    {
        [Key]
        public int DepId { get; set; }
        public string DepName { get;set; }

        public ICollection <Employee> Employees { get; set; }
    }
}
