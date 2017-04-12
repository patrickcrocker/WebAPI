using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Treasures")]
    public class TreasuresController : Controller
    {
        private readonly TreasureContext _context;

        public TreasuresController(TreasureContext context)
        {
            _context = context;
        }

        // GET: api/Treasures
        [HttpGet]
        public IEnumerable<Treasure> GetTreasure()
        {
            return _context.Treasure;
        }

        // GET: api/Treasures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTreasure([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treasure = await _context.Treasure.SingleOrDefaultAsync(m => m.Id == id);

            if (treasure == null)
            {
                return NotFound();
            }

            return Ok(treasure);
        }

        // PUT: api/Treasures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTreasure([FromRoute] int id, [FromBody] Treasure treasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treasure.Id)
            {
                return BadRequest();
            }

            _context.Entry(treasure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreasureExists(id))
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

        // POST: api/Treasures
        [HttpPost]
        public async Task<IActionResult> PostTreasure([FromBody] Treasure treasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Treasure.Add(treasure);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TreasureExists(treasure.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTreasure", new { id = treasure.Id }, treasure);
        }

        // DELETE: api/Treasures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreasure([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var treasure = await _context.Treasure.SingleOrDefaultAsync(m => m.Id == id);
            if (treasure == null)
            {
                return NotFound();
            }

            _context.Treasure.Remove(treasure);
            await _context.SaveChangesAsync();

            return Ok(treasure);
        }

        private bool TreasureExists(int id)
        {
            return _context.Treasure.Any(e => e.Id == id);
        }
    }
}