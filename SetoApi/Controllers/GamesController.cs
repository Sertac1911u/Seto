using AngleSharp.Dom;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SetoApi.Data;
using SetoClass.DTOs.Game;
using SetoClass.Models;
using SetoClass.Settings;

namespace SetoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly FileSettings _fileSettings;
        private readonly IMapper _mapper;

        public GamesController(ApplicationDbContext context, IWebHostEnvironment environment, IOptions<FileSettings> fileSettings, IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _fileSettings = fileSettings.Value;
            _mapper = mapper;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameReadDto>>> GetGames()
        {
            var games = await _context.Games
                .OrderByDescending(g => g.ReleaseDate)
                .ToListAsync();

            var gameDtos = _mapper.Map<IEnumerable<GameReadDto>>(games);

            return Ok(gameDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameReadDto>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            var gameReadDto = _mapper.Map<GameReadDto>(game);
            return gameReadDto;
        }

        [HttpPost]
        public async Task<ActionResult<GameReadDto>> CreateGame([FromBody] GameCreateDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = _mapper.Map<Game>(gameDto);
            game.ReleaseDate = DateTime.UtcNow;
            game.LikeCount = 0;
            game.SaveCount = 0;

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            var gameReadDto = _mapper.Map<GameReadDto>(game);

            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, gameReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] GameUpdateDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGame = await _context.Games.FindAsync(id);
            if (existingGame == null)
            {
                return NotFound();
            }

            _mapper.Map(gameDto, existingGame);

            _context.Entry(existingGame).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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


        [HttpGet("indexgames")]
        public async Task<ActionResult<IEnumerable<GameReadDto>>> GetGamestoIndex()
        {
            var games = await _context.Games
                .OrderByDescending(v => v.ReleaseDate)
                .ToListAsync();

            var GamesReadDto = _mapper.Map<List<GameReadDto>>(games);
            return GamesReadDto;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadGame(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file was uploaded.");

            try
            {
                // Benzersiz bir dosya adı oluştur
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var uploadPath = Path.Combine(_environment.WebRootPath, "games");

                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
                var fileUrl = $"{baseUrl}/games/{fileName}";

                return Ok(new { Url = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error uploading the file.", Detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
