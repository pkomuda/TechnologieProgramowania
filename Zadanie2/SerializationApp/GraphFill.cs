using Library;
using System.Collections.Generic;

namespace SerializationApp
{
    public class GraphFill : DataFill
    {
        public void Fill(DataContext context)
        {
            #region Books (Catalog)
            Catalog c1 = new Catalog("Kroniki Czarnej Kompanii", "Glen Cook");
            Catalog c2 = new Catalog("Zbrodnia i Kara", "Fiodor Dostojewski");
            Catalog c3 = new Catalog("Ogniem i Mieczem", "Henryk Sienkiewicz");
            Catalog c4 = new Catalog("Pan Lodowego Ogrodu", "Jaroslaw Grzedowicz");
            Catalog c5 = new Catalog("Lalka", "Boleslaw Prus");
            context.Books.Add(c1.ID, c1);
            context.Books.Add(c2.ID, c2);
            context.Books.Add(c3.ID, c3);
            context.Books.Add(c4.ID, c4);
            context.Books.Add(c5.ID, c5);
            #endregion
            #region Clients
            List<Catalog> rented = new List<Catalog>();
            rented.Add(c1);
            rented.Add(c3);
            context.Clients.Add(new Client("1", "Jakub", "Nowak", rented));
            context.Clients.Add(new Client("Kazimierz", "Wielki"));
            context.Clients.Add(new Client("Boleslaw", "Chrobry"));
            context.Clients.Add(new Client("Wladyslaw", "Warnenczyk"));
            #endregion
            #region Inventories
            context.Inventories.Add(new Inventory(context.Books[c1.ID], 10));
            context.Inventories.Add(new Inventory(context.Books[c2.ID], 5));
            context.Inventories.Add(new Inventory(context.Books[c3.ID], 7));
            context.Inventories.Add(new Inventory(context.Books[c4.ID], 3));
            context.Inventories.Add(new Inventory(context.Books[c5.ID], 4));
            #endregion
            #region Events
            context.Events.Add(new Rent(context.Clients[1], context.Inventories[1], new System.DateTime(), new System.DateTime(2020, 1, 1)));
            context.Events.Add(new Rent(context.Clients[1], context.Inventories[3], new System.DateTime(), new System.DateTime(2020, 1, 1)));
            context.Events.Add(new Purchase(context.Inventories[2], new System.DateTime(), 5));
            context.Events.Add(new Return(context.Clients[1], context.Inventories[1], new System.DateTime(2022, 2, 2)));
            context.Events.Add(new Discard(context.Inventories[2], new System.DateTime()));
            #endregion
        }
    }
}