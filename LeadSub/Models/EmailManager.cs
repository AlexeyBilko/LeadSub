
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth;

using EASendMail;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.AspNetCore3;

namespace LeadSub.Models
{
    public class EmailManager
    {
        public static async Task<(string,string)> GetToken()
        {
            //ClientSecrets secrets = new ClientSecrets()
            //{
            //    ClientId = "757118647530-4q3qngsol7lso7c44ptiifi8kci5f16e.apps.googleusercontent.com",
            //    ClientSecret = "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"
            //};

            //var credential = new UserCredential(new GoogleAuthorizationCodeFlow(
            //    new GoogleAuthorizationCodeFlow.Initializer
            //    {
            //        ClientSecrets = secrets
            //    }),
            //    "user",
            //    RefreshToken
            //);

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
        
        public static Task SendConfirmCodeAsync(string to,string text,string from,string accessToken)
        {
            return Task.Run(async () =>
            {
                try
                {

                   /*var service = new GmailService(new BaseClientService.Initializer
                    {
                        HttpClientInitializer = credentials
                    });
                    //service.
                    // Gmail SMTP server address
                    var res = await GetToken();
                   //credentials.E
                    /*SmtpServer oServer = new SmtpServer("smtp.gmail.com");
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

                    Console.WriteLine("The email has been submitted to server successfully!");*/
                }
                catch (Exception ep)
                {
                    Console.WriteLine("Exception: {0}", ep.Message);
                }



                /*string sender = "mittsykh@ukr.net";
                MailAddress send = new MailAddress(sender, "LeadSub Manager");
                MailAddress to = new MailAddress(toemail);
                MailMessage message = new MailMessage(send, to);
                message.Body = "<h1>Helloworld</h1>";
              

                message.IsBodyHtml = true;
                ImapClie
                SmtpClient smtp = new SmtpClient("smtp.ukr.net", 465);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(sender, "YbWAtsxyffUOVQnd");
                smtp.Send(message);
                return true;*/


            });
           
        }
    }
}
