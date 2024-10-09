using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Hospital.Model
{
    public class ApplicationUser : IdentityUser<string>
    {
        public object AppointmentsAsDoctor;
        public object AppointmentsAsPatient;

        public string Name { get; set; }
         public Gender Gender { get; set; }
         public string Nationality { get; set; }
         public string Address { get; set; }
         public DateTime DOB { get; set; }
         public string Specialist { get; set; } 


        [NotMapped] 
        public ICollection<Appointments> Appointments { get; set; }  

        public ICollection<Department> Departments { get; set; } 
        public ICollection<Payroll> Payrolls { get; set; }
}
}

   namespace Hospital.Model
  {
    public  enum Gender
   {
        Male ,Female , Other
    }
} 

