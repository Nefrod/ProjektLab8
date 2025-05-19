using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibliotekaChemiczna
{
    public delegate void ReakcjaHandler(Mieszanka wynik);

    public class Eksperymentator
    {
        public List<ISubstancja> BibliotekaSubstancji { get; private set; } = new();
        public List<Mieszanka> Historia { get; private set; } = new();

        public event ReakcjaHandler? UdanaReakcja;
        public event Action<Mieszanka>? NiebezpiecznaReakcja;

        private List<GotowaReceptura> gotoweReakcje = new();

        public Eksperymentator()
        {
            InicjalizujBiblioteke();
            WczytajGotoweReakcje("receptury.json");
        }

        public Mieszanka? SprawdzGotowaReakcje(List<ISubstancja> skladniki)
        {
            foreach (var receptura in gotoweReakcje)
            {
                if (receptura.CzyPasuje(skladniki))
                {
                    var wynik = new Mieszanka
                    {
                        Skladniki = skladniki,
                        Efekt = receptura.Efekt,
                        Kolor = receptura.Kolor
                    };
                    Historia.Add(wynik);
                    UdanaReakcja?.Invoke(wynik);
                    return wynik;
                }
            }
            return null;
        }

        public void ZapiszGotoweReakcje(string plik)
        {
            var json = JsonSerializer.Serialize(gotoweReakcje, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(plik, json);
        }

        public void WczytajGotoweReakcje(string plik)
        {
            if (File.Exists(plik))
            {
                var json = File.ReadAllText(plik);
                gotoweReakcje = JsonSerializer.Deserialize<List<GotowaReceptura>>(json) ?? new List<GotowaReceptura>();
            }
            else
            {
                gotoweReakcje = new List<GotowaReceptura>
                {
                    new GotowaReceptura
                    {
                        Skladniki = new() { "Wodór", "Tlen" },
                        Produkt = "Woda",
                        Efekt = "Kondensacja",
                        Kolor = "Przezroczysty"
                    },
                    new GotowaReceptura
                    {
                        Skladniki = new() { "Sód", "Chlor" },
                        Produkt = "Chlorek sodu",
                        Efekt = "Krystalizacja",
                        Kolor = "Biały"
                    },
                    new GotowaReceptura
                    {
                        Skladniki = new() { "Sód", "Woda" },
                        Produkt = "Wodorotlenek sodu + wodór",
                        Efekt = "Eksplozja",
                        Kolor = "Srebrny"
                    },
                    new GotowaReceptura
                    {
                        Skladniki = new() { "Tlen", "Węgiel" },
                        Produkt = "Dwutlenek węgla",
                        Efekt = "Spalanie",
                        Kolor = "Bezbarwny"
                    }
                };

                ZapiszGotoweReakcje(plik);
            }
        }

        private void InicjalizujBiblioteke()
        {
            BibliotekaSubstancji = new List<ISubstancja>
            { 
            new Substancja { Nazwa = "Wodór", Typ = "Gaz", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Hel", Typ = "Gaz szlachetny", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Lit", Typ = "Metal alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Beryl", Typ = "Ziem alkaliczny", StanSkupienia = "Stały", Kolor = "Szarawy" },
            new Substancja { Nazwa = "Bor", Typ = "Metaloid", StanSkupienia = "Stały", Kolor = "Czarny" },
            new Substancja { Nazwa = "Węgiel", Typ = "Niemental", StanSkupienia = "Stały", Kolor = "Czarny" },
            new Substancja { Nazwa = "Azot", Typ = "Gaz", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Tlen", Typ = "Gaz", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Fluor", Typ = "Halogen", StanSkupienia = "Gaz", Kolor = "Zielonkawy" },
            new Substancja { Nazwa = "Neon", Typ = "Gaz szlachetny", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Sód", Typ = "Metal alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Magnez", Typ = "Ziem alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Glin", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Krzem", Typ = "Metaloid", StanSkupienia = "Stały", Kolor = "Szarawy" },
            new Substancja { Nazwa = "Fosfor", Typ = "Niemental", StanSkupienia = "Stały", Kolor = "Biały" },
            new Substancja { Nazwa = "Siarka", Typ = "Niemental", StanSkupienia = "Stały", Kolor = "Żółty" },
            new Substancja { Nazwa = "Chlor", Typ = "Halogen", StanSkupienia = "Gaz", Kolor = "Zielony" },
            new Substancja { Nazwa = "Argon", Typ = "Gaz szlachetny", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Potas", Typ = "Metal alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Wapń", Typ = "Ziem alkaliczny", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Skand", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Tytan", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Wanad", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Chrom", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Mangan", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Żelazo", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Kobalt", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Niebieskawy" },
            new Substancja { Nazwa = "Nikiel", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Miedź", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Czerwonawy" },
            new Substancja { Nazwa = "Cynk", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Gal", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "German", Typ = "Metaloid", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Arsen", Typ = "Metaloid", StanSkupienia = "Stały", Kolor = "Stalowoszary" },
            new Substancja { Nazwa = "Selen", Typ = "Niemental", StanSkupienia = "Stały", Kolor = "Czerwony" },
            new Substancja { Nazwa = "Brom", Typ = "Halogen", StanSkupienia = "Ciekły", Kolor = "Czerwonobrązowy" },
            new Substancja { Nazwa = "Krypton", Typ = "Gaz szlachetny", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Rubid", Typ = "Metal alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Stront", Typ = "Ziem alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Itr", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Cyrkon", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Niob", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Molibden", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Technet", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrnoszary" },
            new Substancja { Nazwa = "Ruten", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Rod", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Pallad", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Srebro", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Kadm", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrnobiały" },
            new Substancja { Nazwa = "Ind", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Cyna", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Antymon", Typ = "Metaloid", StanSkupienia = "Stały", Kolor = "Srebrnoszary" },
            new Substancja { Nazwa = "Tellur", Typ = "Metaloid", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Jod", Typ = "Halogen", StanSkupienia = "Stały", Kolor = "Fioletowy" },
            new Substancja { Nazwa = "Ksenon", Typ = "Gaz szlachetny", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Cez", Typ = "Metal alkaliczny", StanSkupienia = "Stały", Kolor = "Złoty" },
            new Substancja { Nazwa = "Bar", Typ = "Ziem alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Lantan", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrnobiały" },
            new Substancja { Nazwa = "Cer", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Prazeodym", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Neodym", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Promet", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Sam", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Różowy" },
            new Substancja { Nazwa = "Europ", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Gadolin", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Terb", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Dysproz", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Holm", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Erb", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Tul", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Iterb", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Lutet", Typ = "Lantanowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Hafn", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Tantal", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Wolfram", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Ren", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Osm", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Niebieskawy" },
            new Substancja { Nazwa = "Iryd", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Platyna", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrzystobiały" },
            new Substancja { Nazwa = "Złoto", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Złoty" },
            new Substancja { Nazwa = "Rtęć", Typ = "Metal przejściowy", StanSkupienia = "Ciekły", Kolor = "Srebrzysty" },
            new Substancja { Nazwa = "Tal", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Szaroniebieski" },
            new Substancja { Nazwa = "Ołów", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Bizmut", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Różowy" },
            new Substancja { Nazwa = "Polon", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Astat", Typ = "Halogen", StanSkupienia = "Stały", Kolor = "Ciemnoszary" },
            new Substancja { Nazwa = "Radon", Typ = "Gaz szlachetny", StanSkupienia = "Gaz", Kolor = "Bezbarwny" },
            new Substancja { Nazwa = "Franc", Typ = "Metal alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Rad", Typ = "Ziem alkaliczny", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Aktyn", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Tor", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrzystobiały" },
            new Substancja { Nazwa = "Protaktyn", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrzysty" },
            new Substancja { Nazwa = "Uran", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Zielonkawoszary" },
            new Substancja { Nazwa = "Neptun", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrnoszary" },
            new Substancja { Nazwa = "Pluton", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrzysty" },
            new Substancja { Nazwa = "Ameryk", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Kiur", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Berkel", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Kaliforn", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Einstein", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Ferm", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrnoszary" },
            new Substancja { Nazwa = "Mendelew", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrnoszary" },
            new Substancja { Nazwa = "Nobel", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrnoszary" },
            new Substancja { Nazwa = "Lorens", Typ = "Aktynowiec", StanSkupienia = "Stały", Kolor = "Srebrny" },

            new Substancja { Nazwa = "Rutherford", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Dubn", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Seaborg", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Boh", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Hassium", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Meitnerium", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Darmstadtium", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Roentgen", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Kopernik", Typ = "Metal przejściowy", StanSkupienia = "Stały", Kolor = "Srebrny" },

            new Substancja { Nazwa = "Nihonium", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Srebrzysty" },
            new Substancja { Nazwa = "Flerovium", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Moscovium", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Srebrny" },
            new Substancja { Nazwa = "Livermorium", Typ = "Metal bloku p", StanSkupienia = "Stały", Kolor = "Szary" },
            new Substancja { Nazwa = "Tennessine", Typ = "Halogen", StanSkupienia = "Stały", Kolor = "Ciemnoszary" },
            new Substancja { Nazwa = "Oganesson", Typ = "Gaz szlachetny", StanSkupienia = "Gaz", Kolor = "Bezbarwny" }
            };
        }

        public Mieszanka Polacz(ISubstancja substancja1, ISubstancja substancja2)
        {
            string typ1 = substancja1.Typ.ToLower();
            string typ2 = substancja2.Typ.ToLower();

            bool Czy(string a, string b) =>
                (typ1 == a && typ2 == b) || (typ1 == b && typ2 == a);


            if (Czy("metal", "kwas"))
            {
                return UtworzMieszanke(substancja1, substancja2, "Wydzielanie gazu", "Bąbelkowy");
            }


            if (Czy("gaz", "gaz"))
            {
                var mieszanka = new Mieszanka
                {
                    Skladniki = new List<ISubstancja> { substancja1, substancja2 },
                    Efekt = "Eksplozja",
                    Kolor = "Płomienny"
                };

                NiebezpiecznaReakcja?.Invoke(mieszanka);
                throw new NiebezpiecznaReakcjaException("Niebezpieczna reakcja! Eksplozja gazów.");
            }


            if (Czy("metal", "niemental"))
            {
                return UtworzMieszanke(substancja1, substancja2, "Utlenianie", "Szaroniebieski");
            }


            if (Czy("metal", "metal"))
            {
                return UtworzMieszanke(substancja1, substancja2, "Stopienie", "Metaliczny");
            }


            if (Czy("halogen", "metal"))
            {
                return UtworzMieszanke(substancja1, substancja2, "Reakcja egzotermiczna", "Pomarańczowy");
            }


            if (Czy("niemental", "gaz"))
            {
                return UtworzMieszanke(substancja1, substancja2, "Rozproszenie", "Mglisty");
            }


            if (typ1.Contains("szlachetny") || typ2.Contains("szlachetny"))
            {
                return UtworzMieszanke(substancja1, substancja2, "Brak reakcji", "Szklany");
            }


            return UtworzMieszanke(substancja1, substancja2, "Brak reakcji", "Szary");
        }

        // Pomocnicza metoda
        private Mieszanka UtworzMieszanke(ISubstancja a, ISubstancja b, string efekt, string kolor)
        {
            var mieszanka = new Mieszanka
            {
                Skladniki = new List<ISubstancja> { a, b },
                Efekt = efekt,
                Kolor = kolor
            };

            Historia.Add(mieszanka);
            UdanaReakcja?.Invoke(mieszanka);
            return mieszanka;
        }

        public void ZapiszHistorie(string sciezka)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter(), new ISubstancjaConverter() }
            };

            var json = JsonSerializer.Serialize(Historia, options);
            File.WriteAllText(sciezka, json);
        }


        public void WczytajHistorie(string sciezka)
        {
            if (!File.Exists(sciezka)) return;

            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter(), new ISubstancjaConverter() }
            };

            var json = File.ReadAllText(sciezka);
            var lista = JsonSerializer.Deserialize<List<Mieszanka>>(json, options);
            if (lista != null)
                Historia = lista;
        }
    }
}
