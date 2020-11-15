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
    public class BusinessTravelController : ControllerBase
    {

        private readonly Context _context;
        public BusinessTravelController(Context context)
        {
            _context = context;
        }

        [HttpGet("getBusinessTravelList")]
        public ActionResult getBusinessTravel()
        {

            var travel = _context.BusinessTravel.ToList();
            return new JsonResult(travel);
        }
        [HttpGet("getlistbyId{id}")]
        public async Task<ActionResult<BusinessTravelDTO>> getBusinessTravelById(int Id)
        {

            var travel = await _context.BusinessTravel.FindAsync(Id);
            return new JsonResult(travel);
        }
        [HttpPost("saveEdit")]
        public async Task<ActionResult<BusinessTravelDTO>> PostBusinessTravel(BusinessTravelDTO businessTravelDTO)
        {
            if (businessTravelDTO.Id == 0)
            {

                var bussinessTravel = new BusinessTravel
                {
                    Name = businessTravelDTO.Name
                };
                _context.BusinessTravel.Add(bussinessTravel);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                try
                {
                    var dbBussinessTravel = _context.BusinessTravel.Find(businessTravelDTO.Id);

                    dbBussinessTravel.Name = businessTravelDTO.Name;

                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!BusinessTravelExists(businessTravelDTO.Id))
                        return NotFound();
                    else
                        throw;
                }

            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BusinessTravelDTO>> DeleteBusinessTravel(int id)
        {
            var bussinessTravel = await _context.BusinessTravel.FindAsync(id);
            if(bussinessTravel == null)
            {
                return NotFound();
            };
            _context.BusinessTravel.Remove(bussinessTravel);
            await _context.SaveChangesAsync();
            return NoContent();

        }

        private bool BusinessTravelExists(int id)
        {
            return _context.BusinessTravel.Any(e => e.Id == id);
        }

    }
}
