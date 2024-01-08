using Hospital.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Data
{
    public class ReserveExamination
    {
        [Key]
        public int Id { get; set; }
        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }
        public DateTime ExaminationDate { get; set; }
        public DateTime ExaminationTime { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
