using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AlchemyAccounting.CNF.report.ui
{
    public partial class RptExportInfoForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    Session["companyNM"] = "";
                    Session["companyID"] = "";
                    Session["billFwdate"] = "";
                    Session["branch"] = "";
                    Session["partyNM"] = "";
                    Session["partyID"] = "";
                    Session["remarks"] = "";

                    DateTime td = Global.Timezone(DateTime.Now);
                    txtFWdate.Text = td.ToString("dd/MM/yyyy");
                    Global.txtAdd("SELECT  (COMPNM +'|'+ BRANCHID) AS CB FROM ASL_COMPANY where BRANCHID='DHAKA'", txtBranchID);
                    txtBranchID_TextChanged(txtBranchID, e);
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
                cmd = new SqlCommand("SELECT  (COMPNM +'|'+ BRANCHID) AS CB FROM ASL_COMPANY WHERE COMPNM LIKE '" + prefixText + "%' ", conn);
            }
            else
            {
                cmd = new SqlCommand("SELECT (COMPNM +'|'+ BRANCHID) AS CB FROM ASL_COMPANY WHERE COMPNM LIKE '" + prefixText + "%' ", conn);
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



        protected void txtBranchID_TextChanged(object sender, EventArgs e)
        {
            string companyName = "";
            string branch = "";

            string searchPar = txtBranchID.Text;

            int splitter = searchPar.IndexOf("|");
            if (splitter != -1)
            {
                string[] lineSplit = searchPar.Split('|');

                companyName = lineSplit[0];
                branch = lineSplit[1];


                txtBranchID.Text = companyName.Trim();
                lblbranch.Text = branch.Trim();

            }


            if (txtBranchID.Text == "")
            {
                lblbranchtag.Visible = false;
                Label1.Visible = false;
                lblbranch.Visible = false;
                txtBranchID.Focus();
            }
            else
            {

                lblbranchtag.Visible = true;
                Label1.Visible = true;
                lblbranch.Visible = true;

                Global.lblAdd("select COMPID from ASL_COMPANY where COMPNM='" + txtBranchID.Text + "' and BRANCHID='" + lblbranch.Text + "' ", lblCompanyID);
                txtFWdate.Focus();
            }

        }

        protected void txtFWdate_TextChanged(object sender, EventArgs e)
        {
            txtPartyID.Focus();
        }

        protected void txtPartyID_TextChanged(object sender, EventArgs e)
        {

            Global.lblAdd("select ACCOUNTCD from GL_ACCHART where ACCOUNTNM= '" + txtPartyID.Text + "' ", lblPartyID);
            txtRemarks.Focus();
        }

        protected void txtRemarks_TextChanged(object sender, EventArgs e)
        {
            btnSubmit.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Session["companyNM"] = txtBranchID.Text;
            Session["companyID"] = lblCompanyID.Text;
            Session["billFwdate"] = txtFWdate.Text;
            Session["branch"] = lblbranch.Text;
            Session["partyNM"] = txtPartyID.Text;
            Session["partyID"] = lblPartyID.Text;
            Session["remarks"] = txtRemarks.Text;

            if (txtBranchID.Text == "")
            {

                lblErrmsg.Text = "";
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Office Name missing.";
                txtBranchID.Focus();
            }

            else if (txtPartyID.Text == "")
            {
                lblErrmsg.Text = "";
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Party Name missing.";
                txtPartyID.Focus();
            }

            //else if (txtRemarks.Text == "")
            //{
            //    lblErrmsg.Text = "";
            //    lblErrmsg.Visible = true;
            //    lblErrmsg.Text = "Remarks missing.";
            //    txtPartyID.Focus();
            //}

            else
            {
                lblErrmsg.Text = "";
                lblErrmsg.Visible = false;
                Page.ClientScript.RegisterStartupScript(
                        this.GetType(), "OpenWindow", "window.open('../vis-rep/RptExportInfo.aspx','_newtab');", true);
            }

        }

    }
}