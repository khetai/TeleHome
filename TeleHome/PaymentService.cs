using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using TeleHome.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace TeleHome
{
    public class PaymentService
    {
        private readonly RmlubecoTelehomeContext _db;
        private readonly HttpClient httpClient;

        public PaymentService(RmlubecoTelehomeContext db)
        {
            _db = db;
            httpClient = new HttpClient();
        }

        public async Task<string> CreateOrder(int? sessionId)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(currentDirectory);
            //string privateKeyPath = Path.Combine(AppContext.BaseDirectory, "Telehome.keys", "merchant_name.key");
            //var privateKey = new X509Certificate2("keys\\merchant_name.key");
            var privateKey = new X509Certificate2("C:\\Users\\kamil\\source\\repos\\TeleHome\\TeleHome\\keys\\merchant_name.key");


            string sslCertificatePath = Path.Combine(AppContext.BaseDirectory, "Telehome.keys", "testmerchant.crt");
            var sslCertificate = new X509Certificate2(sslCertificatePath);


            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ClientCertificates.Add(sslCertificate);
            httpClientHandler.ClientCertificates.Add(privateKey);

            var httpClient = new HttpClient(httpClientHandler);


            
            string url = "https://tstpg.kapitalbank.az:5443/Exec";
            string xmlPayload = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
                       <TKKPG>
                           <Request>
                               <Operation>CreateOrder</Operation>
                               <Language>RU</Language>
                               <Order>
                                   <OrderType>Purchase</OrderType>
                                   <Merchant>E1000010</Merchant>
                                   <Amount>123456</Amount>
                                   <Currency>944</Currency>
                                   <Description>xxxxxxxx</Description>
                                   <ApproveURL>/testshopPageReturn.jsp</ApproveURL>
                                   <CancelURL>/testshopPageReturn.jsp</CancelURL>
                                   <DeclineURL>/testshopPageReturn.jsp</DeclineURL>
                                   <SessionID>{sessionId}</SessionID>
                               </Order>
                           </Request>
                       </TKKPG>";
            var httpContent = new StringContent(xmlPayload, Encoding.UTF8, "application/xml");
            var response = await httpClient.PostAsync(url, httpContent);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseContent);
                string orderId = xmlDoc.SelectSingleNode("/TKKPG/Response/Order/OrderID")?.InnerText;
                string paymentUrl = xmlDoc.SelectSingleNode("/TKKPG/Response/Order/URL")?.InnerText;

                // Gerekli verileri kullanarak işlemleri yapın

                return paymentUrl;
            }
            else
            {
                throw new Exception("API isteği başarısız oldu.");
            }
        }

        public async Task<Order> CreateNewOrder(Order order,int? sessionId)
        {
            List<Basket> b = await _db.Baskets.Where(x => x.BasketUserId == sessionId && x.BasketSelected ==true).Include(x=>x.BasketProduct).ToListAsync();
            double total = 0;

              order.OrderUserId = sessionId;
            order.OrderDate = DateTime.Now;
            order.OrderStatus = false;
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            foreach (var basket in b)
            {
                OrderBasket ob = new OrderBasket();
                ob.OrderBasketProductId = (int)basket.BasketProductId;
                //ob.OrderBasketUserId = (int)basket.BasketUserId;
                ob.OrderBasketMoney = (double)basket.BasketProduct.ProductPrice;
                ob.OrderBasketOrderId = order.OrderId;
                ob.OrderBasketCount = (int)basket.BasketQuantity;
                if(basket.BasketProduct.ProductWithoutPrice != null)
                {
                   total += (double)(basket.BasketProduct.ProductWithoutPrice * basket.BasketQuantity);

                }
                else
                {
                    total += (double)(basket.BasketProduct.ProductPrice * basket.BasketQuantity);
                }

                _db.OrderBaskets.Add(ob);
                //await _db.SaveChangesAsync();
            }
            order.OrderMoney = (double)total;
            order.OrderUserId = b.FirstOrDefault().BasketUserId;

            _db.Orders.Update(order);
            await _db.SaveChangesAsync();

            return order;
        }


    }
}
