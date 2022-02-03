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
    public class KassaController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public KassaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/Kassa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kassa>>> GetKassa()
        {
            return await _context.Kassas.ToListAsync();
        }

        // GET: api/Kassa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KassaDto>> GetKassa(int id)
        {
            var Kassa = await _context.Kassas
                .Include(k => k.KassaNominations)
                .ThenInclude(kn => kn.Nomination)
                .SingleOrDefaultAsync(x => x.KassaId == id);

            if (Kassa == null)
            {
                return NotFound();
            }

            //Map everything to DTO's *sigh, there must be a better way*
            var kassaNoms = Kassa?.KassaNominations?.ToList();
            var kassaNominationDtos = new List<KassaNominationDto>();
            kassaNoms?.ForEach(kn => {
                kassaNominationDtos.Add(new KassaNominationDto {
                    KassaNominationId = kn.KassaNominationId,
                    Amount = kn.Amount,
                    Total = kn.Total,
                    NominationId = kn.NominationId
                });
            });
            var kassaDto = new KassaDto {
                KassaContainerId = Kassa.KassaContainerId,
                KassaNominations = kassaNominationDtos
            };

            return kassaDto;
        }

        // PUT: api/Kassa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKassa(int id, Kassa Kassa)
        {
            if (id != Kassa.KassaId)
            {
                return BadRequest();
            }

            _context.Entry(Kassa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KassaExists(id))
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

        // POST: api/Kassa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KassaDto>> PostKassa(KassaDto KassaDto)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            //Save Kassa
            var Kassa = new Kassa {
                Active = true,
                CreatedBy = user.Id,
                UpdatedBy = user.Id,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
                KassaContainerId = KassaDto.KassaContainerId,
                Status = KassaDto.Status,
                Type = KassaDto.Type
            };
            _context.Kassas.Add(Kassa);
            await _context.SaveChangesAsync();

            // Save kassa to kassaContainer.Kassas list
            var kassaContainer = await _context.KassaContainers.FindAsync(Kassa.KassaContainerId);
            kassaContainer.Kassas.Add(Kassa);
            await _context.SaveChangesAsync();

            //TODO Maybe: Map Kassa to return kassa
            var KassaReturnDto = new KassaDto {
                KassaId = Kassa.KassaId,
                KassaContainerId = Kassa.KassaContainerId,
                Type = Kassa.Type,
                Status = Kassa.Status,
                KassaNominations = new List<KassaNominationDto>()
            };

            return CreatedAtAction("GetKassa", new { id = Kassa.KassaId }, KassaReturnDto);
        }

        // DELETE: api/Kassa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKassa(int id)
        {
            var Kassa = await _context.Kassas.FindAsync(id);
            if (Kassa == null)
            {
                return NotFound();
            }

            _context.Kassas.Remove(Kassa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KassaExists(int id)
        {
            return _context.Kassas.Any(e => e.KassaId == id);
        }
    }
}
