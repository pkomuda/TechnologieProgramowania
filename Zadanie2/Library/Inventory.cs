namespace Library
{
    public class Inventory
    {
        public Catalog Catalog { get; }
        public int Amount { get; private set; }

        public Inventory(Catalog catalog, int amount)
        {
            Catalog = catalog;
            Amount = amount;
        }
        public void increaseAmount(int n)
        {
            Amount += System.Math.Abs(n);
        }
        public void reduceAmount(int n)
        {
            Amount -= System.Math.Abs(n);
        }
        public override string ToString()
        {
            return "Catalog: " + Catalog.ID + " Amount: " + Amount;
        }
    }
}
