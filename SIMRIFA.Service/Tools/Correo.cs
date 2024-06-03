using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;
using System.Net;
using SIMRIFA.Model.Models;
using SIMRIFA.Model.Config;

namespace SIMRIFA.Service.Tools
{
	public class Correo : ICorreo
	{
		private readonly IUtils _utils;
		private string EmailRemitente;
		private string NombrelRemitente;
		private string Pass;
		private string Asunto;
		private string Cuerpo;
		private bool Estado;

		public Correo(IUtils utils)
		{
			_utils = utils;
		}

		public async Task<bool> EnviarCorreoSendGrid(List<EmailAddress> emailAddresses, string subject, List<SendGrid.Helpers.Mail.Attachment>? attachments, bool sandbox, string? template, EmailTemplateDto? TemplateData)
		{
			try
			{
				//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
				var apiKey = "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";
				var client = new SendGridClient(apiKey);
				var fromHC = "dayantrading24@gmail.com";
				var from = new EmailAddress()
				{
					Email = fromHC,
					Name = "SIM - Somos la solucion integran de movilidad"
				};
				emailAddresses.Add(from);

				object dynamicTemplateData = default;
				SendGridMessage? msg = default;



				if (!string.IsNullOrEmpty(template))
				{
					dynamicTemplateData = new
					{
						subject = subject,
						titulo = TemplateData.titulo,
						contenido = TemplateData.contenido,
						mensaje = TemplateData.mensaje,
						subtitulo = TemplateData.subtitulo
					};
					msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, emailAddresses, template, dynamicTemplateData);
				}
				else
				{
					msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, emailAddresses, subject, "", TemplateData.contenido);
				}
				msg.Subject = subject;
				if (attachments != null)
				{
					msg.Attachments = attachments;
				}
				var response = await client.SendEmailAsync(msg);
				if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.IsSuccessStatusCode)
				{

					return true;
				}
			}
			catch (Exception Ex)
			{

				return false;
			}

			return false;
		}

		public async Task<bool> EnvioCorreoNetMail(CompradorDto comprador)
		{
			var result = false;

			try
			{

				var MontoFormateado = comprador.Valor.ToString();

				if (MontoFormateado.Length > 2)
				{
					comprador.Valor = MontoFormateado.Substring(0, MontoFormateado.Length - 2);
				}



				var CorreConfig = await _utils.GetConfiguracionCorreoAsync();


				var fromAddress = new MailAddress(CorreConfig.EmaiRemitente, CorreConfig.NombreRemitente);
				var toAddress = new MailAddress(comprador.Correo, $"{comprador.Nombre}");
				string fromPassword = CorreConfig.Password;
				string subject = $"{CorreConfig.Asunto} - {comprador.Nombre}";
				string body = ContruccionBody(comprador, CorreConfig.Cuerpo);

			

				var smtp = new SmtpClient
				{
					Host = "smtp.gmail.com",
					Port = 587,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
				};
				using (var message = new MailMessage(fromAddress, toAddress)
				{
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				})
				{
					//smtp.Send(message);

					await smtp.SendMailAsync(message);

					result = true;
				};
			}
			catch (SmtpException smtpEx)
			{
				Console.WriteLine($"SMTP Error: {smtpEx.StatusCode} - {smtpEx.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al enviar correo: {ex.Message}");
			}

			return result;
		}

		private string ContruccionBody(CompradorDto comprador, string cuerpo = default)
		{
			//cuerpo = cuerpo.Replace("{comprador.Nombre}", comprador.Nombre);
			//cuerpo = cuerpo.Replace("{comprador.Referencia}", comprador.Referencia);
			//cuerpo = cuerpo.Replace("{comprador.Numeros}", comprador.Numeros);
			//cuerpo = cuerpo.Replace("{comprador.Cantidad}", comprador.Cantidad);
			//cuerpo = cuerpo.Replace("{comprador.Valor}", comprador.Valor);

			//var body = cuerpo;

            #region Prueba

            var body = @$"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
                <html dir='ltr' xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>

                <head>
                    <meta charset='UTF-8'>
                    <meta content='width=device-width, initial-scale=1' name='viewport'>
                    <meta name='x-apple-disable-message-reformatting'>
                    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                    <meta content='telephone=no' name='format-detection'>
                    <title></title>
                </head>

                <body>
                    <div dir='ltr' class='es-wrapper-color'>
                        <table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0'>
                            <tbody>
                                <tr>
                                    <td class='esd-email-paddings' valign='top'>
                                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-stripe' esd-custom-block-id='5602' align='center'>
                                                        <table class='es-header-body' style='background-color: #2b2c2c;' width='600' cellspacing='0' cellpadding='0' bgcolor='#2b2c2c' align='center'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-structure es-p10t es-p10b' style='background-color: #e2e3e3;' bgcolor='#e2e3e3' align='left'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-container-frame' width='600' valign='top' align='center'>
                                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                                            <tbody>
                                                                                                <tr class='es-visible-simple-html-only'>
                                                                                                    <tbody>
                                                                                                    </tbody>
                                                                                                    <td align='center' class='esd-block-image' style='font-size: 0px;'><a target='_blank'><img class='adapt-img' src='~/img/LOGO.png' alt style='display: block;' width='600'></a></td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-stripe' align='center'>
                                                        <table class='es-content-body' style='background-color: #2b2c2c;' width='600' cellspacing='0' cellpadding='0' bgcolor='#2b2c2c' align='center'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-structure es-p25t es-p30b es-p20r es-p20l' style='background-color: #020202;' bgcolor='#020202' align='left'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-container-frame' esd-custom-block-id='5616' width='560' valign='top' align='center'>
                                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                                            <tbody>
                                                                               
                                                                                                <tr>
                                                                                                    <td class='esd-block-text es-m-txt-c es-p5t es-p5b es-p20r es-p20l' align='center'>
                                                                                                        <h1 style='color: #e35367; font-size: 55px;'>TU COMPRA!</h1>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class='esd-block-text es-m-txt-c es-p20r es-p20l' align='center'>
                                                                                                        <p style='color: #cccccc;'>Hola {comprador.Nombre},</p>
                                                                                                        <p style='color: #cccccc;'>Hemos recibido correctamente tu pedido #{comprador.Referencia} y lo estamos procesando:<span class='product-description'></span><br></p>
                                                                                                    </td>
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <br>
                                                                                                        <table class='es-content-body' border='1' style='background-color: #ffffff00; color: white; width: 500px;' align='center'>
                                                                                                            <thead>
                                                                                                                <tr>
                                                                                                                    <th>SOAT + TECNO + PASE(carro/moto) + MOTO 0KM</th>
                                                                                                                </tr>
                                                                                                            </thead>
                                                                                                            <tbody>
                                                                                                                <tr>
                                                                                                                    <td> <h1 style='color: #e35367; font-size: 20px;' align='center'>{comprador.Numeros}</h1></td>
                                                                                                                </tr>
                                                                                                            </tbody>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class='esd-block-text es-m-txt-c es-p20r es-p20l' align='center'>
                                                                                                        <p style='color: #cf1b1b;'>[Pedido # {comprador.Referencia}] (abril 30, 2024)</p>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <br>
                                                                                                        <table class='es-content-body' border='1' style='background-color: #ffffff00; color: white; width: 500px;' align='center'>
                                                                                                            <tr>
                                                                                                                <td colspan='2'>Producto</td>
                                                                                                                <td>Cantidad</td>
                                                                                                                <td >Precio</td>
                                                                                                                <!-- <td colspan='2'>2</td> -->
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  colspan='2'>SOAT-TECNO- moto 0 km</td>
                                                                                                                <td>{comprador.Cantidad}</td>
                                                                                                                <td>$ {comprador.Valor}</td>
                                                                                                                <!-- <td rowspan='2'>3</td> --> 
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td colspan='3'>Subtotal:</td>
                                                                                                                <td>$ {comprador.Valor}</td>
                                                                                                            </tr>   
                                                                                                            <tr>
                                                                                                                <td colspan='3'>Métodos de pago:</td>
                                                                                                                <td>PSE</td>
                                                                                                            </tr>   
                                                                                                            <tr>
                                                                                                                <td colspan='3'>Total:</td>
                                                                                                                <td>$ {comprador.Valor}</td>
                                                                                                            </tr>   
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td class='esd-block-text es-m-txt-c es-p20r es-p20l' align='center'>
                                                                                                        <p style='color: #cf1b1b;'>Dirección de facturación</p>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <br>
                                                                                                        <table class='es-content-body' style='background-color: #ffffff00; color: white; width: 500px;' align='center'>
                                                                                                            <tr>
                                                                                                                <td colspan='2'>{comprador.Nombre}</td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td  colspan='2'>Calle 41 -la pola rionegro</td>
                                                                                                            </tr> 
                                                                                                            <tr>
                                                                                                                <td  colspan='2'>305644656 num</td>
                                                                                                            </tr> 
                                                                                                            <tr>
                                                                                                                <td  colspan='2'>correoSIM@gmail.com</td>
                                                                                                            </tr>   
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td class='esd-block-text es-m-txt-c es-p20r es-p20l' align='center'>
                                                                                                        <p style='color: #cccccc; font-size: 14px;'>Buena suerte a todos los participantes y gracias por confiar en nosotros en esta rifa! Su apoyo significa mucho para nosotros. Que la suerte esté de su lado y que disfruten de esta emocionante experiencia.<br></p>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                           
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </body>

                </html>";

            #endregion
            return body;

		}

	}
}
