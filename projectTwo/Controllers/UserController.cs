using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using projectTwo.Data;
using projectTwo.DTOs;
using projectTwo.Models;
using Swashbuckle.Swagger;

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


        [HttpGet]
        public Object GetToken()
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("userid", "1"));
            permClaims.Add(new Claim("name", "bilal"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }
        [HttpPost]
        public String GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                }
                return "Valid";
            }
            else
            {
                return "Invalid";
            }
        }


        [Authorize]
        [HttpPost("getName2")]
        public Object GetName2()
        {
            var identity = User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var name = claims.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                return new
                {
                    data = name
                };

            }
            return null;
        }


    }
}
