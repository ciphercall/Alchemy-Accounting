using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlchemyAccounting.Login.DataAccess;
using AlchemyAccounting.Login.Interface;

namespace AlchemyAccounting.Login.UI
{
    public partial class RegistrationForm : System.Web.UI.Page
    {
        LoginDataAccess dob = new LoginDataAccess();
        LoginInterface iob = new LoginInterface();
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Visible = false;
            //btnSuccess.Visible = false;
        }
        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            string str = "";
            LoginDataAccess dob = new LoginDataAccess();
            if (txtusername.Text == "")
            {
                Label1.Visible = true;
                Label1.Text = "User Name Requred !";
                return;
            }
            if (txtusername.Text != "")
            {
                iob.UserID = txtusername.Text;
                iob.Password = txtpassword.Text;
                iob.Name = txtname.Text;
                iob.Email = txtemail.Text;
                //iob.SecurityQ = txtsecurityqus.Text;
               // iob.SecurityA = txtansewer.Text;
                str = dob.InsertEmpLoginInfo(iob);

                Label1.Visible = true;
                Label1.Text = str;
                //string sr = "Your Registration has been Successfully Completed";
                //string myScript = @"confirm(" + "\"" + sr.ToString() + "\")";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", myScript, true);
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            //if (Label1.Text == "Registration Successfully......")
            //{
            //    btnSuccess.Visible = true;
            //}

        }
        }
    }
