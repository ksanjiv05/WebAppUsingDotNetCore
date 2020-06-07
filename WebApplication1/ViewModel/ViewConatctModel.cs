using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModel
{
    public class ViewConatctModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        
        [MaxLength(100)]
        public string Message { get; set; }
        [Required]
        [MinLength(5)]
        public string Title { get; set; }

    }
}
