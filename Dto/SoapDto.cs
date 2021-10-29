using System.Collections.Generic;

namespace Soaps.Dto
{
    public class SoapDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public bool Available { get; set; }

        public int Quantity { get; set; }

        public SoapTypeDto SoapType { get; set; }

        public string SoapTypeId { get; set; }

        public List<SoapDetailDto> SoapDetails { get; set; }

        public List<ImageDto> Images { get; set;  }
    }
}
