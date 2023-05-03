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

namespace AlchemyAccounting.Stock.UI
{
    public partial class EditPSEntry : System.Web.UI.Page
    {
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
                    ddlPSTP.AutoPostBack = true;
                    ddlPSTP.Focus();
                }
            }
        }

        protected void ddlPSTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PSTP"] = "";
            if (ddlPSTP.Text == "P")
            {
                Session["PSTP"] = "P";
                ShowGrid();
            }
            else if (ddlPSTP.Text == "S")
            {
                Session["PSTP"] = "S";
                ShowGrid();
            }
        }

        /// <summary>
        /// AutoComplete
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <param name="contextKey"></param>
        /// <returns></returns>

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string PSTP = HttpContext.Current.Session["PSTP"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            if (PSTP == "P")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202') and ACCOUNTNM like '" + prefixText + "%' AND STATUSCD = 'P'");
            }

            else if (PSTP == "S")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) in ('20202') and ACCOUNTNM like '" + prefixText + "%' AND STATUSCD = 'P'");
            }
            else
            {
                PSTP = "";
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();

        }
        public void ShowGrid()
        {
            //lblDebitCD.Text = "";
            //lblCreditCD.Text = "";

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT GL_ACCHART.ACCOUNTNM, STK_PS.CITY, STK_PS.ADDRESS, STK_PS.CONTACTNO, STK_PS.EMAIL, STK_PS.WEBID, STK_PS.CPNM, STK_PS.CPNO, " +
                                            " STK_PS.REMARKS, STK_PS.STATUS,STK_PS.PS_ID FROM GL_ACCHART INNER JOIN STK_PS ON GL_ACCHART.ACCOUNTCD = STK_PS.PSID where STK_PS.PSTP = '" + ddlPSTP.Text + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;
            }
            else
            {
                Response.Write("<script>alert('No Data Found');</script>");
                gvDetails.Visible = false;
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            ShowGrid();
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            if (userName == "")
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                Label lblPS_ID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblPS_ID");

                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from STK_PS where PSTP = '" + ddlPSTP.Text + "' and PS_ID = '" + lblPS_ID.Text + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("<script>alert('Successfully Deleted');</script>");
                ShowGrid();
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            ShowGrid();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            if (userName == "")
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                TextBox txtPSNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtPSNMEdit");
                TextBox txtCityEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCityEdit");

                TextBox txtAddressEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAddressEdit");

                TextBox txtContEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtContEdit");
                TextBox txtEmailEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtEmailEdit");
                TextBox txtWebEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtWebEdit");
                TextBox txtCPNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCPNMEdit");
                TextBox txtCPNOEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCPNOEdit");
                TextBox txtRemarksEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");
                DropDownList ddlStatus = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlStatus");
                Label lblPS_IDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblPS_IDEdit");

                Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where ACCOUNTNM like '" + txtPSNMEdit.Text + "%'", lblPSID);

                if (lblPSID.Text != "")
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update STK_PS set PSID='" + lblPSID.Text + "', CITY = '" + txtCityEdit.Text + "', ADDRESS= '" + txtAddressEdit.Text + "', CONTACTNO ='" + txtContEdit.Text + "', EMAIL = '" + txtEmailEdit.Text + "' ,WEBID = '" + txtWebEdit.Text + "',CPNM = '" + txtCPNMEdit.Text + "', CPNO = '" + txtCPNOEdit.Text + "', REMARKS = '" + txtRemarksEdit.Text + "', STATUS = '" + ddlStatus.Text + "', USERID = '" + userName + "' where PSTP = '" + ddlPSTP.Text + "' and PS_ID = '" + lblPS_IDEdit.Text + "' ", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Write("<script>alert('Successfully Updated');</script>");
                    gvDetails.EditIndex = -1;
                    ShowGrid();
                }
                else
                {
                    Response.Write("<script>alert('Select Party or Suplliar Name');</script>");
                }
            }
        }

        protected void txtPSNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox other = (TextBox)row.FindControl("txtPSNMEdit");

            Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where ACCOUNTNM like '" + other.Text + "%'", lblPSID);
        }


    }
}