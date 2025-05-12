using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaChemiczna
{
    public interface ISubstancja
    {
        string Nazwa { get; }
        string StanSkupienia { get; }
        string Typ { get; } // np. Kwas, Zasada, Metal, itd.
        string Kolor { get; }
    }

}
