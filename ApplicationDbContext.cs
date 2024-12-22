using CompensationWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CompensationWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<CompensationDetails> CompensationDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FullName = "Nibraj Halim", Position = "Manager", HireDate = new DateTime(2020, 01, 01), BasicSalary = 50000 },
                new Employee { EmployeeId = 2, FullName = "Sayma Nasrin", Position = "Developer", HireDate = new DateTime(2021, 03, 15), BasicSalary = 40000 },
                new Employee { EmployeeId = 3, FullName = "Abdullah Qader", Position = "HR Officer", HireDate = new DateTime(2019, 07, 10), BasicSalary = 35000 },
                new Employee { EmployeeId = 4, FullName = "Manirul Islam", Position = "Accountant", HireDate = new DateTime(2022, 05, 21), BasicSalary = 37000 },
                new Employee { EmployeeId = 5, FullName = "Sabiha Sulatana", Position = "Sales Executive", HireDate = new DateTime(2021, 11, 13), BasicSalary = 42000 }
            );


            modelBuilder.Entity<CompensationDetails>().HasData(
                new CompensationDetails { PayrollId = 1, EmployeeId = 1, PayMonth = "December 2024", GrossSalary = 52000, Deductions = 2000, NetSalary = 50000 },
                new CompensationDetails { PayrollId = 2, EmployeeId = 2, PayMonth = "December 2024", GrossSalary = 42000, Deductions = 2000, NetSalary = 40000 },
                new CompensationDetails { PayrollId = 3, EmployeeId = 3, PayMonth = "December 2024", GrossSalary = 37000, Deductions = 2000, NetSalary = 35000 },
                new CompensationDetails { PayrollId = 4, EmployeeId = 4, PayMonth = "December 2024", GrossSalary = 39000, Deductions = 2000, NetSalary = 37000 },
                new CompensationDetails { PayrollId = 5, EmployeeId = 5, PayMonth = "December 2024", GrossSalary = 44000, Deductions = 2000, NetSalary = 42000 }
            );
        }
    }
}
