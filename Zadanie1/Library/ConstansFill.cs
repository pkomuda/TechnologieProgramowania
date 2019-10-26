namespace Library
{
    public class ConstansFill : DataFill
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
            Catalog c1 = new Catalog("Krzyzacy", "Henryk Sienkiewicz");
            Catalog c2 = new Catalog("Kroniki Czarnej Kompanii", "Glen Cook");
            Catalog c3 = new Catalog("Pan Tadeusz", "Adam Mickiewicz");
            Catalog c4 = new Catalog("Zbrodnia i Kara", "Fiodor Dostojewski");
            Catalog c5 = new Catalog("Dziady cz. 3", "Adam Mickiewicz");
            Catalog c6 = new Catalog("Ogniem i Mieczem", "Henryk Sienkiewicz");
            Catalog c7 = new Catalog("Pan Lodowego Ogrodu", "Jaroslaw Grzedowicz");
            Catalog c8 = new Catalog("Harry Potter", "J.K. Rowling");
            Catalog c9 = new Catalog("Przedwiosnie", "Stefan Zeromski");
            Catalog c10 = new Catalog("Lalka", "Boleslaw Prus");
            context.Books.Add(c1.ID, c1);
            context.Books.Add(c2.ID, c2);
            context.Books.Add(c3.ID, c3);
            context.Books.Add(c4.ID, c4);
            context.Books.Add(c5.ID, c5);
            context.Books.Add(c6.ID, c6);
            context.Books.Add(c7.ID, c7);
            context.Books.Add(c8.ID, c8);
            context.Books.Add(c9.ID, c9);
            context.Books.Add(c10.ID, c10);
            #endregion
            #region Inventories
            context.Inventories.Add(new Inventory(context.Books[c1.ID], 10));
            context.Inventories.Add(new Inventory(context.Books[c2.ID], 5));
            context.Inventories.Add(new Inventory(context.Books[c3.ID], 7));
            context.Inventories.Add(new Inventory(context.Books[c4.ID], 3));
            context.Inventories.Add(new Inventory(context.Books[c5.ID], 4));
            context.Inventories.Add(new Inventory(context.Books[c6.ID], 8));
            context.Inventories.Add(new Inventory(context.Books[c7.ID], 9));
            context.Inventories.Add(new Inventory(context.Books[c8.ID], 2));
            context.Inventories.Add(new Inventory(context.Books[c9.ID], 13));
            context.Inventories.Add(new Inventory(context.Books[c10.ID], 4));
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
