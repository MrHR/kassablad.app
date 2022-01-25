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

namespace kassablad.app.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NominationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Nomination
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nomination>>> GetNominations()
        {
            return await _context.Nominations.ToListAsync();
        }

        // GET: api/Nomination/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nomination>> GetNomination(int id)
        {
            var nomination = await _context.Nominations.FindAsync(id);

            if (nomination == null)
            {
                return NotFound();
            }

            return nomination;
        }

        private bool NominationExists(int id)
        {
            return _context.Nominations.Any(e => e.NominationId == id);
        }
    }
}
