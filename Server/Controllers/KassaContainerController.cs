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

namespace kassablad.app.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KassaContainerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KassaContainerController(ApplicationDbContext context)
        {
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
        public async Task<ActionResult<KassaContainer>> PostKassaContainer(KassaContainer kassaContainer)
        {
            _context.KassaContainers.Add(kassaContainer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKassaContainer", new { id = kassaContainer.KassaContainerId }, kassaContainer);
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
