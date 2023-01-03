using System.Net.Mail;
using Worigo.Core.Exceptions;

namespace Worigo.Business.EmailEntegre
{
    public class EmailSend
    {
        public static void SendMail(string sendmailadress, string subject, string body)
        {
            try
            {
                sendmailadress = "efe720ygt@hotmail.com";
                subject = "Test";
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient();

                client.Credentials = new System.Net.NetworkCredential("samt51.m@hotmail.com", "1425369As");
                client.Port = 587;
                client.Host = "smtp.outlook.com";
                client.EnableSsl = true;
                mail.To.Add(sendmailadress);
                mail.From = new MailAddress("samt51.m@hotmail.com");
                mail.Subject = "İLK MAİL TESTİMİZ WORİGO";
                mail.IsBodyHtml = true;
                mail.Body = body;
                client.Send(mail);
            }
            catch (System.Exception)
            {
                throw new ClientSideException("An error occurred in the email service.Try again");
            }

        }
    }
}
