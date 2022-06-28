
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
                        Credentials = new NetworkCredential("685e99dfed287c", "c4d18d295d67de"),
                        EnableSsl = true

                    };
                    client.Send("a45419cde6-d79e75@inbox.mailtrap.io", to, "Header", text);
                }
                catch
                {
                    Console.WriteLine("Email Error");
                }
            });
        }
    }
}
