using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaChemiczna
{
    public class Substancja : ISubstancja
    {
        public required string Nazwa { get; set; }
        public required string StanSkupienia { get; set; }
        public required string Typ { get; set; }
        public required string Kolor { get; set; }

        public override string ToString() => $"{Nazwa} ({Typ})";
    }

}
