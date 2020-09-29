using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace homepage
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Butpwd_Click(object sender, EventArgs e)
        {
            DB_FunDayTripEntities dbFundaytrip = new DB_FunDayTripEntities();
            var q = from m in dbFundaytrip.tMembers
                    where m.fEmail_Member == Txtmail.Text
                    select m;
            if (!q.Any())
            {
                Label.Text = "無此帳號";
            }
            else
            {
            //MailMessage mm = new MailMessage("msit1271230@gmail.com", Txtmail.Text);
            //mm.Subject = "驗證碼!";
            //mm.Body = "你好 請妥善保管你的密碼";
            //mm.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.EnableSsl = true;
            //NetworkCredential nc = new NetworkCredential();
            //nc.UserName = "msit1271230@gmail.com";
            //nc.Password = "gijoe1230";
            //smtp.UseDefaultCredentials = true;
            //smtp.Credentials = nc;
            //smtp.Port = 587;
            //smtp.Send(mm);
                Label.Text = "已送出請檢查信箱";
            }
            

        }
    }
}