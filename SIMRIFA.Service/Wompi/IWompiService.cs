using SIMRIFA.Model.Models.Wompi;
using static SIMRIFA.Service.Wompi.WompiService;

namespace SIMRIFA.Service.Wompi
{
	public interface IWompiService
	{
		Task<TransactionResponse> GetTransactionAsync(string transactionId);

		Task<PaymentLinkResponse> CreatePaymentLinkAsync(PaymentLinkRequest request);

	}
}