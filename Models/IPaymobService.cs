public interface IPaymobService
{
    Task<string> GetAuthTokenAsync();
    Task<int> CreateOrderAsync(decimal amount, string merchantOrderId, IEnumerable<object> items);
    Task<string> GetPaymentKeyAsync(int orderId, decimal amount, object billingData);
    string GetIframeUrl(string paymentKey);
}
