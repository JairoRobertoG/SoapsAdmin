using System;
using System.Collections.Generic;

namespace Soaps.Model
{
    public class SoapType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Soap> Soaps { get; set; }
    }
}
