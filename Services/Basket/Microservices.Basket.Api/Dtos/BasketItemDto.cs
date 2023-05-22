namespace Microservices.Basket.Api.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public string BeveragesId { get; set; }
        public string BeveragesName { get; set; }
        public decimal Price { get; set; }
    }
}
