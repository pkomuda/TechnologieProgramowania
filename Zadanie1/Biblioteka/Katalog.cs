using System;

namespace Biblioteka
{
    class Katalog
    {
        public string ID { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public uint RokWydania { get; set; }

        public Katalog()
        {
            ID = Guid.NewGuid().ToString();
            Tytul = "Brak tytulu";
            Autor = "Brak autora";
            RokWydania = 0;
        }

        public Katalog(string tytul, string autor, uint rokWydania)
        {
            ID = Guid.NewGuid().ToString();
            Tytul = tytul;
            Autor = autor;
            RokWydania = rokWydania;
        }

        public Katalog(string id, string tytul, string autor, uint rokWydania)
        {
            ID = id;
            Tytul = tytul;
            Autor = autor;
            RokWydania = rokWydania;
        }
    }
}
