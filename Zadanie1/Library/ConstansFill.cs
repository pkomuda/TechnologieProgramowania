namespace Library
{
    class ConstansFill : DataFill
    {
        public ConstansFill() {}
        public void Fill(DataContext context)
        {
            #region Clients
            context.Clients.Add(new Client("Jan", "Kowalski"));
            context.Clients.Add(new Client("Jakub", "Nowak"));
            context.Clients.Add(new Client("Krzesimir", "Mniejszy"));
            context.Clients.Add(new Client("Kazimierz", "Wielki"));
            context.Clients.Add(new Client("Boleslaw", "Chrobry"));
            context.Clients.Add(new Client("Wladyslaw", "Lokietek"));
            context.Clients.Add(new Client("Wladyslaw", "Jagiello"));
            context.Clients.Add(new Client("Zygmunt", "August"));
            context.Clients.Add(new Client("Zygmunt", "Stary"));
            context.Clients.Add(new Client("Wladyslaw", "Warnenczyk"));
            #endregion
            #region Books (Catalog)
            context.Books.Add("100", new Catalog("Krzyzacy", "Henryk Sienkiewicz"));
            context.Books.Add("101", new Catalog("Kroniki Czarnej Kompanii", "Glen Cook"));
            context.Books.Add("102", new Catalog("Pan Tadeusz", "Adam Mickiewicz"));
            context.Books.Add("103", new Catalog("Zbrodnia i Kara", "Fiodor Dostojewski"));
            context.Books.Add("104", new Catalog("Dziady cz. 3", "Adam Mickiewicz"));
            context.Books.Add("105", new Catalog("Ogniem i Mieczem", "Henryk Sienkiewicz"));
            context.Books.Add("106", new Catalog("Pan Lodowego Ogrodu", "Jaroslaw Grzedowicz"));
            context.Books.Add("107", new Catalog("Harry Potter", "J.K. Rowling"));
            context.Books.Add("108", new Catalog("Przedwiosnie", "Stefan Zeromski"));
            context.Books.Add("109", new Catalog("Lalka", "Boleslaw Prus"));
            #endregion
            #region Inventories
            context.Inventories.Add(new Inventory(context.Books["100"], 10));
            context.Inventories.Add(new Inventory(context.Books["101"], 5));
            context.Inventories.Add(new Inventory(context.Books["102"], 7));
            context.Inventories.Add(new Inventory(context.Books["103"], 3));
            context.Inventories.Add(new Inventory(context.Books["104"], 4));
            context.Inventories.Add(new Inventory(context.Books["105"], 8));
            context.Inventories.Add(new Inventory(context.Books["106"], 9));
            context.Inventories.Add(new Inventory(context.Books["107"], 2));
            context.Inventories.Add(new Inventory(context.Books["108"], 13));
            context.Inventories.Add(new Inventory(context.Books["109"], 4));
            #endregion
            #region Events
            context.Events.Add(new Event(context.Clients[0], context.Inventories[4], new System.DateTime(2019, 10, 20), new System.DateTime(2020, 1, 20)));
            context.Events.Add(new Event(context.Clients[1], context.Inventories[3], new System.DateTime(2019, 12, 2), new System.DateTime(2020, 3, 2)));
            context.Events.Add(new Event(context.Clients[2], context.Inventories[2], new System.DateTime(2019, 11, 13), new System.DateTime(2020, 2, 13)));
            context.Events.Add(new Event(context.Clients[3], context.Inventories[1], new System.DateTime(2019, 9, 11), new System.DateTime(2019, 12, 11)));
            context.Events.Add(new Event(context.Clients[4], context.Inventories[0], new System.DateTime(2019, 8, 22), new System.DateTime(2019, 11, 22))); 
            #endregion
        }
    }
}
