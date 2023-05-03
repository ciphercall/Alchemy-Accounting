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

namespace AlchemyAccounting.Stock.Report.UI
{
    public partial class PartySumStatement : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else if (Session["UserName"].ToString() == "")
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    DateTime today = DateTime.Today.Date;
                    string td = Global.Dayformat(today);
                    txtFrom.Text = td;
                    txtTo.Text = td;
                    Session["ddlType"] = ddlType.Text;
                }
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ddlType"] = "";
            if (ddlType.Text == "SALE")
            {
                Session["ddlType"] = ddlType.Text;
            }
            else if (ddlType.Text == "BUY")
            {
                Session["ddlType"] = ddlType.Text;
            }

            txtPartyNM.Text = "";
            txtPartyNM.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string ddlType = HttpContext.Current.Session["ddlType"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            if (ddlType == "SALE")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE SUBSTRING(ACCOUNTCD,1,5) = '10202' AND LEVELCD=5 AND ACCOUNTNM LIKE '" + prefixText + "%'");
            }

            else if (ddlType == "BUY")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE SUBSTRING(ACCOUNTCD,1,5) = '20202' AND LEVELCD=5 AND ACCOUNTNM LIKE '" + prefixText + "%'");
            }

            else
                ddlType = "";

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtPartyNM_TextChanged(object sender, EventArgs e)
        {
            if (txtPartyNM.Text != "")
            {
                Global.lblAdd(@"Select ACCOUNTCD FROM GL_ACCHART where ACCOUNTNM = '" + txtPartyNM.Text + "'", lblPartyCD);
            }
            else
                txtPartyNM.Text = "";

            btnSearch.Focus();
        }

        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime FrDT = DateTime.Parse(txtFrom.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            DateTime ToDT = DateTime.Parse(txtTo.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            if (txtPartyNM.Text == "")
            {
                Response.Write("<script>alert('Select Party or Supplier.');</script>");
                txtPartyNM.Focus();
            }
            else if (lblPartyCD.Text == "")
            {
                Response.Write("<script>alert('Select Proper Party or Supplier.');</script>");
                txtPartyNM.Focus();
            }

            else if (FrDT > ToDT)
            {
                Response.Write("<script>alert('From Date is Greater than To Date.');</script>");
                btnSearch.Focus();
            }
            else
            {
                Session["Type"] = ddlType.Text;
                Session["PS"] = txtPartyNM.Text;
                Session["PSCODE"] = lblPartyCD.Text;
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptPartySumStatement.aspx','_newtab');", true);
            }
        }
    }
}