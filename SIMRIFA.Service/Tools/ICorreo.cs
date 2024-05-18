using SendGrid.Helpers.Mail;
using SIMRIFA.Model.Models;

namespace SIMRIFA.Service.Tools
{
    public interface ICorreo
	{
		Task<bool> EnviarCorreoSendGrid(List<EmailAddress> emailAddresses, string subject, List<Attachment>? attachments, bool sandbox, string? template, EmailTemplateDto? TemplateData);
		Task<bool> EnvioCorreoNetMail(Comprador comprador);
	}
}