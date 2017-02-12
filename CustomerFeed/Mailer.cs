using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web;
using CustomerFeed.Models;
using System.Net.Mail;

namespace CustomerFeed
{
    public class Mailer
    {
        public static void MailMessage(string to, string subject, string body)
        {
            
            Mail(to, subject, body);

        }

        public static void MailUserPasswordResetRequest(PasswordResetModel passwordResetModel, UserModel userModel)
        {
            try
            {
                string mailServer = ConfigurationManager.AppSettings["mailServer"];
                string from = ConfigurationManager.AppSettings["fromAddress"];
                string body = ConfigurationManager.AppSettings["passwordResetBody"];
                string subject = ConfigurationManager.AppSettings["passwordResetSubject"];
                string resetUrl = ConfigurationManager.AppSettings["passwordResetUrl"];
                string url = "<a href='" + string.Format(resetUrl, passwordResetModel.PasswordResetKey) + "'>here</a>";

                MailMessage mailMessage = new MailMessage();

                mailMessage.IsBodyHtml = true;

                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(new MailAddress(userModel.Email));
                    
                mailMessage.Subject = subject;

                mailMessage.Body = string.Format(body, url);

                SmtpClient client = new SmtpClient();

                client.Send(mailMessage);
            }
            catch(Exception ex)
            {
                Logger.CreateLog("Email failed for message to " + userModel.Email + " exception is " + ex, 1);
            }
        }

        private static void Mail(string to, string subject, string body)
        {
            try
            {
                string mailServer = ConfigurationManager.AppSettings["mailServer"];
                string from = ConfigurationManager.AppSettings["fromAddress"];

                MailMessage mailMessage = new MailMessage();

                mailMessage.IsBodyHtml = true;

                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(new MailAddress(to));

                mailMessage.Subject = subject;

                mailMessage.Body = body;

                SmtpClient client = new SmtpClient();

                client.Send(mailMessage);

            }
            catch (Exception ex)
            {
                Logger.CreateLog("Email failed for message to " + to + " exception is " + ex, 1);
            }
        }
    }
}
