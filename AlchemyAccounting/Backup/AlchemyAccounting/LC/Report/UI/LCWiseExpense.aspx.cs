using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using AlchemyAccounting;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace AlchemyAccounting.LC.Report.UI
{
    public partial class LCWiseExpense : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtLCID.Focus();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListLC(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('4010103','2020103') AND STATUSCD='P' AND ACCOUNTNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtLCID_TextChanged(object sender, EventArgs e)
        {
            lblLCCD.Text = "";
            Global.lblAdd(@"SELECT ACCOUNTCD FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('4010103','2020103') AND STATUSCD='P' AND ACCOUNTNM = '" + txtLCID.Text + "'", lblLCCD);
            btnSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (lblLCCD.Text == "")
            {
                Response.Write("<script>alert('Select L/C ID');</script>");
            }
            else
            {
                Session["LCCD"] = lblLCCD.Text;
                Session["LCID"] = txtLCID.Text;
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptLCWiseExpense.aspx','_newtab');", true);
                //Response.Redirect("~/Accounts/Report/Report/ReportLedgerBook.aspx");
            }
        }
    }
}