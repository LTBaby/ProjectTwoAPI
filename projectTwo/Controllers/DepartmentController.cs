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
    public class DepartmentController : ControllerBase
    {

        private readonly Context _context;
        public DepartmentController(Context context)
        {
            _context = context;
        }

        [HttpGet("getDepartmentList")]
        public ActionResult getDepartmentList()
        {

            var department = _context.Department.ToList();
            return new JsonResult(department);
        }
        [HttpGet("getlistbyId{id}")]
        public async Task<ActionResult<DepartmentDTO>> getDepartmentById(int Id)
        {

            var department = await _context.Department.FindAsync(Id);
            return new JsonResult(department);
        }
        [HttpPost("saveEdit")]
        public async Task<ActionResult<DepartmentDTO>> PostDepartment(DepartmentDTO departmentDTO)
        {
            if (departmentDTO.Id == 0)
            {

                var deparment = new Department
                {
                    Name = departmentDTO.Name
                };
                _context.Department.Add(deparment);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                try
                {
                    var dbDepartment = _context.Department.Find(departmentDTO.Id);

                    dbDepartment.Name = departmentDTO.Name;

                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(departmentDTO.Id))
                        return NotFound();
                    else
                        throw;
                }

            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentDTO>> DeleteDepartment(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if(department == null)
            {
                return NotFound();
            };
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }

    }
}
