using RestaurantOrdering.Models;

namespace RestaurantOrdering.Services
{
    public interface IPosService
    {
        Task<PosResponse> ProcessPaymentAsync(PosRequest request);
        Task<bool> IsTerminalAvailableAsync(string terminalId);
        Task<PosResponse> CancelTransactionAsync(string transactionId);
        Task<PosResponse> RefundTransactionAsync(string transactionId, decimal amount);
        string GenerateTransactionId();
    }
}
