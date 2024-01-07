using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Data
{
    public class Patient : IdentityUser
    {
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual List<Examination> Examinations { get; set; }

        public Patient()
        {
            this.Examinations = new List<Examination>();
        }

    }
}