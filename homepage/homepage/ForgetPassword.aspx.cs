using System;
using System.Collections.Generic;
using System.Drawing;
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
            var q2 = (from m in dbFundaytrip.tMembers
                    where m.fEmail_Member == Txtmail.Text
                    select m.fPassword_Member).FirstOrDefault();
            var q3 = (from m in dbFundaytrip.tMembers
                      where m.fEmail_Member == Txtmail.Text
                      select m.fEmail_Member).FirstOrDefault();
            if (!q.Any())
            {
                Label.Text = "無此帳號";
                Label.ForeColor = Color.Red;
            }
            else
            {
                MailMessage mm = new MailMessage("msit1271230@gmail.com", Txtmail.Text);
                mm.Subject = "驗證碼!";
                mm.Body = string.Format("你好:<h1>{0}會員</h1><br/><h2>請登入之後記得修改你的密碼</h2><br/><h2>你的密碼是</h2><br/><h1 style='color:red'>{1}</h1>", q3, q2);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential();
                nc.UserName = "msit1271230@gmail.com";
                nc.Password = "gijoe1230";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Port = 587;
                smtp.Send(mm);
                Label.Text = "已送出請檢查信箱";
                Label.ForeColor = Color.Green;
            }
            

        }
    }
}