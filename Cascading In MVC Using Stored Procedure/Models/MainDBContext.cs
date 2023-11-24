using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Cascading_In_MVC_Using_Stored_Procedure.Models
{
    public class MainDBContext : DbContext
    {
        public MainDBContext(DbContextOptions options):base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> Statements { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.DepId)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Country)
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.CountryId)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .HasOne(d => d.State)
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.StateId)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .HasOne(d => d.City )
                .WithMany(d => d.Employees)
                .HasForeignKey(d => d.CityId)
                .IsRequired();

        }

        public async Task<string> CreateOrUpdateDepartment(int DepId, string DepName)
        {
            var pDepId = new SqlParameter("@DepId", DepId);
            var pEmpId = new SqlParameter("@DepName", DepName);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE CreateOrUpdateDepartment @DepId,@DepName", pDepId, pEmpId);
            //return "Success: Department created or updated successfully.";
            return res == 1 ? "Success: Department created or updated successfully." : "Error: Department creation or update failed.";
        }

        public async Task<string>CreateOrUpdateEmployee(int EmpId,string EmpName,int Age,string MobileNo,int DepId,int CountryId,int StateId,int CityId)
        {
            var pEmpId = new SqlParameter("@EmpId", EmpId);
            var pEmpName = new SqlParameter("@EmpName", EmpName);
            var pAge = new SqlParameter("@Age", Age);
            var pMobileNo = new SqlParameter("@MobileNo", MobileNo);
            var pDepId = new SqlParameter("@DepId", DepId);
            var pCountryId = new SqlParameter("@CountryId", CountryId);
            var pStateId = new SqlParameter("@StateId", StateId);
            var pCityyId = new SqlParameter("@CityId", CityId);

            var res = await this.Database.ExecuteSqlRawAsync("Execute CreateOrUpdateEmployee @EmpId,@EmpName,@Age,@MobileNo,@DepId,@CountryId,@StateId,@CityId",
                pEmpId,pEmpName,pAge,pMobileNo,pDepId,pCountryId,pStateId,pCityyId);

            return res == 1 ? "Success : Employee Created or updated Successfully" : "Error";

        }

        public async Task<string> DeleteDepartment(int DepId)
        {
            var pDepId = new SqlParameter("@DepId", DepId);

            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE DeleteDepartment @DepId", pDepId);

            return res ==1 ? "Success: Department deleted successfully." : "Error: Department deletion failed. Department with provided ID not found.";
        }

        public async Task<string>DeleteEmployee(int EmpId)
        {
            var pEmpId = new SqlParameter("@EmpId", EmpId);
            var res = await this.Database.ExecuteSqlRawAsync("EXECUTE DeleteEmployee @EmpId", pEmpId);
            return res == 1 ? "Success" : "Error";
        }

        



    }
}