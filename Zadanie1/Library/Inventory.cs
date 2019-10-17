namespace Library
{
    class Inventory
    {
        public Catalog Catalog { get; set; }
        public int Amount { get; set; }

        public Inventory(Catalog catalog, int amount)
        {
            Catalog = catalog;
            Amount = amount;
        }
    }
}
