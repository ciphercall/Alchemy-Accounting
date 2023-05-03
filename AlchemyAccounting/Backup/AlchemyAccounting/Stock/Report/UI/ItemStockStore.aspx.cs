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
    public partial class ItemStockStore : System.Web.UI.Page
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
                    txtDate.Text = td;

                    txtItemNM.Focus();
                }
                txtItemNM.Focus();
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ("SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'");

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ITEMNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtItemNM_TextChanged(object sender, EventArgs e)
        {
            if (txtItemNM.Text != "")
            {
                Global.lblAdd(@"Select ITEMID FROM STK_ITEM where ITEMNM = '" + txtItemNM.Text + "'", lblItemCD);
            }
            else
                txtItemNM.Text = "";

            btnSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime FrDT = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            if (txtItemNM.Text == "")
            {
                Response.Write("<script>alert('Select an Item.');</script>");
                txtItemNM.Focus();
            }
            else if (lblItemCD.Text == "")
            {
                Response.Write("<script>alert('Select Proper Item.');</script>");
                txtItemNM.Focus();
            }

            else
            {
                Session["ItemNM"] = txtItemNM.Text;
                Session["ItemID"] = lblItemCD.Text;
                Session["Date"] = txtDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptItemStockStore.aspx','_newtab');", true);
            }
        }

        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }
    }
}