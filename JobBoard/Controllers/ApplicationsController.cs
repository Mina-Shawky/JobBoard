using JobBoard.Data;
using JobBoard.DTOs;
using JobBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "User")]
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyJob([FromBody] ApplyJobDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();
            var job = await _context.Jobs.FindAsync(model.JobId);
            if (job == null)
                return NotFound("Job not found.");
            var applicationExists = await _context.Applications
                .AnyAsync(a => a.JobId == model.JobId && a.UserId == userId);
            if (applicationExists)
                return BadRequest("You have already applied for this job.");
            var application = new Application
            {
                JobId = model.JobId,
                UserId = userId,
                CoverLetter = model.CoverLetter,
                AppliedAt = DateTime.UtcNow,
                Status = ApplicationStatus.Submitted
            };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Application submitted successfully." });
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return NotFound("No Applied Job");
            var applications = await _context.Applications
                .Where(e => e.UserId == userId)
                .Select(e => new
                {
                    e.Id,
                    e.JobId,
                    e.Job.Title,
                    e.Job.Company,
                    e.Job.Category,
                    e.CoverLetter,
                    e.AppliedAt,
                    e.Status
                }).ToListAsync();
            return Ok(applications);
        }
        [Authorize(Roles = "Recruiter")]
        [HttpGet("{jobId:int}")]
        public async Task<IActionResult> GetJobApplications(int jobId)
        {
            var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (recruiterId == null)
                return Unauthorized();
            var job = await _context.Jobs.FirstOrDefaultAsync(e => e.Id == jobId && e.RecruiterId == recruiterId);
            if (job == null)
                return NotFound("Job not found or you are not the owner");
            var applications = await _context.Applications.Where(e => e.JobId == jobId)
                .OrderByDescending(e => e.AppliedAt)
                .Select(e => new
                {
                    e.Id,
                    UserName = e.User.FirstName + " "+ e.User.LastName,
                    UserEmail = e.User.Email,
                    UserUserName = e.User.UserName,
                    e.CoverLetter,
                    e.AppliedAt,
                    e.Status,
                }).ToListAsync();
            return Ok(applications);
        }
        [Authorize(Roles = "Recruiter")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStatusApplication(int id, UpdateStatusApplicationsDTO model)
        {
            var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (recruiterId == null)
                return Unauthorized();
            var application = await _context.Applications
                .FirstOrDefaultAsync(e => e.Id == id && e.Job.RecruiterId == recruiterId);
            if (application == null)
                return NotFound("Application not found or you are not the owner of the job");
            application.Status = model.Status;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Application status updated successfully." });
        }
    }
}
