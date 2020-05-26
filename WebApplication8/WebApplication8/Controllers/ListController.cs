using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {

        private readonly TodoContext _context;

        public ListController(TodoContext context)
        {
            _context = context;
        }
         [HttpPost]
        public async Task<ActionResult<Films>> PostFilm(Films film)
        {
            _context.TodoItems.Add(film);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFilmById), new { id = film.Id }, film);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Films>>> GetFilms()
        { return await _context.TodoItems.ToListAsync();}
        [HttpGet("{id}")]
        public async Task<ActionResult<Films>> GetFilmById(long id)
        {
            var film = await _context.TodoItems.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilmById(long id)
        {
            var film = await _context.TodoItems.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}