using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetoApi.Data;
using SetoClass.DTOs.Settings;
using SetoClass.Models;

namespace SetoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public SettingsController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        // GET: api/Settings
        [HttpGet]
        public async Task<ActionResult<SettingsReadDto>> GetSettings()
        {
            var settings = await _context.Settings.FirstOrDefaultAsync();

            if (settings == null)
            {
                return NotFound(new { message = "Settings not found." });
            }

            return _mapper.Map<SettingsReadDto>(settings);
        }

        // POST: api/Settings
        [HttpPost]
        public async Task<ActionResult<SettingsReadDto>> CreateSettings([FromBody] SettingsCreateDto settingsCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Settings.AnyAsync())
            {
                return Conflict(new { message = "Settings already exist." });
            }

            var settings = _mapper.Map<Settings>(settingsCreateDto);
            _context.Settings.Add(settings);
            await _context.SaveChangesAsync();

            var settingsReadDto = _mapper.Map<SettingsReadDto>(settings);

            return CreatedAtAction(nameof(GetSettings), settingsReadDto);
        }

        // PUT: api/Settings
        [HttpPut]
        public async Task<IActionResult> UpdateSettings([FromBody] SettingsUpdateDto settingsUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSettings = await _context.Settings.FirstOrDefaultAsync();

            if (existingSettings == null)
            {
                return NotFound(new { message = "Settings not found." });
            }

            _mapper.Map(settingsUpdateDto, existingSettings);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Settings/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, string folder = "uploads")
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file was uploaded.");

            try
            {
                var allowedExtensions = new[] { ".ico", ".png" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest("Only .ico and .png files are allowed.");

                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var uploadPath = Path.Combine(_environment.WebRootPath, folder);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
                var fileUrl = $"{baseUrl}/{folder}/{fileName}";

                return Ok(new { Url = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error uploading the file.", Detail = ex.Message });
            }
        }


    }
}
