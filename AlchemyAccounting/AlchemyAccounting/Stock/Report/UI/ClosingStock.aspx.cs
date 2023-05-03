using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;

namespace AlchemyAccounting.Stock.Report.UI
{
    public partial class ClosingStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else if (Session["UserName"].ToString() == "")
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Global.dropDownAdd(ddlStore, "SELECT STORENM FROM STK_STORE");
                    DateTime today = DateTime.Today.Date;
                    string td = Global.Dayformat(today);
                    txtDate.Text = td;
                    Global.lblAdd(@"Select STOREID from STK_STORE where STORENM = '" + ddlStore.Text + "'", lblStID);
                }
            }
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.lblAdd(@"Select STOREID from STK_STORE where STORENM = '" + ddlStore.Text + "'", lblStID);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                Response.Write("<script>alert('Select Date.');</script>");
            }
            else
            {
                Session["StoreNM"] = ddlStore.Text;
                Session["StoreID"] = lblStID.Text;
                Session["Date"] = txtDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptClosingStock.aspx','_newtab');", true);
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }
    }
}