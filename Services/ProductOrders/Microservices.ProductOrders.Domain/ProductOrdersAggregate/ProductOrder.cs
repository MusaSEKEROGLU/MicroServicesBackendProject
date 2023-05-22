using Microservices.ProductOrders.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microservices.ProductOrders.Domain.ProductOrdersAggregate
{
    public class ProductOrder : Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }

        private readonly List<ProductOrderItem> _orderItems;
        public IReadOnlyCollection<ProductOrderItem> OrderItems => _orderItems;
        public ProductOrder()
        {

        }
        public ProductOrder(string buyerId, Address address)
        {
            _orderItems = new List<ProductOrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }
        public void AddProductOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new ProductOrderItem(productId, productName, pictureUrl, price);

                _orderItems.Add(newOrderItem);
            }
        }
        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
