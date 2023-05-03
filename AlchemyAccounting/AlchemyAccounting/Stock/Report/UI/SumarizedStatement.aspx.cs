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
    public partial class SumarizedStatement : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

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
                    DateTime today = DateTime.Today.Date;
                    string td = Global.Dayformat(today);
                    txtFrom.Text = td;
                    txtTo.Text = td;
                    Session["ddlType"] = ddlType.Text;
                    ddlType.Focus();
                }
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ddlType"] = "";
            if (ddlType.Text == "SALE")
            {
                Session["ddlType"] = ddlType.Text;
                btnSearch.Focus();
            }
            else if (ddlType.Text == "BUY")
            {
                Session["ddlType"] = ddlType.Text;
                btnSearch.Focus();
            }
        }

        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime FrDT = DateTime.Parse(txtFrom.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime ToDT = DateTime.Parse(txtTo.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            if (FrDT > ToDT)
            {
                Response.Write("<script>alert('From Date is Greater than To Date.');</script>");
                btnSearch.Focus();
            }
            else
            {
                Session["Type"] = ddlType.Text;
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptSummarizedStatement.aspx','_newtab');", true);
            }
        }

    }
}