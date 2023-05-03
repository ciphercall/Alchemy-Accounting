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
    public partial class JobBillInformation : System.Web.UI.Page
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

        private void GridShow()
        {
            conn = new SqlConnection(Global.connection);
            conn.Open();

            cmdd = new SqlCommand("SELECT CONVERT(NVARCHAR(20), dbo.CNF_JOBBILL.BILLDT, 103) AS BILLD, dbo.CNF_JOBBILL.BILLNO, dbo.CNF_JOBBILL.EXPSL, dbo.CNF_JOBBILL.EXPID, " +
                   " dbo.CNF_JOBBILL.EXPAMT, dbo.CNF_JOBBILL.BILLAMT, (CASE WHEN EXPPDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),dbo.CNF_JOBBILL.EXPPDT, 103) END) AS EXPPD, dbo.CNF_JOBBILL.REMARKS, dbo.CNF_JOBBILL.BILLSL, dbo.CNF_EXPENSE.EXPNM " +
                   " FROM dbo.CNF_JOBBILL INNER JOIN dbo.CNF_EXPENSE ON dbo.CNF_JOBBILL.EXPID = dbo.CNF_EXPENSE.EXPID WHERE (dbo.CNF_JOBBILL.JOBNO = " + txtJobID.Text + ") AND (dbo.CNF_JOBBILL.JOBTP = '" + txtJobType.Text + "') AND (dbo.CNF_JOBBILL.JOBYY = '" + txtJobYear.Text + "') AND (dbo.CNF_JOBBILL.COMPID ='" + txtCompanyID.Text + "')" +
                   " ORDER BY CNF_JOBBILL.EXPSL", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();


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
                cmd.CommandText = ("SELECT DISTINCT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOBBILL WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT DISTINCT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOBBILL WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "'");
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

                        jbfm.COMPID = txtCompanyID.Text;
                        jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                        jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                        jbfm.JOBTP = txtJobType.Text;
                        jbfm.PARTYID = txtPartyID.Text;
                        jbfm.BILLDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        jbfm.BILLNO = Convert.ToInt64(txtBillNo.Text);

                        DateTime FRDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string FDT = FRDT.ToString("yyyy-MM-dd");

                        Global.lblAdd("select MAX (EXPSL) from CNF_JOBBILL where BILLDT='" + FDT + "' and BILLNO='" + txtBillNo.Text + "' ", lblChkInternalID);

                        if (lblChkInternalID.Text == "")
                        {
                            string cid = "1";
                            lblSlItem.Text = cid;
                            jbfm.EXPSL = Convert.ToInt64(cid);
                        }

                        else
                        {
                            var id = Int64.Parse(lblChkInternalID.Text) + 1;
                            lblSlItem.Text = id.ToString();
                            jbfm.EXPSL = Convert.ToInt64(lblSlItem.Text);
                        }

                        jbfm.EXPID = txtExpenseID.Text;
                        jbfm.EXPAMT = Convert.ToDecimal(txtExpenseAmount.Text);
                        if (txtBillAmount.Text == "")
                            txtBillAmount.Text = "0";
                        jbfm.BILLAMT = Convert.ToDecimal(txtBillAmount.Text);
                        if (txtFwdDate.Text == "")
                            txtFwdDate.Text = "1900-01-01";
                        jbfm.EXPPDT = DateTime.Parse(txtFwdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        jbfm.REMARKS = txtRemarks.Text;
                        if (txtBillSl.Text == "")
                            txtBillSl.Text = "0";
                        jbfm.BILLSL = Convert.ToInt64(txtBillSl.Text);
                        jbfm.InTime = DateTime.Now;
                        //jbfm.UpdateTime = DateTime.Now;
                        jbfm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                        jbfm.Userid = HttpContext.Current.Session["UserName"].ToString();
                        jbfm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                        jbid.SaveJobBillInfo(jbfm);

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
                TextBox txtFwdDate = (TextBox)e.Row.FindControl("txtFwdDate");
                TextBox txtBillNo = (TextBox)e.Row.FindControl("txtBillNo");

                TextBox txtExpenseID = (TextBox)e.Row.FindControl("txtExpenseID");
                TextBox txtExpense = (TextBox)e.Row.FindControl("txtExpense");
                TextBox txtExpenseAmount = (TextBox)e.Row.FindControl("txtExpenseAmount");
                TextBox txtBillAmount = (TextBox)e.Row.FindControl("txtBillAmount");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                TextBox txtBillSl = (TextBox)e.Row.FindControl("txtBillSl");



                //DateTime today = DateTime.Now;
                //string td = Global.Dayformat(today);
                //txtBillDate.Text = td;

                //txtFwdDate.Text = td;

                //string mon = today.ToString("MMM").ToUpper();
                //string year = today.ToString("yyyy");
                if (txtReceiveDate.Text == "")
                {
                    txtBillDate.Text = "1900-01-01";
                }
                else
                    txtBillDate.Text = txtReceiveDate.Text;

                DateTime FRDT = DateTime.Parse(txtBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string FDT = FRDT.ToString("yyyy-MM-dd");

                Global.lblAdd("select MAX (EXPSL) from CNF_JOBBILL where BILLDT='" + FDT + "' and BILLNO='" + txtBillNo.Text + "' ", lblChkInternalID);

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
                TextBox txtExpenseEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtExpenseEdit");
                TextBox txtExpenseAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtExpenseAmountEdit");
                TextBox txtBillAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBillAmountEdit");
                TextBox txtRemarksEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksEdit");
                TextBox txtBillSlEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtBillSlEdit");


                jbfm.COMPID = txtCompanyID.Text;
                jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                jbfm.JOBTP = txtJobType.Text;
                jbfm.PARTYID = txtPartyID.Text;

                jbfm.BILLDT = DateTime.Parse(lblBillDateEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jbfm.BILLNO = Convert.ToInt64(txtBillNoEdit.Text);

                jbfm.EXPSL = Convert.ToInt64(lblSlEdit.Text);

                jbfm.EXPID = txtExpenseIDEdit.Text;
                jbfm.EXPAMT = Convert.ToDecimal(txtExpenseAmountEdit.Text);
                jbfm.BILLAMT = Convert.ToDecimal(txtBillAmountEdit.Text);
                if (txtFwdDateEdit.Text == "")
                    txtFwdDateEdit.Text = "1900-01-01";
                jbfm.EXPPDT = DateTime.Parse(txtFwdDateEdit.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jbfm.REMARKS = txtRemarksEdit.Text;
                if (txtBillSlEdit.Text == "")
                    txtBillSlEdit.Text = "0";
                jbfm.BILLSL = Convert.ToInt64(txtBillSlEdit.Text);
                //jbfm.InTime = DateTime.Now;
                jbfm.UpdateTime = DateTime.Now;
                jbfm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                jbfm.UpdateuserID = HttpContext.Current.Session["UserName"].ToString();
                jbfm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                lblErrmsg.Visible = false;

                jbid.UpdateJobBillInfo(jbfm);

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
                Label lblExpense = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblExpense");
                Label lblExpenseAmount = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblExpenseAmount");
                Label lblBillAmount = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillAmount");
                Label lblRemarks = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblRemarks");
                Label lblBillSl = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblBillSl");


                jbfm.COMPID = txtCompanyID.Text;
                jbfm.JOBYY = Convert.ToInt64(txtJobYear.Text);
                jbfm.JOBNO = Convert.ToInt64(txtJobID.Text);
                jbfm.JOBTP = txtJobType.Text;
                jbfm.PARTYID = txtPartyID.Text;

                jbfm.BILLDT = DateTime.Parse(lblBillDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jbfm.BILLNO = Convert.ToInt64(lblBillNo.Text);

                jbfm.EXPSL = Convert.ToInt64(lblSl.Text);

                jbfm.EXPID = lblExpenseID.Text;
                jbfm.EXPAMT = Convert.ToDecimal(lblExpenseAmount.Text);
                jbfm.BILLAMT = Convert.ToDecimal(lblBillAmount.Text);
                if (lblFwdDate.Text == "")
                    lblFwdDate.Text = "1900-01-01";
                jbfm.EXPPDT = DateTime.Parse(lblFwdDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                jbfm.REMARKS = lblRemarks.Text;
                if (lblBillSl.Text == "")
                    lblBillSl.Text = "0";
                jbfm.BILLSL = Convert.ToInt64(lblBillSl.Text);
                jbfm.InTime = DateTime.Now;
                jbfm.UpdateTime = DateTime.Now;
                jbfm.UserPC = HttpContext.Current.Session["PCName"].ToString();
                jbfm.IPAddress = HttpContext.Current.Session["IpAddress"].ToString();

                lblErrmsg.Visible = false;

                jbid.RemoveJobBillInfo(jbfm);

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
                      this.GetType(), "OpenWindow", "window.open('../report/vis-rep/rpt-bill-details.aspx','_newtab');", true);
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