using System;

namespace Library
{
    public class RandomFill : DataFill
    {
        public RandomFill() { }

        public void Fill(DataContext context)
        {
            Random rand = new Random();
            #region Clients
            string[] firstNames = new string [] { "Jan", "Jakub", "Krzesimir", "Kazimierz", "Boleslaw", "Wladyslaw",
                                                  "Zygmunt", "Przmyslaw", "Michal", "Maciej", "Czeslaw", "Kamil",
                                                  "Wlodzimierz", "Jaroslaw", "Grzegorz", "Antoni", "Zbigniew"
                                                };
            string[] lastNames = new string[] { "Kowalski", "Nowak", "Mniejszy", "Wiekszy", "Chrobry", "Lokietek",
                                                  "Jagiello", "August", "Stary", "Mlody", "Warnenczyk", "Wisniewski",
                                                  "Zielinski", "Malinowski", "Kwiatkowski", "Krzykliwy", "Kantor"
                                                };
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            context.Clients.Add(new Client(firstNames[rand.Next(0, 16)], lastNames[rand.Next(0, 16)]));
            #endregion
            #region Books (Catalog)
            Tuple<string, string>[] books = new Tuple<string, string>[] {
                          Tuple.Create("Krzyzacy", "Henryk Sienkiewicz"), Tuple.Create("Ogniem i Mieczem", "Henryk Sienkiewicz"),
                          Tuple.Create("Pan Tadeusz", "Adam Mickiewicz"), Tuple.Create("Pan Lodowego Ogrodu", "Jaroslaw Grzedowicz"),
                          Tuple.Create("Kroniki Czarnej Kompanii", "Glen Cook"), Tuple.Create("Harry Potter", "J.K. Rowling"),
                          Tuple.Create("Zbrodnia i Kara", "Fiodor Dostojewski"), Tuple.Create("Przedwiosnie", "Stefan Zeromski"),
                          Tuple.Create("Dziady cz. 3", "Adam Mickiewicz"), Tuple.Create("Lalka", "Boleslaw Prus")
            };
            #endregion
            #region Inventories
            int index = rand.Next(0, 9);
            string[] id = new string[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };
            context.Books.Add(id[0], new Catalog(books[index].Item1, books[index].Item2));
            index = rand.Next(0, 9);
            context.Books.Add(id[1], new Catalog(books[index].Item1, books[index].Item2));
            index = rand.Next(0, 9);
            context.Books.Add(id[2], new Catalog(books[index].Item1, books[index].Item2));
            context.Inventories.Add(new Inventory(context.Books[id[0]], rand.Next(0, 50)));
            context.Inventories.Add(new Inventory(context.Books[id[1]], rand.Next(0, 50)));
            context.Inventories.Add(new Inventory(context.Books[id[2]], rand.Next(0, 50))); 
            #endregion
        }
    }
}