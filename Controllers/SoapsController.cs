using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soaps.Dto;
using Soaps.Model;
using Soaps.Model.Data;

namespace Soaps.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SoapsController : ControllerBase
    {
        private readonly MvcSoapContext _context;
        private readonly IMapper _mapper;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public SoapsController(MvcSoapContext context, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("upload")]
        [Obsolete]
        public IActionResult Upload()
        {
            try
            {
                var files = Request.Form.Files;
                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }

                return Ok();
            }
            catch
            {
                return Ok();
            }
        }

        // GET: api/Soaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoapDto>>> Get()
        {
            try
            {
                List<SoapDto> soapDtos = new List<SoapDto>();
                List<Soap> soaps = await _context.Soaps
                                         .Include(t => t.SoapType)
                                         .Include(d => d.SoapDetails)
                                         .Include(t => t.Images)
                                         .ToListAsync();

                _mapper.Map(soaps, soapDtos);

                return soapDtos;
            }
            catch (Exception e)
            {
                return new List<SoapDto>();
            }
        }

        // GET: api/Soaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoapDto>> Get(int id)
        {
            var soap = await _context.Soaps
                                     .Include(t => t.SoapType)
                                     .Include(d => d.SoapDetails)
                                     .Include(t => t.Images)
                                     .FirstOrDefaultAsync(e => e.Id == id);

            if (soap == null)
            {
                return NotFound();
            }

            SoapDto soapDto = new SoapDto();
            _mapper.Map(soap, soapDto);

            return soapDto;
        }

        // PUT: api/Soaps/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<SoapDto>>> Put(int id, SoapDto soapDto)
        {
            if (id != soapDto.Id)
            {
                return BadRequest();
            }

            Soap soap = await _context.Soaps
                                      .Include(t => t.SoapType)
                                      .Include(d => d.SoapDetails)
                                      .Include(t => t.Images)
                                      .FirstOrDefaultAsync(e => e.Id == id);
            
            soap.SoapDetails.Clear();
            soap.Name = soapDto.Name;
            soap.Description = soapDto.Description;
            soap.Available = soapDto.Available;
            soap.Price = soapDto.Price;
            soap.SoapType = await _context.SoapTypes.FindAsync(int.Parse(soapDto.SoapTypeId));
            foreach (var soapDetail in soapDto.SoapDetails)
            {
                soap.SoapDetails.Add(new SoapDetail
                {
                    Id = soapDetail.Id,
                    Name = soapDetail.Name
                });
            }

            if (soapDto.Images.Any())
            {
                soap.Images.Clear();
                foreach (var image in soapDto.Images)
                {
                    soap.Images.Add(new Image
                    {
                        Id = image.Id,
                        Name = image.Name
                    });
                }
            }

            _context.Entry(soap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                List<SoapDto> soapDtos = new List<SoapDto>();
                List<Soap> soaps = await _context.Soaps
                                         .Include(t => t.SoapType)
                                         .Include(d => d.SoapDetails)
                                         .Include(t => t.Images)
                                         .ToListAsync();

                _mapper.Map(soaps, soapDtos);

                return soapDtos;
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
        }

        // POST: api/Soaps
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SoapDto>> Post([FromBody]SoapDto soapDto)
        {
            try
            {
                Soap soap = new Soap();
                _mapper.Map(soapDto, soap);
                soap.SoapType = await _context.SoapTypes.FindAsync(int.Parse(soapDto.SoapTypeId));
                _context.Soaps.Add(soap);
                await _context.SaveChangesAsync();
                _mapper.Map(soap, soapDto);

                return CreatedAtAction("Get", new { id = soapDto.Id }, soapDto);
            }
            catch (ArgumentException)
            {
                return this.ValidationProblem();
            }
            catch
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
