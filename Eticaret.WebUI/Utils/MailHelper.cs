using Eticaret.Core.Entities;
using System.Net;
using System.Net.Mail;

namespace Eticaret.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.furkanbilge.com",587);
            smtpClient.Credentials = new NetworkCredential("info@furkanbilge.com","mailşifresi");
            smtpClient.EnableSsl = true; //ssl kullanıyorsan true yap
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@furkanbilge.com");
            message.To.Add("bilgi@furkanbilge.com");
            message.Subject = "Siteden Mesaj Geldi";
            message.Body = $"İsim: {contact.Name} - Soyisim: {contact.Surname} - Email: {contact.Email} - Telefon: {contact.Phone} - Mesaj: {contact.Message}";
            message.IsBodyHtml = true;
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}
