using Azure.Core;
using Core.Models;
using IPara.DeveloperPortal.Core;
using IPara.DeveloperPortal.Core.Entity;
using IPara.DeveloperPortal.Core.Request;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Product = IPara.DeveloperPortal.Core.Entity.Product;

namespace Service.Services.Payment
{
    public class PaymentService
    {
        public string CartPayment(PaymentModel model)
        {

            Decimal Amount = Convert.ToDecimal(model.Total) * 100;//1.00TL için 100 girilmelidir.

            string CardHolderName = model.CardHolderName;
            string CardNumber = model.CardNumber;
            string CardExpireDateMonth = model.ExpirateDateMonth;
            string CardExpireDateYear = model.ExpirateDateYear;
            string CardCVV2 = model.CardCVV2;
            string MerchantOrderId = model.MerchantOrderId;
            string CustomerId = "97969037"; //Müsteri Numarasi
            string MerchantId = "73822"; //Magaza Kodu
            string OkUrl = model.OkUrl;
            string FailUrl = model.FailUrl;
            string UserName = "ipekapi"; //  api rollü kullanici adı
            string Password = "123456";//  api rollü kullanici sifresi
            SHA1 sha = new SHA1CryptoServiceProvider();
            string HashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(Password)));
            string hashstr = MerchantId + MerchantOrderId + Amount.ToString() + OkUrl + FailUrl + UserName + HashedPassword;
            byte[] hashbytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(hashstr);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            string hash = Convert.ToBase64String(inputbytes);
            string HashData = hash;

            //string gServer = "https://sanalpos.kuveytturk.com.tr/ServiceGateWay/Home/ThreeDModelPayGate";
            string gServer = "https://boatest.kuveytturk.com.tr/boa.virtualpos.services/Home/ThreeDModelPayGate";
            string
            postdata = "<KuveytTurkVPosMessage xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>";
            postdata = postdata + "<APIVersion>TDV2.0.0</APIVersion>";
            postdata = postdata + "<OkUrl>" + OkUrl + "</OkUrl>";
            postdata = postdata + "<FailUrl>" + FailUrl + "</FailUrl>";
            postdata = postdata + "<HashData>" + HashData + "</HashData>";
            postdata = postdata + "<MerchantId>" + MerchantId + "</MerchantId>";
            postdata = postdata + "<CustomerId>" + CustomerId + "</CustomerId>";
            postdata = postdata + "<UserName>" + UserName + "</UserName>";
            postdata = postdata + "<CardNumber>" + CardNumber + "</CardNumber>";
            postdata = postdata + "<CardExpireDateYear>" + CardExpireDateYear + "</CardExpireDateYear>";
            postdata = postdata + "<CardExpireDateMonth>" + CardExpireDateMonth + "</CardExpireDateMonth>";
            postdata = postdata + "<CardCVV2>" + CardCVV2 + "</CardCVV2>";
            postdata = postdata + "<CardHolderName>" + CardHolderName + "</CardHolderName>";
            postdata = postdata + "<CardType>Troy</CardType>";
            postdata = postdata + "<TransactionType>Sale</TransactionType>";
            postdata = postdata + "<InstallmentCount>0</InstallmentCount>";
            postdata = postdata + "<Amount>" + Amount + "</Amount>";
            postdata = postdata + "<DisplayAmount>" + Amount + "</DisplayAmount>";
            postdata = postdata + "<CurrencyCode>0949</CurrencyCode>";
            postdata = postdata + "<MerchantOrderId>" + MerchantOrderId + "</MerchantOrderId>";
            postdata = postdata + "<TransactionSecurity>3</TransactionSecurity>";
            postdata = postdata + "</KuveytTurkVPosMessage>";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            byte[] buffer = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(gServer);
            WebReq.Timeout = 5 * 60 * 1000;

            WebReq.Method = "POST";
            WebReq.ContentType = "application/xml";
            WebReq.ContentLength = buffer.Length;

            WebReq.CookieContainer = new CookieContainer();
            Stream ReqStream = WebReq.GetRequestStream();
            ReqStream.Write(buffer, 0, buffer.Length);

            ReqStream.Close();

            WebResponse WebRes = WebReq.GetResponse();
            Stream ResStream = WebRes.GetResponseStream();
            StreamReader ResReader = new StreamReader(ResStream);
            string responseString = ResReader.ReadToEnd();

            return responseString;

        }
        
        Settings settings = new Settings();

        public string PaytrPayment(PaymentModel model, List<Product> products, Order order) {

            settings.PublicKey = "F4LH7J65J7UBYUD";
            settings.PrivateKey = "F4LH7J65J7UBYUD08ETP1H1N2";
            settings.Version = "1.0";
            settings.Mode = "P";
            settings.HashString = String.Empty;
            settings.TransactionDate = Helper.GetTransactionDateString();
            settings.BaseUrl = "https://api.ipara.com/";

            ThreeDPaymentRequest request = new();
            request.OrderId = Guid.NewGuid().ToString();
            request.Version = settings.Version;
            request.Amount = model.Total.ToString().Replace(",","");
            request.CardOwnerName = model.CardHolderName;
            request.CardNumber = model.CardNumber;
            request.CardExpireMonth = model.ExpirateDateMonth;
            request.CardExpireYear = model.ExpirateDateYear;
            request.Installment = model.Installment.ToString();
            request.Cvc = model.CardCVV2;
            request.TransactionDate = Helper.GetTransactionDateString();
            request.Mode = settings.Mode;
            request.Language = "tr-TR";
            request.SuccessUrl = model.OkUrl;
            request.FailUrl = model.FailUrl;

            request.Purchaser = new();
            request.Purchaser.Name = order.Name;
            request.Purchaser.SurName = order.Surname;
            request.Purchaser.Email = order.Email;
            request.Purchaser.ClientIp = model.IpAdress;

            string hashString = settings.PrivateKey + request.OrderId + request.Amount + request.Mode + request.CardOwnerName + request.CardNumber + request.CardExpireMonth + request.CardExpireYear + request.Cvc + request.UserId + request.Purchaser.Name + request.Purchaser.SurName + request.Purchaser.Email + request.TransactionDate;

            request.Token = Helper.CreateToken(settings.PublicKey, hashString);

            request.Products = products;

            string threeDform = ThreeDPaymentRequest.Execute(request, settings);

            return threeDform;
        
        }
    }
}
