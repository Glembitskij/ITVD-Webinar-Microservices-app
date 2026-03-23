namespace Store.Orders.Api.Services
{
    public interface IStockClient
    {
        Task<StockResponse?> GetProductAsync(int productId);
    }
}
