using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AlchemyAccounting.CNF.report.ui
{
    public partial class ExpenseRegisterForm : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn;
        SqlCommand cmdd;

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
                }
            }
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            txtExpenseNM.Focus();

        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            txtExpenseNM.Focus();
        }

        protected void txtExpenseNM_TextChanged(object sender, EventArgs e)
        {

            if (txtExpenseNM.Text == "")
            {
                txtExpenseID.Text = "";
            }
            else
            {
                Global.txtAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM='" + txtExpenseNM.Text + "' AND STATUSCD ='P'", txtExpenseID);
            }

            btnSubmit.Focus();
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
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) IN ('1020101', '1020301') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P'", conn);
            }
            else
            {
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) IN ('1020101', '1020301') and ACCOUNTNM LIKE '" + prefixText + "%' AND STATUSCD = 'P' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')", conn);
            }
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtExpenseNM.Text == "" || txtExpenseID.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Input Date Missing.";
            }

            else
            {
                Session["fromdate"] = txtFromDate.Text;
                Session["todate"] = txtToDate.Text;
                Session["expenseID"] = txtExpenseID.Text;

                Page.ClientScript.RegisterStartupScript(
                          this.GetType(), "OpenWindow", "window.open('../vis-rep/ExpenseRegisterDetails.aspx','_newtab');", true);
                
                lblErrmsg.Text = "";
                lblErrmsg.Visible = false;

            }
        }
    }
}