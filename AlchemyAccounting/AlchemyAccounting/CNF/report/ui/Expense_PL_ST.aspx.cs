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
    public partial class Expense_PL_ST : System.Web.UI.Page
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
                    DateTime td = Global.Timezone(DateTime.Now);
                    txtFromDate.Text = td.ToString("dd/MM/yyyy");
                    txtToDate.Text = td.ToString("dd/MM/yyyy");
                    txtExpenseNM.Focus();
                }
            }
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            txtToDate.Focus();

        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            txtExpenseNM.Focus();
        }

        protected void txtExpenseNM_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(" select EXPID from CNF_EXPENSE where EXPNM='" + txtExpenseNM.Text + "' ", txtExpenseID);
            btnSubmit.Focus();
        }


        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListExpenseID(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            cmd.CommandType = CommandType.Text;
            if (uTp == "ADMIN")
            {
                cmd.CommandText = ("SELECT EXPNM FROM CNF_EXPENSE WHERE EXPNM LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT EXPNM FROM CNF_EXPENSE WHERE EXPNM LIKE '" + prefixText + "%' ");
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EXPNM"].ToString());
            return CompletionSet.ToArray();

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Session["fromdate"] = txtFromDate.Text;
            Session["todate"] = txtToDate.Text;
            Session["expenseID"] = txtExpenseID.Text;

            Page.ClientScript.RegisterStartupScript(
                      this.GetType(), "OpenWindow", "window.open('../vis-rep/RptExpense_PL_ST.aspx','_newtab');", true);

        }

    }
}