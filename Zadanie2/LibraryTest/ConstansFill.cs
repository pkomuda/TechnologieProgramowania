using Library;
using System.Collections.Generic;

namespace LibraryTest
{
    public class ConstansFill : DataFill
    {
        public ConstansFill() {}
        public void Fill(DataContext context)
        {
           
            #region Books (Catalog)
            Catalog c1 = new Catalog("1", "Krzyzacy", "Henryk Sienkiewicz");
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
            #region Clients
            List<Catalog> rented = new List<Catalog>();
            rented.Add(c1);
            rented.Add(c3);
            context.Clients.Add(new Client("1", "Jan", "Kowalski", rented));
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
            context.Events.Add(new Rent(context.Clients[1], context.Inventories[1], new System.DateTime(), new System.DateTime(2020, 1, 1)));
            context.Events.Add(new Purchase(context.Inventories[2], new System.DateTime(), 5));
            context.Events.Add(new Return(context.Clients[1], context.Inventories[1], new System.DateTime(2022, 2, 2)));
            context.Events.Add(new Discard(context.Inventories[2], new System.DateTime()));
            #endregion
        }
    }
}
