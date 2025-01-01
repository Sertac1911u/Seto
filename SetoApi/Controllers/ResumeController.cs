using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetoApi.Data;
using SetoClass.DTOs.Resume;
using SetoClass.Models;

namespace SetoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public ResumeController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _environment = webHostEnvironment;
        }

        // GET: api/Resume
        [HttpGet]
        public async Task<ActionResult<ResumeReadDto>> GetResume()
        {
            var resume = await _context.Resumes
                .Include(r => r.Skills)
                .Include(r => r.Socials)
                .FirstOrDefaultAsync();

            if (resume == null)
            {
                return NotFound(new { message = "Resume not found." });
            }

            return _mapper.Map<ResumeReadDto>(resume);
        }

        // POST: api/Resume
        [HttpPost]
        public async Task<ActionResult<ResumeReadDto>> CreateResume([FromBody] ResumeCreateDto resumeCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tek bir Resume olmasını sağlamak için mevcut bir Resume varsa eklemeyin
            if (await _context.Resumes.AnyAsync())
            {
                return Conflict(new { message = "A resume already exists." });
            }

            var resume = _mapper.Map<Resume>(resumeCreateDto);

            // HTML içeriğini sanitize etmek için gerekli (güvenlik önlemi)
            var sanitizer = new Ganss.Xss.HtmlSanitizer();
            resume.Education = sanitizer.Sanitize(resume.Education);
            resume.Experience = sanitizer.Sanitize(resume.Experience);
            resume.Certification = sanitizer.Sanitize(resume.Certification);

            _context.Resumes.Add(resume);
            await _context.SaveChangesAsync();

            var resumeReadDto = _mapper.Map<ResumeReadDto>(resume);

            return CreatedAtAction(nameof(GetResume), resumeReadDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResume([FromBody] ResumeUpdateDto resumeUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingResume = await _context.Resumes
                .Include(r => r.Skills)
                .Include(r => r.Socials)
                .FirstOrDefaultAsync();

            if (existingResume == null)
            {
                return NotFound(new { message = "Resume not found." });
            }

            // Güncellenen Resume bilgilerini eşle
            _mapper.Map(resumeUpdateDto, existingResume);

            // Skills Güncelleme
            var updatedSkills = _mapper.Map<List<ResumeSkill>>(resumeUpdateDto.Skills);
            _context.ResumeSkills.RemoveRange(existingResume.Skills
                .Where(skill => !updatedSkills.Any(updated => updated.Id == skill.Id)));

            foreach (var skill in updatedSkills)
            {
                if (skill.Id == 0)
                {
                    existingResume.Skills.Add(skill); // Yeni eklenen skill
                }
                else
                {
                    var existingSkill = existingResume.Skills.FirstOrDefault(s => s.Id == skill.Id);
                    if (existingSkill != null)
                    {
                        _mapper.Map(skill, existingSkill); // Mevcut skill güncelle
                    }
                }
            }

            // Socials Güncelleme
            var updatedSocials = _mapper.Map<List<ResumeSocial>>(resumeUpdateDto.Socials);
            _context.ResumeSocials.RemoveRange(existingResume.Socials
                .Where(social => !updatedSocials.Any(updated => updated.Id == social.Id)));

            foreach (var social in updatedSocials)
            {
                if (social.Id == 0)
                {
                    existingResume.Socials.Add(social); // Yeni eklenen social
                }
                else
                {
                    var existingSocial = existingResume.Socials.FirstOrDefault(s => s.Id == social.Id);
                    if (existingSocial != null)
                    {
                        _mapper.Map(social, existingSocial); // Mevcut social güncelle
                    }
                }
            }

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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, string folder = "uploads")
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file was uploaded.");

            try
            {
                var allowedImageExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var allowedPdfExtensions = new[] { ".pdf" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                // Geçerli dosya türlerini kontrol et
                if (!allowedImageExtensions.Contains(fileExtension) && !allowedPdfExtensions.Contains(fileExtension))
                    return BadRequest("Only image and PDF files are allowed.");

                // Dosya için benzersiz bir ad oluştur
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var uploadPath = Path.Combine(_environment.WebRootPath, folder);

                // Klasörü oluştur
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);

                // Dosyayı kaydet
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


        // DELETE: api/Resume
        [HttpDelete]
        public async Task<IActionResult> DeleteResume()
        {
            var resume = await _context.Resumes
                .Include(r => r.Skills)
                .Include(r => r.Socials)
                .FirstOrDefaultAsync();

            if (resume == null)
            {
                return NotFound(new { message = "Resume not found." });
            }

            _context.ResumeSkills.RemoveRange(resume.Skills);
            _context.ResumeSocials.RemoveRange(resume.Socials);
            _context.Resumes.Remove(resume);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
