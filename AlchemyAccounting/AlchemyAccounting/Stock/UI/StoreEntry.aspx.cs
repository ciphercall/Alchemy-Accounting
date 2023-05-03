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
    public partial class StoreEntry : System.Web.UI.Page
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
                    BindEmployeeDetails();
                    lblMaxStID.Visible = false;
                    lblSTID.Visible = false;
                }
            }
        }

        protected void BindEmployeeDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from STK_STORE", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                TextBox txtSTNM = (TextBox)gvDetails.FooterRow.FindControl("txtSTNM");
                txtSTNM.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                gvDetails.Rows[0].Cells.Clear();
                gvDetails.Rows[0].Cells.Add(new TableCell());
                gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                TextBox txtSTNM = (TextBox)gvDetails.FooterRow.FindControl("txtSTNM");
                txtSTNM.Focus();
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindEmployeeDetails();
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);



            if (e.CommandName.Equals("AddNew"))
            {
                string STID;
                STID = gvDetails.FooterRow.Cells[0].Text;
                TextBox txtSTNM = (TextBox)gvDetails.FooterRow.FindControl("txtSTNM");
                TextBox txtAddress = (TextBox)gvDetails.FooterRow.FindControl("txtAddress");
                TextBox txtContact = (TextBox)gvDetails.FooterRow.FindControl("txtContact");
                TextBox txtRemarks = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");

                query = ("insert into STK_STORE ( STOREID, STORENM, ADDRESS, CONTACTNO, REMARKS, USERPC, USERID, IPADDRESS) " +
                         "values(@STID,'" + txtSTNM.Text + "','" + txtAddress.Text + "','" + txtContact.Text + "','" + txtRemarks.Text + "','',@USERID,'')");

                comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@STID", STID);
                comm.Parameters.AddWithValue("@USERID", userName);

                conn.Open();
                int result = comm.ExecuteNonQuery();
                conn.Close();
                BindEmployeeDetails();
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            Label lblSTID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSTID");

            conn.Open();
            SqlCommand cmd = new SqlCommand("delete FROM STK_STORE where STOREID = '" + lblSTID.Text + "'", conn);
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            if (result == 1)
            {
                BindEmployeeDetails();
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindEmployeeDetails();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            Label lblSTID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSTID");
            TextBox txtSTNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtSTNMEdit");
            TextBox txtAddressEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAddressEdit");
            TextBox txtContactEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtContactEdit");
            TextBox txtRemarksEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");

            conn.Open();
            SqlCommand cmd = new SqlCommand("update STK_STORE set STORENM='" + txtSTNMEdit.Text + "', ADDRESS='" + txtAddressEdit.Text + "', CONTACTNO='" + txtContactEdit.Text + "', REMARKS = '" + txtRemarksEdit.Text + "', USERID = '" + userName + "' where STOREID = '" + lblSTID.Text + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvDetails.EditIndex = -1;
            BindEmployeeDetails();
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Global.lblAdd(@"select MAX(STOREID) from STK_STORE", lblMaxStID);

                if (lblMaxStID.Text == "")
                {
                    lblSTID.Text = "S01";
                }
                else
                {
                    string MaxSTID = lblMaxStID.Text;
                    string STId = MaxSTID.Substring(1, 2);
                    string mid_v, C_ID;
                    int ID = int.Parse(STId);
                    int CID = ID + 1;
                    if (CID < 10)
                    {
                        mid_v = CID.ToString();
                        C_ID = "0" + mid_v;
                    }
                    else
                        C_ID = CID.ToString();
                    string FID = "S" + C_ID.ToString();
                    lblSTID.Text = FID;
                }
                e.Row.Cells[0].Text = lblSTID.Text;
            }
        }
    }
}