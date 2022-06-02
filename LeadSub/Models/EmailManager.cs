
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using EASendMail;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth;

namespace LeadSub.Models
{
    public class EmailManager
    {
        private static async Task<(string,string)> GetToken()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            new ClientSecrets
            {
                 ClientId = "757118647530-4q3qngsol7lso7c44ptiifi8kci5f16e.apps.googleusercontent.com",
                 ClientSecret = "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"
            },
            new[] { "email", "profile", "https://mail.google.com/" },
            "user",
            CancellationToken.None
             );

            var jwtPayload = GoogleJsonWebSignature.ValidateAsync(credential.Token.IdToken).Result;
            var username = jwtPayload.Email;
            return (
                username,
                credential.Token.AccessToken
                );
        }
        public static Task SendConfirmCodeAsync(string to,string text)
        {
            return Task.Run(async () =>
            {
                try
                {
                    // Gmail SMTP server address
                    var res = await GetToken();

                    SmtpServer oServer = new SmtpServer("smtp.gmail.com");
                    // enable SSL connection
                    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                    // Using 587 port, you can also use 465 port
                    oServer.Port = 587;

                    // use Gmail SMTP OAUTH 2.0 authentication
                    oServer.AuthType = SmtpAuthType.XOAUTH2;
                    // set user authentication
                    oServer.User = res.Item1;
                    // use access token as password
                    oServer.Password = res.Item2;

                    SmtpMail oMail = new SmtpMail("TryIt");
                    // Your gmail email address
                    oMail.From = res.Item1;
                    oMail.To = to;

                    oMail.Subject = "test email from gmail account with OAUTH 2";
                    oMail.TextBody = "this is a test email sent from c# project with gmail.";

                    Console.WriteLine("start to send email using OAUTH 2.0 ...");

                    SmtpClient oSmtp = new SmtpClient();
                    oSmtp.SendMail(oServer, oMail);

                    Console.WriteLine("The email has been submitted to server successfully!");
                }
                catch (Exception ep)
                {
                    Console.WriteLine("Exception: {0}", ep.Message);
                }
            });
           
        }
    }
}
