using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string Category { get; set; }
        [Required]
        [MaxLength(150)]
        public string Company { get; set; }
        [Required]
        [MaxLength(150)]
        public string Location { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaryMin { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaryMax { get; set; }
        public DateTime DateTime {get;set; } = DateTime.Now;

        [ForeignKey("User")]
        public string RecruiterId { get; set; }
        public virtual ApplicationUser User { get; set; } = default!;

    }
}
