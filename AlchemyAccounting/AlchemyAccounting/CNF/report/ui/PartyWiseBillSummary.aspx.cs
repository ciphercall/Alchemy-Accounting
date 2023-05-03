using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.CNF.report.ui
{
    public partial class PartyWiseBillSummary : System.Web.UI.Page
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
                    DateTime td = Global.Timezone(DateTime.Now);
                    string brCD = HttpContext.Current.Session["BrCD"].ToString();


                    txtDateFromBill.Text = td.ToString("dd/MM/yyyy");
                    txtDateToBill.Text = td.ToString("dd/MM/yyyy");
                    Global.txtAdd("SELECT (BRANCHID +'|'+ COMPNM) AS CB FROM ASL_COMPANY WHERE COMPID='" + brCD + "'", txtBranchIDBill);
                    txtBranchIDBill_TextChanged(sender, e);
                    lblCompanyIDBill.Text = brCD;

                    txtBranchIDBill.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListParty(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            if (uTp == "ADMIN")
            {
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
            }
            else
            {
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')", conn);
            }
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtparty_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd("select ACCOUNTCD from GL_ACCHART where ACCOUNTNM= '" + txtparty.Text + "' ", txtpatyID);
            txtDateFromBill.Focus();
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
        protected void ddlJobTypeBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtparty.Focus();
        }
        protected void txtBranchIDBill_TextChanged(object sender, EventArgs e)
        {
            if (txtBranchIDBill.Text == "")
            {
                lblbranchtagBill.Visible = false;
                Label1Bill.Visible = false;
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select Branch Name.";
                lblbranchBill.Visible = false;
                txtBranchIDBill.Focus();
            }
            else
            {
                string companyName = "";
                string branch = "";

                string searchPar = txtBranchIDBill.Text;

                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    branch = lineSplit[0];
                    companyName = lineSplit[1];

                    txtBranchIDBill.Text = companyName.Trim();
                    lblbranchBill.Text = branch.Trim();
                }


                lblbranchtagBill.Visible = true;
                Label1Bill.Visible = true;
                lblbranchBill.Visible = true;
                lblCompanyIDBill.Text = "";
                Global.lblAdd("select COMPID from ASL_COMPANY where COMPNM='" + txtBranchIDBill.Text + "' and BRANCHID='" + lblbranchBill.Text + "' ", lblCompanyIDBill);
                if (lblCompanyIDBill.Text == "")
                {
                    txtBranchIDBill.Text = "";
                    lblbranchtagBill.Visible = false;
                    Label1Bill.Visible = false;
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select Branch Name.";
                    lblbranchBill.Visible = false;
                    txtBranchIDBill.Focus();
                }
                else
                    ddlJobTypeBill.Focus();
            }
        }
        protected void btnViewBill_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                DateTime frdt = DateTime.Parse(txtDateFromBill.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                DateTime tdt = DateTime.Parse(txtDateToBill.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                if (txtBranchIDBill.Text == "")
                {
                    lblErrmsg.Text = "";
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Office Name missing.";
                    txtBranchIDBill.Focus();
                } 
                else if (txtpatyID.Text == "")
                {
                    lblErrmsg.Text = "";
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select Party Name.";
                    txtparty.Text = "";
                    txtparty.Focus();
                }
                else if (frdt > tdt)
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "From date should smaller than To date";
                    txtDateFromBill.Focus();
                }
                else
                {
                    Session["Companyid"] = null;
                    Session["company"] = null;
                    Session["partynm"] = null;
                    Session["partyid"] = null;
                    Session["JOBYY"] = null;
                    Session["JOBTP"] = null;
                    Session["branch"] = null;
                    Session["partyName"] = null;
                    Session["partyId"] = null;

                    Session["Companyid"] = lblCompanyIDBill.Text;
                    Session["company"] = lblbranchBill.Text;
                    Session["FromDate"] = txtDateFromBill.Text;
                    Session["Todate"] = txtDateToBill.Text;
                    Session["JOBTP"] = ddlJobTypeBill.Text;
                    Session["branch"] = ddlJobTypeBill.Text;
                    Session["partyName"] = txtparty.Text;
                    Session["partyId"] = txtpatyID.Text;

                    lblErrmsg.Text = "";
                    lblErrmsg.Visible = false;
                    Page.ClientScript.RegisterStartupScript(
                             this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-PartyWiseBill.aspx','_newtab');", true);
                }
            }
        }
    }
}