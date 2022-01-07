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

namespace kassablad.app.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KassaContainerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public KassaContainerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/KassaContainer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KassaContainer>>> GetKassaContainers()
        {
            
            return await _context.KassaContainers.ToListAsync();
        }

        // GET: api/KassaContainer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KassaContainer>> GetKassaContainer(int id)
        {
            var kassaContainer = await _context.KassaContainers.FindAsync(id);

            if (kassaContainer == null)
            {
                return NotFound();
            }

            return kassaContainer;
        }

        // PUT: api/KassaContainer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKassaContainer(int id, KassaContainer kassaContainer)
        {
            if (id != kassaContainer.KassaContainerId)
            {
                return BadRequest();
            }

            _context.Entry(kassaContainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KassaContainerExists(id))
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

        // POST: api/KassaContainer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KassaContainerReturnDto>> PostKassaContainer(KassaContainerDto kcDto)
        {
            // map the dto to the entity model
            ApplicationUser user = await _userManager.GetUserAsync(User);
            var kassaContainer = new KassaContainer() 
            {
                Active = true,
                Activiteit = kcDto.Activiteit,
                Afroomkluis = kcDto.Afroomkluis,
                BeginUur = kcDto.BeginUur,
                EindUur = kcDto.EindUur,
                Bezoekers = kcDto.Bezoekers,
                Concept = kcDto.Concept,
                Notes = kcDto.Notes,
                InkomstBar = kcDto.InkomstBar,
                InkomstLidkaart = kcDto.InkomstLidkaart,
                CreatedBy = user.Id,
                UpdatedBy = user.Id,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };
            _context.KassaContainers.Add(kassaContainer);
            await _context.SaveChangesAsync();
            
            // Save the current tapper to the joining table
            var kcApplicationUsers = new KassaContainerApplicationUser()
            {
                ApplicationUserId = user.Id,
                KassaContainerId = kassaContainer.KassaContainerId,
                StartTime = DateTime.Now
            };
            _context.KassaContainerApplicationUsers.Add(kcApplicationUsers);
            await _context.SaveChangesAsync();

            // map the kassacontainer to the return dto
            KassaContainerReturnDto kassaContainerReturnDto = new KassaContainerReturnDto {
                KassaContainerId = kassaContainer.KassaContainerId,
                Activiteit = kassaContainer.Activiteit,
                Afroomkluis = kassaContainer.Afroomkluis,
                BeginUur = kassaContainer.BeginUur,
                EindUur = kassaContainer.EindUur,
                Bezoekers = kassaContainer.Bezoekers,
                Concept = kassaContainer.Concept,
                Notes = kassaContainer.Notes,
                InkomstBar = kassaContainer.InkomstBar,
                InkomstLidkaart = kassaContainer.InkomstLidkaart,
                CreatedBy = kassaContainer.CreatedBy,
                UpdatedBy = kassaContainer.UpdatedBy,
                DateAdded = kassaContainer.DateAdded,
                DateUpdated = kassaContainer.DateUpdated,
                Tappers = kassaContainer.ApplicationUsers.Select(u => new UserDto {
                    Email = u.Email,
                    Id = u.Id
                }).ToList()
            };

            return CreatedAtAction("GetKassaContainer", new { id = kassaContainer.KassaContainerId }, kassaContainerReturnDto);
        }

        // DELETE: api/KassaContainer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKassaContainer(int id)
        {
            var kassaContainer = await _context.KassaContainers.FindAsync(id);
            if (kassaContainer == null)
            {
                return NotFound();
            }

            _context.KassaContainers.Remove(kassaContainer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KassaContainerExists(int id)
        {
            return _context.KassaContainers.Any(e => e.KassaContainerId == id);
        }
    }
}
