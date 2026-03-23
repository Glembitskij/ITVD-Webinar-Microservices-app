using System.Collections.Concurrent;

namespace Store.Stock.Api.Services
{
    public class InventoryService
    {
        private readonly ConcurrentDictionary<int, int> _items = new();

        public InventoryService()
        {
            _items.TryAdd(1, 10); // Ноутбук
            _items.TryAdd(2, 50); // Мишка
        }

        public int GetQuantity(int productId) => _items.GetValueOrDefault(productId, 0);

        public bool Reserve(int productId, int quantity)
        {
            if (!_items.ContainsKey(productId) || _items[productId] < quantity)
            {
                return false;
            }

            _items[productId] -= quantity;
            {
                return true;
            }
        }
    }
}
