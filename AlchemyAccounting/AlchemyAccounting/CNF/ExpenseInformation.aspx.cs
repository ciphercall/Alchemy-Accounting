using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace AlchemyAccounting.CNF
{
    public partial class ExpenseInformation : System.Web.UI.Page
    {
        //private String strConnString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;

        SqlConnection conn;
        SqlCommand cmdd;

        ExpenseInformationModel eim = new ExpenseInformationModel();
        ExpenseInformationDataAccess eida = new ExpenseInformationDataAccess();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    txtcat.Focus();

                    Global.lblAdd("select MAX(EXPCID) from CNF_EXPMST ", lblcid);
                    if (lblcid.Text == "")
                    {
                        txtcatID.Text = "I1";
                    }
                    else
                    {
                        var resultString = Regex.Match(lblcid.Text, @"\d+").Value;
                        var id = Int32.Parse(resultString) + 1;
                        txtcatID.Text = "I" + id;
                    }

                    GridShow();

                }
            }
        }



        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListCatID(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("SELECT EXPCNM FROM CNF_EXPMST WHERE EXPCNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EXPCNM"].ToString());
            return CompletionSet.ToArray();
        }

        private void GridShow()
        {

            conn = new SqlConnection(Global.connection);
            conn.Open();

            cmdd = new SqlCommand("SELECT CNF_EXPENSE.EXPNM, CNF_EXPENSE.EXPCID, CNF_EXPENSE.EXPID, CNF_EXPENSE.REMARKS" +
                    " FROM CNF_EXPENSE INNER JOIN" +
                     " CNF_EXPMST ON CNF_EXPENSE.EXPCID = CNF_EXPMST.EXPCID " +
                     " where CNF_EXPENSE.EXPCID = '" + txtcatID.Text + "' ", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
                txtParticulars.Focus();

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


            }
        }

        protected void txtcat_TextChanged(object sender, EventArgs e)
        {

            Global.lblAdd("select MAX(EXPCID) from CNF_EXPMST ", lblcid);

            if (lblcid.Text == "")
            {
                txtcatID.Text = "I1";
            }

            else
            {
                var resultString = Regex.Match(lblcid.Text, @"\d+").Value;
                var id = Int32.Parse(resultString) + 1;

                txtcatID.Text = "I" + id;
            }

            Global.txtAdd("SELECT EXPCID FROM CNF_EXPMST WHERE EXPCNM= '" + txtcat.Text + "'", txtcatID);
            GridShow();

            TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
            txtParticulars.Focus();

        }

        protected void txtcatID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = txtcatID.Text;

                Global.lblAdd("select MAX(EXPID) from CNF_EXPENSE where EXPCID='" + txtcatID.Text + "' ", lblChkInternalID);

                if (lblChkInternalID.Text == "")
                {
                    string cid = txtcatID.Text + "01";
                    e.Row.Cells[1].Text = cid;
                    
                }

                else
                {
                    //var resultString = Regex.Match(lblChkInternalID.Text, @"\d+").Value;
                    //var id = Int32.Parse(resultString) + 1;
                    var resultString = lblChkInternalID.Text.Substring(2,2);
                    var id = Int64.Parse(resultString) + 1;

                    if(id < 10)
                        e.Row.Cells[1].Text = txtcatID.Text + "0" + id;
                    else
                        e.Row.Cells[1].Text = txtcatID.Text + id;
                }
            }

        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            GridShow();
        }

        private bool Previousdata(string id)
        {
            bool bflag = false;
            DataTable table = new DataTable();

            try
            {
                ExpenseInformationDataAccess dob = new ExpenseInformationDataAccess();
                table = dob.showExpenseInfo(id);
                DataSet userDS = new DataSet();
            }
            catch (Exception ex)
            {
                table = null;
                Response.Write(ex.Message);
            }
            if (table != null)
            {
                if (table.Rows.Count > 0)
                    bflag = true;
            }
            return bflag;
        }


        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Label lblID = (Label)gvDetails.FooterRow.FindControl("lblID");
            Label lblExpense = (Label)gvDetails.FooterRow.FindControl("lblExpense");
            TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
            TextBox txtRemarks = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");



            if (e.CommandName.Equals("SaveCon"))
            {


                if (Previousdata(gvDetails.FooterRow.Cells[0].Text) == false)
                {
                    eim.EXPCNM = txtcat.Text;
                    eim.EXPCID = gvDetails.FooterRow.Cells[0].Text;
                    eim.InTime = DateTime.Now;
                    eim.UpdateTime = DateTime.Now;
                    eim.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    eim.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                    eida.MstInput(eim);
                }

                if (txtParticulars.Text == "")
                {
                    lblErrMsg.Visible = true;
                    lblErrMsg.Text = "particular input missing";
                    txtParticulars.Focus();
                }

                else
                {
                    eim.EXPCID = gvDetails.FooterRow.Cells[0].Text;
                    eim.EXPID = gvDetails.FooterRow.Cells[1].Text;
                    eim.EXPNM = txtParticulars.Text;
                    eim.REMARKS = txtRemarks.Text;
                    eim.InTime = DateTime.Now;
                    eim.UpdateTime = DateTime.Now;
                    eim.UserPC = HttpContext.Current.Session["PCName"].ToString();
                    eim.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                    eida.SaveExpenseInfo(eim);
                }
                GridShow();
            }

        }
        public void Refresh()
        {
            //Label lblID = (Label)gvDetails.FooterRow.FindControl("lblID");
            //Label lblExpense = (Label)gvDetails.FooterRow.FindControl("lblExpense");
            //TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
            //TextBox txtRemarks = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");

            //txtParticulars.Text = "";
            //txtRemarks.Text = "";
            txtcat.Text = "";

            Global.lblAdd("select MAX(EXPCID) from CNF_EXPMST ", lblcid);
            if (lblcid.Text == "")
            {
                txtcatID.Text = "I1";
            }
            else
            {
                var resultString = Regex.Match(lblcid.Text, @"\d+").Value;
                var id = Int32.Parse(resultString) + 1;

                txtcatID.Text = "I" + id;
            }


            GridShow();
            txtcat.Focus();

        }


        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblID");
            Label lblExpense = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblExpense");


            eim.EXPCID = lblID.Text;
            eim.EXPID = lblExpense.Text;




            string EXPCID = "";
            string EXPID = "";
            string EXPNM = "";
            string REMARKS = "";
            string USERPC = "";
            string INTIME = "";
            string UPDATETIME = "";
            string IPADDRESS = "";
            

            string userName = HttpContext.Current.Session["UserName"].ToString();
            string userpc = HttpContext.Current.Session["PCName"].ToString(); ;
            string ipadd = HttpContext.Current.Session["IpAddress"].ToString();


            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmdselectdata = new SqlCommand("SELECT * FROM CNF_EXPENSE WHERE EXPCID =@EXPCID AND EXPID =@EXPID", conn);
            cmdselectdata.Parameters.Clear();

            cmdselectdata.Parameters.Add("@EXPCID", SqlDbType.NVarChar).Value = eim.EXPCID;
            cmdselectdata.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = eim.EXPID;

            SqlDataReader dr = cmdselectdata.ExecuteReader();
            while (dr.Read())
            {
                EXPCID = dr["EXPCID"].ToString();
                EXPID = dr["EXPID"].ToString();
                EXPNM = dr["EXPNM"].ToString();
                REMARKS = dr["REMARKS"].ToString();
                USERPC = dr["USERPC"].ToString();
                INTIME = dr["INTIME"].ToString();
                UPDATETIME = dr["UPDATETIME"].ToString();
                IPADDRESS = dr["IPADDRESS"].ToString();
            }
            dr.Close();

            string alldata = EXPCID + ", " + EXPID + ", " + EXPNM + ", " + REMARKS + ", " + USERPC + ", " + INTIME
                + ", " + UPDATETIME + ", " + IPADDRESS;

            eim.InTM = DateTime.Now;


            SqlCommand cmdinsert = new SqlCommand("insert into ASL_DLT values('CNF_EXPENSE',@DESCRP,@USERPC,@USERID,@INTIME,@IPADD)", conn);
            cmdinsert.Parameters.AddWithValue("@DESCRP", alldata);
            cmdinsert.Parameters.AddWithValue("@USERPC", userpc);
            cmdinsert.Parameters.AddWithValue("@USERID", userName);
            cmdinsert.Parameters.AddWithValue("@INTIME", eim.InTM);
            cmdinsert.Parameters.AddWithValue("@IPADD", ipadd);

            cmdinsert.ExecuteNonQuery();




            eida.DeleteExpenseInfo(eim);

            gvDetails.EditIndex = -1;
            GridShow();


            TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
            TextBox txtRemarks = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");


            txtParticulars.Focus();


        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            GridShow();

            TextBox txtParticularsEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtParticularsEdit");
            txtParticularsEdit.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblIDEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblIDEdit");
            Label lblExpenseEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblExpenseEdit");
            TextBox txtParticularsEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtParticularsEdit");
            TextBox txtRemarksEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");


            Label lblID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblID");
            Label lblExpense = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblExpense");

            TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
            TextBox txtRemarks = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");


            if (txtParticularsEdit.Text == "")
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "particular input missing";
                txtParticularsEdit.Focus();
            }

            else
            {
                eim.EXPCID = lblIDEdit.Text;
                eim.EXPID = lblExpenseEdit.Text;

                eim.EXPNM = txtParticularsEdit.Text;
                eim.REMARKS = txtRemarksEdit.Text;
                eim.InTime = DateTime.Now;
                eim.UpdateTime = DateTime.Now;
                eim.UserPC = HttpContext.Current.Session["PCName"].ToString();
                eim.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                eida.EditExpenseInfo(eim);

                gvDetails.EditIndex = -1;
                GridShow();


                txtParticulars.Focus();

            }
        }
    }

}