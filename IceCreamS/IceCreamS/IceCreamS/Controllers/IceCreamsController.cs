using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IceCreamS.Data;
using IceCreamS.Models;

namespace IceCreamS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IceCreamsController : ControllerBase
    {
        private readonly IceCreamSContext _context;

        public IceCreamsController(IceCreamSContext context)
        {
            _context = context;
        }

        // GET: api/IceCreams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IceCream>>> GetIceCream()
        {
            return await _context.IceCream.ToListAsync();
        }

        // GET: api/IceCreams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IceCream>> GetIceCream(int id)
        {
            var iceCream = await _context.IceCream.FindAsync(id);

            if (iceCream == null)
            {
                return NotFound();
            }

            return iceCream;
        }

        // PUT: api/IceCreams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIceCream(int id, IceCream iceCream)
        {
            if (id != iceCream.Id)
            {
                return BadRequest();
            }

            _context.Entry(iceCream).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IceCreamExists(id))
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

        // POST: api/IceCreams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IceCream>> PostIceCream(IceCream iceCream)
        {
            _context.IceCream.Add(iceCream);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIceCream", new { id = iceCream.Id }, iceCream);
        }

        // DELETE: api/IceCreams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIceCream(int id)
        {
            var iceCream = await _context.IceCream.FindAsync(id);
            if (iceCream == null)
            {
                return NotFound();
            }

            _context.IceCream.Remove(iceCream);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IceCreamExists(int id)
        {
            return _context.IceCream.Any(e => e.Id == id);
        }
    }
}
