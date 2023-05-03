using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.Party.UI
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(Global.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["email"] == null)
            {
                Response.Redirect("~/Login/UI/PartyLogin.aspx");
            }
            else
            {
                
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string oldpass = txtoldpass.Text;
            string newpass = txtnewpass.Text;
            string conpass = txtconfirmpass.Text;
            string chakpass = "";

            if (newpass==conpass)
            {
                string email = Session["email"].ToString();
                SqlCommand cmd = new SqlCommand("Select LOGINPW From CNF_PARTY where LOGINID='"+email+"'", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    chakpass = dr["LOGINPW"].ToString();
                }
                dr.Close();
                if (oldpass==chakpass)
                {
                    SqlCommand cmdupd = new SqlCommand("Update CNF_PARTY Set  LOGINPW='"+newpass+"' where LOGINID='"+email+"'", con);
                    cmdupd.ExecuteNonQuery();
                    Response.Write("<script>alert('Password updated successfully.')</script>");
                    Response.Redirect("../../Login/UI/PartyLogin.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Old Password is wrong.')</script>");
                    refresh();
                }
                con.Close();
            }
            else
            {
                Response.Write("<script>alert('Password is not match.')</script>");
                refresh();
            }
        }

        public void refresh()
        {
            txtoldpass.Text = "";
            txtnewpass.Text = "";
            txtconfirmpass.Text = "";
        }
    }
}