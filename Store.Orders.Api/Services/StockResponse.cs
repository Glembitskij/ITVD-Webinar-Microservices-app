namespace Store.Orders.Api.Services;

public record StockResponse(int Id, string Name, decimal Price, int Quantity);

public class StockClient(HttpClient http) : IStockClient
{
    public async Task<StockResponse?> GetProductAsync(int productId)
    {
        try
        {
            return await http.GetFromJsonAsync<StockResponse>($"api/stock/{productId}");
        }
        catch 
        { 
            return null; 
        }
    }
}