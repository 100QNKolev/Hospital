using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Data
{
    public class Examination
    {
        [Key]
        public int ExaminationId { get; set; }
        public string PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime DateOfExamination { get; set; }
        [ForeignKey("Id")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Description { get; set; }
    }
}