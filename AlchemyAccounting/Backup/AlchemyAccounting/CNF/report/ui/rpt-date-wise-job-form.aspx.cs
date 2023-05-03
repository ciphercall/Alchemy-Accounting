using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.CNF.report.ui
{
    public partial class rpt_date_wise_job_form : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = DateTime.Now;
                    txtFromDate.Text = td.ToString("dd/MM/yyyy");
                    txtToDate.Text = td.ToString("dd/MM/yyyy");
                    txtFromDate.Focus();
                    Session["fromdate"] = "";
                    Session["todate"] = "";
                }
            }
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            txtToDate.Focus();
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            btnSubmit.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime frdt = DateTime.Parse(txtFromDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime tdt = DateTime.Parse(txtToDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            if(frdt>tdt)
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "From date should smaller than To date";
                txtFromDate.Focus();
            }
            else
            {
                lblErrmsg.Visible = false;
            Session["fromdate"] = txtFromDate.Text;
            Session["todate"] = txtToDate.Text;

            Page.ClientScript.RegisterStartupScript(
                      this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-date-wise-job.aspx','_newtab');", true);
                
        }
    }
    }
}