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
using AlchemyAccounting.CNF.model;
using AlchemyAccounting.CNF.dataAccess;

namespace AlchemyAccounting.CNF.ui
{
    public partial class cnf_job_expense : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        SqlConnection conn;
        SqlCommand cmdd;
        cnf_model iob = new cnf_model();
        cnf_data dob = new cnf_data();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                if (!IsPostBack)
                {
                    DateTime td = DateTime.Now;
                    txtExDT.Text = td.ToString("dd/MM/yyyy");
                    string mon = td.ToString("MMM").ToUpper();
                    string year = td.ToString("yy");
                    lblMY.Text = "";
                    lblMY.Text = mon + "-" + year;
                    check_Invoice_No();
                    GridShow();
                    txtJobNo.Focus();
                }
            }
        }

        public void check_Invoice_No()
        {
            lblInvoiceNo.Text = "";
            Global.lblAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "'", lblInvoiceNo);
            if (lblInvoiceNo.Text == "")
            {
                txtNo.Text = "1";
            }
            else
            {
                Int64 trns, ftrns = 0;
                trns = Convert.ToInt64(lblInvoiceNo.Text);
                ftrns = trns + 1;
                txtNo.Text = ftrns.ToString();
            }
        }

        protected void txtExDT_TextChanged(object sender, EventArgs e)
        {
            if (txtExDT.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select a date.";
                txtExDT.Focus();
            }
            else
            {
                lblError.Visible = false;

                DateTime exDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string mon = exDT.ToString("MMM").ToUpper();
                string year = exDT.ToString("yy");
                lblMY.Text = "";
                lblMY.Text = mon + "-" + year;
                if (btnEdit.Text == "Edit")
                {
                    check_Invoice_No();
                    txtJobNo.Focus();
                }
                else
                {
                    string uTp = HttpContext.Current.Session["UserTp"].ToString();
                    string brCD = HttpContext.Current.Session["BrCD"].ToString();
                    if (uTp == "ADMIN")
                    {
                        Global.dropDownAddWithSelect(ddlInvoice, "SELECT TRANSNO FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "'");
                    }
                    else
                    {
                        Global.dropDownAddWithSelect(ddlInvoice, "SELECT TRANSNO FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND COMPID ='" + brCD + "'");
                    }
                    ddlInvoice.Focus();
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
                cmd.CommandText = ("SELECT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%'");
            }
            else
            {
                cmd.CommandText = ("SELECT (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (CONVERT(NVARCHAR(20),JOBNO,103) + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "'");
            }

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["JOBPAR"].ToString());
            return CompletionSet.ToArray();

        }

        protected void txtJobNo_TextChanged(object sender, EventArgs e)
        {
            if (txtJobNo.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select job no.";
                txtJobNo.Focus();
            }
            else
            {
                lblError.Visible = false;

                string jobno = "";
                string jobyear = "";
                string jobtp = "";
                string searchPar = txtJobNo.Text;

                int splitter = searchPar.IndexOf("|");
                if (splitter != -1)
                {
                    string[] lineSplit = searchPar.Split('|');

                    jobno = lineSplit[0];
                    jobyear = lineSplit[1];
                    jobtp = lineSplit[2];

                    txtJobNo.Text = jobno.Trim();
                    txtJobYear.Text = jobyear.Trim();
                    txtJobTP.Text = jobtp.Trim();
                    txtCompID.Text = "";
                    Global.txtAdd("SELECT COMPID FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBYY =" + jobyear + " AND JOBTP ='" + jobtp + "'", txtCompID);
                    txtExpenseNM.Focus();
                }
                else
                {
                    txtJobNo.Text = "";
                    txtJobYear.Text = "";
                    txtJobTP.Text = "";
                    txtCompID.Text = "";
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListExpense(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            // Try to use parameterized inline query/sp to protect sql injection
            SqlCommand cmd = new SqlCommand("", conn);
            if (uTp == "ADMIN")
            {
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202','10201','10203') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%'", conn);
            }
            else
            {
                cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,5) IN ('10202','10201','10203') AND STATUSCD = 'P' and ACCOUNTNM LIKE '" + prefixText + "%' AND (BRANCHCD ='" + brCD + "' OR BRANCHCD IS NULL OR BRANCHCD ='')", conn);
            }
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtExpenseNM_TextChanged(object sender, EventArgs e)
        {
            if (txtExpenseNM.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select expense";
                txtExpenseNM.Focus();
            }
            else
            {
                lblError.Visible = false;

                txtExCD.Text = "";
                Global.txtAdd("SELECT ACCOUNTCD FROM GL_ACCHART WHERE ACCOUNTNM ='" + txtExpenseNM.Text + "' AND STATUSCD = 'P'", txtExCD);
                if (txtExCD.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select expense";
                    txtExpenseNM.Text = "";
                    txtExpenseNM.Focus();
                }
                else
                {
                    if (lblMY.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select date.";
                        txtExDT.Focus();
                    }
                    else if (txtJobNo.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select job no.";
                        txtJobNo.Focus();
                    }
                    else
                    {
                        lblError.Visible = false;
                        iob.ExDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.ExMY = lblMY.Text;
                        iob.JobNo = Convert.ToInt64(txtJobNo.Text);
                        iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                        iob.JobTP = txtJobTP.Text;
                        iob.CompID = txtCompID.Text;
                        iob.ExpenseCD = txtExCD.Text;

                        iob.InTM = DateTime.Now;
                        iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                        iob.Userpc = HttpContext.Current.Session["PCName"].ToString();

                        if (btnEdit.Text == "Edit")
                        {
                            int dcNo = 0;
                            conn = new SqlConnection(Global.connection);
                            conn.Open();

                            cmdd = new SqlCommand(" SELECT max(TRANSNO) AS TRANSNO FROM CNF_JOBEXPMST where TRANSMY='" + lblMY.Text + "'", conn);
                            SqlDataReader daIN = cmdd.ExecuteReader();
                            if (daIN.Read())
                            {
                                string trNo = daIN["TRANSNO"].ToString();
                                if (trNo == "")
                                    trNo = "0";
                                else
                                    trNo = daIN["TRANSNO"].ToString();

                                int trans, Ftrans;
                                if (Convert.ToInt16(trNo) >= Convert.ToInt16(txtNo.Text))
                                {
                                    trans = Convert.ToInt16(trNo);
                                    Ftrans = trans + 1;
                                    dcNo = Ftrans;
                                    txtNo.Text = dcNo.ToString();
                                }
                                else
                                {
                                    dcNo = Convert.ToInt16(txtNo.Text);
                                }
                            }
                            daIN.Close();
                            conn.Close();

                            iob.InvoiceNO = dcNo;
                            iob.UserNM = HttpContext.Current.Session["UserName"].ToString();
                            dob.save_cnf_job_expense_master(iob);
                            txtRemarks.Focus();
                        }
                        else
                        {
                            if (ddlInvoice.Text == "Select" || ddlInvoice.Text == "")
                            {
                                lblError.Visible = true;
                                lblError.Text = "Select Doc No.";
                                ddlInvoice.Focus();
                            }
                            else
                            {
                                iob.UpTM = DateTime.Now;
                                iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
                                iob.UpdateUser = HttpContext.Current.Session["UserName"].ToString();

                                dob.update_cnf_job_expense_master(iob);
                                dob.update_cnf_job_expense_top(iob);
                                txtRemarks.Focus();
                            }
                        }


                        //conn.Open();
                        //cmdd = new SqlCommand("SELECT * FROM CNF_JOBEXPMST where TRANSMY='" + lblMY.Text + "' AND TRANSNO = " + iob.InvoiceNO + "", conn);
                        //SqlDataAdapter da1 = new SqlDataAdapter(cmdd);
                        //DataSet ds1 = new DataSet();
                        //da1.Fill(ds1);
                        //conn.Close();

                        //if (ds1.Tables[0].Rows.Count > 0)
                        //{
                        //    //dob.updateCollectorCollectionInfo(iob);
                        //}
                        //else
                        //{

                        //}


                    }
                }
            }
        }

        protected void txtRemarks_TextChanged(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (lblMY.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select date.";
                    txtExDT.Focus();
                }
                else if (txtJobNo.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select job no.";
                    txtJobNo.Focus();
                }
                else if (txtExCD.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select expense.";
                    txtExpenseNM.Focus();
                }
                else
                {
                    lblError.Visible = false;
                    iob.ExDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.ExMY = lblMY.Text;
                    if (btnEdit.Text == "Edit")
                        iob.InvoiceNO = Convert.ToInt64(txtNo.Text);
                    else
                        iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);

                    iob.JobNo = Convert.ToInt64(txtJobNo.Text);
                    iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                    iob.JobTP = txtJobTP.Text;
                    iob.CompID = txtCompID.Text;
                    iob.ExpenseCD = txtExCD.Text;
                    iob.RemarksTOP = txtRemarks.Text;

                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.Userpc = HttpContext.Current.Session["PCName"].ToString();

                    iob.UserNM = HttpContext.Current.Session["UserName"].ToString();

                    dob.update_cnf_job_expense_remarks_master(iob);

                    TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
                    txtParticulars.Focus();
                }

            }
        }

        private void GridShow()
        {

            conn = new SqlConnection(Global.connection);

            if (btnEdit.Text == "Edit")
            {
                if (txtNo.Text == "")
                    iob.InvoiceNO = 0;
                else
                    iob.InvoiceNO = Convert.ToInt64(txtNo.Text);
            }
            else
            {
                if (ddlInvoice.Text == "Select" || ddlInvoice.Text == "")
                {
                    iob.InvoiceNO = 0;
                }
                else
                    iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
            }

            string jobno, jobyear = "";
            if (txtJobNo.Text == "")
                jobno = "0";
            else
                jobno = txtJobNo.Text;
            if (txtJobYear.Text == "")
                jobyear = "0";
            else
                jobyear = txtJobYear.Text;

            conn.Open();
            cmdd = new SqlCommand("SELECT CNF_JOBEXPMST.TRANSDT, CNF_JOBEXPMST.TRANSMY, CNF_JOBEXPMST.TRANSNO, CNF_JOBEXPMST.COMPID, CNF_JOBEXPMST.JOBYY, " +
                      " CNF_JOBEXPMST.JOBTP, CNF_JOBEXPMST.JOBNO, CNF_JOBEXPMST.EXPCD, CNF_JOBEXPMST.REMARKS AS REMARKS_TOP, CNF_JOBEXP.SLNO, " +
                      " CNF_JOBEXP.EXPID, CNF_JOBEXP.EXPAMT, CNF_JOBEXP.REMARKS AS REMARKS_BOT, CNF_EXPENSE.EXPNM FROM CNF_JOBEXPMST INNER JOIN " +
                      " CNF_JOBEXP ON CNF_JOBEXPMST.TRANSMY = CNF_JOBEXP.TRANSMY AND CNF_JOBEXPMST.TRANSNO = CNF_JOBEXP.TRANSNO INNER JOIN " +
                      " CNF_EXPENSE ON CNF_JOBEXP.EXPID = CNF_EXPENSE.EXPID WHERE (CNF_JOBEXP.TRANSMY = '" + lblMY.Text + "') AND (CNF_JOBEXP.TRANSNO = " + iob.InvoiceNO + ") " +
                      " AND (CNF_JOBEXP.JOBNO ='" + jobno + "') AND (CNF_JOBEXP.JOBTP ='" + txtJobTP.Text + "') AND (CNF_JOBEXP.JOBYY ='" + jobyear + "')" +
                      " ORDER BY CNF_JOBEXP.SLNO", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmdd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                gvDetails.Visible = true;

                if (gvDetails.EditIndex == -1)
                {
                    Decimal totAmt = 0;
                    Decimal a = 0;
                    foreach (GridViewRow grid in gvDetails.Rows)
                    {
                        Label Per = (Label)grid.Cells[3].FindControl("lblAmount");
                        if (Per.Text == "")
                        {
                            Per.Text = "0";
                        }
                        else
                        {
                            Per.Text = Per.Text;
                        }
                        String Perf = Per.Text;
                        totAmt = Convert.ToDecimal(Perf);
                        a += totAmt;
                        txtTotalAmount.Text = a.ToString();
                    }
                    a += totAmt;
                }

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

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (btnEdit.Text == "Edit")
                {
                    if (txtNo.Text == "")
                        iob.InvoiceNO = 0;
                    else
                        iob.InvoiceNO = Convert.ToInt64(txtNo.Text);
                }
                else
                {
                    if (ddlInvoice.Text == "Select" || ddlInvoice.Text == "")
                    {
                        iob.InvoiceNO = 0;
                    }
                    else
                        iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
                }

                Global.lblAdd("SELECT MAX(SLNO) AS SLNO FROM CNF_JOBEXP WHERE TRANSMY ='" + lblMY.Text + "' AND TRANSNO ='" + iob.InvoiceNO + "'", lblExsl);

                int sl, fSl = 0;

                if (lblExsl.Text == "")
                {
                    e.Row.Cells[0].Text = "1";
                }
                else
                {
                    sl = Convert.ToInt16(lblExsl.Text);
                    fSl = sl + 1;

                    e.Row.Cells[0].Text = fSl.ToString();
                }
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListParticulars(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("", conn);

            //string uTp = HttpContext.Current.Session["UserTp"].ToString();
            //string brCD = HttpContext.Current.Session["BrCD"].ToString();

            cmd.CommandType = CommandType.Text;
            //if (uTp == "ADMIN")
            //{
            cmd.CommandText = ("SELECT EXPNM FROM CNF_EXPENSE WHERE EXPNM LIKE '" + prefixText + "%'");
            //}
            //else
            //{
            //    cmd.CommandText = ("SELECT (JOBNO + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) AS JOBPAR FROM CNF_JOB WHERE (JOBNO + '|' + CONVERT(NVARCHAR(20),JOBYY,103) + '|' + JOBTP) LIKE '" + prefixText + "%' AND COMPID ='" + brCD + "'");
            //}

            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["EXPNM"].ToString());
            return CompletionSet.ToArray();

        }

        protected void txtParticulars_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtParticulars = (TextBox)row.FindControl("txtParticulars");
            TextBox txtCode = (TextBox)row.FindControl("txtCode");
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");

            if (txtParticulars.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select particulars.";
                txtParticulars.Focus();
            }
            else
            {
                txtCode.Text = "";
                Global.txtAdd("SELECT EXPID FROM CNF_EXPENSE WHERE EXPNM ='" + txtParticulars.Text + "'", txtCode);
                if (txtCode.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select particulars.";
                    txtParticulars.Text = "";
                    txtParticulars.Focus();
                }
                else
                    txtAmount.Focus();
            }
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["UserName"] == null)
                Response.Redirect("~/Login/UI/Login.aspx");
            else
            {
                string serial = gvDetails.FooterRow.Cells[0].Text;
                TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
                TextBox txtCode = (TextBox)gvDetails.FooterRow.FindControl("txtCode");
                TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");
                TextBox txtRemarksG = (TextBox)gvDetails.FooterRow.FindControl("txtRemarksG");

                if (e.CommandName.Equals("SaveCon"))
                {
                    if (lblMY.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select date.";
                        txtExDT.Focus();
                    }
                    else if (txtJobNo.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select job no.";
                        txtJobNo.Focus();
                    }
                    else if (txtExCD.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select expense.";
                        txtExCD.Focus();
                    }
                    else if (txtCode.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select particulars.";
                        txtParticulars.Focus();
                    }
                    else if (txtAmount.Text == "" || txtAmount.Text == ".00" || txtAmount.Text == "0")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Type amount.";
                        txtAmount.Focus();
                    }
                    else
                    {
                        iob.ExDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

                        iob.ExMY = lblMY.Text;
                        if (btnEdit.Text == "Edit")
                            iob.InvoiceNO = Convert.ToInt64(txtNo.Text);
                        else
                            iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
                        iob.JobNo = Convert.ToInt64(txtJobNo.Text);
                        iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                        iob.JobTP = txtJobTP.Text;
                        iob.CompID = txtCompID.Text;
                        iob.ExpenseCD = txtExCD.Text;
                        iob.RemarksTOP = txtRemarks.Text;
                        iob.Sl = Convert.ToInt64(serial);
                        iob.ExpensesID = txtCode.Text;
                        iob.Amount = Convert.ToDecimal(txtAmount.Text);
                        iob.RemarksBOT = txtRemarksG.Text;

                        iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                        iob.Userpc = HttpContext.Current.Session["PCName"].ToString();

                        iob.InTM = DateTime.Now;
                        iob.UserNM = HttpContext.Current.Session["UserName"].ToString();
                        dob.save_cnf_job_expense(iob);

                        GridShow();
                        txtParticulars.Focus();
                    }
                }
                else if (e.CommandName.Equals("Complete"))
                {
                    if (lblMY.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select date.";
                        txtExDT.Focus();
                    }
                    else if (txtJobNo.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select job no.";
                        txtJobNo.Focus();
                    }
                    else if (txtExCD.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select expense.";
                        txtExCD.Focus();
                    }
                    else if (txtCode.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select particulars.";
                        txtParticulars.Focus();
                    }
                    else if (txtAmount.Text == "" || txtAmount.Text == ".00" || txtAmount.Text == "0")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Type amount.";
                        txtAmount.Focus();
                    }
                    else
                    {
                        iob.ExDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        iob.ExMY = lblMY.Text;
                        if (btnEdit.Text == "Edit")
                            iob.InvoiceNO = Convert.ToInt64(txtNo.Text);
                        else
                            iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
                        iob.JobNo = Convert.ToInt64(txtJobNo.Text);
                        iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                        iob.JobTP = txtJobTP.Text;
                        iob.CompID = txtCompID.Text;
                        iob.ExpenseCD = txtExCD.Text;
                        iob.RemarksTOP = txtRemarks.Text;
                        iob.Sl = Convert.ToInt64(serial);
                        iob.ExpensesID = txtCode.Text;
                        iob.Amount = Convert.ToDecimal(txtAmount.Text);
                        iob.RemarksBOT = txtRemarksG.Text;

                        iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                        iob.Userpc = HttpContext.Current.Session["PCName"].ToString();

                        iob.InTM = DateTime.Now;
                        iob.UserNM = HttpContext.Current.Session["UserName"].ToString();
                        dob.save_cnf_job_expense(iob);

                        Refresh();
                    }
                }

                else if (e.CommandName.Equals("SavePrint"))
                {

                    RefreshSession();

                    if (lblMY.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select date.";
                        txtExDT.Focus();
                    }
                    else if (txtJobNo.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select job no.";
                        txtJobNo.Focus();
                    }
                    else if (txtExCD.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select expense.";
                        txtExCD.Focus();
                    }
                    else if (txtCode.Text == "")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Select particulars.";
                        txtParticulars.Focus();
                    }
                    else if (txtAmount.Text == "" || txtAmount.Text == ".00" || txtAmount.Text == "0")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Type amount.";
                        txtAmount.Focus();
                    }
                    else
                    {
                        iob.ExDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        Session["Jobdate"] = txtExDT.Text;

                        iob.ExMY = lblMY.Text;
                        Session["transmy"] = iob.ExMY.ToString();

                        if (btnEdit.Text == "Edit")
                        {
                            iob.InvoiceNO = Convert.ToInt64(txtNo.Text);
                            Session["InvoiceNo"] = iob.InvoiceNO;
                        }
                        else
                        {
                            iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
                            Session["InvoiceNo"] = iob.InvoiceNO;
                        }

                        iob.JobNo = Convert.ToInt64(txtJobNo.Text);
                        Session["JobNo"] = iob.JobNo;

                        iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                        Session["Jobyear"] = iob.JobYear;

                        iob.JobTP = txtJobTP.Text;
                        Session["JobType"] = iob.JobTP;

                        iob.CompID = txtCompID.Text;
                        Session["Compid"] = iob.CompID;

                        iob.ExpenseCD = txtExCD.Text;
                        Session["expenseCD"] = iob.ExpenseCD;

                        iob.RemarksTOP = txtRemarks.Text;
                        Session["remarksT"] = iob.RemarksTOP;


                        iob.Sl = Convert.ToInt64(serial);
                        iob.ExpensesID = txtCode.Text;
                        iob.Amount = Convert.ToDecimal(txtAmount.Text);
                        iob.RemarksBOT = txtRemarksG.Text;

                        iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                        iob.Userpc = HttpContext.Current.Session["PCName"].ToString();

                        iob.InTM = DateTime.Now;
                        iob.UserNM = HttpContext.Current.Session["UserName"].ToString();
                        dob.save_cnf_job_expense(iob);

                        Refresh();

                        Page.ClientScript.RegisterStartupScript(
                        this.GetType(), "OpenWindow", "window.open('../report/vis-rep/RptExpense_VR.aspx','_newtab');", true);
                    }
                }
            }
        }

        public void RefreshSession()
        {
            Session["Jobdate"] = "";
            Session["transmy"] = "";
            Session["InvoiceNo"] = "";
            Session["JobNo"] = "";
            Session["Jobyear"] = "";
            Session["JobType"] = "";
            Session["Compid"] = "";
            Session["expenseCD"] = "";
            Session["remarksT"] = "";
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

            TextBox txtParticularsEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtParticularsEdit");
            txtParticularsEdit.Focus();
        }

        protected void txtParticularsEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtParticularsEdit = (TextBox)row.FindControl("txtParticularsEdit");
            TextBox txtCodeEdit = (TextBox)row.FindControl("txtCodeEdit");
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");

            if (txtParticularsEdit.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select particulars.";
                txtParticularsEdit.Focus();
            }
            else
            {
                txtCodeEdit.Text = "";
                Global.txtAdd("SELECT EXPID FROM CNF_EXPENSE WHERE EXPNM ='" + txtParticularsEdit.Text + "'", txtCodeEdit);
                if (txtCodeEdit.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select particulars.";
                    txtParticularsEdit.Text = "";
                    txtParticularsEdit.Focus();
                }
                else
                    txtAmountEdit.Focus();
            }
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                Label lblSlEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSlEdit");
                TextBox txtParticularsEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtParticularsEdit");
                TextBox txtCodeEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCodeEdit");
                TextBox txtAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAmountEdit");
                TextBox txtRemarksGEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksGEdit");

                if (lblMY.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select date.";
                    txtExDT.Focus();
                }
                else if (txtJobNo.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select job no.";
                    txtJobNo.Focus();
                }
                else if (txtExCD.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select expense.";
                    txtExCD.Focus();
                }
                else if (txtCodeEdit.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select particulars.";
                    txtParticularsEdit.Focus();
                }
                else if (txtAmountEdit.Text == "" || txtAmountEdit.Text == ".00" || txtAmountEdit.Text == "0")
                {
                    lblError.Visible = true;
                    lblError.Text = "Type amount.";
                    txtAmountEdit.Focus();
                }
                else
                {
                    iob.ExDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.ExMY = lblMY.Text;
                    if (btnEdit.Text == "Edit")
                        iob.InvoiceNO = Convert.ToInt64(txtNo.Text);
                    else
                        iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
                    iob.JobNo = Convert.ToInt64(txtJobNo.Text);
                    iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                    iob.JobTP = txtJobTP.Text;
                    iob.CompID = txtCompID.Text;
                    iob.ExpenseCD = txtExCD.Text;
                    iob.RemarksTOP = txtRemarks.Text;
                    iob.Sl = Convert.ToInt64(lblSlEdit.Text);
                    iob.ExpensesID = txtCodeEdit.Text;
                    iob.Amount = Convert.ToDecimal(txtAmountEdit.Text);
                    iob.RemarksBOT = txtRemarksGEdit.Text;

                    iob.Ipaddress = HttpContext.Current.Session["IpAddress"].ToString();
                    iob.Userpc = HttpContext.Current.Session["PCName"].ToString();
                    iob.UpdateUser = HttpContext.Current.Session["UserName"].ToString();
                    iob.UpTM = DateTime.Now;

                    dob.update_cnf_job_expense(iob);

                    gvDetails.EditIndex = -1;
                    GridShow();

                    TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
                    txtParticulars.Focus();
                }
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                Label lblSL = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblSL");

                if (lblMY.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select date.";
                    txtExDT.Focus();
                }
                else if (txtJobNo.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select job no.";
                    txtJobNo.Focus();
                }
                else if (txtExCD.Text == "")
                {
                    lblError.Visible = true;
                    lblError.Text = "Select expense.";
                    txtExCD.Focus();
                }
                else
                {
                    iob.ExDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.ExMY = lblMY.Text;
                    if (btnEdit.Text == "Edit")
                        iob.InvoiceNO = Convert.ToInt64(txtNo.Text);
                    else
                        iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
                    iob.JobNo = Convert.ToInt64(txtJobNo.Text);
                    iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                    iob.JobTP = txtJobTP.Text;
                    iob.CompID = txtCompID.Text;
                    iob.ExpenseCD = txtExCD.Text;

                    iob.Sl = Convert.ToInt64(lblSL.Text);

                    conn = new SqlConnection(Global.connection);
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM CNF_JOBEXP where TRANSMY='" + iob.ExMY + "' AND TRANSNO = " + iob.InvoiceNO + "", conn);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    conn.Close();
                    if (ds1.Tables[0].Rows.Count > 1)
                    {
                        dob.delete_cnf_job_expense(iob);
                        GridShow();
                    }
                    else
                    {
                        dob.delete_cnf_job_expense(iob);
                        dob.delete_cnf_job_expense_master(iob);

                        if (btnEdit.Text == "Edit")
                        {
                            Refresh();
                        }
                        else
                        {
                            Global.dropDownAddWithSelect(ddlInvoice, "SELECT TRANSNO FROM CNF_JOBEXPMST WHERE TRANSMY='" + iob.ExMY + "'  ORDER BY TRANSNO");
                            ddlInvoice.SelectedIndex = -1;
                            Refresh();
                        }
                        GridShow();
                    }
                }
            }
        }

        private void Refresh()
        {
            lblError.Visible = false;
            txtJobNo.Text = "";
            txtJobYear.Text = "";
            txtJobTP.Text = "";
            txtCompID.Text = "";
            txtExCD.Text = "";
            txtExpenseNM.Text = "";
            txtRemarks.Text = "";
            txtTotalAmount.Text = ".00";
            if (btnEdit.Text == "Edit")
                check_Invoice_No();
            else
                ddlInvoice.SelectedIndex = -1;

            GridShow();
            txtJobNo.Focus();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                lblError.Visible = false;
                btnEdit.Text = "New";
                btnPrint.Visible = true;
                txtNo.Visible = false;
                ddlInvoice.Visible = true;

                string uTp = HttpContext.Current.Session["UserTp"].ToString();
                string brCD = HttpContext.Current.Session["BrCD"].ToString();

                if (uTp == "ADMIN")
                {
                    Global.dropDownAddWithSelect(ddlInvoice, "SELECT TRANSNO FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "'");
                }
                else
                {
                    Global.dropDownAddWithSelect(ddlInvoice, "SELECT TRANSNO FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND COMPID ='" + brCD + "'");
                }
                ddlInvoice.Focus();
            }
            else
            {
                lblError.Visible = false;
                btnEdit.Text = "Edit";
                btnPrint.Visible = false;
                txtNo.Visible = true;
                ddlInvoice.Visible = false;
                Refresh();
                txtJobNo.Focus();
            }
        }

        protected void ddlInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInvoice.Text == "Select")
            {
                Refresh();
                lblError.Visible = true;
                lblError.Text = "Select invoice";
                ddlInvoice.Focus();
            }
            else
            {
                lblError.Visible = false;
                Global.txtAdd("SELECT CONVERT(NVARCHAR(20),TRANSDT,103)AS TRANSDT FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND TRANSNO =" + ddlInvoice.Text + "", txtExDT);
                Global.txtAdd("SELECT JOBNO FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND TRANSNO =" + ddlInvoice.Text + "", txtJobNo);
                Global.txtAdd("SELECT JOBYY FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND TRANSNO =" + ddlInvoice.Text + "", txtJobYear);
                Global.txtAdd("SELECT JOBTP FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND TRANSNO =" + ddlInvoice.Text + "", txtJobTP);
                Global.txtAdd("SELECT COMPID FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND TRANSNO =" + ddlInvoice.Text + "", txtCompID);
                Global.txtAdd("SELECT EXPCD FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND TRANSNO =" + ddlInvoice.Text + "", txtExCD);
                Global.txtAdd("SELECT GL_ACCHART.ACCOUNTNM FROM CNF_JOBEXPMST INNER JOIN GL_ACCHART ON CNF_JOBEXPMST.EXPCD = GL_ACCHART.ACCOUNTCD " +
                    " WHERE CNF_JOBEXPMST.TRANSMY ='" + lblMY.Text + "' AND CNF_JOBEXPMST.TRANSNO =" + ddlInvoice.Text + " AND GL_ACCHART.STATUSCD = 'P'", txtExpenseNM);
                Global.txtAdd("SELECT REMARKS FROM CNF_JOBEXPMST WHERE TRANSMY ='" + lblMY.Text + "' AND TRANSNO =" + ddlInvoice.Text + "", txtRemarks);
                GridShow();

                TextBox txtParticulars = (TextBox)gvDetails.FooterRow.FindControl("txtParticulars");
                txtParticulars.Focus();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            RefreshSession();

            if (lblMY.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select date.";
                txtExDT.Focus();
            }
            else if (txtJobNo.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select job no.";
                txtJobNo.Focus();
            }
            else if (txtExCD.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select expense.";
                txtExCD.Focus();
            }
            else if (ddlInvoice.Text == "Select" || ddlInvoice.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Select expense.";
                ddlInvoice.Focus();
            }
            else
            {
                iob.ExDT = DateTime.Parse(txtExDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                Session["Jobdate"] = txtExDT.Text;

                iob.ExMY = lblMY.Text;
                Session["transmy"] = iob.ExMY.ToString();

                iob.InvoiceNO = Convert.ToInt64(ddlInvoice.Text);
                Session["InvoiceNo"] = iob.InvoiceNO;

                iob.JobNo = Convert.ToInt64(txtJobNo.Text);
                Session["JobNo"] = iob.JobNo;

                iob.JobYear = Convert.ToInt16(txtJobYear.Text);
                Session["Jobyear"] = iob.JobYear;

                iob.JobTP = txtJobTP.Text;
                Session["JobType"] = iob.JobTP;

                iob.CompID = txtCompID.Text;
                Session["Compid"] = iob.CompID;

                iob.ExpenseCD = txtExCD.Text;
                Session["expenseCD"] = iob.ExpenseCD;

                iob.RemarksTOP = txtRemarks.Text;
                Session["remarksT"] = iob.RemarksTOP;

                Page.ClientScript.RegisterStartupScript(
                this.GetType(), "OpenWindow", "window.open('../report/vis-rep/RptExpense_VR.aspx','_newtab');", true);
            }
        }
    }
}
