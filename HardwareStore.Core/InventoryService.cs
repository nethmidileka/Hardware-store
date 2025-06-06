using System;
using System.Collections.Generic;
using System.Linq;

namespace HardwareStore.Core
{
    public class InventoryService
    {
        private static List<InventoryItem> _inventoryItems = new List<InventoryItem>();
        private static int _nextId = 1;

        public List<InventoryItem> GetAllItems()
        {
            return _inventoryItems;
        }

        public InventoryItem GetItemById(int productId)
        {
            return _inventoryItems.FirstOrDefault(item => item.ProductId == productId);
        }

        public InventoryItem AddItem(InventoryItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Item name cannot be null or empty.", nameof(item.Name));
            }

            if (item.UnitPrice <= 0)
            {
                throw new ArgumentException("Item unit price must be positive.", nameof(item.UnitPrice));
            }

            item.ProductId = _nextId++;
            _inventoryItems.Add(item);
            return item;
        }

        public InventoryItem UpdateItem(InventoryItem itemToUpdate)
        {
            if (itemToUpdate == null)
            {
                throw new ArgumentNullException(nameof(itemToUpdate), "Item to update cannot be null.");
            }

            var existingItem = _inventoryItems.FirstOrDefault(item => item.ProductId == itemToUpdate.ProductId);
            if (existingItem != null)
            {
                // Update properties of the existing item
                existingItem.Name = itemToUpdate.Name;
                existingItem.Description = itemToUpdate.Description;
                existingItem.QuantityInStock = itemToUpdate.QuantityInStock;
                existingItem.UnitPrice = itemToUpdate.UnitPrice;
                existingItem.Supplier = itemToUpdate.Supplier;
                return existingItem;
            }
            return null; // Item not found
        }

        public bool DeleteItem(int productId)
        {
            var itemToRemove = _inventoryItems.FirstOrDefault(item => item.ProductId == productId);
            if (itemToRemove != null)
            {
                _inventoryItems.Remove(itemToRemove);
                return true;
            }
            return false; // Item not found
        }
    }
}
