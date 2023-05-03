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

namespace AlchemyAccounting.LC.UI
{
    public partial class LCChargeInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
            }
            else
            {
                BindLcChargeData();
            }
        }

        protected void BindLcChargeData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from LC_CHARGE ORDER BY CHARGEID", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                DropDownList ddlType = (DropDownList)gvDetails.FooterRow.FindControl("ddlType");
                ddlType.Focus();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                int columncount = gvDetails.Rows[0].Cells.Count;
                //gvDetails.Rows[0].Cells.Clear();
                //gvDetails.Rows[0].Cells.Add(new TableCell());
                //gvDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                //gvDetails.Rows[0].Cells[0].Text = "No Records Found";
                gvDetails.Rows[0].Visible = false;
                DropDownList ddlType = (DropDownList)gvDetails.FooterRow.FindControl("ddlType");
                ddlType.Focus();
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {

                DropDownList ddlType = (DropDownList)e.Row.FindControl("ddlType");
                if (ddlType.Text == "BANK")
                {
                    Global.lblAdd(@"select MAX(CHARGEID) from LC_CHARGE where CHARGEID like 'I1%'", lblMxChargeID);
                    string ChargeID;
                    string mxCD, OItemCD;
                        //, mid, subItemCD;
                    int subCD, incrItCD;
                    if (lblMxChargeID.Text == "")
                    {
                        ChargeID = "I101";
                    }
                    else
                    {
                        mxCD = lblMxChargeID.Text;
                        OItemCD = mxCD.Substring(1, 3);
                        subCD = int.Parse(OItemCD);
                        incrItCD = subCD + 1;
                        //if (incrItCD < 10)
                        //{
                        //    mid = incrItCD.ToString();
                        //    subItemCD = "00" + mid;
                        //}
                        //else if (incrItCD < 100)
                        //{
                        //    mid = incrItCD.ToString();
                        //    subItemCD = "0" + mid;
                        //}
                        //else
                        //    subItemCD = incrItCD.ToString();

                        ChargeID = "I" + incrItCD;
                    }

                    TextBox txtChargeID = (TextBox)e.Row.FindControl("txtChargeID");
                    txtChargeID.Text = ChargeID;
                }
                else
                {
                    Global.lblAdd(@"select MAX(CHARGEID) from LC_CHARGE where CHARGEID like 'I2%'", lblMxChargeID);
                    string ChargeID;
                    string mxCD, OItemCD;
                        //, mid, subItemCD;
                    int subCD, incrItCD;
                    if (lblMxChargeID.Text == "")
                    {
                        ChargeID = "I201";
                    }
                    else
                    {
                        mxCD = lblMxChargeID.Text;
                        OItemCD = mxCD.Substring(1, 3);
                        subCD = int.Parse(OItemCD);
                        incrItCD = subCD + 1;
                        //if (incrItCD < 10)
                        //{
                        //    mid = incrItCD.ToString();
                        //    subItemCD = "00" + mid;
                        //}
                        //else if (incrItCD < 100)
                        //{
                        //    mid = incrItCD.ToString();
                        //    subItemCD = "0" + mid;
                        //}
                        //else
                        //    subItemCD = incrItCD.ToString();

                        ChargeID = "I" + incrItCD;
                    }

                    TextBox txtChargeID = (TextBox)e.Row.FindControl("txtChargeID");
                    txtChargeID.Text = ChargeID;
                }
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlType = (DropDownList)row.FindControl("ddlType");
            if (ddlType.Text == "BANK")
            {
                Global.lblAdd(@"select MAX(CHARGEID) from LC_CHARGE where CHARGEID like 'I1%'", lblMxChargeID);
                string ChargeID;
                string mxCD, OItemCD;
                    //, mid, subItemCD;
                int subCD, incrItCD;
                if (lblMxChargeID.Text == "")
                {
                    ChargeID = "I101";
                }
                else
                {
                    mxCD = lblMxChargeID.Text;
                    OItemCD = mxCD.Substring(1, 3);
                    subCD = int.Parse(OItemCD);
                    incrItCD = subCD + 1;
                    //if (incrItCD < 10)
                    //{
                    //    mid = incrItCD.ToString();
                    //    subItemCD = "00" + mid;
                    //}
                    //else if (incrItCD < 100)
                    //{
                    //    mid = incrItCD.ToString();
                    //    subItemCD = "0" + mid;
                    //}
                    //else
                    //    subItemCD = incrItCD.ToString();

                    ChargeID = "I" + incrItCD;
                }

                TextBox txtChargeID = (TextBox)row.FindControl("txtChargeID");
                txtChargeID.Text = ChargeID;
            }
            else
            {
                Global.lblAdd(@"select MAX(CHARGEID) from LC_CHARGE where CHARGEID like 'I2%'", lblMxChargeID);
                string ChargeID;
                string mxCD, OItemCD;
                    //, mid, subItemCD;
                int subCD, incrItCD;
                if (lblMxChargeID.Text == "")
                {
                    ChargeID = "I201";
                }
                else
                {
                    mxCD = lblMxChargeID.Text;
                    OItemCD = mxCD.Substring(1, 3);
                    subCD = int.Parse(OItemCD);
                    incrItCD = subCD + 1;
                    //if (incrItCD < 10)
                    //{
                    //    mid = incrItCD.ToString();
                    //    subItemCD = "00" + mid;
                    //}
                    //else if (incrItCD < 100)
                    //{
                    //    mid = incrItCD.ToString();
                    //    subItemCD = "0" + mid;
                    //}
                    //else
                    //    subItemCD = incrItCD.ToString();

                    ChargeID = "I" + incrItCD;
                }

                TextBox txtChargeID = (TextBox)row.FindControl("txtChargeID");
                txtChargeID.Text = ChargeID;
            }
            TextBox txtChargeNM = (TextBox)row.FindControl("txtChargeNM");
            txtChargeNM.Focus();
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            BindLcChargeData();
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
                string s = "";
                string ChargeID;
                SqlTransaction tran = null;
                try
                {
                    ChargeID = gvDetails.FooterRow.Cells[1].Text;
                    DropDownList ddlType = (DropDownList)gvDetails.FooterRow.FindControl("ddlType");
                    TextBox txtChargeID = (TextBox)gvDetails.FooterRow.FindControl("txtChargeID");
                    TextBox txtChargeNM = (TextBox)gvDetails.FooterRow.FindControl("txtChargeNM");
                    TextBox txtRemarks = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");

                    if (txtChargeNM.Text == "")
                    {
                        Response.Write("<script>alert('Type Charge Name.');</script>");
                        txtChargeNM.Focus();
                    }
                    else
                    {
                        query = (" INSERT INTO LC_CHARGE (CHARGETP, CHARGEID, CHARGENM, REMARKS, USERPC, USERID) " +
                             " VALUES ('" + ddlType.Text + "','" + txtChargeID.Text + "','" + txtChargeNM.Text + "','" + txtRemarks.Text + "','','" + userName + "')");

                        comm = new SqlCommand(query, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();
                        BindLcChargeData();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

            Label lblChrgeID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblChrgeID");
            string s = "";
            SqlTransaction tran = null;

            try
            {
                query = ("Delete FROM LC_CHARGE where CHARGEID = '" + lblChrgeID.Text + "' ");
                comm = new SqlCommand(query, conn);
                conn.Open();
                tran = conn.BeginTransaction();
                comm.Transaction = tran;
                int result = comm.ExecuteNonQuery();
                tran.Commit();
                conn.Close();

                BindLcChargeData();
            }
            catch(Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            BindLcChargeData();

            TextBox txtChrgeIDEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtChrgeIDEdit");
            Session["Charge"] = txtChrgeIDEdit.Text;

            DropDownList ddlTypeEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlTypeEdit");
            Global.lblAdd(@"select CHARGETP from LC_CHARGE where CHARGEID='" + txtChrgeIDEdit.Text + "'", lblLCType);
            ddlTypeEdit.Text = lblLCType.Text;
            ddlTypeEdit.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string userName = HttpContext.Current.Session["UserName"].ToString();

            //TextBox txtChrgeIDEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChrgeIDEdit");
            //Session["Charge"] = txtChrgeIDEdit.Text;
            string charge = Session["Charge"].ToString();

            string s = "";
            SqlTransaction tran = null;

            try
            {
                DropDownList ddlTypeEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlTypeEdit");
                TextBox txtChrgeIDEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChrgeIDEdit");
                TextBox txtChargeNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChargeNMEdit");
                TextBox txtRemarksEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");

                conn.Open();
                tran = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("UPDATE LC_CHARGE SET CHARGETP = '" + ddlTypeEdit.Text + "', CHARGEID='" + txtChrgeIDEdit.Text + "', CHARGENM='" + txtChargeNMEdit.Text + "', REMARKS='" + txtRemarksEdit.Text + "', USERID = '" + userName + "' where CHARGEID = '" + charge + "'", conn);
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();
                tran.Commit();
                conn.Close();
                gvDetails.EditIndex = -1;

                BindLcChargeData();
            }
            catch (Exception ex)
            {
                s = ex.Message;
            }
        }

        protected void ddlTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            DropDownList ddlTypeEdit = (DropDownList)row.FindControl("ddlTypeEdit");
            if (ddlTypeEdit.Text == "BANK")
            {
                Global.lblAdd(@"select MAX(CHARGEID) from LC_CHARGE where CHARGEID like 'I1%'", lblMxChargeID);
                string ChargeID;
                string mxCD, OItemCD;
                //, mid, subItemCD;
                int subCD, incrItCD;
                if (lblMxChargeID.Text == "")
                {
                    ChargeID = "I101";
                }
                else
                {
                    mxCD = lblMxChargeID.Text;
                    OItemCD = mxCD.Substring(1, 3);
                    subCD = int.Parse(OItemCD);
                    incrItCD = subCD + 1;
                    //if (incrItCD < 10)
                    //{
                    //    mid = incrItCD.ToString();
                    //    subItemCD = "00" + mid;
                    //}
                    //else if (incrItCD < 100)
                    //{
                    //    mid = incrItCD.ToString();
                    //    subItemCD = "0" + mid;
                    //}
                    //else
                    //    subItemCD = incrItCD.ToString();

                    ChargeID = "I" + incrItCD;
                }

                TextBox txtChrgeIDEdit = (TextBox)row.FindControl("txtChrgeIDEdit");
                txtChrgeIDEdit.Text = ChargeID;
            }
            else
            {
                Global.lblAdd(@"select MAX(CHARGEID) from LC_CHARGE where CHARGEID like 'I2%'", lblMxChargeID);
                string ChargeID;
                string mxCD, OItemCD;
                //, mid, subItemCD;
                int subCD, incrItCD;
                if (lblMxChargeID.Text == "")
                {
                    ChargeID = "I201";
                }
                else
                {
                    mxCD = lblMxChargeID.Text;
                    OItemCD = mxCD.Substring(1, 3);
                    subCD = int.Parse(OItemCD);
                    incrItCD = subCD + 1;
                    //if (incrItCD < 10)
                    //{
                    //    mid = incrItCD.ToString();
                    //    subItemCD = "00" + mid;
                    //}
                    //else if (incrItCD < 100)
                    //{
                    //    mid = incrItCD.ToString();
                    //    subItemCD = "0" + mid;
                    //}
                    //else
                    //    subItemCD = incrItCD.ToString();

                    ChargeID = "I" + incrItCD;
                }

                TextBox txtChrgeIDEdit = (TextBox)row.FindControl("txtChrgeIDEdit");
                txtChrgeIDEdit.Text = ChargeID;
            }
            TextBox txtChargeNMEdit = (TextBox)row.FindControl("txtChargeNMEdit");
            txtChargeNMEdit.Focus();
        }

    }
}