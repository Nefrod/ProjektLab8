using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using BibliotekaChemiczna;

namespace MainForm
{
    public partial class Form1 : Form
    {
        private Eksperymentator eksperymentator = new();
        private List<ISubstancja> zaznaczone = new();  
        private IEnumerable<object> periodicTable;
        private bool blokujZmianeListy = false;
        private Dictionary<string, string> mapaSymboli = new(); 
        private Dictionary<string, Button> przyciskiPierwiastkow = new();
        private ToolTip toolTipPierwiastki = new();
        private Dictionary<string, Color> koloryDomyslne = new(); 

        public Form1()
        {
            InitializeComponent();

            eksperymentator.UdanaReakcja += PokazReakcje;
            eksperymentator.NiebezpiecznaReakcja += mieszanka =>
            {
                MessageBox.Show($"Niebezpieczna reakcja: {mieszanka.Efekt}", "Uwaga!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };

            comboBoxFiltr.Items.Add("Wszystkie");
            comboBoxFiltr.Items.AddRange(eksperymentator.BibliotekaSubstancji
                .Select(s => s.Typ)
                .Distinct()
                .OrderBy(t => t)
                .ToArray());

            comboBoxFiltr.SelectedIndex = 0;

            OdswiezListeSubstancji();
            GenerujUkładOkresowy();  
        }

        private void OdswiezListeSubstancji()
        {
            var wybrane = comboBoxFiltr.SelectedItem?.ToString();
            var lista = eksperymentator.BibliotekaSubstancji;

            if (!string.IsNullOrWhiteSpace(wybrane) && wybrane != "Wszystkie")
                lista = lista.Where(s => s.Typ == wybrane).ToList();

            listBoxSubstancje.DataSource = lista;
            toolTip1.SetToolTip(listBoxSubstancje, "Wybierz co najmniej 2 substancje.");

            listBoxSubstancje.ClearSelected(); 
        }

        private void comboBoxFiltr_SelectedIndexChanged(object sender, EventArgs e)
        {
            OdswiezListeSubstancji();
        }

        private void listBoxSubstancje_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blokujZmianeListy) return;

            foreach (var btn in przyciskiPierwiastkow.Values)
                btn.BackColor = Color.LightBlue;

            zaznaczone.Clear();

            foreach (var item in listBoxSubstancje.SelectedItems.OfType<ISubstancja>())
            {
                zaznaczone.Add(item);

                if (mapaSymboli.TryGetValue(item.Nazwa, out var symbol) &&
                    przyciskiPierwiastkow.TryGetValue(symbol, out var btn))
                {
                    btn.BackColor = Color.LightGreen;
                }
            }
        }


        private void buttonPolacz_Click(object sender, EventArgs e)
        {
            var zaznaczone = listBoxSubstancje.SelectedItems.Cast<ISubstancja>().ToList();

            if (zaznaczone.Count < 2)
            {
                MessageBox.Show("Wybierz co najmniej 2 substancje.");
                return;
            }

            var wynik = eksperymentator.SprawdzGotowaReakcje(zaznaczone);
            if (wynik != null)
            {
                DodajDoTabeli(wynik);
                PokazReakcje(wynik);
                return;
            }

            if (zaznaczone.Count == 2)
            {
                try
                {
                    var wynikPolaczenia = eksperymentator.Polacz(zaznaczone[0], zaznaczone[1]);
                    DodajDoTabeli(wynikPolaczenia);
                    PokazReakcje(wynikPolaczenia);
                }
                catch (NiebezpiecznaReakcjaException ex)
                {
                    MessageBox.Show(ex.Message, "Reakcja niebezpieczna!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Brak znanej reakcji dla tych składników i nie można połączyć więcej niż 2 w niestandardowy sposób.");
            }
        }

        private void DodajDoTabeli(Mieszanka mieszanka)
        {
            dataGridMieszanki.Rows.Add(
                string.Join(" + ", mieszanka.Skladniki.Select(s => s.Nazwa)),
                mieszanka.Efekt,
                mieszanka.Kolor);
        }

        private void PokazReakcje(Mieszanka mieszanka)
        {
            labelEfekt.Text = $"Efekt: {mieszanka.Efekt}, Kolor: {mieszanka.Kolor}";
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eksperymentator.ZapiszHistorie("eksperymenty.json");
        }

        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eksperymentator.WczytajHistorie("eksperymenty.json");

            dataGridMieszanki.Rows.Clear();
            foreach (var m in eksperymentator.Historia)
            {
                dataGridMieszanki.Rows.Add(
                    string.Join(" + ", m.Skladniki.Select(s => s.Nazwa)),
                    m.Efekt,
                    m.Kolor);
            }
        }

        private void GenerujUkładOkresowy()
        {
            panelTablica.Controls.Clear();

            int cellSize = 40;
            int padding = 5;

            var układ = new Dictionary<string, (int X, int Y)>
            {
                { "H", (1, 1) }, { "He", (18, 1) }, { "Li", (1, 2) }, { "Be", (2, 2) }, { "B", (13, 2) }, { "C", (14, 2) },
                { "N", (15, 2) }, { "O", (16, 2) }, { "F", (17, 2) }, { "Ne", (18, 2) }, { "Na", (1, 3) }, { "Mg", (2, 3) },
                { "Al", (13, 3) }, { "Si", (14, 3) }, { "P", (15, 3) }, { "S", (16, 3) }, { "Cl", (17, 3) }, { "Ar", (18, 3) },
                { "K", (1, 4) }, { "Ca", (2, 4) }, { "Sc", (3, 4) }, { "Ti", (4, 4) }, { "V", (5, 4) }, { "Cr", (6, 4) },
                { "Mn", (7, 4) }, { "Fe", (8, 4) }, { "Co", (9, 4) }, { "Ni", (10, 4) }, { "Cu", (11, 4) }, { "Zn", (12, 4) },
                { "Ga", (13, 4) }, { "Ge", (14, 4) }, { "As", (15, 4) }, { "Se", (16, 4) }, { "Br", (17, 4) }, { "Kr", (18, 4) },
                { "Rb", (1, 5) }, { "Sr", (2, 5) }, { "Y", (3, 5) }, { "Zr", (4, 5) }, { "Nb", (5, 5) }, { "Mo", (6, 5) },
                { "Tc", (7, 5) }, { "Ru", (8, 5) }, { "Rh", (9, 5) }, { "Pd", (10, 5) }, { "Ag", (11, 5) }, { "Cd", (12, 5) },
                { "In", (13, 5) }, { "Sn", (14, 5) }, { "Sb", (15, 5) }, { "Te", (16, 5) }, { "I", (17, 5) }, { "Xe", (18, 5) },
                { "Cs", (1, 6) }, { "Ba", (2, 6) }, { "La", (3, 6) }, { "Hf", (4, 6) }, { "Ta", (5, 6) }, { "W", (6, 6) },
                { "Re", (7, 6) }, { "Os", (8, 6) }, { "Ir", (9, 6) }, { "Pt", (10, 6) }, { "Au", (11, 6) }, { "Hg", (12, 6) },
                { "Tl", (13, 6) }, { "Pb", (14, 6) }, { "Bi", (15, 6) }, { "Po", (16, 6) }, { "At", (17, 6) }, { "Rn", (18, 6) },
                { "Fr", (1, 7) }, { "Ra", (2, 7) }, { "Ac", (3, 7) }, { "Rf", (4, 7) }, { "Db", (5, 7) }, { "Sg", (6, 7) },
                { "Bh", (7, 7) }, { "Hs", (8, 7) }, { "Mt", (9, 7) }, { "Ds", (10, 7) }, { "Rg", (11, 7) }, { "Cn", (12, 7) },
                { "Nh", (13, 7) }, { "Fl", (14, 7) }, { "Mc", (15, 7) }, { "Lv", (16, 7) }, { "Ts", (17, 7) }, { "Og", (18, 7) },
                { "Ce", (4, 9) }, { "Pr", (5, 9) }, { "Nd", (6, 9) }, { "Pm", (7, 9) }, { "Sm", (8, 9) }, { "Eu", (9, 9) },
                { "Gd", (10, 9) }, { "Tb", (11, 9) }, { "Dy", (12, 9) }, { "Ho", (13, 9) }, { "Er", (14, 9) }, { "Tm", (15, 9) },
                { "Yb", (16, 9) }, { "Lu", (17, 9) }, { "Th", (4, 10) }, { "Pa", (5, 10) }, { "U", (6, 10) }, { "Np", (7, 10) },
                { "Pu", (8, 10) }, { "Am", (9, 10) }, { "Cm", (10, 10) }, { "Bk", (11, 10) }, { "Cf", (12, 10) }, { "Es", (13, 10) },
                { "Fm", (14, 10) }, { "Md", (15, 10) }, { "No", (16, 10) }, { "Lr", (17, 10) }
            };

            
            mapaSymboli = new Dictionary<string, string>
            {
                { "Wodór", "H" }, { "Hel", "He" }, { "Lit", "Li" }, { "Beryl", "Be" }, { "Bor", "B" }, { "Węgiel", "C" },
                { "Azot", "N" }, { "Tlen", "O" }, { "Fluor", "F" }, { "Neon", "Ne" }, { "Sód", "Na" }, { "Magnez", "Mg" },
                { "Glin", "Al" }, { "Krzem", "Si" }, { "Fosfor", "P" }, { "Siarka", "S" }, { "Chlor", "Cl" }, { "Argon", "Ar" },
                { "Potas", "K" }, { "Wapń", "Ca" }, { "Skand", "Sc" }, { "Tytan", "Ti" }, { "Wanad", "V" }, { "Chrom", "Cr" },
                { "Mangan", "Mn" }, { "Żelazo", "Fe" }, { "Kobalt", "Co" }, { "Nikiel", "Ni" }, { "Miedź", "Cu" },
                { "Cynk", "Zn" }, { "Gal", "Ga" }, { "German", "Ge" }, { "Arsen", "As" }, { "Selen", "Se" }, { "Brom", "Br" },
                { "Krypton", "Kr" }, { "Rubid", "Rb" }, { "Stront", "Sr" }, { "Itr", "Y" }, { "Cyrkon", "Zr" }, { "Niob", "Nb" },
                { "Molibden", "Mo" }, { "Technet", "Tc" }, { "Ruten", "Ru" }, { "Rod", "Rh" }, { "Pallad", "Pd" },
                { "Srebro", "Ag" }, { "Kadm", "Cd" }, { "Ind", "In" }, { "Cyna", "Sn" }, { "Antymon", "Sb" }, { "Tellur", "Te" },
                { "Jod", "I" }, { "Ksenon", "Xe" }, { "Cez", "Cs" }, { "Bar", "Ba" }, { "Lantan", "La" }, { "Cer", "Ce" },
                { "Prazeodym", "Pr" }, { "Neodym", "Nd" }, { "Promet", "Pm" }, { "Sam", "Sm" }, { "Europ", "Eu" },
                { "Gadolin", "Gd" }, { "Terb", "Tb" }, { "Dysproz", "Dy" }, { "Holm", "Ho" }, { "Erb", "Er" },
                { "Tul", "Tm" }, { "Iterb", "Yb" }, { "Lutet", "Lu" }, { "Hafn", "Hf" }, { "Tantal", "Ta" }, { "Wolfram", "W" },
                { "Ren", "Re" }, { "Osm", "Os" }, { "Iryd", "Ir" }, { "Platyna", "Pt" }, { "Złoto", "Au" }, { "Rtęć", "Hg" },
                { "Tal", "Tl" }, { "Ołów", "Pb" }, { "Bizmut", "Bi" }, { "Polon", "Po" }, { "Astat", "At" }, { "Radon", "Rn" },
                { "Franc", "Fr" }, { "Rad", "Ra" }, { "Aktyn", "Ac" }, { "Tor", "Th" }, { "Protaktyn", "Pa" }, { "Uran", "U" },
                { "Neptun", "Np" }, { "Pluton", "Pu" }, { "Ameryk", "Am" }, { "Kiur", "Cm" }, { "Berkel", "Bk" },
                { "Kaliforn", "Cf" }, { "Einstein", "Es" }, { "Ferm", "Fm" }, { "Mendelew", "Md" }, { "Nobel", "No" }, { "Lorens", "Lr" },
                { "Rutherford", "Rf" }, { "Dubn", "Db" }, { "Seaborg", "Sg" }, { "Boh", "Bh" }, { "Hassium", "Hs" },
                { "Meitnerium", "Mt" }, { "Darmstadtium", "Ds" }, { "Roentgen", "Rg" }, { "Kopernik", "Cn" },
                { "Nihonium", "Nh" }, { "Flerovium", "Fl" }, { "Moscovium", "Mc" }, { "Livermorium", "Lv" },
                { "Tennessine", "Ts" }, { "Oganesson", "Og" }

            };

            var symbolToSubstancja = new Dictionary<string, ISubstancja>();

            foreach (var sub in eksperymentator.BibliotekaSubstancji)
            {
                if (mapaSymboli.TryGetValue(sub.Nazwa, out var symbol) && układ.ContainsKey(symbol))
                {
                    symbolToSubstancja[symbol] = sub;
                }
            }

            foreach (var (symbol, (x, y)) in układ)
            {
                var btn = new Button
                {
                    Text = symbol,
                    Width = cellSize,
                    Height = cellSize,
                    Location = new Point(x * (cellSize + padding), y * (cellSize + padding)),
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat
                };

                
                ISubstancja substancja = null;
                string nazwa = mapaSymboli.FirstOrDefault(kv => kv.Value == symbol).Key;

                if (!string.IsNullOrEmpty(nazwa))
                {
                    substancja = eksperymentator.BibliotekaSubstancji.FirstOrDefault(s => s.Nazwa == nazwa);

                    if (substancja != null)
                    {
                        
                        var kolor = substancja.Typ switch
                        {
                            var t when t.Contains("Gaz") => Color.LightSkyBlue,
                            var t when t.Contains("Metal alkaliczny") || t.Contains("Ziem") => Color.Gold,
                            var t when t.Contains("Metal") => Color.LightSalmon,
                            var t when t.Contains("Halogen") => Color.Plum,
                            var t when t.Contains("Niemetal") => Color.LightGreen,
                            var t when t.Contains("Metaloid") => Color.LightGray,
                            _ => Color.LightBlue
                        };

                        btn.BackColor = kolor;
                        koloryDomyslne[symbol] = kolor; 

                        
                        toolTipPierwiastki.SetToolTip(btn, $"{substancja.Nazwa} – {substancja.Typ}");
                    }
                }

                
                przyciskiPierwiastkow[symbol] = btn;

                btn.Click += (s, e) =>
                {
                    
                    if (substancja == null || !listBoxSubstancje.Items.Contains(substancja))
                    {
                        MessageBox.Show("Substancja nie jest dostępna w obecnym filtrze.", "Niedostępne", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (zaznaczone.Any(z => z.Nazwa == substancja.Nazwa))
                    {
                        zaznaczone.RemoveAll(z => z.Nazwa == substancja.Nazwa);
                        btn.BackColor = koloryDomyslne[symbol]; 
                    }
                    else
                    {
                        zaznaczone.Add(substancja);
                        btn.BackColor = Color.LimeGreen; 
                    }

                    
                    blokujZmianeListy = true;
                    listBoxSubstancje.SelectedItem = null;
                    listBoxSubstancje.ClearSelected();
                    for (int i = 0; i < listBoxSubstancje.Items.Count; i++)
                    {
                        if (listBoxSubstancje.Items[i] is ISubstancja item && zaznaczone.Any(z => z.Nazwa == item.Nazwa))
                            listBoxSubstancje.SetSelected(i, true);
                    }
                    blokujZmianeListy = false;
                };

                panelTablica.Controls.Add(btn);
            }
        }
    }
}
