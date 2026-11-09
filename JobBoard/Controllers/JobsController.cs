using JobBoard.Data;
using JobBoard.DTOs;
using JobBoard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobBoard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetJobs(string? query = null)
        {
            var jobs = _context.Jobs.Select(e => new GetAllJobsDTO
            {
                Title = e.Title,
                Company = e.Company,
                Location = e.Location,
                SalaryMin = e.SalaryMin,
                SalaryMax = e.SalaryMax,
                Description = e.Description,
                DateTime = e.DateTime,
                Category = e.Category
            }).OrderByDescending(e => e.DateTime).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                jobs = jobs.Where(e=>
                e.Title.Contains(query) || e.Company.Contains(query) || e.Category.Contains(query)
                );
            }

            var data = await jobs.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _context.Jobs.Select(e => new GetByIdJobDTO
            {
                Id = e.Id,
                Title = e.Title,
                Company = e.Company,
                Location = e.Location,
                SalaryMin = e.SalaryMin,
                SalaryMax = e.SalaryMax,
                Description = e.Description,
                DateTime = e.DateTime,
                Category = e.Category
            }).FirstOrDefaultAsync(e=>e.Id == id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }
        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobDTO model)
        {
            var recruiter = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Job job = new Job
            {
                Title = model.Title,
                Category = model.Category,
                Company = model.Company,
                Description = model.Description,
                Location = model.Location,
                SalaryMax = model.SalaryMax,
                SalaryMin = model.SalaryMin,
                RecruiterId = recruiter
            };
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, job);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CreateJobDTO dto)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job is null) 
                return NotFound();
            var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (job.RecruiterId != recruiterId) 
                return Forbid();

            job.Title = dto.Title;
            job.Company = dto.Company;
            job.Location = dto.Location;
            job.Description = dto.Description;
            job.SalaryMin = dto.SalaryMin;
            job.SalaryMax = dto.SalaryMax;
            job.Category = dto.Category;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job is null)
                return NotFound();
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
