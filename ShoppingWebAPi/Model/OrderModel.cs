namespace ShoppingWebAPi.Model
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int Product_id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
