using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.report.ui
{
    public partial class job_bill_status_station : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = Global.Timezone(DateTime.Now);
                    txtJobYear.Text = td.ToString("yyyy");

                    txtBranchID.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListBranchID(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            if (uTp == "ADMIN")
            {
                cmd = new SqlCommand("SELECT (BRANCHID +'|'+ COMPNM) AS CB FROM ASL_COMPANY WHERE BRANCHID LIKE '" + prefixText + "%' ", conn);
            }
            else
            {
                cmd = new SqlCommand("SELECT (BRANCHID +'|'+ COMPNM) AS CB FROM ASL_COMPANY WHERE BRANCHID LIKE '" + prefixText + "%' ", conn);
            }
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["CB"].ToString());
            return CompletionSet.ToArray();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListYear(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);

            cmd = new SqlCommand("SELECT DISTINCT JOBYY FROM CNF_JOB WHERE JOBYY  LIKE '" + prefixText + "%' ", conn);

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["JOBYY"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtBranchID_TextChanged(object sender, EventArgs e)
        {
            if (txtBranchID.Text == "")
            {
                lblbranchtag.Visible = false;
                Label1.Visible = false;
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select Branch Name.";
                lblbranch.Visible = false;
                txtBranchID.Focus();
            }
            else
            {
                string companyName = "";
                string branch = "";

                string searchPar = txtBranchID.Text;

                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    branch = lineSplit[0];
                    companyName = lineSplit[1];

                    txtBranchID.Text = companyName.Trim();
                    lblbranch.Text = branch.Trim();
                }


                lblbranchtag.Visible = true;
                Label1.Visible = true;
                lblbranch.Visible = true;
                lblCompanyID.Text = "";
                Global.lblAdd("select COMPID from ASL_COMPANY where COMPNM='" + txtBranchID.Text + "' and BRANCHID='" + lblbranch.Text + "' ", lblCompanyID);
                if (lblCompanyID.Text == "")
                {
                    txtBranchID.Text = "";
                    lblbranchtag.Visible = false;
                    Label1.Visible = false;
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select Branch Name.";
                    lblbranch.Visible = false;
                    txtBranchID.Focus();
                }
                else
                    ddlRegID.Focus();
            }
        }

        protected void ddlRegID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlJobType.Focus();
        }

        protected void txtJobYear_TextChanged(object sender, EventArgs e)
        {
            btnView.Focus();
        }

        protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtJobYear.Focus();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (txtBranchID.Text == "")
                {
                    lblErrmsg.Text = "";
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Office Name missing.";
                    txtBranchID.Focus();
                }
                else if (txtJobYear.Text == "")
                {
                    lblErrmsg.Text = "";
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Job Year missing.";
                    txtJobYear.Focus();
                }

                else
                {
                    Session["Companyid"] = null;
                    Session["company"] = null;
                    Session["station"] = null;
                    Session["JOBYY"] = null;
                    Session["JOBTP"] = null;
                    Session["branch"] = null;

                    Session["Companyid"] = lblCompanyID.Text;
                    Session["company"] = txtBranchID.Text;
                    Session["station"] = ddlRegID.Text;
                    Session["JOBYY"] = txtJobYear.Text;
                    Session["JOBTP"] = ddlJobType.Text;
                    Session["branch"] = ddlJobType.Text;

                    lblErrmsg.Text = "";
                    lblErrmsg.Visible = false;
                    Page.ClientScript.RegisterStartupScript(
                             this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-job-bill-status-station.aspx','_newtab');", true);
                }
            }
        }
    }
}