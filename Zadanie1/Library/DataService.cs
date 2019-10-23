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

        public Catalog GetCatalog(String catalogId)
        {
            return dataRepository.GetCatalog(catalogId);
        }
        
        public IEnumerable<Catalog> AllCatalogs()
        {
            return dataRepository.GetAllCatalogs();
        }

        public Client GetClient(String clientId)
        {
            return dataRepository.GetClient(clientId);
        }
        
        public IEnumerable<Client> AllClients()
        {
            return dataRepository.GetAllClients();
        }

        public Event GetEvent(String eventId)
        {
            return dataRepository.GetEvent(eventId);
        }
        
        public IEnumerable<Event> AllEvents()
        {
            return dataRepository.GetAllEvents();
        }

        public Inventory GetInventory(String inventoryId)
        {
            return dataRepository.GetInventory(inventoryId);
        }
        
        public IEnumerable<Inventory> AllInventories()
        {
            return dataRepository.GetAllInventories();
        }

        public ObservableCollection<Event> EventsForClient(Client client)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in AllEvents())
            {
                if (ev.Client == client)
                    events.Add(ev);
            }
            return events;
        }
        
        public ObservableCollection<Event> EventsForClientById(String id)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in AllEvents())
            {
                if (ev.Client.ID == id)
                    events.Add(ev);
            }
            return events;
        }

        public ObservableCollection<Event> EventsBetweenDates(DateTime from, DateTime to)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in AllEvents())
            {
                if (ev.BorrowDate.CompareTo(from)>=0 && ev.ReturnDate.CompareTo(to)<=0)
                    events.Add(ev);
            }
            return events;
        }
        
        public ObservableCollection<Event> EventsBeforeDate(DateTime date)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in AllEvents())
            {
                if (ev.ReturnDate.CompareTo(date)<=0)
                    events.Add(ev);
            }
            return events;
        }
        
        public ObservableCollection<Event> EventsAfterDate(DateTime date)
        {
            ObservableCollection<Event> events = new ObservableCollection<Event>();
            foreach (Event ev in AllEvents())
            {
                if (ev.ReturnDate.CompareTo(date)>=0)
                    events.Add(ev);
            }
            return events;
        }

        public Event Borrow(String clientId, String inventoryId)
        {
            if (GetInventory(inventoryId).Amount == 0)
                throw new InvalidOperationException("Book: "+GetInventory(inventoryId).Catalog.Title+" not available.");
            if (GetClient(clientId).RentedCatalogs.Contains(GetInventory(inventoryId).Catalog))
                throw new InvalidOperationException("Client: "+GetClient(clientId).FirstName+" "+GetClient(clientId).LastName+" has already rented the book: "+GetInventory(inventoryId).Catalog.Title+".");
            if (GetClient(clientId).Penalty >= 10)
                throw new InvalidOperationException("Client: " + GetClient(clientId).FirstName + " " + GetClient(clientId).LastName + " has exceeded the maximum penalty amount, they have to pay it to rent more books");
            // Ustawienie daty zwrotu na tydzień od wypożyczenia
            Event ev = new Event(GetClient(clientId), GetInventory(inventoryId), DateTime.Today, DateTime.Today.AddDays(7));
            dataRepository.AddEvent(ev);
            GetInventory(inventoryId).Amount--;
            GetClient(clientId).RentedCatalogs.Add(GetInventory(inventoryId).Catalog);
            return ev;
        }

        public bool Return(String clientId, String catalogId)
        {
            if (!GetClient(clientId).RentedCatalogs.Contains(GetCatalog(catalogId)))
                return false;
            GetInventory(catalogId).Amount++;
            return true;
        }

        public void ProlongRent(String eventId, int days)
        {
            GetEvent(eventId).ReturnDate.AddDays(days);
        }

        public void PenalizeClients()
        {
            foreach (Client c in AllClients())
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
            foreach (Client c in AllClients())
                sum += c.Penalty;
            return sum;
        }
    }
}
