using System.ComponentModel.DataAnnotations;

namespace Soaps.Model
{
    public class SoapDetail
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Soap Soap { get; set; }
    }
}
