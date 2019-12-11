namespace DatabaseLibrary
{
    public class MyProduct : Product
    {
        public string CountryOfOrigin { get; set; }

        public MyProduct(Product product, string countryOfOrigin) : base()
        {
            foreach (var property in product.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(product));
            }
            this.CountryOfOrigin = countryOfOrigin;
        }
    }
}
