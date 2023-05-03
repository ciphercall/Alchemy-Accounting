using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using AlchemyAccounting.cr_user.Interface;
using AlchemyAccounting.cr_user.Dataaccess;
using AlchemyAccounting;

namespace AlchemyAccounting.cr_user.ui
{
    public partial class create_user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Global.dropDownAddWithSelect(ddlBranch,"SELECT COSTPNM FROM GL_COSTP ORDER BY CATID");
                    txtName.Focus();
                }
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Type Email Address.";
                txtEmail.Focus();
            }
            else
            {
                lblMsg.Visible = false;
                txtUsid.Text = txtEmail.Text;
                ddlBranch.Focus();
            }
        }

        protected void txtConpas_TextChanged(object sender, EventArgs e)
        {
            if (txtConpas.Text == "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Type Confirm Password.";
                txtConpas.Focus();
            }
            else
            {
                if (txtConpas.Text != txtPass.Text)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Password Mismatch.";
                    txtConpas.Text = "";
                    txtConpas.Focus();
                }
                else
                {
                    lblMsg.Visible = false;
                    ddlBranch.Focus();
                }
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.Text == "Select")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Select Branch";
                ddlBranch.Focus();
            }
            else
            {
                Global.lblAdd("SELECT CATID FROM GL_COSTP WHERE COSTPNM ='" + ddlBranch.Text + "'", lblBrachCD);
                ddlUserTP.Focus();
            }
        }

        protected void ddlUserTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPass.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                string OpenUser = HttpContext.Current.Session["UserName"].ToString();
                string PcName = HttpContext.Current.Session["PCName"].ToString();
                string ip = HttpContext.Current.Session["IpAddress"].ToString();

                string query = "";
                SqlCommand comm = new SqlCommand(query, con);

                AlchemyAccounting.cr_user.Interface.crinterface iob = new AlchemyAccounting.cr_user.Interface.crinterface();
                AlchemyAccounting.cr_user.Dataaccess.crdataacces dob = new AlchemyAccounting.cr_user.Dataaccess.crdataacces();

                if (txtName.Text == "")
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Type Name.";
                    txtName.Focus();
                }
                else if (txtUsid.Text == "")
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User ID Can't blank.";
                    txtUsid.Focus();
                }
                else if (txtPass.Text == "")
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Provide a Password.";
                    txtPass.Focus();
                }
                else if (txtPass.Text == "")
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Type Password.";
                    txtConpas.Focus();
                }
                else if (txtPass.Text != txtConpas.Text)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Password doesn't match.";
                    txtConpas.Focus();
                }
                else
                {
                    lblchkUser.Text = "";
                    Global.lblAdd(@"Select UserID FROM User_Registration where UserID = '" + txtUsid.Text + "'", lblchkUser);

                    if (lblchkUser.Text == "")
                    {

                        lblMsg.Visible = true;

                        iob.Name = txtName.Text;
                        iob.Email = txtEmail.Text;
                        iob.Usid = txtUsid.Text;
                        iob.Branch = lblBrachCD.Text;
                        iob.UserType = ddlUserTP.Text;
                        iob.Pass = txtPass.Text;
                        iob.Openuser = OpenUser;
                        iob.Pcname = PcName;
                        iob.Ipaddress = ip;

                        dob.insertUser(iob);
                        lblMsg.Visible = true;
                        lblMsg.Text = "User id Created";
                        refresh();
                    }
                    else
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "User ID alreary exist. Try another.";
                        txtUsid.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public void refresh()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtUsid.Text = "";
            txtConpas.Text = "";
            txtPass.Text = "";
            ddlBranch.SelectedIndex = -1;
            ddlUserTP.SelectedIndex = -1;
            lblBrachCD.Text = "";
            txtName.Focus();
        }
    }
}