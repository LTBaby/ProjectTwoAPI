using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectTwo.Data;
using projectTwo.DTOs;
using projectTwo.Models;

namespace projectTwo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobRoleController : ControllerBase
    {

        private readonly Context _context;
        public JobRoleController(Context context)
        {
            _context = context;
        }

        [HttpGet("getJobRoleList")]
        public ActionResult getJobRoleList()
        {

            var jobRoles = _context.JobRole.ToList();
            return new JsonResult(jobRoles);
        }
        [HttpGet("getlistbyId{id}")]
        public async Task<ActionResult<JobRoleDTO>> getDepartmentById(int Id)
        {

            var jobRole = await _context.JobRole.FindAsync(Id);
            return new JsonResult(jobRole);
        }
        [HttpPost("saveEdit")]
        public async Task<ActionResult<DepartmentDTO>> Post(JobRoleDTO jobRoleDTO)
        {
            if (jobRoleDTO.Id == 0)
            {

                var jobRole = new JobRole
                {
                    Name = jobRoleDTO.Name
                };
                _context.JobRole.Add(jobRole);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                try
                {
                    var dbJobRole = _context.JobRole.Find(jobRoleDTO.Id);

                    dbJobRole.Name = jobRoleDTO.Name;

                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!JobRoleExists(jobRoleDTO.Id))
                        return NotFound();
                    else
                        throw;
                }

            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobRoleDTO>> DeleteJobRole(int id)
        {
            var jobRole = await _context.JobRole.FindAsync(id);
            if(jobRole == null)
            {
                return NotFound();
            };
            _context.JobRole.Remove(jobRole);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        private bool JobRoleExists(int id)
        {
            return _context.JobRole.Any(e => e.Id == id);
        }

    }
}
