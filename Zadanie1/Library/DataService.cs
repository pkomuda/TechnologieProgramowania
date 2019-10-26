using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library
{
    class DataService
    {
        private DataRepository dataRepository;

        public DataService()
        {
            dataRepository = new DataRepository();
        }

        public string GetCatalog(string catalogId)
        {
            return dataRepository.GetCatalog(catalogId).ToString();
        }
        
        public string AllCatalogs()
        {
            return dataRepository.GetAllCatalogs().ToString();
        }

        public string GetClient(string clientId)
        {
            return dataRepository.GetClient(clientId).ToString();
        }
        
        public string AllClients()
        {
            return dataRepository.GetAllClients().ToString();
        }

        public string GetEvent(string eventId)
        {
            return dataRepository.GetEvent(eventId).ToString();
        }
        
        public string AllEvents()
        {
            return dataRepository.GetAllEvents().ToString();
        }

        public string GetInventory(string inventoryId)
        {
            return dataRepository.GetInventory(inventoryId).ToString();
        }
        
        public IEnumerable<Inventory> AllInventories()
        {
            return dataRepository.GetAllInventories();
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

        public Event RentBook(string clientId, string inventoryId)
        {
            if (dataRepository.GetInventory(inventoryId).Amount == 0)
                throw new InvalidOperationException("Book: "+ dataRepository.GetInventory(inventoryId).Catalog.Title+" not available.");
            if (dataRepository.GetClient(clientId).RentedCatalogs.Contains(dataRepository.GetInventory(inventoryId).Catalog))
                throw new InvalidOperationException("Client: "+ dataRepository.GetClient(clientId).FirstName+" "+ dataRepository.GetClient(clientId).LastName+" has already rented the book: "+ dataRepository.GetInventory(inventoryId).Catalog.Title+".");
            if (dataRepository.GetClient(clientId).Penalty >= 10)
                throw new InvalidOperationException("Client: " + dataRepository.GetClient(clientId).FirstName + " " + dataRepository.GetClient(clientId).LastName + " has exceeded the maximum penalty amount, they have to pay it to rent more books");
            // Ustawienie daty zwrotu na tydzień od wypożyczenia
            Event ev = dataRepository.CreateRent(dataRepository.GetClient(clientId), dataRepository.GetInventory(inventoryId), DateTime.Today, DateTime.Today.AddDays(7));
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(inventoryId).reduceAmount(1);
            dataRepository.GetClient(clientId).RentedCatalogs.Add(dataRepository.GetInventory(inventoryId).Catalog);
            return ev;
        }

        public Event ReturnBook(string clientId, string catalogId)
        {
            if (!dataRepository.GetClient(clientId).RentedCatalogs.Contains(dataRepository.GetCatalog(catalogId)))
                throw new InvalidOperationException("Client: "+ dataRepository.GetClient(clientId).FirstName+" "+ dataRepository.GetClient(clientId).LastName+" has already rented the book: "+ dataRepository.GetInventory(catalogId).Catalog.Title+".");
            Event ev = dataRepository.CreateReturn(dataRepository.GetClient(clientId), dataRepository.GetInventory(catalogId), DateTime.Today);
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(catalogId).increaseAmount(1);
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
            return ev;
        }

        public Event PurchaseBook(string catalogId, int amount)
        {
            Event ev = dataRepository.CreatePurchase(dataRepository.GetInventory(catalogId), DateTime.Today, amount);
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(catalogId).increaseAmount(amount);
            return ev;
        }

        public Event DiscardBook(string catalogId, int amount)
        {
            if (dataRepository.GetInventory(catalogId).Amount < amount)
                throw new InvalidOperationException("Cannot discard " + amount + " catalogs, inventory only contains " + dataRepository.GetInventory(catalogId).Amount + ".");
            Event ev = dataRepository.CreateDiscard(new Inventory(dataRepository.GetCatalog(catalogId), amount), DateTime.Today);
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(catalogId).reduceAmount(amount);
            return ev;
        }

        public void ProlongRent(string eventId, int days)
        {
            if (!(dataRepository.GetEvent(eventId) is Rent rent))
                throw new InvalidOperationException("Event with ID: " + eventId + " is not of type Rent.");
            rent.ReturnDate.AddDays(days);
        }

        public int PenaltySumForAllClients()
        {
            int sum = 0;
            foreach (Client c in dataRepository.GetAllClients())
                sum += c.Penalty;
            return sum;
        }
    }
}
