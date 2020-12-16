using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailHelp
{
    public class SendEmailHelp
    {
        public static bool SendEmailByQQ(string toEmail, string emailTitle, string emailBody)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.qq.com";
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            MailMessage mm = new MailMessage();
            client.Port = 25;

            mm.From = new MailAddress("1909224052@qq.com");
            mm.To.Add(new MailAddress(toEmail));
            mm.Subject = emailTitle;
            mm.Body = emailBody;
            mm.IsBodyHtml = false;
            mm.Priority = MailPriority.High;

            client.Credentials = new NetworkCredential("1909224052", "klhbwubwqczhcbdg");
            
            //发送
            try
            {
                client.Send(mm); //发送邮件
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                client.Dispose(); //释放资源
            }
        }
    }
}
