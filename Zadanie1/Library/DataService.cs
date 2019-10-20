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

        public IEnumerable<Catalog> AllCatalogs()
        {
            return dataRepository.GetAllCatalog();
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

        public Event AddEvent(Client client, Inventory inventory)
        {
            // Ustawienie daty zwrotu na tydzień od wypożyczenia
            Event ev = new Event(client, inventory, DateTime.Now, DateTime.Now.AddDays(7));
            dataRepository.AddEvent(ev);
            return dataRepository.GetEvent(ev.ID);
        }

        // W tych dwóch metodach nie jestem pewien czy o to chodzi
        public IEnumerable<Catalog> GetAllCatalog()
        {
            return dataRepository.GetAllCatalog();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return dataRepository.GetAllEvents();
        }
    }
}
