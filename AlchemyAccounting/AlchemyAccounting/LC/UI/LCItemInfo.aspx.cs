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
using System.Drawing;

namespace AlchemyAccounting.LC.UI
{
    public partial class LCItemInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
            }
            else
                txtLCName.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetLCListame(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM GL_ACCHART WHERE ACCOUNTCD LIKE '4010103%' AND STATUSCD='P' AND ACCOUNTNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtLCName_TextChanged(object sender, EventArgs e)
        {
            Global.txtAdd(@"SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTCD LIKE '4010103%' AND STATUSCD='P' AND ACCOUNTNM = '" + txtLCName.Text + "'", txtLCCD);
            btnSubmit.Focus();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtLCCD.Text == "")
            {
                Response.Write("<script>alert('Select L/C ID?');</script>");
                txtLCName.Focus();
            }
            else
            {
                GridLcItemShow();
                gvDetails.Visible = true;
            }
        }

        protected void GridLcItemShow()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            
            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT LC_ITEM.LCID, LC_ITEM.ITEMID, LC_ITEM.QTY, LC_ITEM.RATE, LC_ITEM.AMOUNT, LC_ITEM.REMARKS, LC_ITEM.SL, LC_ITEM.USERPC, LC_ITEM.USERID, " +
                                            " LC_ITEM.INTIME, LC_ITEM.IPADDRESS, STK_ITEM.ITEMNM FROM LC_ITEM INNER JOIN STK_ITEM ON LC_ITEM.ITEMID = STK_ITEM.ITEMID WHERE LC_ITEM.LCID ='" + txtLCCD.Text + "' ORDER BY LC_ITEM.SL", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();

                TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
                txtItemNM.Focus();
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
                gvDetails.Rows[0].Visible = false;

                TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
                txtItemNM.Focus();
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetItemName(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(" SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'", conn);
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
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
            TextBox txtItemID = (TextBox)gvDetails.FooterRow.FindControl("txtItemID");
            TextBox txtQty = (TextBox)gvDetails.FooterRow.FindControl("txtQty");
            Global.txtAdd(@" SELECT ITEMID FROM STK_ITEM WHERE ITEMNM = '" + txtItemNM.Text + "'", txtItemID);

            txtQty.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetItemNameEdit(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(" SELECT ITEMNM FROM STK_ITEM WHERE ITEMNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ITEMNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtItemNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtItemNMEdit = (TextBox)row.FindControl("txtItemNMEdit");
            TextBox txtItemIDEdit = (TextBox)row.FindControl("txtItemIDEdit");
            TextBox txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            Global.txtAdd(@" SELECT ITEMID FROM STK_ITEM WHERE ITEMNM = '" + txtItemNMEdit.Text + "'", txtItemIDEdit);

            txtQtyEdit.Focus();
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtQty = (TextBox)row.FindControl("txtQty");
            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

            decimal qty = 0;
            decimal rate = 0;

            if (txtQty.Text == "")
                qty = 0;
            else
                qty = Convert.ToDecimal(txtQty.Text);
            if (txtRate.Text == ".00" || txtRate.Text == "")
            {
                rate = 0;
                txtAmount.Text = ".00";
            }
            else
            {
                rate = Convert.ToDecimal(txtRate.Text);
                decimal amt = qty*rate;
                txtAmount.Text = amt.ToString();
            }

            txtRemarks.Focus();
        }

        protected void txtRateEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtQtyEdit = (TextBox)row.FindControl("txtQtyEdit");
            TextBox txtRateEdit = (TextBox)row.FindControl("txtRateEdit");
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");
            TextBox txtRemarksEdit = (TextBox)row.FindControl("txtRemarksEdit");

            decimal qty = 0;
            decimal rate = 0;

            if (txtQtyEdit.Text == "")
                qty = 0;
            else
                qty = Convert.ToDecimal(txtQtyEdit.Text);
            if (txtRateEdit.Text == ".00" || txtRateEdit.Text =="")
            {
                rate = 0;
                txtAmountEdit.Text = ".00";
            }
            else
            {
                rate = Convert.ToDecimal(txtRateEdit.Text);
                decimal amt = qty * rate;
                txtAmountEdit.Text = amt.ToString();
            }

            txtRemarksEdit.Focus();
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            GridLcItemShow();
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            if (e.CommandName.Equals("AddNew"))
            {
                TextBox txtItemNM = (TextBox)gvDetails.FooterRow.FindControl("txtItemNM");
                TextBox txtItemID = (TextBox)gvDetails.FooterRow.FindControl("txtItemID");
                TextBox txtQty = (TextBox)gvDetails.FooterRow.FindControl("txtQty");
                TextBox txtRate = (TextBox)gvDetails.FooterRow.FindControl("txtRate");
                if (txtRate.Text == "" || txtRate.Text == ".00")
                {
                    txtRate.Text = "0";
                }
                else
                    txtRate.Text = txtRate.Text;
                TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");
                TextBox txtRemarks = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");
                string remarks = txtRemarks.Text;

                string s = "";
                SqlTransaction tran = null;

                if (txtLCCD.Text == "")
                {
                    Response.Write("<script>alert('Select L/C ID.');</script>");
                    txtLCName.Focus();
                }
                else if (txtItemID.Text == "")
                {
                    Response.Write("<script>alert('Select Item Name.');</script>");
                    txtItemNM.Focus();
                }
                else if (txtQty.Text == "")
                {
                    Response.Write("<script>alert('Type Quatity.');</script>");
                    txtQty.Focus();
                }
                //else if (txtAmount.Text == "")
                //{
                //    Response.Write("<script>alert('Type Amount.');</script>");
                //    txtAmount.Focus();
                //}
                else
                {
                    try
                    {
                        conn.Open();
                        tran = conn.BeginTransaction();
                        SqlCommand cmd = new SqlCommand(" INSERT INTO LC_ITEM (LCID, ITEMID, QTY, RATE, AMOUNT, REMARKS, USERPC, USERID, IPADDRESS) " +
                                                        " VALUES  ('" + txtLCCD.Text + "','" + txtItemID.Text + "'," + txtQty.Text + "," + txtRate.Text + "," + txtAmount.Text + ",@Remarks,'" + PCName + "','" + userName + "','" + ipAddress + "')",conn);

                        cmd.Parameters.AddWithValue("@Remarks",remarks);

                        
                        cmd.Transaction = tran;
                        int result = cmd.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        s = ex.Message;
                    }

                    //gvDetails.EditIndex = -1;

                    GridLcItemShow();

                    txtItemNM.Focus();
                }
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

            string s = "";
            SqlTransaction tran = null;

            Label lblItemSl = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblItemSl");

            try
            {
                query = (" DELETE FROM LC_ITEM WHERE LCID = '" + txtLCCD.Text + "' AND SL = " + lblItemSl.Text + " ");

                comm = new SqlCommand(query, conn);

                conn.Open();
                tran = conn.BeginTransaction();
                comm.Transaction = tran;
                int result = comm.ExecuteNonQuery();
                tran.Commit();
                conn.Close();

                GridLcItemShow();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            GridLcItemShow();

            TextBox txtItemNMEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtItemNMEdit");
            txtItemNMEdit.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            TextBox txtItemNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtItemNMEdit");
            TextBox txtItemIDEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtItemIDEdit");
            TextBox txtQtyEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtQtyEdit");
            TextBox txtRateEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRateEdit");
            if (txtRateEdit.Text == "" || txtRateEdit.Text == ".00")
            {
                txtRateEdit.Text = "0";
            }
            else
                txtRateEdit.Text = txtRateEdit.Text;
          
            TextBox txtAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAmountEdit");
            TextBox txtRemarksEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");
            string remarks = txtRemarksEdit.Text;
            Label lblItemSLEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblItemSLEdit");

            string s = "";
            SqlTransaction tran = null;

            if (txtItemIDEdit.Text == "")
            {
                Response.Write("<script>alert('Select Item Name.');</script>");
                txtItemNMEdit.Focus();
            }
            else if (txtQtyEdit.Text == "")
            {
                Response.Write("<script>alert('Type Qty.');</script>");
                txtQtyEdit.Focus();
            }
            //else if (txtAmountEdit.Text == "" || txtAmountEdit.Text == ".00")
            //{
            //    Response.Write("<script>alert('Type Amount.');</script>");
            //    txtAmountEdit.Focus();
            //}
            else
            {
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand(" UPDATE LC_ITEM SET ITEMID = '" + txtItemIDEdit.Text + "', QTY = " + txtQtyEdit.Text + ", RATE = " + txtRateEdit.Text + ", AMOUNT = " + txtAmountEdit.Text + ", REMARKS = @Remarks, USERPC = '" + PCName + "', USERID = '" + userName + "', IPADDRESS = '" + ipAddress + "' " +
                                                    " WHERE LCID = '" + txtLCCD.Text + "' AND SL = '" + lblItemSLEdit.Text + "' ", conn);

                    cmd.Parameters.AddWithValue("@Remarks", remarks);


                    cmd.Transaction = tran;
                    int result = cmd.ExecuteNonQuery();
                    tran.Commit();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }

                gvDetails.EditIndex = -1;
                GridLcItemShow();
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}