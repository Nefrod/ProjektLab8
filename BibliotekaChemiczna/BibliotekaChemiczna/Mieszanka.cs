using System.Collections.Generic;
using System.Linq;

namespace BibliotekaChemiczna
{
    public class Mieszanka
    {
        public List<ISubstancja> Skladniki { get; set; } = new();
        public string Efekt { get; set; } = string.Empty;
        public string Kolor { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{string.Join(" + ", Skladniki.Select(s => s.Nazwa))} → {Efekt} ({Kolor})";
        }
    }
}
