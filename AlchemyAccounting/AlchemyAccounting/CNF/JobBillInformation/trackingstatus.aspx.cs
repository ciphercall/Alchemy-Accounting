using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.JobBillInformation
{
    public partial class trackingstatus : System.Web.UI.Page
    {

        SqlConnection conn;
        SqlCommand cmdd;

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        JobBillInformationModel jbfm = new JobBillInformationModel();
        JobBillInformationDataAccess jbid = new JobBillInformationDataAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    txtJobID.Focus();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListJob_No_Year_Type(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            cmd.CommandType = CommandType.Text;
            if (uTp == "ADMIN")
            {
                cmd.CommandText = ("SELECT DISTINCT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT DISTINCT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "'");
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["JOBPAR"].ToString());
            return CompletionSet.ToArray();

        }

        protected void txtJobID_TextChanged(object sender, EventArgs e)
        {
            if (txtJobID.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select job no.";
                txtJobID.Focus();
            }
            else
            {
                lblErrmsg.Visible = false;

                string jobno = "";
                string jobyear = "";
                string jobtp = "";
                string searchPar = txtJobID.Text;

                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    jobno = lineSplit[0];
                    jobyear = lineSplit[1];
                    jobtp = lineSplit[2];

                    txtJobID.Text = jobno.Trim();
                    txtJobYear.Text = jobyear.Trim();
                    txtJobType.Text = jobtp.Trim();
                    txtCompanyID.Text = "";

                    Global.txtAdd("SELECT COMPID FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtCompanyID);
                    Global.txtAdd("SELECT (BRANCHID + '-' + COMPNM) FROM ASL_COMPANY WHERE COMPID = '" + txtCompanyID.Text + "' ", txtCompanyNM);

                    Global.txtAdd("SELECT PARTYID FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtPartyID);
                    Global.txtAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD = '" + txtPartyID.Text + "' ", txtPartyNM);

                    Global.txtAdd("SELECT CONVERT(NVARCHAR(20),TRANSDT,103) AS TRANSD FROM CNF_JOBSTATUS WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtReceiveDate);
                    GridShow();
                    DropDownList ddlstatusFoot = (DropDownList)gvDetails.FooterRow.FindControl("ddlstatusFoot");
                    ddlstatusFoot.Focus();

                }
                else
                {
                    txtJobID.Text = "";
                    txtJobYear.Text = "";
                    txtJobType.Text = "";
                    txtCompanyID.Text = "";
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select job no.";
                    txtJobID.Focus();
                }
            }
        }

        private void GridShow()
        {
            conn = new SqlConnection(Global.connection);
            conn.Open();

            cmdd = new SqlCommand(@"Select CONVERT(varchar(10),STATSDT,105) AS STATSDT, STATUS, REMARKS, SERIAL From CNF_DOCSTATS 
                                where COMPID=@COMPID and JOBTP=@JOBTP and JOBYY=@JOBYY and JOBNO=@JOBNO", conn);
            cmdd.Parameters.Clear();
            cmdd.Parameters.AddWithValue("@COMPID", txtCompanyID.Text.Trim());
            cmdd.Parameters.AddWithValue("@JOBTP", txtJobType.Text.Trim());
            cmdd.Parameters.AddWithValue("@JOBYY", txtJobYear.Text.Trim());
            cmdd.Parameters.AddWithValue("@JOBNO", txtJobID.Text.Trim());
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                DropDownList ddlstatusFoot = (DropDownList)gvDetails.FooterRow.FindControl("ddlstatusFoot");
                ddlstatusFoot.Focus();

                

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
                gvDetails.FooterRow.Visible = true;
            }
        }


        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TextBox StatusDatefoot = (TextBox)gvDetails.FooterRow.FindControl("StatusDatefoot");
                DropDownList ddlstatusFoot = (DropDownList)gvDetails.FooterRow.FindControl("ddlstatusFoot");
                TextBox txtREMARKSfoot = (TextBox)gvDetails.FooterRow.FindControl("txtREMARKSfoot");
                

                if (e.CommandName.Equals("SaveCon"))
                {

                    if (txtCompanyID.Text == "")
                    {
                        lblErrMsgExist.Visible = true;
                        lblErrMsgExist.Text = "Company ID is missing";
                        txtCompanyID.Focus();
                    }
                    else if (StatusDatefoot.Text == "")
                    {
                        lblErrMsgExist.Visible = true;
                        lblErrMsgExist.Text = "Date is missing";
                        StatusDatefoot.Focus();
                    }
                    else if (ddlstatusFoot.Text == "")
                    {
                        lblErrMsgExist.Visible = true;
                        lblErrMsgExist.Text = "Status is missing";
                        ddlstatusFoot.Focus();
                    }
                    
                    else
                    {
                        lblErrmsg.Visible = false;

                        jbfm.COMPID = txtCompanyID.Text;
                        jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                        jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                        jbfm.JOBTP = txtJobType.Text;

                        DateTime FRDT = DateTime.Parse(StatusDatefoot.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string FDT = FRDT.ToString("yyyy-MM-dd");


                        jbfm.Statusdt = DateTime.Parse(StatusDatefoot.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        jbfm.Status = ddlstatusFoot.Text;
                        jbfm.REMARKS = txtREMARKSfoot.Text;
                        
                        jbfm.InTime = DateTime.Now;
                        //jbfm.UpdateTime = DateTime.Now;
                        jbfm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                        jbfm.Userid = HttpContext.Current.Session["UserName"].ToString();
                        jbfm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                        jbid.SaveJobBillInfo_Tracking(jbfm);

                        ddlstatusFoot.Focus();
                        GridShow();
                    }
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        public void Refresh()
        {
            txtJobID.Text = "";
            txtJobType.Text = "";
            txtJobYear.Text = "";
            txtPartyID.Text = "";
            txtPartyNM.Text = "";
            txtCompanyID.Text = "";
            txtCompanyNM.Text = "";

            lblErrmsg.Text = "";
            lblErrMsgExist.Text = "";
        }


        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {

                TextBox StatusDatefoot = (TextBox)e.Row.FindControl("StatusDatefoot");
                DropDownList ddlstatusFoot = (DropDownList)e.Row.FindControl("ddlstatusFoot");
                TextBox txtREMARKSfoot = (TextBox)e.Row.FindControl("lblREMARKSfoot");
                Label lblStatusDate = (Label)e.Row.FindControl("lblStatusDate");

                //DateTime today = DateTime.Now;
                //string td = Global.Dayformat(today);
                //txtBillDate.Text = td;

                //txtFwdDate.Text = td;

                //string mon = today.ToString("MMM").ToUpper();
                //string year = today.ToString("yyyy");
                StatusDatefoot.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DateTime FRDT = DateTime.Parse(StatusDatefoot.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string FDT = FRDT.ToString("yyyy-MM-dd");


                ddlstatusFoot.Focus();
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            GridShow();
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {

            lblddladd.Text = "";

            Label srialitem = (Label)gvDetails.Rows[e.NewEditIndex].FindControl("srialitem");
            jbfm.COMPID = txtCompanyID.Text;
            jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
            jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
            jbfm.JOBTP = txtJobType.Text;
            jbfm.Serial = Convert.ToInt64(srialitem.Text);

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();

            SqlCommand cmdselectdata = new SqlCommand(@"SELECT STATUS From CNF_DOCSTATS 
                             where COMPID=@COMPID and JOBTP=@JOBTP and JOBYY=@JOBYY and JOBNO=@JOBNO and SERIAL=@SERIAL", conn);
            cmdselectdata.Parameters.Clear();
            cmdselectdata.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jbfm.COMPID;
            cmdselectdata.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jbfm.JOBYY;
            cmdselectdata.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jbfm.JOBTP;
            cmdselectdata.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jbfm.JOBNO;
            cmdselectdata.Parameters.Add("@SERIAL", SqlDbType.BigInt).Value = jbfm.Serial;

            SqlDataReader dr = cmdselectdata.ExecuteReader();
            while (dr.Read())
            {
                lblddladd.Text = dr["STATUS"].ToString();
            }

            gvDetails.EditIndex = e.NewEditIndex;
            GridShow();

            //TextBox txtBillAmountEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtBillAmountEdit");

            



            DropDownList ddlstatusEdit = (DropDownList)gvDetails.Rows[e.NewEditIndex].FindControl("ddlstatusEdit");
            ddlstatusEdit.Text = lblddladd.Text;
            ddlstatusEdit.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TextBox StatusDateEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("StatusDateEdit");
                DropDownList ddlstatusEdit = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlstatusEdit");
                TextBox txtREMARKSEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtREMARKSEdit");
                Label serialedit = (Label)gvDetails.Rows[e.RowIndex].FindControl("serialedit");
                jbfm.COMPID = txtCompanyID.Text;
                jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                jbfm.JOBTP = txtJobType.Text;
                jbfm.Serial = Convert.ToInt64(serialedit.Text);
                

                jbfm.Statusdt = DateTime.Parse(StatusDateEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jbfm.Status = ddlstatusEdit.Text;
                jbfm.REMARKS = txtREMARKSEdit.Text;

                //jbfm.InTime = DateTime.Now;
                jbfm.UpdateTime = DateTime.Now;
                jbfm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                jbfm.UpdateuserID = HttpContext.Current.Session["UserName"].ToString();
                jbfm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                lblErrmsg.Visible = false;

                jbid.UpdateJobBillInfo_Tracking(jbfm);

                gvDetails.EditIndex = -1;



                GridShow();

                TextBox StatusDatefoot = (TextBox)gvDetails.FooterRow.FindControl("StatusDatefoot");
                StatusDatefoot.Focus();
                
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {



                Label srialitem = (Label)gvDetails.Rows[e.RowIndex].FindControl("srialitem");

                jbfm.COMPID = txtCompanyID.Text;
                jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                jbfm.JOBTP = txtJobType.Text;
                jbfm.Serial = Convert.ToInt64(srialitem.Text);


                lblErrmsg.Visible = false;

                string COMPID = "";
                string JOBYY = "";
                string JOBTP = "";
                string JOBNO = "";
                string SERIAL = "";
                string STATSDT = "";
                string STATUS = "";
                string REMARKS = "";
                string USERPC = "";
                string UPDATEPC = "";
                string USERID = "";
                string UPDATEUSERID = "";
                string INTIME = "";
                string UPDATETIME = "";
                string IPADDRESS = "";
                string UPDATEIP = "";

                string userName = HttpContext.Current.Session["UserName"].ToString();
                string userpc = HttpContext.Current.Session["PCName"].ToString(); ;
                string ipadd = HttpContext.Current.Session["IpAddress"].ToString();


                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);

                conn.Open();

                SqlCommand cmdselectdata = new SqlCommand(@"SELECT * From CNF_DOCSTATS 
                             where COMPID=@COMPID and JOBTP=@JOBTP and JOBYY=@JOBYY and JOBNO=@JOBNO and SERIAL=@SERIAL", conn);
                cmdselectdata.Parameters.Clear();
                cmdselectdata.Parameters.Add("@COMPID", SqlDbType.NVarChar).Value = jbfm.COMPID;
                cmdselectdata.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = jbfm.JOBYY;
                cmdselectdata.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = jbfm.JOBTP;
                cmdselectdata.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = jbfm.JOBNO;
                cmdselectdata.Parameters.Add("@SERIAL", SqlDbType.BigInt).Value = jbfm.Serial;

                SqlDataReader dr = cmdselectdata.ExecuteReader();
                while (dr.Read())
                {
                   

                    SERIAL = dr["SERIAL"].ToString();
                    COMPID = dr["COMPID"].ToString();
                    JOBYY = dr["JOBYY"].ToString();
                    JOBTP = dr["JOBTP"].ToString();
                    JOBNO = dr["JOBNO"].ToString();
                    STATSDT = dr["STATSDT"].ToString();
                    STATUS = dr["STATUS"].ToString();
                    REMARKS = dr["REMARKS"].ToString();
                    
                    USERPC = dr["USERPC"].ToString();
                    UPDATEPC = dr["UPDATEPC"].ToString();
                    USERID = dr["USERID"].ToString();
                    UPDATEUSERID = dr["UPDATEUSERID"].ToString();
                    INTIME = dr["INTIME"].ToString();
                    UPDATETIME = dr["UPDATETIME"].ToString();
                    IPADDRESS = dr["IPADDRESS"].ToString();
                    UPDATEIP = dr["UPDATEIP"].ToString();

                }
                dr.Close();

                string alldata = SERIAL + ", " + COMPID + ", " + JOBYY + ", " + JOBTP + ", " + JOBNO + ", " + STATSDT
                    + ", " + STATUS + ", " + REMARKS + ", " + USERPC + ", " + UPDATEPC + ", " + USERID + ", " + UPDATEUSERID + ", " + INTIME
                    + ", " + UPDATETIME + ", " + IPADDRESS + ", " + UPDATEIP;

                jbfm.InTM = DateTime.Now;


                SqlCommand cmdinsert = new SqlCommand("insert into ASL_DLT values('CNF_DOCSTATS',@DESCRP,@USERPC,@USERID,@INTIME,@IPADD)", conn);
                cmdinsert.Parameters.AddWithValue("@DESCRP", alldata);
                cmdinsert.Parameters.AddWithValue("@USERPC", userpc);
                cmdinsert.Parameters.AddWithValue("@USERID", userName);
                cmdinsert.Parameters.AddWithValue("@INTIME", jbfm.InTM);
                cmdinsert.Parameters.AddWithValue("@IPADD", ipadd);

                cmdinsert.ExecuteNonQuery();




                jbid.RemoveJobBillInfo_Tracking(jbfm);

                gvDetails.EditIndex = -1;

                GridShow();

                TextBox StatusDatefoot = (TextBox)gvDetails.FooterRow.FindControl("StatusDatefoot");
                StatusDatefoot.Focus();


            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListExpenseID_Name(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            cmd.CommandType = CommandType.Text;
            if (uTp == "ADMIN")
            {
                cmd.CommandText = ("SELECT (EXPNM + '|' +  EXPID) AS EXP FROM CNF_EXPENSE WHERE ( EXPNM + '|' +  EXPID ) LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT (EXPNM + '|' +  EXPID) AS EXP FROM CNF_EXPENSE WHERE (EXPNM + '|' + EXPID) LIKE '" + prefixText + "%' ");
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EXP"].ToString());
            return CompletionSet.ToArray();

        }


        protected void txtExpense_TextChanged(object sender, EventArgs e)
        {
            TextBox txtExpenseID = (TextBox)gvDetails.FooterRow.FindControl("txtExpenseID");
            TextBox txtExpense = (TextBox)gvDetails.FooterRow.FindControl("txtExpense");
            TextBox txtBillAmount = (TextBox)gvDetails.FooterRow.FindControl("txtBillAmount");

            if (txtExpense.Text == "")
            {
                lblErrMsgExist.Visible = true;
                lblErrMsgExist.Text = "Select Expense Name.";
                txtExpense.Focus();
            }
            else
            {
                lblErrMsgExist.Visible = false;

                string expnm = "";
                string expID = "";

                string searchPar = txtExpense.Text;

                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    expnm = lineSplit[0];
                    expID = lineSplit[1];


                    txtExpense.Text = expnm.Trim();
                    txtExpenseID.Text = expID.Trim();
                    txtBillAmount.Focus();
                }
                else
                {
                    txtExpenseID.Text = "";
                    txtExpense.Text = "";
                    lblErrmsg.Visible = true;
                    lblErrmsg.Text = "Select expense.";
                    txtExpense.Focus();
                }
            }
        }

        protected void txtFwdDateEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtBillSlEdit = (TextBox)row.FindControl("txtBillSlEdit");
            txtBillSlEdit.Focus();
        }

        protected void txtFwdDate_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtFwdDate = (TextBox)row.FindControl("txtFwdDate");
            txtFwdDate.Focus();
        }

        //protected void btnPrint_Click(object sender, EventArgs e)
        //{
        //    if (txtJobID.Text == "")
        //    {
        //        lblErrmsg.Visible = true;
        //        lblErrmsg.Text = "Select job no.";
        //        txtJobID.Focus();
        //    }
        //    else
        //    {
        //        Session["jobno"] = null;
        //        Session["jobtp"] = null;
        //        Session["jobyear"] = null;
        //        Session["compid"] = null;
        //        Session["partyid"] = null;
        //        Session["jobdt"] = null;

        //        Session["jobno"] = txtJobID.Text;
        //        Session["jobtp"] = txtJobType.Text;
        //        Session["jobyear"] = txtJobYear.Text;
        //        Session["compid"] = txtCompanyID.Text;
        //        Session["partyid"] = txtPartyID.Text;
        //        Session["jobdt"] = txtReceiveDate.Text;

        //        Page.ClientScript.RegisterStartupScript(
        //              this.GetType(), "OpenWindow", "window.open('../report/vis-rep/rpt-bill-details.aspx','_newtab');", true);
        //    }
        //}

        protected void txtBillSlEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            ImageButton imgbtnUpdate = (ImageButton)row.FindControl("imgbtnUpdate");
            imgbtnUpdate.Focus();
        }

        protected void txtBillSl_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            ImageButton imgbtnAdd = (ImageButton)row.FindControl("imgbtnAdd");
            imgbtnAdd.Focus();
        }
    }
}