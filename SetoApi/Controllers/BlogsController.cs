using AngleSharp.Dom;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SetoApi.Data;
using SetoClass.DTOs.Blog;
using SetoClass.Models;
using SetoClass.Settings;

namespace SetoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BlogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly FileSettings _fileSettings;
        private readonly IMapper _mapper;

        public BlogsController(ApplicationDbContext context, IWebHostEnvironment environment, IOptions<FileSettings> fileSettings, IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _fileSettings = fileSettings.Value;
            _mapper = mapper;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogReadDto>>> GetBlogs()
        {
            var blogs = await _context.Blogs.ToListAsync();
            var blogsReadDto = _mapper.Map<List<BlogReadDto>>(blogs);
            return blogsReadDto;
        }

        [HttpGet("indexblogs")]
        public async Task<ActionResult<IEnumerable<BlogReadDto>>> GetBlogstoIndex()
        {
            var indexBlogs = await _context.Blogs.Take(4).ToListAsync();
            var indexBlogsReadDTO = _mapper.Map<List<BlogReadDto>>(indexBlogs);
            return indexBlogsReadDTO;
        }


        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogReadDto>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            var blogReadDto = _mapper.Map<BlogReadDto>(blog);
            return blogReadDto;
        }

        [HttpGet("random")]
        public async Task<ActionResult<IEnumerable<BlogReadDto>>> GetRandomBlogs(int count = 4)
        {
            var totalBlogs = await _context.Blogs.CountAsync();
            if (totalBlogs == 0)
                return NotFound(new { message = "No blogs found." });

            var randomBlogs = await _context.Blogs
                .OrderBy(b => Guid.NewGuid())
                .Take(count)
                .ToListAsync();

            var blogDtos = _mapper.Map<IEnumerable<BlogReadDto>>(randomBlogs);

            return Ok(blogDtos);
        }

        // POST: api/Blogs
        [HttpPost]
        public async Task<ActionResult<BlogReadDto>> CreateBlog([FromBody] BlogCreateDto blogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = _mapper.Map<Blog>(blogDto);
            blog.Date = DateTime.UtcNow;
            blog.Likes = 0;
            blog.Comments = 0;
            blog.Saves = 0;
            blog.Views = 0;

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            var blogReadDto = _mapper.Map<BlogReadDto>(blog);

            return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blogReadDto);
        }

        // PUT: api/Blogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogUpdateDto blogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingBlog = await _context.Blogs.FindAsync(id);
            if (existingBlog == null)
            {
                return NotFound();
            }

            _mapper.Map(blogDto, existingBlog);

            _context.Entry(existingBlog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file was uploaded.");

            try
            {
                // Benzersiz bir dosya adı oluştur
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                // Dosyanın kaydedileceği klasör
                var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");

                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Tam dosya yolu
                var filePath = Path.Combine(uploadPath, fileName);

                // Dosyayı kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // API'nin base URL'sini oluştur
                var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";

                // Tam URL (wwwroot kısmı olmadan)
                var imageUrl = $"{baseUrl}/uploads/{fileName}";

                // Başarılı dönüş
                return Ok(new { Url = imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error uploading the file.", Detail = ex.Message });
            }
        }
        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound(new { message = "Blog not found." });
            }

            try
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Blog deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting the blog.", detail = ex.Message });
            }
        }



        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
