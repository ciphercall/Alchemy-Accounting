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

namespace AlchemyAccounting.Accounts.Report.UI
{
    public partial class RptNotesAcc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtFrom.Text = td;
                txtTo.Text = td;
            }
        }

        protected void ddlLevelID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ddlLevelID"] = "";
            if (ddlLevelID.Text == "1")
            {
                Session["ddlLevelID"] = ddlLevelID.Text;
                txtHeadNM.Text = "";
                txtHeadNM.Focus();
                lblAccHeadCD.Text = "";
                Session["LevelCD"] = "";
                Session["AccNM"] = "";
            }
            else if (ddlLevelID.Text == "2")
            {
                Session["ddlLevelID"] = ddlLevelID.Text;
                txtHeadNM.Text = "";
                txtHeadNM.Focus();
                lblAccHeadCD.Text = "";
                Session["LevelCD"] = "";
                Session["AccNM"] = "";
            }
            else if (ddlLevelID.Text == "3")
            {
                Session["ddlLevelID"] = ddlLevelID.Text;
                txtHeadNM.Text = "";
                txtHeadNM.Focus();
                lblAccHeadCD.Text = "";
                Session["LevelCD"] = "";
                Session["AccNM"] = "";
            }
            else if (ddlLevelID.Text == "4")
            {
                Session["ddlLevelID"] = ddlLevelID.Text;
                txtHeadNM.Text = "";
                txtHeadNM.Focus();
                lblAccHeadCD.Text = "";
                Session["LevelCD"] = "";
                Session["AccNM"] = "";
            }
            else
            {
                return;
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string ddlLevel = HttpContext.Current.Session["ddlLevelID"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            if (ddlLevel == "1")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT (ACCOUNTNM+' (L - '+convert(nvarchar(10),LEVELCD,103)+')')as ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD like '1%' and LEVELCD between 1 and 4 and ACCOUNTNM like '" + prefixText + "%'");
            }

            else if (ddlLevel == "2")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT (ACCOUNTNM+' (L - '+convert(nvarchar(10),LEVELCD,103)+')')as ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD like '2%' and LEVELCD between 1 and 4  and ACCOUNTNM like '" + prefixText + "%'");
            }
            else if (ddlLevel == "3")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT (ACCOUNTNM+' (L - '+convert(nvarchar(10),LEVELCD,103)+')')as ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD like '3%'  and LEVELCD between 1 and 4  and ACCOUNTNM like '" + prefixText + "%'");
            }
            else if (ddlLevel == "4")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT (ACCOUNTNM+' (L - '+convert(nvarchar(10),LEVELCD,103)+')')as ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD like '4%'  and LEVELCD between 1 and 4  and ACCOUNTNM like '" + prefixText + "%'");
            }
            else
            {
                ddlLevel = "";
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtHeadNM_TextChanged(object sender, EventArgs e)
        {
            Session["LevelCD"] = "";
            Session["AccNM"] = "";
            if (txtHeadNM.Text != "")
            {
                string txtHDNM = txtHeadNM.Text;
                string trimHDNM = txtHDNM.Substring(0, txtHDNM.Length - 8);
                int Lvl = txtHDNM.LastIndexOf(" (");
                string l = txtHDNM.Substring(Lvl);
                string Level = l.Substring(6,1);

                Session["AccNM"] = trimHDNM;
                Session["LevelCD"] = Level;

                lblAccHeadCD.Text = "";

                Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where LEVELCD= '" + Level + "' and ACCOUNTNM = '" + trimHDNM + "'", lblAccHeadCD);
            }
            else
                txtHeadNM.Text = "";
            txtHeadNM.Focus();

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
            if (ddlLevelID.Text == "SELECT") 
            {
                Response.Write("<script>alert('Select Transaction Type.');</script>");
                ddlLevelID.Focus();
            }
            else if(txtHeadNM.Text=="")
            {
                Response.Write("<script>alert('Select Account Head.');</script>");
                txtHeadNM.Focus();
            }
            else if(txtFrom.Text == "")
            {
                Response.Write("<script>alert('Select From Date.');</script>");
            }
            else if (txtTo.Text == "")
            {
                Response.Write("<script>alert('Select To Date.');</script>");
            }
            else
            {
                Session["TransLevel"]= ddlLevelID.Text;
                Session["AccCode"] = lblAccHeadCD.Text;
                string AccNM = Session["AccNM"].ToString();
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;
                string LevelCD = Session["LevelCD"].ToString();

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptNotesAccount.aspx','_newtab');", true);
            }
        }
    }
}