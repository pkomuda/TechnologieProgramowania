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

        public string GetCatalog(String catalogId)
        {
            return dataRepository.GetCatalog(catalogId).ToString();
        }
        
        public string AllCatalogs()
        {
            return dataRepository.GetAllCatalogs().ToString();
        }

        public string GetClient(String clientId)
        {
            return dataRepository.GetClient(clientId).ToString();
        }
        
        public string AllClients()
        {
            return dataRepository.GetAllClients().ToString();
        }

        public string GetEvent(String eventId)
        {
            return dataRepository.GetEvent(eventId).ToString();
        }
        
        public string AllEvents()
        {
            return dataRepository.GetAllEvents().ToString();
        }

        public string GetInventory(String inventoryId)
        {
            return dataRepository.GetInventory(inventoryId).ToString();
        }
        
        public IEnumerable<Inventory> AllInventories()
        {
            return dataRepository.GetAllInventories();
        }

        public ObservableCollection<Event> EventsForClient(Client client)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev.Client == client)
                    events.Add(ev);
            }
            return events;
        }
        
        public ObservableCollection<Event> EventsForClientById(String id)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev.Client.ID == id)
                    events.Add(ev);
            }
            return events;
        }

        public ObservableCollection<Event> EventsBetweenDates(DateTime from, DateTime to)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev.BorrowDate.CompareTo(from)>=0 && ev.ReturnDate.CompareTo(to)<=0)
                    events.Add(ev);
            }
            return events;
        }
        
        public ObservableCollection<Event> EventsBeforeDate(DateTime date)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev.ReturnDate.CompareTo(date)<=0)
                    events.Add(ev);
            }
            return events;
        }
        
        public ObservableCollection<Event> EventsAfterDate(DateTime date)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in dataRepository.GetAllEvents())
            {
                if (ev.ReturnDate.CompareTo(date)>=0)
                    events.Add(ev);
            }
            return events;
        }

        public Event Borrow(String clientId, String inventoryId)
        {
            if (dataRepository.GetInventory(inventoryId).Amount == 0)
                throw new InvalidOperationException("Book: "+ dataRepository.GetInventory(inventoryId).Catalog.Title+" not available.");
            if (dataRepository.GetClient(clientId).RentedCatalogs.Contains(dataRepository.GetInventory(inventoryId).Catalog))
                throw new InvalidOperationException("Client: "+ dataRepository.GetClient(clientId).FirstName+" "+ dataRepository.GetClient(clientId).LastName+" has already rented the book: "+ dataRepository.GetInventory(inventoryId).Catalog.Title+".");
            if (dataRepository.GetClient(clientId).Penalty >= 10)
                throw new InvalidOperationException("Client: " + dataRepository.GetClient(clientId).FirstName + " " + dataRepository.GetClient(clientId).LastName + " has exceeded the maximum penalty amount, they have to pay it to rent more books");
            // Ustawienie daty zwrotu na tydzień od wypożyczenia
            Event ev = new Event(dataRepository.GetClient(clientId), dataRepository.GetInventory(inventoryId), DateTime.Today, DateTime.Today.AddDays(7));
            dataRepository.AddEvent(ev);
            dataRepository.GetInventory(inventoryId).reduceAmount(1);
            dataRepository.GetClient(clientId).RentedCatalogs.Add(dataRepository.GetInventory(inventoryId).Catalog);
            return ev;
        }

        public bool Return(String clientId, String catalogId)
        {
            if (!dataRepository.GetClient(clientId).RentedCatalogs.Contains(dataRepository.GetCatalog(catalogId)))
                return false;
            dataRepository.GetInventory(catalogId).increaseAmount(1);
            return true;
        }

        public void ProlongRent(String eventId, int days)
        {
            dataRepository.GetEvent(eventId).ReturnDate.AddDays(days);
        }

        public void PenalizeClients()
        {
            foreach (Client c in dataRepository.GetAllClients())
            {
               foreach (Event ev in EventsForClient(c))
                {
                    if (ev.ReturnDate.CompareTo(DateTime.Today) > 0)
                        c.Penalty += (int)(ev.ReturnDate - DateTime.Today).TotalDays;
                }
            }
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
