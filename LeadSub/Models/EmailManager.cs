using BLL.DTO;
using BLL.Services;
using LeadSub.Models.ViewModels;
using System.Net;
using System.Net.Mail;

namespace LeadSub.Models
{
    public class EmailManager
    {
        public static Task SendConfirmCodeAsync(string to,string text)
        {
            return Task.Run(() =>
            {
                string from = "papych1905@gmail.com";
                string password = "AdolfHitler";

                MailAddress sender = new MailAddress(from, "LeadSub Manager");
                MailAddress recipient = new MailAddress(to);
                using (MailMessage message = new MailMessage(from, to))
                {
                    message.IsBodyHtml = true;
                    message.Body = text;
                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(from, password);
                        smtpClient.Send(message);
                    }
                }
            });
           
        }
    }
}
