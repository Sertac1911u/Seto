using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetoClass.DTOs.ResumeSocial
{
    public class ResumeSocialUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Platform { get; set; }

        [Required]
        [MaxLength(300)]
        public string Url { get; set; }
    }
}
