using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Accounts.Report.UI
{
    public partial class RptReceiptsPayStat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtFrom.Text = td;
                txtTo.Text = td;
                txtFromSelect.Text = td;
                txtToSelect.Text = td;
                Global.DropDownAddTextWithValue(ddlAccessCode, @"SELECT CATNM,CATID FROM GL_COSTPMST ORDER BY CATID");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "" || txtTo.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else
            {
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptReceiptPaymentState.aspx','_newtab');", true);
                //Response.Redirect("~/Accounts/Report/Report/ReportLedgerBook.aspx");
            }
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }
        protected void btnSearchSelect_Click(object sender, EventArgs e)
        {
            if (ddlAccessCode.Text == "Select")
            {
                Response.Write("<script>alert('Select Access Code');</script>");
            }
            else if (txtFrom.Text == "" || txtTo.Text == "")
            {
                Response.Write("<script>alert('Fill Required Data');</script>");
            }
            else
            {
                Session["From"] = txtFromSelect.Text;
                Session["To"] = txtToSelect.Text;
                Session["AccessCd"] = ddlAccessCode.SelectedValue;
                Session["AccessName"] = ddlAccessCode.SelectedItem.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptReceiptPaymentStateAccessSelect.aspx','_newtab');", true);
                //Response.Redirect("~/Accounts/Report/Report/ReportLedgerBook.aspx");
            }
        }
    }
}