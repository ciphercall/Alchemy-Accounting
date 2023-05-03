using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.CNF.JobBillInformation
{
    public partial class JobBillInformation : Page
    {

        SqlConnection _conn;
        private SqlCommand _cmdd;

        readonly IFormatProvider _dateformat = new CultureInfo("fr-FR", true);

        readonly JobBillInformationModel _jbfm = new JobBillInformationModel();
        readonly JobBillInformationDataAccess _jbid = new JobBillInformationDataAccess();

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

        private void GridShow()
        {
            _conn = new SqlConnection(Global.connection);
            _conn.Open();

            _cmdd = new SqlCommand("SELECT CONVERT(NVARCHAR(20), dbo.CNF_JOBBILL.BILLDT, 103) AS BILLD, dbo.CNF_JOBBILL.BILLNO, dbo.CNF_JOBBILL.EXPSL, dbo.CNF_JOBBILL.EXPID, " +
                   " dbo.CNF_JOBBILL.EXPAMT, (CASE WHEN substring(CNF_JOBBILL.EXPID,1,2)='I1' THEN dbo.CNF_JOBBILL.EXPAMT ELSE dbo.CNF_JOBBILL.BILLAMT END)  AS BILLAMT, " +
                                  "(CASE WHEN EXPPDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),dbo.CNF_JOBBILL.EXPPDT, 103) END) AS EXPPD, dbo.CNF_JOBBILL.REMARKS, dbo.CNF_JOBBILL.BILLSL, dbo.CNF_EXPENSE.EXPNM " +
                   " FROM dbo.CNF_JOBBILL INNER JOIN dbo.CNF_EXPENSE ON dbo.CNF_JOBBILL.EXPID = dbo.CNF_EXPENSE.EXPID WHERE (dbo.CNF_JOBBILL.JOBNO = " + txtJobID.Text + ") AND (dbo.CNF_JOBBILL.JOBTP = '" + txtJobType.Text + "') AND (dbo.CNF_JOBBILL.JOBYY = '" + txtJobYear.Text + "') AND (dbo.CNF_JOBBILL.COMPID ='" + txtCompanyID.Text + "')" +
                   " ORDER BY CNF_JOBBILL.EXPSL", _conn);

            SqlDataAdapter da = new SqlDataAdapter(_cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            _conn.Close();


            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                TextBox txtExpense = (TextBox)gvDetails.FooterRow.FindControl("txtExpense");
                txtExpense.Focus();

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
                gvDetails.FooterRow.Visible = false;
            }
        }

        [WebMethod(), ScriptMethod()]
        public static string[] GetCompletionListJob_No_Year_Type(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            var cmd = new SqlCommand("", conn);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            cmd.CommandType = CommandType.Text;
            if (uTp == "ADMIN")
            {
                cmd.CommandText = (@"SELECT DISTINCT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR , JOBNO , JOBYY 
                FROM CNF_JOB WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%' order by JOBYY DESC, JOBNO DESC");
            }
            else
            {
                cmd.CommandText = (@"SELECT DISTINCT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR , JOBNO , JOBYY
                FROM CNF_JOB WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "' order by JOBYY DESC, JOBNO DESC");
            }

            conn.Open();
            List<String> completionSet = new List<string>();
            var oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                completionSet.Add(oReader["JOBPAR"].ToString());
            return completionSet.ToArray();

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

                string searchPar = txtJobID.Text;

                int splitter = searchPar.IndexOf("|", StringComparison.Ordinal);
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    var jobno = lineSplit[0];
                    var jobyear = lineSplit[1];
                    var jobtp = lineSplit[2];

                    txtJobID.Text = jobno.Trim();
                    txtJobYear.Text = jobyear.Trim();
                    txtJobType.Text = jobtp.Trim();
                    txtCompanyID.Text = "";

                    Global.txtAdd("SELECT COMPID FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtCompanyID);
                    Global.txtAdd("SELECT ASSV_BDT FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtAssValue);
                    Global.txtAdd("SELECT (BRANCHID + '-' + COMPNM) FROM ASL_COMPANY WHERE COMPID = '" + txtCompanyID.Text + "' ", txtCompanyNM);

                    Global.txtAdd("SELECT PARTYID FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtPartyID);
                    Global.txtAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD = '" + txtPartyID.Text + "' ", txtPartyNM);

                    Global.txtAdd("SELECT CONVERT(NVARCHAR(20),TRANSDT,103) AS TRANSD FROM CNF_JOBSTATUS WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtReceiveDate);
                    GridShow();
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

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                TextBox txtBillDate = (TextBox)gvDetails.FooterRow.FindControl("txtBillDate");
                Label lblSlItem = (Label)gvDetails.FooterRow.FindControl("lblSlItem");
                TextBox txtFwdDate = (TextBox)gvDetails.FooterRow.FindControl("txtFwdDate");
                TextBox txtBillNo = (TextBox)gvDetails.FooterRow.FindControl("txtBillNo");

                TextBox txtExpenseID = (TextBox)gvDetails.FooterRow.FindControl("txtExpenseID");
                TextBox txtExpense = (TextBox)gvDetails.FooterRow.FindControl("txtExpense");
                TextBox txtExpenseAmount = (TextBox)gvDetails.FooterRow.FindControl("txtExpenseAmount");
                TextBox txtBillAmount = (TextBox)gvDetails.FooterRow.FindControl("txtBillAmount");
                TextBox txtRemarks = (TextBox)gvDetails.FooterRow.FindControl("txtRemarks");
                TextBox txtBillSl = (TextBox)gvDetails.FooterRow.FindControl("txtBillSl");

                if (e.CommandName.Equals("SaveCon"))
                {
                    if (txtCompanyID.Text == "")
                    {
                        lblErrMsgExist.Visible = true;
                        lblErrMsgExist.Text = "particular input missing";
                        txtCompanyID.Focus();
                    }
                    else if (txtExpenseID.Text == "")
                    {
                        lblErrMsgExist.Visible = true;
                        lblErrMsgExist.Text = "particular input missing";
                        txtExpense.Focus();
                    }
                    else if (txtExpense.Text == "")
                    {
                        lblErrMsgExist.Visible = true;
                        lblErrMsgExist.Text = "particular input missing";
                        txtExpense.Focus();
                    }
                    else if (txtExpenseAmount.Text == "")
                    {
                        lblErrMsgExist.Visible = true;
                        lblErrMsgExist.Text = "particular input missing";
                        txtExpenseAmount.Focus();
                    }
                    else
                    {
                        lblErrmsg.Visible = false;

                        _jbfm.COMPID = txtCompanyID.Text;
                        _jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                        _jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                        _jbfm.JOBTP = txtJobType.Text;
                        _jbfm.PARTYID = txtPartyID.Text;
                        _jbfm.BILLDT = DateTime.Parse(txtBillDate.Text, _dateformat, DateTimeStyles.AssumeLocal);
                        _jbfm.BILLNO = Convert.ToInt64(txtBillNo.Text);

                        DateTime frdt = DateTime.Parse(txtBillDate.Text, _dateformat, DateTimeStyles.AssumeLocal);
                        string fdt = frdt.ToString("yyyy-MM-dd");

                        Global.lblAdd("select MAX (EXPSL) from CNF_JOBBILL where BILLDT='" + fdt + "' and BILLNO='" + txtBillNo.Text + "' ", lblChkInternalID);

                        if (lblChkInternalID.Text == "")
                        {
                            string cid = "1";
                            lblSlItem.Text = cid;
                            _jbfm.EXPSL = Convert.ToInt64(cid);
                        }

                        else
                        {
                            var id = Int64.Parse(lblChkInternalID.Text) + 1;
                            lblSlItem.Text = id.ToString();
                            _jbfm.EXPSL = Convert.ToInt64(lblSlItem.Text);
                        }

                        _jbfm.EXPID = txtExpenseID.Text;
                        _jbfm.EXPAMT = Convert.ToDecimal(txtExpenseAmount.Text);
                        if (txtBillAmount.Text == "")
                            txtBillAmount.Text = "0";
                        _jbfm.BILLAMT = Convert.ToDecimal(txtBillAmount.Text);
                        if (txtFwdDate.Text == "")
                            txtFwdDate.Text = "1900-01-01";
                        _jbfm.EXPPDT = DateTime.Parse(txtFwdDate.Text, _dateformat, DateTimeStyles.AssumeLocal);
                        _jbfm.REMARKS = txtRemarks.Text;
                        if (txtBillSl.Text == "")
                            txtBillSl.Text = "0";
                        _jbfm.BILLSL = Convert.ToInt64(txtBillSl.Text);
                        _jbfm.InTime = DateTime.Now;
                        //jbfm.UpdateTime = DateTime.Now;
                        _jbfm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                        _jbfm.Userid = HttpContext.Current.Session["UserName"].ToString();
                        _jbfm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                        _jbid.SaveJobBillInfo(_jbfm);

                        txtExpense.Focus();
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
                TextBox txtBillDate = (TextBox)e.Row.FindControl("txtBillDate");
                Label lblSlItem = (Label)e.Row.FindControl("lblSlItem");
                TextBox txtBillNo = (TextBox)e.Row.FindControl("txtBillNo");

                TextBox txtExpense = (TextBox)e.Row.FindControl("txtExpense");


                //DateTime today = DateTime.Now;
                //string td = Global.Dayformat(today);
                //txtBillDate.Text = td;

                //txtFwdDate.Text = td;

                //string mon = today.ToString("MMM").ToUpper();
                //string year = today.ToString("yyyy");
                txtBillDate.Text = txtReceiveDate.Text == "" ? "1900-01-01" : txtReceiveDate.Text;

                DateTime frdt = DateTime.Parse(txtBillDate.Text, _dateformat, DateTimeStyles.AssumeLocal);
                string fdt = frdt.ToString("yyyy-MM-dd");

                Global.lblAdd("select MAX (EXPSL) from CNF_JOBBILL where BILLDT='" + fdt + "' and BILLNO='" + txtBillNo.Text + "' ", lblChkInternalID);

                if (lblChkInternalID.Text == "")
                {
                    string cid = "1";
                    lblSlItem.Text = cid;
                }

                else
                {
                    var id = Int64.Parse(lblChkInternalID.Text) + 1;
                    lblSlItem.Text = id.ToString();
                }

                Global.txtAdd("SELECT dbo.CNF_JOBBILL.BILLNO FROM dbo.CNF_JOBBILL INNER JOIN dbo.CNF_EXPENSE ON dbo.CNF_JOBBILL.EXPID = dbo.CNF_EXPENSE.EXPID " +
                    " WHERE (dbo.CNF_JOBBILL.JOBNO = " + txtJobID.Text + ") AND (dbo.CNF_JOBBILL.JOBTP = '" + txtJobType.Text + "') AND (dbo.CNF_JOBBILL.JOBYY = '" + txtJobYear.Text + "') AND (dbo.CNF_JOBBILL.COMPID ='" + txtCompanyID.Text + "')", txtBillNo);

                txtExpense.Focus();
            }
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDetails.EditIndex = -1;
            GridShow();
        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDetails.EditIndex = e.NewEditIndex;
            GridShow();

            TextBox txtBillAmountEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtBillAmountEdit");
            txtBillAmountEdit.Focus();
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblBillDateEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillDateEdit");
                Label lblSlEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSlEdit");
                TextBox txtFwdDateEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtFwdDateEdit");
                TextBox txtBillNoEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBillNoEdit");

                TextBox txtExpenseIDEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtExpenseIDEdit");
                TextBox txtExpenseAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtExpenseAmountEdit");
                TextBox txtBillAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBillAmountEdit");
                TextBox txtRemarksEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");
                TextBox txtBillSlEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBillSlEdit");


                _jbfm.COMPID = txtCompanyID.Text;
                _jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                _jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                _jbfm.JOBTP = txtJobType.Text;
                _jbfm.PARTYID = txtPartyID.Text;

                _jbfm.BILLDT = DateTime.Parse(lblBillDateEdit.Text, _dateformat, DateTimeStyles.AssumeLocal);
                _jbfm.BILLNO = Convert.ToInt64(txtBillNoEdit.Text);

                _jbfm.EXPSL = Convert.ToInt64(lblSlEdit.Text);

                _jbfm.EXPID = txtExpenseIDEdit.Text;
                _jbfm.EXPAMT = Convert.ToDecimal(txtExpenseAmountEdit.Text);
                _jbfm.BILLAMT = Convert.ToDecimal(txtBillAmountEdit.Text);
                if (txtFwdDateEdit.Text == "")
                    txtFwdDateEdit.Text = "1900-01-01";
                _jbfm.EXPPDT = DateTime.Parse(txtFwdDateEdit.Text, _dateformat, DateTimeStyles.AssumeLocal);
                _jbfm.REMARKS = txtRemarksEdit.Text;
                if (txtBillSlEdit.Text == "")
                    txtBillSlEdit.Text = "0";
                _jbfm.BILLSL = Convert.ToInt64(txtBillSlEdit.Text);
                //jbfm.InTime = DateTime.Now;
                _jbfm.UpdateTime = DateTime.Now;
                _jbfm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                _jbfm.UpdateuserID = HttpContext.Current.Session["UserName"].ToString();
                _jbfm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                lblErrmsg.Visible = false;

                _jbid.UpdateJobBillInfo(_jbfm);

                gvDetails.EditIndex = -1;

                GridShow();

                TextBox txtExpense = (TextBox)gvDetails.FooterRow.FindControl("txtExpense");
                txtExpense.Focus();
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
                Label lblBillDate = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillDate");
                Label lblSl = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSl");
                Label lblFwdDate = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblFwdDate");
                Label lblBillNo = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillNo");

                Label lblExpenseID = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblExpenseID");
                Label lblExpenseAmount = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblExpenseAmount");
                Label lblBillAmount = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillAmount");
                Label lblRemarks = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblRemarks");
                Label lblBillSl = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillSl");


                _jbfm.COMPID = txtCompanyID.Text;
                _jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                _jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                _jbfm.JOBTP = txtJobType.Text;
                _jbfm.PARTYID = txtPartyID.Text;

                _jbfm.BILLDT = DateTime.Parse(lblBillDate.Text, _dateformat, DateTimeStyles.AssumeLocal);
                _jbfm.BILLNO = Convert.ToInt64(lblBillNo.Text);

                _jbfm.EXPSL = Convert.ToInt64(lblSl.Text);

                _jbfm.EXPID = lblExpenseID.Text;
                _jbfm.EXPAMT = Convert.ToDecimal(lblExpenseAmount.Text);
                _jbfm.BILLAMT = Convert.ToDecimal(lblBillAmount.Text);
                if (lblFwdDate.Text == "")
                    lblFwdDate.Text = "1900-01-01";
                _jbfm.EXPPDT = DateTime.Parse(lblFwdDate.Text, _dateformat, DateTimeStyles.AssumeLocal);
                _jbfm.REMARKS = lblRemarks.Text;
                if (lblBillSl.Text == "")
                    lblBillSl.Text = "0";
                _jbfm.BILLSL = Convert.ToInt64(lblBillSl.Text);
                _jbfm.InTime = DateTime.Now;
                _jbfm.UpdateTime = DateTime.Now;
                _jbfm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                _jbfm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                lblErrmsg.Visible = false;


                string processdt = "";
                string compid = "";
                string jobyy = "";
                string jobtp = "";
                string jobno = "";
                string partyid = "";
                string billdt = "";
                string billno = "";
                string expsl = "";
                string expid = "";
                string expamt = "";
                string billamt = "";
                string exppdt = "";
                string remarks = "";
                string billsl = "";
                string userid = "";
                string updateuserid = "";
                string intime = "";
                string updatetime = "";
                string ipaddress = "";
                string deleteuserpc = "";

                string userName = HttpContext.Current.Session["UserName"].ToString();
                string userpc = HttpContext.Current.Session["PCName"].ToString();
                string ipadd = HttpContext.Current.Session["IpAddress"].ToString();
                

                string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                SqlConnection conn = new SqlConnection(connectionString);
                
                conn.Open();

                SqlCommand cmdselectdata = new SqlCommand("SELECT * FROM CNF_JOBBILL where BILLDT=@BILLDT and BILLNO=@BILLNO and EXPSL=@EXPSL and JOBYY=@JOBYY and JOBTP=@JOBTP and JOBNO=@JOBNO and EXPID=@EXPID", conn);
                cmdselectdata.Parameters.Add("@BILLDT", SqlDbType.SmallDateTime).Value = _jbfm.BILLDT;
                cmdselectdata.Parameters.Add("@BILLNO", SqlDbType.BigInt).Value = _jbfm.BILLNO;
                cmdselectdata.Parameters.Add("@EXPSL", SqlDbType.BigInt).Value = _jbfm.EXPSL;
                cmdselectdata.Parameters.Add("@JOBYY", SqlDbType.BigInt).Value = _jbfm.JOBYY;
                cmdselectdata.Parameters.Add("@JOBTP", SqlDbType.NVarChar).Value = _jbfm.JOBTP;
                cmdselectdata.Parameters.Add("@JOBNO", SqlDbType.BigInt).Value = _jbfm.JOBNO;
                cmdselectdata.Parameters.Add("@EXPID", SqlDbType.NVarChar).Value = _jbfm.EXPID;

                SqlDataReader dr = cmdselectdata.ExecuteReader();
                while (dr.Read())
                {
                    processdt = dr["PROCESSDT"].ToString();
                    compid = dr["COMPID"].ToString();
                    jobyy = dr["JOBYY"].ToString();
                    jobtp = dr["JOBTP"].ToString();
                    jobno = dr["JOBNO"].ToString();
                    partyid = dr["PARTYID"].ToString();
                    billdt = dr["BILLDT"].ToString();
                    billno = dr["BILLNO"].ToString();
                    expsl = dr["EXPSL"].ToString();
                    expid = dr["EXPID"].ToString();
                    expamt = dr["EXPAMT"].ToString();
                    billamt = dr["BILLAMT"].ToString();
                    exppdt = dr["EXPPDT"].ToString();
                    remarks = dr["REMARKS"].ToString();
                    billsl = dr["BILLSL"].ToString();
                    deleteuserpc = dr["USERPC"].ToString();
                    userid = dr["USERID"].ToString();
                    updateuserid = dr["UPDATEUSERID"].ToString();
                    intime = dr["INTIME"].ToString();
                    updatetime = dr["UPDATETIME"].ToString();
                    ipaddress = dr["IPADDRESS"].ToString();
                    
                }
                dr.Close();

                string alldata = processdt + ", " + compid + ", " + jobyy + ", " + jobtp + ", " + jobno + ", " + partyid
                    + ", " + billdt + ", " + billno + ", " + expsl + ", " + expid + ", " + expamt + ", " + billamt + ", " + exppdt
                    + ", " + remarks + ", " + billsl + ", " + deleteuserpc + ", " + userid + ", " + updateuserid + ", " + intime + ", " + updatetime + ", " + ipaddress;

                _jbfm.InTM = DateTime.Now;


                SqlCommand cmdinsert = new SqlCommand("insert into ASL_DLT values('CNF_JOBBILL',@DESCRP,@USERPC,@USERID,@INTIME,@IPADD)", conn);
                cmdinsert.Parameters.AddWithValue("@DESCRP", alldata);
                cmdinsert.Parameters.AddWithValue("@USERPC", userpc);
                cmdinsert.Parameters.AddWithValue("@USERID", userName);
                cmdinsert.Parameters.AddWithValue("@INTIME", _jbfm.InTM);
                cmdinsert.Parameters.AddWithValue("@IPADD", ipadd);

                cmdinsert.ExecuteNonQuery();




                _jbid.RemoveJobBillInfo(_jbfm);

                gvDetails.EditIndex = -1;

                GridShow();

                TextBox txtExpense = (TextBox)gvDetails.FooterRow.FindControl("txtExpense");
                txtExpense.Focus();


            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }


        [WebMethod(), ScriptMethod()]
        public static string[] GetCompletionListExpenseID_Name(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            var cmd = new SqlCommand("", conn);

            string uTp = HttpContext.Current.Session["UserTp"].ToString();

            cmd.CommandType = CommandType.Text;
            if (uTp == "ADMIN")
            {
                cmd.CommandText = ("SELECT (EXPNM + '|' +  EXPID) AS EXP FROM CNF_EXPENSE WHERE ( EXPNM + '|' +  EXPID ) LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT (EXPNM + '|' +  EXPID) AS EXP FROM CNF_EXPENSE WHERE (EXPNM + '|' + EXPID) LIKE '" + prefixText + "%' ");
            }

            conn.Open();
            List<String> completionSet = new List<string>();
            var oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                completionSet.Add(oReader["EXP"].ToString());
            return completionSet.ToArray();

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

                string searchPar = txtExpense.Text;

                int splitter = searchPar.IndexOf("|", StringComparison.Ordinal);
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    var expnm = lineSplit[0];
                    var expID = lineSplit[1];


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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtJobID.Text == "")
            {
                lblErrmsg.Visible = true;
                lblErrmsg.Text = "Select job no.";
                txtJobID.Focus();
            }
            else
            {
                Session["jobno"] = null;
                Session["jobtp"] = null;
                Session["jobyear"] = null;
                Session["compid"] = null;
                Session["partyid"] = null;
                Session["jobdt"] = null;

                Session["jobno"] = txtJobID.Text;
                Session["jobtp"] = txtJobType.Text;
                Session["jobyear"] = txtJobYear.Text;
                Session["compid"] = txtCompanyID.Text;
                Session["partyid"] = txtPartyID.Text;
                Session["jobdt"] = txtReceiveDate.Text;

                Page.ClientScript.RegisterStartupScript(
                      GetType(), "OpenWindow", "window.open('../report/vis-rep/rpt-bill-details.aspx','_newtab');", true);
            }
        }

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