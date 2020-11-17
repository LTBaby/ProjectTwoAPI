using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using projectTwo.Data;
using projectTwo.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace projectTwo.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;
        public AuthService(IConfiguration configuration, Context context)
        {
            _context = context;
            _configuration = configuration;
        }

        public ActionResult LTBabyAuthenticate(AuthModel model)
        {
            try
            {
                if ((model.EmployeeNumber == null) || (model.EmployeeNumber == "0") || (model.EmployeeNumber == "string") || (model.EmployeeNumber == "String") || (model.EmployeeNumber is null))
                {
                    return null;
                }
                if ((model.Password == null) || (model.Password == "0") || (model.Password == "string") || (model.Password == "String") || (model.Password is null))
                {
                    return null;
                }

                User exstuser = _context.User.Where(x => x.Password == model.Password && (x.EmployeeNumber == model.EmployeeNumber;

                if (exstuser == null) return null;
                //{

                //    //return new StatusCodeResult(404);
                //    ////return new JsonResult("ssss");
                //    //// throw new ArgumentException("Name cannot be null or empty.", "Name");
                //}


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, exstuser.ClientKeyEncode.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var mtoken = GenerateJSONWebToken(); //tokenHandler.CreateToken(tokenDescriptor);
                if (mtoken == null) return null;

                var getuserRole = userRoleCode(exstuser.Id);
                if (getuserRole == null) return null;

                var accountObject = DbContext.PerAccountUser.Where(x => x.UserId == exstuser.Id)
                   .Include("User")
                   .Select(p => new
                   {
                       UserKey = p.User.ClientKeyEncode,
                       Token = mtoken,
                       UserRoles = getuserRole,
                       Account = (from a in Context.Account
                                  where p.AccountId == a.Id
                                  select new
                                  {
                                      AccountKey = a == null ? "Not Assign" : a.AccountKeyEncode,
                                      AccountName = a == null ? "Not Assign" : a.Name,
                                      AccountId = a == null ? 0 : a.Id,
                                  }).FirstOrDefault()

                   }).ToList();

                return new JsonResult(accountObject);
            }
            catch (Exception e)
            {
                return new JsonResult(e.ToString());
            }
        }
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
