using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcmeCorpVinylProductCalalogueApi.Context;
using AcmeCorpVinylProductCalalogueApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace AcmeCorpVinylProductCalalogueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VinylAlbumsController : ControllerBase
    {
        private readonly VinylProductCatalogueContext _context;

        public VinylAlbumsController(VinylProductCatalogueContext context)
        {
            _context = context;
        }

        // GET: api/VinylAlbums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VinylAlbum>>> GetVinylAlbums()
        {
            return await _context.VinylAlbums.ToListAsync();
        }

        // GET: api/VinylAlbums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VinylAlbum>> GetVinylAlbum(Guid id)
        {
            var vinylAlbum = await _context.VinylAlbums.FindAsync(id);

            if (vinylAlbum == null)
            {
                return NotFound();
            }

            return vinylAlbum;
        }

        // PUT: api/VinylAlbums/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVinylAlbum(Guid id, VinylAlbum vinylAlbum)
        {
            if (id != vinylAlbum.Id)
            {
                return BadRequest();
            }

            _context.Entry(vinylAlbum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VinylAlbumExists(id))
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

        // POST: api/VinylAlbums
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<VinylAlbum>> PostVinylAlbum(VinylAlbum vinylAlbum)
        {
            _context.VinylAlbums.Add(vinylAlbum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVinylAlbum", new { id = vinylAlbum.Id }, vinylAlbum);
        }

        // DELETE: api/VinylAlbums/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VinylAlbum>> DeleteVinylAlbum(Guid id)
        {
            var vinylAlbum = await _context.VinylAlbums.FindAsync(id);
            if (vinylAlbum == null)
            {
                return NotFound();
            }

            _context.VinylAlbums.Remove(vinylAlbum);
            await _context.SaveChangesAsync();

            return vinylAlbum;
        }

        private bool VinylAlbumExists(Guid id)
        {
            return _context.VinylAlbums.Any(e => e.Id == id);
        }
    }
}
