namespace Library
{
    interface ISerializeCollection
    {
        void saveClients();
        void loadClients();
        void saveCatalogs();
        void loadCatalogs();
        void saveInventories();
        void loadInventories();
        void saveEvents();
        void loadEvents();
    }
}
