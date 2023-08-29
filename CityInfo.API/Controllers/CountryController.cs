using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityInfo.API.DbContexts;
using CityInfo.API.Models;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CityInfoContext _context;

        public CountryController(CityInfoContext context)
        {
            _context = context;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountryDto()
        {
          if (_context.CountryDto == null)
          {
              return NotFound();
          }
            return await _context.CountryDto.ToListAsync();
        }

        // GET: api/Country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountryDto(int id)
        {
          if (_context.CountryDto == null)
          {
              return NotFound();
          }
            var countryDto = await _context.CountryDto.FindAsync(id);

            if (countryDto == null)
            {
                return NotFound();
            }

            return countryDto;
        }

        // PUT: api/Country/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryDto(int id, CountryDto countryDto)
        {
            if (id != countryDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryDtoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Country
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountryDto>> PostCountryDto(CountryDto countryDto)
        {
          if (_context.CountryDto == null)
          {
              return Problem("Entity set 'CityInfoContext.CountryDto'  is null.");
          }
            _context.CountryDto.Add(countryDto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryDto", new { id = countryDto.Id }, countryDto);
        }

        // DELETE: api/Country/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountryDto(int id)
        {
            if (_context.CountryDto == null)
            {
                return NotFound();
            }
            var countryDto = await _context.CountryDto.FindAsync(id);
            if (countryDto == null)
            {
                return NotFound();
            }

            _context.CountryDto.Remove(countryDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryDtoExists(int id)
        {
            return (_context.CountryDto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
