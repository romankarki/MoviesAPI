using Codeology_Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Codeology_Tests.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MoviesContext _context;
    public MoviesController(MoviesContext context)
    {
        _context = context;
    }

    [HttpGet("GetMovies")]
    public async Task<List<Movies>> Get()
    {
        var results = await _context.MoviesDB.ToListAsync();
        return results;
    }

    [HttpPost("PostMovie")]
    public async Task<IActionResult> Post(MovieRequest movie)
    {
        Movies data = new Movies
        {
            Name = movie.Name,
            Rating = movie.Rating
        };
        await _context.MoviesDB.AddAsync(data);
        var response = await _context.SaveChangesAsync();
        if(response > 0)
            return Ok(data);
        return BadRequest(0);
    }

    [HttpPut("EditMovie")]
    public async Task<IActionResult> Edit(MovieRequest movie)
    {
        Movies data = new Movies
        {
            Id = movie.Id,
            Name = movie.Name,
            Rating = movie.Rating
        };
        var toUpdate = await _context.MoviesDB.FindAsync(movie.Id);
        if (toUpdate == null) return NotFound();
        toUpdate.Name = movie.Name;
        toUpdate.Rating = movie.Rating;

        await _context.SaveChangesAsync();
        return Ok(data);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var toDelete = await _context.MoviesDB.FindAsync(id);
        if (toDelete == null) return NotFound();
        _context.MoviesDB.Remove(toDelete);
        await _context.SaveChangesAsync();
        return Ok("Sucessfully deleted your Movie");
    }
}
