using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soaps.Model;
using Soaps.Model.Data;

namespace Soaps.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SoapsController : ControllerBase
    {
        private readonly MvcSoapContext _context;

        public SoapsController(MvcSoapContext context)
        {
            _context = context;
        }

        // GET: api/Soaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Soap>>> Get()
        {
            return await _context.Soaps.ToListAsync();
        }

        // GET: api/Soaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Soap>> Get(int id)
        {
            var soap = await _context.Soaps.FindAsync(id);

            if (soap == null)
            {
                return NotFound();
            }

            return soap;
        }

        // PUT: api/Soaps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoap(int id, Soap soap)
        {
            if (id != soap.Id)
            {
                return BadRequest();
            }

            _context.Entry(soap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoapExists(id))
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

        // POST: api/Soaps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Soap>> Post([FromBody]Soap soap)
        {
            try
            {
                _context.Soaps.Add(soap);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = soap.Id }, soap);
            }
            catch (ArgumentException)
            {
                return this.ValidationProblem();
            }
            catch (Exception)
            {
                return this.StatusCode(500, "Internal Server error");
            }
        }

        // DELETE: api/Soaps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Soap>> DeleteSoap(int id)
        {
            var soap = await _context.Soaps.FindAsync(id);
            if (soap == null)
            {
                return NotFound();
            }

            _context.Soaps.Remove(soap);
            await _context.SaveChangesAsync();

            return soap;
        }

        private bool SoapExists(int id)
        {
            return _context.Soaps.Any(e => e.Id == id);
        }
    }
}
