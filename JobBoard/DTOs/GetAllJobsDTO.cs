namespace JobBoard.DTOs
{
    public class GetAllJobsDTO
    {
        public string Title { get; set; }
        public string Company { get; set; } 
        public string Category { get; set; } 
        public string Location { get; set; }
        public string Description { get; set; } 
        public decimal SalaryMin { get; set; }
        public decimal SalaryMax { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
