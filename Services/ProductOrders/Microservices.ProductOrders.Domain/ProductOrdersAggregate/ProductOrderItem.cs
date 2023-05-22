using Microservices.ProductOrders.Domain.Core;
using System;

namespace Microservices.ProductOrders.Domain.ProductOrdersAggregate
{
    public class ProductOrderItem : Entity
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public Decimal Price { get; private set; }

        public ProductOrderItem()
        {

        }

        public ProductOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public void UpdateProductOrderItem(string productName, string pictureUrl, decimal price)
        {
            ProductName = productName;
            Price = price;
            PictureUrl = pictureUrl;
        }
    }
}
