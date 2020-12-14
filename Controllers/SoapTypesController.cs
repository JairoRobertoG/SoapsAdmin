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
    public class SoapTypesController : ControllerBase
    {
        private readonly MvcSoapContext _context;

        public SoapTypesController(MvcSoapContext context)
        {
            _context = context;
        }

        // GET: api/Soaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoapType>>> Get()
        {
            return await _context.SoapTypes.ToListAsync();
        }

        // GET: api/Soaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoapType>> Get(int id)
        {
            var soapType = await _context.SoapTypes.FindAsync(id);

            if (soapType == null)
            {
                return NotFound();
            }

            return soapType;
        }

        // PUT: api/Soaps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoap(int id, SoapType soapType)
        {
            if (id != soapType.Id)
            {
                return BadRequest();
            }

            _context.Entry(soapType).State = EntityState.Modified;

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
        public async Task<ActionResult<Soap>> Post([FromBody] SoapType soapType)
        {
            try
            {
                _context.SoapTypes.Add(soapType);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = soapType.Id }, soapType);
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
        public async Task<ActionResult<SoapType>> DeleteSoap(int id)
        {
            var soapType = await _context.SoapTypes.FindAsync(id);
            if (soapType == null)
            {
                return NotFound();
            }

            _context.SoapTypes.Remove(soapType);
            await _context.SaveChangesAsync();

            return soapType;
        }

        private bool SoapExists(int id)
        {
            return _context.SoapTypes.Any(e => e.Id == id);
        }
    }
}
