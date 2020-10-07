using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace homepage.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            //
            return View();
        }

        public ActionResult LvVIP()
        {
            //創造一組亂數字串不重複的訂單編號
            var str = "123456789ABCDEFGHIJKLMNPQRSTUVWXYZabcdefhijklmnorstuvwxz";
            var next = new Random();
            var builder = new StringBuilder();
            for (var i = 0; i < 10; i++)
            {
                builder.Append(str[next.Next(0, str.Length)]);
            }

            //MerchantID(不可變動)，MerchantTradeNo(亂數訂單編號)，MerchantTradeDate(抓取當前日期時間)
            int MerchantID = 2000132;
            var MerchantTradeNo = builder;
            string MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            string ReturnUrl = "http://localhost:53676/Home/Index/" + MerchantTradeNo;
            string ProductName = "升級營利會員";
            int Amount = 99;
            //把需要的資料作串接
            string Url = "HashKey=5294y06JbISpM5x9&ChoosePayment=ALL&ChooseSubPayment=&ClientBackURL=" + ReturnUrl + "&EncryptType=1&ItemName="
                + ProductName
                + "&MerchantID="
                + MerchantID
                + "&MerchantTradeDate="
                + MerchantTradeDate
                + "&MerchantTradeNo="
                + MerchantTradeNo
                + "&PaymentType=aio&ReturnURL=" + ReturnUrl + "&StoreID=&TotalAmount=" + Amount + "&TradeDesc=建立全金流測試訂單&HashIV=v77hoKGq4kWxNNIS";



            //串接好的資料轉成Encoded
            var Encoded = System.Web.HttpUtility.UrlEncode(Url);
            //Encoded 轉成 小寫 encoded
            var encoded = Encoded.ToLower();
            //呼叫sha256_hash(encoded)轉換成SHA256 在轉換大寫
            string SHA256 = sha256_hash(encoded).ToUpper();
            //把資料傳到前端
            ViewBag.MerchantID = MerchantID;
            ViewBag.MerchantTradeNo = MerchantTradeNo;
            ViewBag.MerchantTradeDate = MerchantTradeDate;
            ViewBag.SHA256 = SHA256;
            ViewBag.Url = ReturnUrl;
            ViewBag.ProductName = ProductName;
            ViewBag.Amount = Amount;
            return View();
        }


        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}