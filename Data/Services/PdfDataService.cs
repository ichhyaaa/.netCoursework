
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class PdfDataService
    {

        private OrderServices _orderServices { get; set; }

        public PdfDataService(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        public List<Order> ProduceOrderList(string pdfType, string _date)
        {
            List<Order> orders = _orderServices.GetOrders();

            if (pdfType.ToLower() == "monthly")
            {
                orders = orders.Where(o => _date == o.OrderDate.ToString("MM-yyyy")).ToList();
            }
            else if (pdfType.ToLower() == "daily")
            {
                orders = orders.Where(o => _date == o.OrderDate.ToString("dd-MM-yyyy")).ToList();
            }

            return orders;
        }

        public List<OrderedProduct> GetCoffeList(List<Order> orders)
        {
            List<OrderedProduct> purchasedItems = orders
            .SelectMany(customerOrder => customerOrder.OrderItems)
            .ToList();

            List<OrderedProduct> coffeeItems = purchasedItems
                .Where(item => item.ItemType == "Coffee")
                .ToList();

            List<OrderedProduct> topCoffeeItemsByQuantity = coffeeItems
                .GroupBy(coffeeItem => coffeeItem.ItemName)
                .Select(groupedCoffeeItems =>
                {
                    string coffeeName = groupedCoffeeItems.Key;
                    int totalQuantityOrdered = groupedCoffeeItems.Sum(item => item.Quantity);

                    return new OrderedProduct
                    {
                        ItemName = coffeeName,
                        Quantity = totalQuantityOrdered,
                    };
                })
                .ToList();

            return topCoffeeItemsByQuantity.Take(5).ToList();
        }

        public List<OrderedProduct> GetAddInList(List<Order> orders)
        {
            List<OrderedProduct> purchasedItems = orders
            .SelectMany(order => order.OrderItems)
            .ToList();

            List<OrderedProduct> addInsItems = purchasedItems.Where(item => item.ItemType == "Add-in").ToList();

            List<OrderedProduct> topaddInsItemsByQuantity = addInsItems
            .GroupBy(addIn => addIn.ItemName)
            .Select(group =>
            {
                var itemName = group.Key;
                int totalQuantity = group.Sum(orderItem => orderItem.Quantity);

                return new OrderedProduct
                {
                    ItemName = itemName,
                    Quantity = totalQuantity,
                };
            }).ToList();

            return topaddInsItemsByQuantity.Take(5).ToList();
        }


    }
}
