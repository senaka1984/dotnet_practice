namespace Practice_Api.Models
{
    public class Product
    {
       IEnumerable<ProductItem> ProductItems { get; set; }

    }

    public class ProductItem
    {
        public int Id { get; set; } = 0;    
        public string Name { get; set; }    
        public decimal Price { get; set; }
        public string Description { get; set; }



    }
}
