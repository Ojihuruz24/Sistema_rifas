using Microsoft.AspNetCore.DataProtection;

namespace SIMRIFA.Tools.Autenticacion
{
	public class ProteccionDato 
	{
		private readonly IDataProtector _dataProtector;

		public ProteccionDato(IDataProtectionProvider dataProtectionProvider)
		{
			_dataProtector = dataProtectionProvider.CreateProtector(nameof(ProteccionDato));
		}

		public string Desproteger(string data)
		{
			var rawJson = _dataProtector.Unprotect(data);
			return rawJson;
		}

		public string Proteger(string data)
		{
			var secureJson = _dataProtector.Protect(data);
			return secureJson;
		}
	}
}
