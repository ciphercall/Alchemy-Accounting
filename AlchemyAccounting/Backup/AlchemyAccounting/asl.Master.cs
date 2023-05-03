using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.Net.Mail;

namespace AlchemyAccounting
{
    public partial class asl : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                MailMessage mm = new MailMessage("shamim08sust@gmail.com", "paji_kishor@ymail.com")
                {
                    Subject = "Complain/suggestion/Inquery",
                    IsBodyHtml = true,
                    Body = txtmsg.Text
                };

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("shamim08sust@gmail.com", "getlifepouch")

                };

                smtp.Send(mm);
            }

            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
    }
}