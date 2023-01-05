using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Mail
{
    public class MailService
    {
        SmtpClient mailClient = new SmtpClient("mail.ipeksalevi.com", 587);
        NetworkCredential cred = new NetworkCredential("siparis@ipeksalevi.com", "Ce0El7Iw");
        MailMessage contact = new MailMessage();

        public void OrderMail(MailModel model) {

            mailClient.Credentials = cred;

            contact.From = new MailAddress(model.From, model.Title);
            contact.Subject = model.Subject;
            contact.IsBodyHtml = true;
            contact.Body = "<table style=\"border:30px solid #d21e4e; padding:20px; width:800px; font-size:13px; font-family:arial;\" cellpadding=\"0\" cellspacing=\"0\">" +
                                "<tr style=\"background:#fff;\">" +
                                    "<td colspan=\"2\" style=\"padding-bottom:15px;\">" +
                                        "<div style=\"padding-bottom:10px; border-bottom:1px solid #ccc;\"><img src='http://www.ipeksalevi.com/images/maillogo.png' /></div>" +
                                        "<h2 style=\"font-size:18px; text-align:center;\">Siparişiniz Tamamlandı</h2>" +
                                        "<p>Siparişiniz ile ilgili sizi aramamız gerekebilir.</p>" +
                                        "<p>Bu nedenle <span style=\"font-size:16px; font-weight:bold;\">+90 542 6009171</span> No'lu telefondan gelen çağrıları lütfen cevaplayınız.</p>" +
                                        "<p>Siparişiniz ile ilgili detayları sipariş takip sayfasından görebilirsiniz.</p>" +
                                        "<p>Siparişlerinizle ilgili her türlü soru ve önerileriniz için bizi yukarıdaki telefondan arayabilir whatsapp üzerinden yazabilirsiniz.</p>" +
                                        "<p><span style=\"font-size:16px; font-weight:bold;\">Sipariş Numaranız: <span style=\"color:#916e5a;\">" + model.OrderNo + "</span></span></p>" +
                                        "<p>Teşekkürler. Siparişinizi <a href=\"http://www.ipeksalevi.com/siparis-takip.aspx?siparisKodu=" + model.OrderNo + "\">sipariş takip</a> linkinden takip edebilirsiniz.</p>" +
                                        "<p><span style=\"font-size:16px; font-weight:bold;\">Lütfen Sipariş Numaranızı Kayıt Edin.</span></p>" +
                                        "<p><b>Not:</b> Siparişinizi web sitemizde \"Sipariş Takip\" linkinden sipariş numaranızı yazıp arama butonuna basarak veya direkt <a href=\"http://www.ipeksalevi.com/siparis-takip.aspx?siparisKodu=" + model.OrderNo + "\">Buraya</a> tıklayarak takip edebilirsiniz.</p>" +
                                      "</td>" +
                                "</tr>" +
                                "<tr style=\"background:#fff; text-align:center;\">" +
                                    "<td style=\" border-top:3px solid #d21e4e; padding-top:10px;\">" +
                                        "Her Türlü Soru ve görüşleriniz için arayabilir;<br/><span style=\"font-size:18px;\">+90 542 600 9171</span><br/>veya<br/>Whataspp üzerinden ulaşabilirsiniz." +
                                    "</td>" +
                                    "<td style=\" border-top:3px solid #d21e4e; padding-top:10px;\">" +
                                        "<div style=\"font-size:16px;\"><img src='http://www.ipeksalevi.com/images/mailsepet.png' />TAKİPTE KAL, <b>RENKLERİ YAKALA</b><br/><a href='https://www.facebook.com/ipeksalevi'><img src='http://www.ipeksalevi.com/images/mailface.png' /></a><a href='http://instagram.com/ipeksalevi'><img src='http://www.ipeksalevi.com/images/mailinstag.png' /></a><br/><a href=\"http://www.ipeksalevi.com\">www.ipeksalevi.com</a></div>" +
                                    "</td>" +
                                "</tr>" +
                           "</table>";
            contact.Bcc.Add(model.SendMail);

            try
            {
                mailClient.Send(contact);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
