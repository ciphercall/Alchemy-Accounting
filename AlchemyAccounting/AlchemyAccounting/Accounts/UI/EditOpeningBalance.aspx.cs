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
using AlchemyAccounting.Accounts.DataAccess;
using AlchemyAccounting.Accounts.Interface;
namespace AlchemyAccounting.Accounts.UI
{
    public partial class EditOpeningBalance : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowGrid();    
            }
        }
        public void ShowGrid()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT dbo.GL_MASTER.TRANSNO, convert(nvarchar(20),dbo.GL_MASTER.TRANSDT,103) as TRANSDT, dbo.GL_ACCHART.ACCOUNTNM, dbo.GL_MASTER.DEBITAMT, dbo.GL_MASTER.CREDITAMT " +
                                           " FROM dbo.GL_MASTER INNER JOIN dbo.GL_ACCHART ON dbo.GL_MASTER.DEBITCD = dbo.GL_ACCHART.ACCOUNTCD WHERE (dbo.GL_MASTER.TRANSTP = 'OPEN') order by TRANSNO", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
            else
            {
                Response.Write("<script>alert('No Data Found');</script>");
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtAccNM_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            //NamingContainer return the container that the control sits in
            TextBox other = (TextBox)row.FindControl("txtAccNM");

            Global.lblAdd(@"Select ACCOUNTCD from GL_ACCHART where ACCOUNTNM like '" + other.Text + "%'", lblDebitCD);
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

            Label lblDocNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblVouchNo");
            Label lblOpenDT = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblDate");
            DateTime OpenDT = DateTime.Parse(lblOpenDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from GL_MASTER where TRANSTP = 'OPEN' and TRANSDT = '" + OpenDT + "' and TRANSNO = '" + lblDocNo.Text + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvDetails.EditIndex = -1;
            ShowGrid();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            Label lblDocNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblDocNo");
            Label lblOpenDT = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblOpenDT");
            DateTime OpenDT = DateTime.Parse(lblOpenDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            TextBox txtAccNM = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAccNM");
            TextBox txtDebitAmnt = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtDebitAmnt");
            TextBox txtCreditAmt = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCreditAmt");
            decimal dbAmt;
            if (txtDebitAmnt.Text == "")
            {
                 dbAmt = 0;
            }
            else
                dbAmt = Convert.ToDecimal(txtDebitAmnt.Text);

            decimal cdAmt; 
            if(txtCreditAmt.Text=="")
            {
                cdAmt=0;
            }
            else
                cdAmt = Convert.ToDecimal(txtCreditAmt.Text);
            if (dbAmt > 0 && cdAmt>0)
            {
                Response.Write("<script>alert('Fill Either Debit Amount or Credit.');</script>");
            }
            else
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("update GL_MASTER set DEBITCD='" + lblDebitCD.Text + "', DEBITAMT = '" + txtDebitAmnt.Text + "' , CREDITAMT= '" + txtCreditAmt.Text + "'  where TRANSTP = 'OPEN' and TRANSDT = '" + OpenDT + "' and TRANSNO = '" + lblDocNo.Text + "'", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("<script>alert('Successfully Updated');</script>");
                gvDetails.EditIndex = -1;
                ShowGrid();
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            ShowGrid();
        }


    }
}