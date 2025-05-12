using System.Collections.Generic;
using System.Linq;

namespace BibliotekaChemiczna
{
    public class GotowaReceptura
    {
        public List<string> Skladniki { get; set; } = new();
        public string Produkt { get; set; } = string.Empty;
        public string Efekt { get; set; } = string.Empty;
        public string Kolor { get; set; } = string.Empty;

        public bool CzyPasuje(List<ISubstancja> podane)
        {
            var nazwy = podane.Select(s => s.Nazwa).OrderBy(x => x).ToList();
            var wymagane = Skladniki.OrderBy(x => x).ToList();
            return nazwy.SequenceEqual(wymagane);
        }
    }
}
