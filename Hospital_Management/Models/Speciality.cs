using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.Models
{
    public class Specialty
    {
        [Key]
        public int SpecialtyID { get; set; }

        [Required]
        [StringLength(100)]
        public string SpecialtyName { get; set; }

        public virtual ICollection<DoctorSpecialty> DoctorSpecialties { get; set; }
    }
}
