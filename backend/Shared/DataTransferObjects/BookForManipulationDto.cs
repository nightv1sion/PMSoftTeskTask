using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract class BookForManipulationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Author { get; set; }
    }
}
