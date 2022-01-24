#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kassablad.app.Server.Data;
using kassablad.app.Server.Models;
using kassablad.app.Shared;

namespace kassablad.app.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class FKassaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FKassaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FKassa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FKassa>>> GetFKassa()
        {
            return await _context.FKassa.ToListAsync();
        }

        // GET: api/FKassa/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<FKassa>> GetFKassa(int id)
        // {
        //     var fKassa = await _context.FKassa.FindAsync(id);

        //     if (fKassa == null)
        //     {
        //         return NotFound();
        //     }

        //     return fKassa;
        // }

        // GET: api/FKassa/Café
        [HttpGet("{fKassaNaam}")]
        public async Task<ActionResult<FKassaDto>> GetFKassaByName(string fKassaNaam)
        {
            var fKassa = await _context.FKassa
                .Where(x => x.FKassaNaam == fKassaNaam)
                .Include(x => x.KassaContainers)
                .FirstOrDefaultAsync();

            if(fKassa == null)
            {
                return NotFound();
            }

            var kassaContainer = fKassa.KassaContainers?.LastOrDefault();
            var fKassaDto = new FKassaDto() {
                FKassaId = fKassa.FKassaId,
                FKassaNaam = fKassa.FKassaNaam,
                KassaContainerDto = kassaContainer != null ? 
                    new KassaContainerReturnDto() {
                        Activiteit = kassaContainer.Activiteit,
                        Afroomkluis = kassaContainer.Afroomkluis,
                        BeginUur = kassaContainer.BeginUur,
                        Bezoekers = kassaContainer.Bezoekers,
                        State = kassaContainer.State.ToString(),
                        DateAdded = kassaContainer.DateAdded,
                        DateUpdated = kassaContainer.DateUpdated,
                        EindUur = kassaContainer.EindUur,
                        InkomstBar = kassaContainer.InkomstBar,
                        InkomstLidkaart = kassaContainer.InkomstLidkaart,
                        KassaContainerId = kassaContainer.KassaContainerId,
                        Notes = kassaContainer.Notes,
                        LatestKassaId = kassaContainer.Kassas?.FirstOrDefault().KassaId
                    } : null
            };

            return fKassaDto;
        }

        // PUT: api/FKassa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFKassa(int id, FKassa fKassa)
        {
            if (id != fKassa.FKassaId)
            {
                return BadRequest();
            }

            _context.Entry(fKassa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FKassaExists(id))
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

        // POST: api/FKassa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FKassa>> PostFKassa(FKassa fKassa)
        {
            _context.FKassa.Add(fKassa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFKassa", new { id = fKassa.FKassaId }, fKassa);
        }

        // DELETE: api/FKassa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFKassa(int id)
        {
            var fKassa = await _context.FKassa.FindAsync(id);
            if (fKassa == null)
            {
                return NotFound();
            }

            _context.FKassa.Remove(fKassa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FKassaExists(int id)
        {
            return _context.FKassa.Any(e => e.FKassaId == id);
        }
    }
}
