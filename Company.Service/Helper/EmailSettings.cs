using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            // Identifing the Host (mail server like gmail, yahoo) the we will send the email from ( ex: Host -> gmail, port -> 587)
            var client = new SmtpClient("smtp.gmail.com", 587); // Smtp -> send mail transfer portcol
            // Protcol
            client.EnableSsl = true;
            // Credintials: Sender email on that host
            client.Credentials = new NetworkCredential("hana.khaled862@gmail.com", "wzylqnjeiwxcgdhg");
            // Sending Email
            client.Send("hana.khaled862@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
