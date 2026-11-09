using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public enum ApplicationStatus { Submitted, Reviewed, Accepted, Rejected }
    public class Application
    {
        public int Id { get; set; }
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job Job { get; set; } = default!;
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = default!;

        public string CoverLetter { get; set; } = "";
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Submitted;
    }
}
