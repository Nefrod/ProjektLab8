using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotekaChemiczna
{
    public class NiebezpiecznaReakcjaException : Exception
    {
        public NiebezpiecznaReakcjaException(string message) : base(message) { }
    }
}
