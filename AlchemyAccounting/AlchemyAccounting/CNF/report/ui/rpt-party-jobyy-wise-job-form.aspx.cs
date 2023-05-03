using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AlchemyAccounting.CNF.report.ui
{
    public partial class rpt_party_jobyy_wise_job_form : System.Web.UI.Page
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
                    txtJobYY.Text = td.ToString("yyyy");
                    txtparty.Focus();
                    Session["partyid"] = "";
                    Session["year"] = "";
                    Session["partyname"] = "";

                }
            }
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
            txtJobYY.Focus();
        }

        protected void txtJobYY_TextChanged(object sender, EventArgs e)
        {
            btnSubmit.Focus();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (txtpatyID.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Insert party name";
                txtparty.Focus();
            }
            else if (txtJobYY.Text == "")
            {
                lblErrmsg.Visible = true ;
                lblErrmsg.Text = "Insert Year";
                txtJobYY.Focus();
            }
            else
            {
                lblErrmsg.Visible = false;
                Session["partyid"] = txtpatyID.Text;
                Session["year"] = txtJobYY.Text;
                Session["partyname"] = txtparty.Text;

                Page.ClientScript.RegisterStartupScript(
                           this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-party-jobyy-wise-job.aspx','_newtab');", true);
            }
        }
    }
}