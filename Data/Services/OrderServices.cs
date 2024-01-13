
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class OrderServices
    {
        public List<Order> GetOrders()
        {
            string filePath = Utils.GetOrdersPath();

            if (!File.Exists(filePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<Order>>(json);
        }

        public void PurchaseOrder(Order order)
        {
            List<Order> orders = GetOrders();
            orders.Add(order);

            string filePath = Utils.GetOrdersPath();

            var json = JsonSerializer.Serialize(orders);

            File.WriteAllText(filePath, json);
        }


        public void AddItemToCart(List<OrderedProduct> items, Guid itemID, string itemName, String itemType, Double itemPrice)
        {

            OrderedProduct item = items.FirstOrDefault(x => x.ItemID.ToString() == itemID.ToString() && x.ItemType == itemType);

            if (item != null)
            {
                item.Quantity++;
                item.TotalPrice = item.Quantity * itemPrice;

                return;
            }

            item = new()
            {
                ItemID = itemID,
                ItemName = itemName,
                ItemType = itemType,
                Quantity = 1,
                Price = itemPrice,
                TotalPrice = itemPrice
            };

            items.Add(item);

        }

        public void RemoveFromCart(List<OrderedProduct> items, Guid orderItemID)
        {
            OrderedProduct orderItem = items.FirstOrDefault(x => x.ID == orderItemID);

            if (orderItem != null)
            {
                items.Remove(orderItem);
            }
        }

        public void AdjustOrderItemQuantity(List<OrderedProduct> items, Guid orderItemID, string action)
        {
            OrderedProduct item = items.FirstOrDefault(x => x.ID == orderItemID);

            if (item != null)
            {
                if (action == "add")
                {
                    item.Quantity++;
                    item.TotalPrice = item.Quantity * item.Price;
                }
                else if (action == "subtract" && item.Quantity > 1)
                {
                    item.Quantity--;
                    item.TotalPrice = item.Quantity * item.Price;
                }
            }
        }


        public double CalcGrandTotal(IEnumerable<OrderedProduct> Elements)
        {
            double amount = 0;

            foreach (var item in Elements)
            {
                amount += item.TotalPrice;
            }
            return amount;
        }

        


    }
}
