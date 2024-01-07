using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Data
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string FistName { get; set; }
        public string LastName{ get; set; }
        public string Description { get; set; }
        public virtual List<Patient> Patients { get; set; }

        public Doctor()
        {
            this.Patients = new List<Patient>();
        }

    }
}
