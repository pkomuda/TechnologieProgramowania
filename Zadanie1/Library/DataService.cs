using System;
using System.Collections.ObjectModel;

namespace Library
{
    public class DataService
    {
        private DataRepository dataRepository;

        public DataService()
        {
            dataRepository = new DataRepository();
        }

        public void AddClient(string id, string firstName, string lastName)
        {
            dataRepository.AddClient(id, firstName, lastName);
        }
        public void AddClient(string firstName, string lastName)
        {
            dataRepository.AddClient(firstName, lastName);
        }
        public void AddInventory(string catalogID, int amount)
        {
            dataRepository.AddInventory(catalogID, amount);
        }
        public void AddCatalog(string id, string title, string author)
        {
            dataRepository.AddCatalog(id, title, author);
        }
        public string GetCatalog(string catalogId)
        {
            return dataRepository.GetCatalog(catalogId).ToString();
        }
        
        public string AllCatalogs()
        {
            string catalogs = "";
            foreach (Catalog c in dataRepository.GetAllCatalogs())
            {
                catalogs += c + "\n";
            }
            return catalogs;
        }

        public string GetClient(string clientId)
        {
            return dataRepository.GetClient(clientId).ToString();
        }
        
        public string AllClients()
        {
            string clients = "";
            foreach (Client c in dataRepository.GetAllClients())
            {
                clients += c + "\n";
            }
            return clients;
        }

        public string GetEvent(string eventId)
        {
            return dataRepository.GetEvent(eventId).ToString();
        }
        
        public string AllEvents()
        {
            string events = "";
            foreach (Event e in dataRepository.GetAllEvents())
            {
                events += e + "\n";
            }
            return events;
        }

        public string GetInventory(string inventoryId)
        {
            return dataRepository.GetInventory(inventoryId).ToString();
        }
        
        public string AllInventories()
        {
            string inventories = "";
            foreach (Inventory i in dataRepository.GetAllInventories())
            {
                inventories += i + "\n";
            }
            return inventories;
        }
        
        public ObservableCollection<Event> EventsForClient(string clientId)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev is Rent rent && rent.Client.ID == clientId || ev is Return @return && @return.Client.ID == clientId)
                    events.Add(ev);
            }
            return events;
        }

        public ObservableCollection<Event> EventsBetweenDates(DateTime from, DateTime to)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev.Date >= from && ev.Date <= to)
                    events.Add(ev);
            }
            return events;
        }
        
        public ObservableCollection<Event> EventsBeforeDate(DateTime date)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev.Date <= date)
                    events.Add(ev);
            }
            return events;
        }

        public ObservableCollection<Event> EventsAfterDate(DateTime date)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev.Date >= date)
                    events.Add(ev);
            }
            return events;
        }

        public string CurrentlyRentedBooks(string clientId)
        {
            string rented = "";
            foreach (Catalog c in dataRepository.GetClient(clientId).RentedCatalogs)
                rented += c + "\n";
            return rented;
        }
        
        public int CurrentlyRentedBooksNumber(string clientId)
        {
            return dataRepository.GetClient(clientId).RentedCatalogs.Count;
        }

        public int NumberOfBooks(string catalogId)
        {
            return dataRepository.GetInventory(catalogId).Amount;
        }

        public int NumberOfEvents()
        {
            int number = 0;
            foreach (Event e in dataRepository.GetAllEvents())
                number++;
            return number;
        }

        public void RentBook(string clientId, string inventoryId)
        {
            if (dataRepository.GetInventory(inventoryId).Amount == 0)
                throw new InvalidOperationException("Book: "+ dataRepository.GetInventory(inventoryId).Catalog.Title+" not available.");
            if (dataRepository.GetClient(clientId).RentedCatalogs.Contains(dataRepository.GetInventory(inventoryId).Catalog))
                throw new InvalidOperationException("Client: "+ dataRepository.GetClient(clientId).FirstName+" "+ dataRepository.GetClient(clientId).LastName+" has already rented the book: "+ dataRepository.GetInventory(inventoryId).Catalog.Title+".");
            if (dataRepository.GetClient(clientId).Penalty > 10)
                throw new InvalidOperationException("Client: " + dataRepository.GetClient(clientId).FirstName + " " + dataRepository.GetClient(clientId).LastName + " has exceeded the maximum penalty amount, they have to pay it to rent more books");
            // Ustawienie daty zwrotu na tydzień od wypożyczenia
            Event ev = dataRepository.CreateRent(dataRepository.GetClient(clientId), dataRepository.GetInventory(inventoryId), DateTime.Today, DateTime.Today.AddDays(7));
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(inventoryId).reduceAmount(1);
            dataRepository.GetClient(clientId).RentedCatalogs.Add(dataRepository.GetInventory(inventoryId).Catalog);
        }

        public void ReturnBook(string clientId, string catalogId)
        {
            if (!dataRepository.GetClient(clientId).RentedCatalogs.Contains(dataRepository.GetCatalog(catalogId)))
                throw new InvalidOperationException("Client: "+ dataRepository.GetClient(clientId).FirstName+" "+ dataRepository.GetClient(clientId).LastName+" has already rented the book: "+ dataRepository.GetInventory(catalogId).Catalog.Title+".");
            Event ev = dataRepository.CreateReturn(dataRepository.GetClient(clientId), dataRepository.GetInventory(catalogId), DateTime.Today);
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(catalogId).increaseAmount(1);
            dataRepository.GetClient(clientId).RentedCatalogs.Remove(dataRepository.GetInventory(catalogId).Catalog);
            Rent last = dataRepository.CreateRent(null, null, DateTime.MinValue, DateTime.MaxValue);
            foreach (Event e in EventsForClient(clientId))
            {
                if (e is Rent rent && rent.Inventory.Catalog.ID == catalogId && rent.Date > last.Date)
                {
                    last.Date = rent.Date;
                    last.ReturnDate = rent.ReturnDate;
                }
            }
            if (DateTime.Today > last.ReturnDate)
                dataRepository.GetClient(clientId).Penalty += (int)(DateTime.Today - last.ReturnDate).TotalDays;
        }

        public void PurchaseBook(string catalogId, int amount)
        {
            Event ev = dataRepository.CreatePurchase(dataRepository.GetInventory(catalogId), DateTime.Today, amount);
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(catalogId).increaseAmount(amount);
        }

        public void DiscardBook(string catalogId, int amount)
        {
            if (dataRepository.GetInventory(catalogId).Amount < amount)
                throw new InvalidOperationException("Cannot discard " + amount + " catalogs, inventory only contains " + dataRepository.GetInventory(catalogId).Amount + ".");
            Event ev = dataRepository.CreateDiscard(new Inventory(dataRepository.GetCatalog(catalogId), amount), DateTime.Today);
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(catalogId).reduceAmount(amount);
        }

        public void ProlongRent(string eventId, int days)
        {
            if (!(dataRepository.GetEvent(eventId) is Rent rent))
                throw new InvalidOperationException("Event with ID: " + eventId + " is not of type Rent.");
            rent.ReturnDate = rent.ReturnDate.AddDays(days);
        }

        public int PenaltySumForAllClients()
        {
            int sum = 0;
            foreach (Client c in dataRepository.GetAllClients())
                sum += c.Penalty;
            return sum;
        }

        public string AllNotifications()
        {
            string notifications = "";
            foreach (string s in dataRepository.GetNotifications())
                notifications += s + "\n";
            return notifications;
        }
    }
}
