namespace DatabaseLibrary
{
    public class MyProduct : Product
    {
        public string CountryOfOrigin { get; set; }

        public MyProduct(Product product, string countryOfOrigin)
        {
            foreach (var property in product.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(this, property.GetValue(product));
                }
            }
            this.CountryOfOrigin = countryOfOrigin;
        }

        public override string ToString()
        {
            return Name + "; " + CountryOfOrigin;
        }
    }
}
