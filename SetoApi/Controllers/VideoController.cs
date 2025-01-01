using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetoApi.Data;
using AutoMapper;
using SetoClass.DTOs.Video;
using SetoClass.Models;

namespace SetoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public VideosController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _environment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoReadDto>>> GetVideos()
        {
            var videos = await _context.Videos
                .OrderByDescending(v => v.UploadDate)
                .ToListAsync();

            var videoDtos = _mapper.Map<IEnumerable<VideoReadDto>>(videos);

            return Ok(videoDtos);
        }

        [HttpGet("indexvideos")]
        public async Task<ActionResult<IEnumerable<VideoReadDto>>> GetVideostoIndex()
        {
            var videos = await _context.Videos
                .OrderByDescending(v => v.UploadDate)
                .ToListAsync();

            var videosReadDto = _mapper.Map<List<VideoReadDto>>(videos);
            return videosReadDto;
        }

        // GET: api/Videos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoReadDto>> GetVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            var videoReadDto = _mapper.Map<VideoReadDto>(video);
            return videoReadDto;
        }

        // GET: api/Videos/GetRandom
        [HttpGet("GetRandom")]
        public async Task<ActionResult<IEnumerable<VideoReadDto>>> GetRandomVideos()
        {
            var randomVideos = await _context.Videos
                                            .OrderBy(v => Guid.NewGuid())
                                            .Take(4)
                                            .ToListAsync();

            var randomVideosReadDto = _mapper.Map<List<VideoReadDto>>(randomVideos);
            return randomVideosReadDto;
        }

        // POST: api/Videos
        [HttpPost]
        public async Task<ActionResult<VideoReadDto>> CreateVideo([FromBody] VideoCreateDto videoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var video = _mapper.Map<Video>(videoDto);
            video.UploadDate = DateTime.UtcNow;
            video.Likes = 0;
            video.Views = 0;
            video.Saves = 0;

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            var videoReadDto = _mapper.Map<VideoReadDto>(video);

            return CreatedAtAction(nameof(GetVideo), new { id = video.Id }, videoReadDto);
        }

        // PUT: api/Videos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVideo(int id, [FromBody] VideoUpdateDto videoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVideo = await _context.Videos.FindAsync(id);
            if (existingVideo == null)
            {
                return NotFound();
            }

            _mapper.Map(videoDto, existingVideo);

            _context.Entry(existingVideo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
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

        // DELETE: api/Videos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadMedia(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file was uploaded.");

            try
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var uploadPath = Path.Combine(_environment.WebRootPath, "uploads/videos");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
                var fileUrl = $"{baseUrl}/uploads/videos/{fileName}";

                return Ok(new { Url = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error uploading file.", Detail = ex.Message });
            }
        }

    }
}
