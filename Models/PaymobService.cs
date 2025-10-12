using System.Text.Json;

public class PaymobService : IPaymobService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly string _apiKey;
    private readonly int _integrationId;
    private readonly string _iframeId;
    private readonly string _baseUrl;

    public PaymobService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
        _apiKey = _config["Paymob:ApiKey"] ?? throw new ArgumentNullException("Paymob:ApiKey");
        _integrationId = int.Parse(_config["Paymob:IntegrationId"] ?? "0");
        _iframeId = _config["Paymob:IframeId"] ?? throw new ArgumentNullException("Paymob:IframeId");
        _baseUrl = _config["Paymob:BaseUrl"]?.TrimEnd('/') ?? "https://accept.paymob.com";
    }

    // 1) Authenticate -> get token
    public async Task<string> GetAuthTokenAsync()
    {
        var req = new { api_key = _apiKey };
        var res = await _http.PostAsJsonAsync($"{_baseUrl}/api/auth/tokens", req);

        //res.EnsureSuccessStatusCode();
        
        var body = await res.Content.ReadFromJsonAsync<JsonElement>();
        // Paymob returns: { "token": "..." }
        return body.GetProperty("token").GetString()!;
    }

    // 2) Create order (register order with Paymob) -> returns order id
    public async Task<int> CreateOrderAsync(decimal amount, string merchantOrderId, IEnumerable<object> items)
    {
        var amountCents = (int)Math.Round(amount * 100); // Paymob uses cents
        // Build payload based on Paymob docs (items structure can be simple)
        // Must pass auth token in header
        var token = await GetAuthTokenAsync();
        var payload = new
        {
            auth_token = token,
            delivery_needed = false,
            amount_cents = amountCents,
            currency = "EGP",
            merchant_order_id = merchantOrderId,
            items = items
        };

        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var res = await _http.PostAsJsonAsync($"{_baseUrl}/ecommerce/orders", payload);
        //res.EnsureSuccessStatusCode();
        var b = await res.Content.ReadAsStringAsync();

        Console.WriteLine("Paymob response: " + b);
        Console.WriteLine("Status code: " + res.StatusCode);
        var body = await res.Content.ReadFromJsonAsync<JsonElement>();
        // example response includes "id"
        int orderId = body.GetProperty("id").GetInt32();
        return orderId;
    }

    // 3) Get payment_key (for that order) -> will return token for iframe
    public async Task<string> GetPaymentKeyAsync(int orderId, decimal amount, object billingData)
    {
        var amountCents = (int)Math.Round(amount * 100);
        var payload = new
        {
            amount_cents = amountCents,
            expiration = 3600,
            order_id = orderId,
            billing_data = billingData,
            currency = "EGP",
            integration_id = _integrationId
        };

        var token = await GetAuthTokenAsync();
        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var res = await _http.PostAsJsonAsync($"{_baseUrl}/api/acceptance/payment_keys", payload);
        res.EnsureSuccessStatusCode();
        var body = await res.Content.ReadFromJsonAsync<JsonElement>();
        var paymentToken = body.GetProperty("token").GetString()!;
        return paymentToken;
    }

    public string GetIframeUrl(string paymentKey)
    {
        // iframe id provided by Paymob dashboard
        return $"{_baseUrl}/api/acceptance/iframes/{_iframeId}?payment_token={Uri.EscapeDataString(paymentKey)}";
    }
}
