
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class ItemServices
    {

        private readonly List<Product> _itemList = new()
        {
             // New Coffee Products
            new Product { ProductName = "Americano", ProductPrice = 250.0, ProductType = "Coffee" },
            new Product { ProductName = "Affogato", ProductPrice = 350.0, ProductType = "Coffee" },
            new Product { ProductName = "Iced Coffee", ProductPrice = 200.0, ProductType = "Coffee" },
            new Product { ProductName = "Lungo", ProductPrice = 400.0, ProductType = "Coffee" },
            new Product { ProductName = "Irish Coffee", ProductPrice = 300.0, ProductType = "Coffee" },
            new Product { ProductName = "Cold Brew", ProductPrice = 500.0, ProductType = "Coffee" },


            // New Add-in Products
            new Product { ProductName = "Chocolate", ProductPrice = 50.0, ProductType = "Add-in" },
            new Product { ProductName = "Cinnamon and Nutmeg", ProductPrice = 80.0, ProductType = "Add-in" },
            new Product { ProductName = "Lemons and limes", ProductPrice = 40.0, ProductType = "Add-in" },
            new Product { ProductName = "Butter", ProductPrice = 100.0, ProductType = "Add-in" },
            new Product { ProductName = "Peppermint Oil", ProductPrice = 70.0, ProductType = "Add-in" },
            new Product { ProductName = "Lavender", ProductPrice = 120.0, ProductType = "Add-in" }
        };

        public void SaveItems(List<Product> coffeeList)
        {

            string dataPath = Utils.GetDesktopPath();

            string itemsPath = Utils.GetItemsPath();


            var json = JsonSerializer.Serialize(coffeeList);

            File.WriteAllText(itemsPath, json);
        }


        public List<Product> GetItemsFromFile()
        {
            string itemsPath = Utils.GetItemsPath();


            if (!File.Exists(itemsPath))
            {
                return new List<Product>();
            }

            var json = File.ReadAllText(itemsPath);

            return JsonSerializer.Deserialize<List<Product>>(json);
        }


        public void SeedItems()
        {
            List<Product> itemsList = GetItemsFromFile();

            if (itemsList.Count == 0)
            {
                SaveItems(_itemList);
            }
        }


        public Product GetItemByID(string itemID)
        {

            List<Product> items = GetItemsFromFile();
            Product item = items.FirstOrDefault(i => i.ProductID.ToString() == itemID);

            System.Diagnostics.Debug.WriteLine(itemID);

            return item;
        }


        public void UpdateItem(string itemID, double itemPrice)
        {
            List<Product> items = GetItemsFromFile();

            Product itemToUpdate = items.FirstOrDefault(i => i.ProductID.ToString() == itemID.ToString());

            itemToUpdate.ProductPrice = Math.Round(itemPrice, 2);

            SaveItems(items);
        }

        public void AddItem(Product item)
        {
            List<Product> items = GetItemsFromFile();

            items.Add(item);

            SaveItems(items);
        }

        public void DeleteItem(string itemID)
        {
            List<Product> items = GetItemsFromFile();

            Product itemToDelete = items.FirstOrDefault(i => i.ProductID.ToString() == itemID.ToString());

            items.Remove(itemToDelete);

            SaveItems(items);
        }
    }
}
