using RestaurantOrdering.Models;
using System.Text;
using System.Text.Json;

namespace RestaurantOrdering.Services
{
    public class PosService : IPosService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PosService> _logger;
        private readonly IConfiguration _configuration;

        public PosService(HttpClient httpClient, ILogger<PosService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<PosResponse> ProcessPaymentAsync(PosRequest request)
        {
            try
            {
                _logger.LogInformation($"Processing POS payment: {request.TransactionId}, Amount: {request.Amount:C}");

                // Simulated POS integration - replace with actual POS provider API
                await Task.Delay(2000); // Simulate processing time

                // For demo purposes, simulate random success/failure
                var random = new Random();
                var success = random.NextDouble() > 0.1; // 90% success rate

                if (success)
                {
                    return new PosResponse
                    {
                        Success = true,
                        TransactionId = request.TransactionId,
                        ApprovalCode = GenerateApprovalCode(),
                        ResponseTime = DateTime.UtcNow
                    };
                }
                else
                {
                    return new PosResponse
                    {
                        Success = false,
                        TransactionId = request.TransactionId,
                        ErrorMessage = "Kartınız onaylanmadı. Lütfen farklı bir kart deneyin.",
                        ResponseTime = DateTime.UtcNow
                    };
                }

                // Real POS integration example (commented out)
                /*
                var posEndpoint = _configuration["PosSettings:Endpoint"];
                var apiKey = _configuration["PosSettings:ApiKey"];

                var requestData = new
                {
                    amount = request.Amount,
                    transaction_id = request.TransactionId,
                    terminal_id = request.TerminalId,
                    currency = "TRY"
                };

                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                
                var response = await _httpClient.PostAsync(posEndpoint, content);
                var responseJson = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    var posResponse = JsonSerializer.Deserialize<PosResponse>(responseJson);
                    return posResponse;
                }
                else
                {
                    return new PosResponse
                    {
                        Success = false,
                        TransactionId = request.TransactionId,
                        ErrorMessage = "POS bağlantı hatası",
                        ResponseTime = DateTime.UtcNow
                    };
                }
                */
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing POS payment: {request.TransactionId}");
                return new PosResponse
                {
                    Success = false,
                    TransactionId = request.TransactionId,
                    ErrorMessage = "Sistem hatası. Lütfen tekrar deneyin.",
                    ResponseTime = DateTime.UtcNow
                };
            }
        }

        public async Task<bool> IsTerminalAvailableAsync(string terminalId)
        {
            try
            {
                // Simulate terminal availability check
                await Task.Delay(500);
                return true; // For demo, always return true

                // Real implementation would check terminal status
                /*
                var checkEndpoint = $"{_configuration["PosSettings:Endpoint"]}/terminals/{terminalId}/status";
                var response = await _httpClient.GetAsync(checkEndpoint);
                return response.IsSuccessStatusCode;
                */
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error checking terminal availability: {terminalId}");
                return false;
            }
        }

        public async Task<PosResponse> CancelTransactionAsync(string transactionId)
        {
            try
            {
                _logger.LogInformation($"Cancelling POS transaction: {transactionId}");

                // Simulate cancellation
                await Task.Delay(1000);

                return new PosResponse
                {
                    Success = true,
                    TransactionId = transactionId,
                    ApprovalCode = "CANCELLED",
                    ResponseTime = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error cancelling POS transaction: {transactionId}");
                return new PosResponse
                {
                    Success = false,
                    TransactionId = transactionId,
                    ErrorMessage = "İptal işlemi başarısız",
                    ResponseTime = DateTime.UtcNow
                };
            }
        }

        public async Task<PosResponse> RefundTransactionAsync(string transactionId, decimal amount)
        {
            try
            {
                _logger.LogInformation($"Processing refund for transaction: {transactionId}, Amount: {amount:C}");

                // Simulate refund processing
                await Task.Delay(1500);

                return new PosResponse
                {
                    Success = true,
                    TransactionId = $"REF_{transactionId}",
                    ApprovalCode = GenerateApprovalCode(),
                    ResponseTime = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing refund: {transactionId}");
                return new PosResponse
                {
                    Success = false,
                    TransactionId = transactionId,
                    ErrorMessage = "İade işlemi başarısız",
                    ResponseTime = DateTime.UtcNow
                };
            }
        }

        public string GenerateTransactionId()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"TXN_{timestamp}_{random}";
        }

        private string GenerateApprovalCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
