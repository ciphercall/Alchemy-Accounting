using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net;
using System.Data;

namespace AlchemyAccounting.Login.UI
{
    public partial class PartyLogin : System.Web.UI.Page
    {
        SqlConnection con=new SqlConnection(Global.connection);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserName"] = null;
                Session["IpAddress"] = null;
                Session["PCName"] = null;
                Session["BrCD"] = null;
                Session["UserTp"] = null;

            }
            //RegisterHyperLink.NavigateUrl = "~/Login/UI/Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //RegisterHyperLink.NavigateUrl = "~/Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            txtUserName.Focus();
        }
        protected void loginButton_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text != "" || txtPassword.Text != "")
            {
                string logmail = txtUserName.Text.Trim();
                string logpass = txtPassword.Text.Trim();
                string dbpass = "";

                SqlCommand cmdpass = new SqlCommand("Select LOGINPW From CNF_PARTY where LOGINID=@Email",con);
                cmdpass.Parameters.Clear();
                cmdpass.Parameters.AddWithValue("@Email", logmail);
                con.Open();
                SqlDataReader drpass = cmdpass.ExecuteReader();
                while (drpass.Read())
                {
                    dbpass = drpass["LOGINPW"].ToString();
                }
                drpass.Close();
                con.Close();
                if (logpass==dbpass)
                {
                    lblErrmsg.Visible = false;
                    Session["email"] = logmail;
                    Response.Redirect("../../Party/UI/track.aspx");
                }
                else
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "User id or Password is incorrect.";
                }

            }
            else
            {
                lblErrmsg.Text = "Fill user name or password.";
            }

        }



        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "User Name Missing.";
            }
            else
            {
                lblErrmsg.Visible = false;
                txtPassword.Focus();
            }
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Password Missing.";
            }
            else
            {
                lblErrmsg.Visible = false;
                loginButton.Focus();
            }
        }
    }
}