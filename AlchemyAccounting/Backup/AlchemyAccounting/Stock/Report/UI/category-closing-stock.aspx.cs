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
    public partial class category_closing_stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
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
                    txtCategoryNM.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT CATNM FROM STK_ITEMMST WHERE CATNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["CATNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtCategoryNM_TextChanged(object sender, EventArgs e)
        {
            if (txtCategoryNM.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select Category.";
                txtCategoryNM.Focus();
            }
            else
            {
                Global.lblAdd("SELECT CATID FROM STK_ITEMMST WHERE CATNM ='" + txtCategoryNM.Text + "'", lblCatID);
                if (lblCatID.Text == "")
                {
                    txtCategoryNM.Text = "";
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "Select Category.";
                    txtCategoryNM.Focus();
                }
                else
                {
                    lblErrMsg.Visible = false;
                    btnSearch.Focus();
                }
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtDate.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select Date.";
                txtCategoryNM.Focus();
            }
            else if (txtCategoryNM.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Select Category.";
                txtCategoryNM.Focus();
            }
            else
            {
                lblErrMsg.Visible = false;

                Session["catNM"] = txtCategoryNM.Text;
                Session["cateID"] = lblCatID.Text;
                Session["Date"] = txtDate.Text;

                Page.ClientScript.RegisterStartupScript(
                    this.GetType(), "OpenWindow", "window.open('../Report/rpt-category-closing-stock.aspx','_newtab');", true);
            }
        }
    }
}