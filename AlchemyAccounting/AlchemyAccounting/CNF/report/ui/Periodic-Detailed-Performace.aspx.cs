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
    public partial class rpt_Periodic_Detailed_Performace : System.Web.UI.Page
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
                    //txtDateFrom.Text = td.ToString("dd/MM/yyyy");
                    //txtDateTo.Text = td.ToString("dd/MM/yyyy");
                    //txtDateFromForSummury.Text = td.ToString("dd/MM/yyyy");
                    //txtDateToForSummury.Text = td.ToString("dd/MM/yyyy");
                    string brCD = HttpContext.Current.Session["BrCD"].ToString();
                   // Global.txtAdd("SELECT (BRANCHID +'|'+ COMPNM) AS CB FROM ASL_COMPANY WHERE COMPID='" + brCD + "'", txtBranchID);
                   // txtBranchID_TextChanged(sender, e);
                    //lblCompanyID.Text = brCD;
                    //
                    

                    txtDateFromBill.Text = td.ToString("dd/MM/yyyy");
                    txtDateToBill.Text = td.ToString("dd/MM/yyyy");
                    txtDateFromBillSummarty.Text = td.ToString("dd/MM/yyyy");
                    txtDateToBillSummarty.Text = td.ToString("dd/MM/yyyy");
                    Global.txtAdd("SELECT (BRANCHID +'|'+ COMPNM) AS CB FROM ASL_COMPANY WHERE COMPID='" + brCD + "'", txtBranchIDBill);
                    txtBranchIDBill_TextChanged(sender, e);
                    lblCompanyIDBill.Text = brCD;

                    txtBranchIDBill.Focus();
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

        //protected void txtBranchID_TextChanged(object sender, EventArgs e)
        //{
        //    if (txtBranchID.Text == "")
        //    {
        //        lblbranchtag.Visible = false;
        //        Label1.Visible = false;
        //        lblErrmsg.Visible = true;
        //        lblErrmsg.Text = "Select Branch Name.";
        //        lblbranch.Visible = false;
        //        txtBranchID.Focus();
        //    }
        //    else
        //    {
        //        string companyName = "";
        //        string branch = "";

        //        string searchPar = txtBranchID.Text;

        //        int splitter = searchPar.IndexOf("|");
        //        if (splitter != -1)
        //        {
        //            string[] lineSplit = searchPar.Split('|');

        //            branch = lineSplit[0];
        //            companyName = lineSplit[1];

        //            txtBranchID.Text = companyName.Trim();
        //            lblbranch.Text = branch.Trim();
        //        }


        //        lblbranchtag.Visible = true;
        //        Label1.Visible = true;
        //        lblbranch.Visible = true;
        //        lblCompanyID.Text = "";
        //        Global.lblAdd("select COMPID from ASL_COMPANY where COMPNM='" + txtBranchID.Text + "' and BRANCHID='" + lblbranch.Text + "' ", lblCompanyID);
        //        if (lblCompanyID.Text == "")
        //        {
        //            txtBranchID.Text = "";
        //            lblbranchtag.Visible = false;
        //            Label1.Visible = false;
        //            lblErrmsg.Visible = true;
        //            lblErrmsg.Text = "Select Branch Name.";
        //            lblbranch.Visible = false;
        //            txtBranchID.Focus();
        //        }
        //        else
        //            ddlJobType.Focus();
        //    }
        //}

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

       
        //protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtDateFrom.Focus();
        //}
      
        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    if (Session["UserName"] == null)
        //        Response.Redirect("~/Login/UI/Login.aspx");
        //    else
        //    {
        //        DateTime frdt = DateTime.Parse(txtDateFrom.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //        DateTime tdt = DateTime.Parse(txtDateTo.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //        if (txtBranchID.Text == "")
        //        {
        //            lblErrmsg.Text = "";
        //            lblErrmsg.Visible = true;
        //            lblErrmsg.Text = "Office Name missing.";
        //            txtBranchID.Focus();
        //        }
        //        else if (frdt > tdt)
        //        {
        //            lblErrmsg.Visible = true;
        //            lblErrmsg.Text = "From date should smaller than To date";
        //            txtDateFrom.Focus();
        //        }
        //        else
        //        {
        //            Session["Companyid"] = null;
        //            Session["company"] = null;
        //            Session["partynm"] = null;
        //            Session["partyid"] = null;
        //            Session["JOBYY"] = null;
        //            Session["JOBTP"] = null;
        //            Session["branch"] = null;

        //            Session["Companyid"] = lblCompanyID.Text;
        //            Session["company"] = lblbranch.Text;
        //            Session["FromDate"] = txtDateFrom.Text;
        //            Session["Todate"] = txtDateTo.Text;
        //            Session["JOBTP"] = ddlJobType.Text;
        //            Session["branch"] = ddlJobType.Text;

        //            lblErrmsg.Text = "";
        //            lblErrmsg.Visible = false;
        //            Page.ClientScript.RegisterStartupScript(
        //                     this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-Periodic-Detailed-Performace.aspx','_newtab');", true);
        //        }
        //    }
        //}

        //protected void btnSummary_Click(object sender, EventArgs e)
        //{
        //    if (Session["UserName"] == null)
        //        Response.Redirect("~/Login/UI/Login.aspx");
        //    else
        //    {
        //        DateTime frdt = DateTime.Parse(txtDateFromForSummury.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //        DateTime tdt = DateTime.Parse(txtDateToForSummury.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //        if (frdt > tdt)
        //        {
        //            lblErrmsg.Visible = true;
        //            lblErrmsg.Text = "From date should smaller than To date";
        //            txtDateFrom.Focus();
        //        }
        //        else
        //        {
        //            Session["Companyid"] = null;
        //            Session["company"] = null;
        //            Session["partynm"] = null;
        //            Session["partyid"] = null;
        //            Session["JOBYY"] = null;
        //            Session["JOBTP"] = null;
        //            Session["branch"] = null;

        //            Session["Companyid"] = lblCompanyID.Text;
        //            Session["company"] = lblbranch.Text;
        //            Session["FromDate"] = txtDateFromForSummury.Text;
        //            Session["Todate"] = txtDateToForSummury.Text;
        //            Session["JOBTP"] = ddlJobType.Text;
        //            Session["branch"] = ddlJobType.Text;

        //            lblErrmsg.Text = "";
        //            lblErrmsg.Visible = false;
        //            Page.ClientScript.RegisterStartupScript(
        //                     this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-Periodic-summarized-Performace.aspx','_newtab');", true);
        //        }
        //    }
        //}








        protected void ddlJobTypeBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDateFromBill.Focus();
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

                    Session["Companyid"] = lblCompanyIDBill.Text;
                    Session["company"] = lblbranchBill.Text;
                    Session["FromDate"] = txtDateFromBill.Text;
                    Session["Todate"] = txtDateToBill.Text;
                    Session["JOBTP"] = ddlJobTypeBill.Text;
                    Session["branch"] = ddlJobTypeBill.Text;

                    lblErrmsg.Text = "";
                    lblErrmsg.Visible = false;
                    Page.ClientScript.RegisterStartupScript(
                             this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-Periodic-Detailed-Performace-Bill-Date-Wise.aspx','_newtab');", true);
                }
            }
        }

        protected void btnBillSummary_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                DateTime frdt = DateTime.Parse(txtDateToBillSummarty.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                DateTime tdt = DateTime.Parse(txtDateToBillSummarty.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                if (frdt > tdt)
                {
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "From date should smaller than To date";
                    txtDateToBillSummarty.Focus();
                }
                else
                {
                    Session["Companyid"] = lblCompanyIDBill.Text;
                    Session["FromDateBillSummary"] = null; 
                    Session["TodateBillSummary"] = null;

                    Session["FromDateBillSummary"] = txtDateFromBillSummarty.Text;
                    Session["TodateBillSummary"] = txtDateToBillSummarty.Text;

                    lblErrmsg.Text = "";
                    lblErrmsg.Visible = false;
                    Page.ClientScript.RegisterStartupScript(
                             this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-Periodic-summarized-Performace-Bill.aspx','_newtab');", true);
                }
            }
        }

    }
}