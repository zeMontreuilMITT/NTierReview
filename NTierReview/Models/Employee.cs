using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NTierReview.Models
{
    public class Employee
    {
        [Key]
        public int Id { get;set; }

        [DisplayName("Name")]
        [MaxLength(200)]
        [Required]
        public string FullName { get;set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =("{0:DD/MM/YYYY}"))]
        [DisplayName("Hiring Date")]
        [Required]        
        public DateTime HiringDate { get;set; }

        [Range(0, Double.MaxValue)]
        [Required]
        public decimal Salary { get; set; }
    }
}
