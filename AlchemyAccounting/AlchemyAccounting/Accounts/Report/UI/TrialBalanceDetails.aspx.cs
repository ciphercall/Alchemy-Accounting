using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.Accounts.Report.UI
{
    public partial class TrialBalanceDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtFromDate.Text = td;
                txtToDate.Text = td;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFromDate.Text == "")
            {
                Response.Write("<script>alert('Select From Date.');</script>");
            }
            else if (txtToDate.Text == "")
            {
                Response.Write("<script>alert('Select To Date.');</script>");
            }
            else
            {
                Session["FromDate"] = txtFromDate.Text;
                Session["ToDate"] = txtToDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptTrialBalanceDetails.aspx','_newtab');", true);
            }
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }
    }
}