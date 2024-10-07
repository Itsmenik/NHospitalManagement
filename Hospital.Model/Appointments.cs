using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Model
{
    public class Appointments
    {
        // Change these to the appropriate type, usually string for user IDs
        public string DoctorId { get; set; }
        public string PatientId { get; set; }

        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        // If you do not want to map these navigation properties, use [NotMapped]
        [NotMapped]
        public ApplicationUser Doctor { get; set; }

        [NotMapped]
        public ApplicationUser Patient { get; set; }
    }
}
