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
    public partial class RptItemRegister : System.Web.UI.Page
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

                    Global.dropDownAdd(ddlStore, "Select STORENM FROM STK_STORE");
                }
                Global.lblAdd(@"Select STOREID FROM STK_STORE where STORENM = '" + ddlStore.Text + "'", lblStoreID);
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

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.lblAdd(@"Select STOREID FROM STK_STORE where STORENM = '" + ddlStore.Text + "'", lblStoreID);
            txtItemNM.Text = "";
            txtItemNM.Focus();
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
            
            else if(FrDT > ToDT)
            {
                Response.Write("<script>alert('From Date is Greater than To Date.');</script>");
                btnSearch.Focus();
            }
            else
            {
                Session["StoreNm"] = ddlStore.Text;
                Session["StoreID"] = lblStoreID.Text;
                Session["ItemNM"] = txtItemNM.Text;
                Session["ItemID"] = lblItemCD.Text; 
                Session["From"] = txtFrom.Text;
                Session["To"] = txtTo.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rptItemRegister.aspx','_newtab');", true);
            }
        }
    }
}