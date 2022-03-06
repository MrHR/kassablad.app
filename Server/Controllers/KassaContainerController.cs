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
        public async Task<ActionResult<KassaContainerReturnDto>> GetKassaContainer(int id)
        {
            //FindAsync vs SingleOrDefaultAsync: https://stackoverflow.com/questions/21817569/use-of-include-with-async-await
            var kassaContainer = await _context.KassaContainers
                .Include(k => k.Kassas)
                .ThenInclude(kn => kn.KassaNominations)
                .SingleOrDefaultAsync(x => x.KassaContainerId == id);

            if (kassaContainer == null)
            {
                return NotFound();
            }

            // var kassa = kassaContainer.Kassas.LastOrDefault();
            // var kassaNoms = kassa?.KassaNominations?.ToList();
            // var kassaNominationDtos = new List<KassaNominationDto>();
            // kassaNoms?.ForEach(kn => {
            //     kassaNominationDtos.Add(new KassaNominationDto {
            //         KassaNominationId = kn.KassaNominationId,
            //         Amount = kn.Amount,
            //         Total = kn.Total,
            //         NominationId = kn.NominationId,
            //         Nomination = kn.Nomination != null ? new NominationDto {
            //             NominationId = kn.Nomination.NominationId,
            //             Nom = kn.Nom != null ? new NomDto {
            //                 Currency = kn.Nom.Currency,
            //                 Multiplier = kn.Nom.Multiplier
            //             } : null
            //         } : null
            //     });
            // });
            var kassaContainerReturnDto = new KassaContainerReturnDto()
            {
                Activiteit = kassaContainer.Activiteit,
                Afroomkluis = kassaContainer.Afroomkluis,
                BeginUur = kassaContainer.BeginUur,
                Bezoekers = kassaContainer.Bezoekers,
                DateAdded = kassaContainer.DateAdded,
                DateUpdated = kassaContainer.DateUpdated,
                EindUur = kassaContainer.EindUur,
                FKassaId = kassaContainer.FKassaId,
                InkomstBar = kassaContainer.InkomstBar,
                InkomstLidkaart = kassaContainer.InkomstLidkaart,
                KassaContainerId = kassaContainer.KassaContainerId,
                Notes = kassaContainer.Notes,
                State = kassaContainer.State.ToString(),
                KassaDto = kassaContainer.Kassas.Select(kassa => new KassaDto() 
                {
                    KassaId = kassa == null ? 0 : kassa.KassaId,
                    Type = kassa?.Type,
                    KassaNominations = kassa.KassaNominations
                        .Select(kn => new KassaNominationDto {
                            KassaNominationId = kn.KassaNominationId,
                            Amount = kn.Amount,
                            Total = kn.Total,
                            NominationId = kn.NominationId,
                            Nomination = kn.Nomination != null ? new NominationDto {
                                NominationId = kn.Nomination.NominationId,
                                Nom = kn.Nom != null ? new NomDto {
                                    Currency = kn.Nom.Currency,
                                    Multiplier = kn.Nom.Multiplier
                                } : null
                            } : null
                        }).ToList()
                }).LastOrDefault(),
                // lstKassaDtos = new List<KassaDto>()
                lstKassaDtos = kassaContainer.Kassas.Select(k => new KassaDto {
                    KassaId = k.KassaId,
                    Status = k.Status,
                    Type = k.Type,
                    KassaNominations = k.KassaNominations
                        .Select(x => new KassaNominationDto {
                            Total = x.Total
                        })
                        .ToList()
                }).ToList()
            };
            // kassaContainer.Kassas.ToList().ForEach(k => {
            //     kassaContainerReturnDto.lstKassaDtos.Add(new KassaDto {
            //         KassaId = k.KassaId,
            //         Status = k.Status,
            //         Type = k.Type,
            //         KassaNominations = k.KassaNominations
            //             .Select(x => new KassaNominationDto {
            //                 Total = x.Total
            //             })
            //             .ToList()
            //     });
            // });
            

            return kassaContainerReturnDto;
        }

        // [HttpGet("getKassaContainerTotal")]
        // public async Task<IActionResult<KassaContainerTotalDto>>(int id, KassaContainerDto)

        // PUT: api/KassaContainer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKassaContainer(int id, KassaContainerDto kassaContainerDto)
        {
            if (id != kassaContainerDto.KassaContainerId)
            {
                return BadRequest();
            }

            ApplicationUser user = await _userManager.GetUserAsync(User);
            //Don't save these fields
            var excluded = new[] 
            { 
                "CreatedBy", 
                "DateAdded",
                "FKassaId"
            };
            var kassaContainer = new KassaContainer()
            {
                KassaContainerId = kassaContainerDto.KassaContainerId,
                Active = true,
                Activiteit = kassaContainerDto.Activiteit,
                Afroomkluis = kassaContainerDto.Afroomkluis,
                BeginUur = kassaContainerDto.BeginUur,
                EindUur = kassaContainerDto.EindUur,
                Bezoekers = kassaContainerDto.Bezoekers,
                UpdatedBy = user.Id,
                DateUpdated = DateTime.Now,
                InkomstLidkaart = kassaContainerDto.InkomstLidkaart,
                InkomstBar = kassaContainerDto.InkomstBar,
                Notes = kassaContainerDto.Notes,
                State = (States) Enum.Parse(typeof(States), kassaContainerDto.State, true)
            };

            //Entities can have multiple states: https://docs.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state
            //Use this to not save certain fields that should be unmutable
            _context.Entry(kassaContainer).State = EntityState.Modified;

            foreach(var name in excluded)
            {
                _context.Entry(kassaContainer).Property(name).IsModified = false;
            }

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
    
        //PUT: api/kassaContainer/updateState
        [HttpPut("updateState")]
        public async Task<IActionResult> PutKassaContainer(int id, string state, KassaContainerReturnDto kassaContainerDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var kassaContainer = await _context.KassaContainers
                .Where(kc => 
                    kc.KassaContainerId == id
                    && kc.Active == true)
                .FirstOrDefaultAsync();
            kassaContainer.UpdatedBy = user.Id;
            kassaContainer.DateUpdated = DateTime.Now;
            kassaContainer.State = (States) Enum.Parse(typeof(States), state, true);

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
                State = (States) Enum.Parse(typeof(States), kcDto.State, true), //parse string to enum
                Notes = kcDto.Notes,
                InkomstBar = kcDto.InkomstBar,
                InkomstLidkaart = kcDto.InkomstLidkaart,
                CreatedBy = user.Id,
                UpdatedBy = user.Id,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
                FKassaId = kcDto.FKassaId
            };
            _context.KassaContainers.Add(kassaContainer);

            //Save the kassacontainer to the FKassa Collection
            var fKassa = _context.FKassa.Where(x => x.FKassaId == kassaContainer.FKassaId).FirstOrDefault();
            fKassa.KassaContainers.Add(kassaContainer);
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
                State = kassaContainer.State.ToString(),
                Notes = kassaContainer.Notes,
                InkomstBar = kassaContainer.InkomstBar,
                InkomstLidkaart = kassaContainer.InkomstLidkaart,
                CreatedBy = kassaContainer.CreatedBy,
                UpdatedBy = kassaContainer.UpdatedBy,
                DateAdded = kassaContainer.DateAdded,
                DateUpdated = kassaContainer.DateUpdated,
                FKassaId = kassaContainer.FKassaId,
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
