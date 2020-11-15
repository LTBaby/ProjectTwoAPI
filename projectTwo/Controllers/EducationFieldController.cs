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
    public class EducationFieldController : ControllerBase
    {

        private readonly Context _context;
        public EducationFieldController(Context context)
        {
            _context = context;
        }

        [HttpGet("getEducationFieldList")]
        public ActionResult getEducationFieldList()
        {

            var education = _context.EducationField.ToList();
            return new JsonResult(education);
        }
        [HttpGet("getlistbyId{id}")]
        public async Task<ActionResult<EducationFieldDTO>> getEducationFieldById(int Id)
        {

            var education = await _context.EducationField.FindAsync(Id);
            return new JsonResult(education);
        }
        [HttpPost("saveEdit")]
        public async Task<ActionResult<EducationFieldDTO>> PostEducationField(EducationFieldDTO educationFieldDTO)
        {
            if (educationFieldDTO.Id == 0)
            {

                var educationField = new EducationField
                {
                    Name = educationFieldDTO.Name
                };
                _context.EducationField.Add(educationField);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                try
                {
                    var dbEducationField = _context.EducationField.Find(educationFieldDTO.Id);

                    dbEducationField.Name = educationFieldDTO.Name;

                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!EducationFieldExists(educationFieldDTO.Id))
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
            var educationField = await _context.EducationField.FindAsync(id);
            if(educationField == null)
            {
                return NotFound();
            };
            _context.EducationField.Remove(educationField);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        private bool EducationFieldExists(int id)
        {
            return _context.EducationField.Any(e => e.Id == id);
        }

    }
}
