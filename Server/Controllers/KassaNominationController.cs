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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace kassablad.app.Server
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KassaNominationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public KassaNominationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/KassaNomination
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KassaNomination>>> GetKassaNominations()
        {
            return await _context.KassaNominations.ToListAsync();
        }

        // GET: api/KassaNomination/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KassaNominationDto>> GetKassaNomination(int id)
        {
            var kassaNomination = await _context.KassaNominations.FindAsync(id);

            if (kassaNomination == null)
            {
                return NotFound();
            }

            var kassaNominationDto = new KassaNominationDto {
                Amount = kassaNomination.Amount,
                KassaId = kassaNomination.KassaId,
                NominationId = kassaNomination.NominationId,
                Total = kassaNomination.Total
            };

            return kassaNominationDto;
        }

        //GET: api/kassanomination/listByKassaId
        [HttpGet("listByKassaId")]
        public async Task<ActionResult<List<KassaNominationDto>>> GetKassaNominations(int KassaId)
        {
            var knList = await _context.KassaNominations
                .Where(x => 
                    x.KassaId == KassaId
                    && x.Active == true)
                .Select(x => new KassaNominationDto {
                    Amount = x.Amount,
                    KassaId = x.KassaId,
                    KassaNominationId = x.KassaNominationId,
                    NominationId = x.NominationId,
                    Total = x.Total,
                    Multiplier = x.Nom.Multiplier
                })
                .ToListAsync();

            if(knList.Count <= 0)
            {
                return NotFound();
            }

            return knList;
        }

        //GET: api/kassanomination/byKassaId
        [HttpGet("byKassaId")]
        public async Task<ActionResult<KassaNominationDto>> GetKassaNomination(int kassaId, int nominationId)
        {
            var kassaNominationDto = await _context.KassaNominations
                .Where(x => 
                    x.KassaId == kassaId 
                    && x.NominationId == nominationId
                    && x.Active == true)
                .Select(x => new KassaNominationDto {
                    Amount = x.Amount,
                    KassaId = x.KassaId,
                    KassaNominationId = x.KassaNominationId,
                    NominationId = x.NominationId,
                    Total = x.Total,
                    Multiplier = x.Nom.Multiplier
                })
                .FirstOrDefaultAsync();
            
            if(kassaNominationDto == null)
            {
                return NotFound();
            }

            return kassaNominationDto;
        }

        // PUT: api/KassaNomination/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKassaNomination(int id, KassaNominationDto kassaNominationDto)
        {
            if (id != kassaNominationDto.KassaNominationId)
            {
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(User);
            var excluded = new[]
            {
                "CreatedBy",
                "DateAdded"
            };
            var kassaNomination = new KassaNomination()
            {
                Active = true,
                Amount = kassaNominationDto.Amount,
                DateUpdated = DateTime.Now,
                KassaId = kassaNominationDto.KassaId,
                KassaNominationId = kassaNominationDto.KassaNominationId,
                NominationId = kassaNominationDto.NominationId
            };

            _context.Entry(kassaNomination).State = EntityState.Modified; // for explanation check kassaContainerController

            foreach(var name in excluded)
            {
                _context.Entry(kassaNomination).Property(name).IsModified = false;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KassaNominationExists(id))
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

        // POST: api/KassaNomination
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KassaNomination>> PostKassaNomination(KassaNominationDto kassaNominationDto)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var nomination = await _context.Nominations.FindAsync(kassaNominationDto.NominationId);
            var kassaNomination = new KassaNomination {
                Active = true,
                Amount = kassaNominationDto.Amount,
                CreatedBy = user.Id,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
                KassaId = kassaNominationDto.KassaId,
                NominationId = kassaNominationDto.NominationId,
                Nom = new Nom {
                    Multiplier = nomination.Nom.Multiplier,
                    Currency = nomination.Nom.Currency
                }
            };
            _context.KassaNominations.Add(kassaNomination);
            await _context.SaveChangesAsync();

            kassaNominationDto.KassaNominationId = kassaNomination.KassaNominationId;

            return CreatedAtAction("GetKassaNomination", new { id = kassaNomination.KassaNominationId }, kassaNominationDto);
        }

        // DELETE: api/KassaNomination/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKassaNomination(int id)
        {
            var kassaNomination = await _context.KassaNominations.FindAsync(id);
            if (kassaNomination == null)
            {
                return NotFound();
            }

            _context.KassaNominations.Remove(kassaNomination);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KassaNominationExists(int id)
        {
            return _context.KassaNominations.Any(e => e.KassaNominationId == id);
        }
    }
}
