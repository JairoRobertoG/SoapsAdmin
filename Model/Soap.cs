
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soaps.Model
{
    public class Soap
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public SoapType SoapType { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        public ICollection<SoapDetail> SoapDetails { get; set; }
    }
}
