using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SIMRIFA.Model.Models;
using SIMRIFA.Service.Tools;

namespace SIMRIFA.Service.Correo
{
    public class CorreoServicio : ICorreoServicio
	{
		private ICorreo _correo;
		public CorreoServicio(ICorreo correo)
		{
			_correo = correo;
		}

		public async Task EnvioCorreoSendGrid(List<string> destinatarios, string asunto, string contenido)
		{
			bool emailEnviado = false;
			var listaEmails = new List<EmailAddress>();
			EmailTemplateDto emailTemplateDto = new();
			try
			{
				foreach (var destinatario in destinatarios)
				{
					listaEmails.Add(new EmailAddress(destinatario));
				}
				emailTemplateDto.subject = asunto;
				emailTemplateDto.contenido = contenido;
				emailEnviado = await _correo.EnviarCorreoSendGrid(listaEmails, asunto, null, false, "d-5ff8c55fdc5e4c2d95fe1f39ba13b581", emailTemplateDto);
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public async Task<bool> EnvioCorreoMailNet(CompradorDto comprador)
		{
			return await _correo.EnvioCorreoNetMail(comprador);
		}
	}
}
