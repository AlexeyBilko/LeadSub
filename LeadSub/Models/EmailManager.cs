
using System.Net;
using System.Net.Mail;

namespace LeadSub.Models
{
    public class EmailManager
    {
        public static Task SendText(string to,string text)
        {
            return Task.Run(() =>
            {
                try
                {
                    var client = new SmtpClient("smtp.mailtrap.io", 465)
                    {
                        Credentials = new NetworkCredential("e9701b479f2109", "f67274ff57d21d"),
                        EnableSsl = true

                    };
                    client.Send("ee4b55900e-9f79ad@inbox.mailtrap.io", to, "Header", text);
                }
                catch
                {
                    Console.WriteLine("Email Error");
                }
            });
        }
    }
}
