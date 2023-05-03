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
    public partial class rpt_jobtypw_wise_form : System.Web.UI.Page
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
                    ddljobtype.Focus();
                    Session["jobtype"] = "";
                    Session["year"] = "";

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

    

        protected void txtJobYY_TextChanged(object sender, EventArgs e)
        {
            btnSubmit.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtJobYY.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Year Missing";
                txtJobYY.Focus();
            }
            else
            {
                lblErrmsg.Visible = false;
                Session["jobtype"] = ddljobtype.Text;
                Session["year"] = txtJobYY.Text;

                Page.ClientScript.RegisterStartupScript(
                            this.GetType(), "OpenWindow", "window.open('../vis-rep/rpt-jobtypw-wise.aspx','_newtab');", true);
            }
        }
        protected void ddljobtype_TextChanged(object sender, EventArgs e)
        {
            txtJobYY.Focus();
        }

      
    }
}