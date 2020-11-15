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
    public class UserController : ControllerBase
    {

        private readonly Context _context;
        public UserController(Context context)
        {
            _context = context;
        }

        [HttpGet("getUserList")]
        public ActionResult getUserList()
        {

            var users = _context.User.ToList();
            return new JsonResult(users);
        }
        [HttpGet("getlistbyId{id}")]
        public async Task<ActionResult<JobRoleDTO>> getDepartmentById(int Id)
        {

            var user = await _context.User.FindAsync(Id);
            return new JsonResult(user);
        }
        //[HttpPost("saveEdit")]
        //public async Task<ActionResult<UserDTO>> Post(UserDTO usersDTO)
        //{
        //    if (usersDTO.Id == 0)
        //    {

        //        var user = new JobRole
        //        {
        //            Name = usersDTO.Name
        //        };
        //        _context.User.Add(user);
        //        await _context.SaveChangesAsync();
        //        return NoContent();
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var dbJobRole = _context.JobRole.Find(usersDTO.Id);

        //            dbJobRole.Name = usersDTO.Name;

        //            await _context.SaveChangesAsync();
        //        } catch (DbUpdateConcurrencyException)
        //        {
        //            if (!JobRoleExists(usersDTO.Id))
        //                return NotFound();
        //            else
        //                throw;
        //        }

        //    }
        //    return NoContent();
        //}
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobRoleDTO>> DeleteJobRole(int id)
        {
            var user = await _context.User.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            };
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.EmployeeNumber == id);
        }

    }
}
