using System;

namespace Biblioteka
{
    public class Wykaz
    {
        public string ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        public Wykaz()
        {
            ID = Guid.NewGuid().ToString();
            Imie = "Brak imienia";
            Nazwisko = "Brak nazwiska";
        }

        public Wykaz(string imie, string nazwisko)
        {
            ID = Guid.NewGuid().ToString();
            Imie = imie;
            Nazwisko = nazwisko;
        }

        public Wykaz(string id, string imie, string nazwisko)
        {
            ID = id;
            Imie = imie;
            Nazwisko = nazwisko;
        }
    }
}
