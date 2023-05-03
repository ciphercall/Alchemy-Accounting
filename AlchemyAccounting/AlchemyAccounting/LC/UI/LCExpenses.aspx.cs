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
using System.Text.RegularExpressions;

namespace AlchemyAccounting.LC.UI
{
    public partial class LCExpenses : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
            }
            else
            {
                LC_Start();
            }
        }

        public void LC_Start()
        {
            DateTime today = DateTime.Today.Date;
            string td = Global.Dayformat(today);
            txtDate.Text = td;
            DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");

            string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
            string year = today.ToString("yy");
            lblMY.Text = mon + "-" + year;
            Global.lblAdd(@"Select max(TRANSNO) FROM LC_EXPMST where TRANSMY='" + lblMY.Text + "' and TRANSTP = 'MPAY'", lblMxNo);
            if (lblMxNo.Text == "")
            {
                txtNo.Text = "1";
            }
            else
            {
                int iNo = int.Parse(lblMxNo.Text);
                int totIno = iNo + 1;
                txtNo.Text = totIno.ToString();
            }

            txtLCName.Focus();

            GridExpensesShow();
        }

        protected void GridExpensesShow()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT LC_EXPENSE.TRANSTP, LC_EXPENSE.TRANSDT, LC_EXPENSE.TRANSMY, LC_EXPENSE.TRANSNO, LC_EXPENSE.LCCD, LC_EXPENSE.LCINVNO, LC_EXPENSE.CHARGESL, LC_EXPENSE.CHARGEID, LC_EXPENSE.AMOUNT, LC_EXPENSE.CNBCD, LC_EXPENSE.REMARKS, LC_EXPENSE.USERPC, " +
                                            " LC_EXPENSE.USERID, LC_EXPENSE.INTIME, LC_EXPENSE.IPADDRESS, LC_CHARGE.CHARGENM, GL_ACCHART.ACCOUNTNM FROM LC_EXPMST INNER JOIN LC_EXPENSE ON LC_EXPMST.TRANSTP = LC_EXPENSE.TRANSTP AND LC_EXPMST.TRANSMY = LC_EXPENSE.TRANSMY AND " +
                                            " LC_EXPMST.TRANSNO = LC_EXPENSE.TRANSNO INNER JOIN LC_CHARGE ON LC_EXPENSE.CHARGEID = LC_CHARGE.CHARGEID INNER JOIN GL_ACCHART ON LC_EXPENSE.CNBCD = GL_ACCHART.ACCOUNTCD WHERE LC_EXPMST.TRANSTP = 'MPAY' AND LC_EXPMST.TRANSDT = '" + TrDt + "' AND LC_EXPMST.TRANSMY = '" + lblMY.Text + "' AND LC_EXPMST.TRANSNO = " + txtNo.Text + " AND " +
                                            " LC_EXPMST.LCCD = '" + txtLCCD.Text + "' ORDER BY LC_EXPENSE.CHARGESL", conn);
            //AND LC_EXPMST.LCINVNO = '" + txtInvoiceNo.Text + "'
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                
                //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'",txtInNo);
                TextBox txtChargeNM = (TextBox)gvDetails.FooterRow.FindControl("txtChargeNM");
                txtChargeNM.Focus();
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

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            if (btnExpenseEdit.Text == "EDIT")
            {
                DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string mon = transdate.ToString("MMM").ToUpper();
                string year = transdate.ToString("yy");
                lblMY.Text = mon + "-" + year;
                Global.lblAdd(@"Select max(TRANSNO) FROM LC_EXPMST where TRANSMY='" + lblMY.Text + "' and TRANSTP = 'MPAY'", lblMxNo);
                if (lblMxNo.Text == "")
                {
                    txtNo.Text = "1";
                }
                else
                {
                    int iNo = int.Parse(lblMxNo.Text);
                    int totIno = iNo + 1;
                    txtNo.Text = totIno.ToString();
                }

                txtLCName.Focus();
            }

            else
            {
                DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string TrDate = transdate.ToString("yyyy/MM/dd");

                string month = transdate.ToString("MMM").ToUpper();
                string years = transdate.ToString("yy");
                lblMY.Text = month + "-" + years;

                Global.dropDownAddWithSelect(ddlNo,"SELECT DISTINCT TRANSNO FROM LC_EXPMST WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblMY.Text + "' and TRANSTP='MPAY'");
                ddlNo.Focus();
            }
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListLC(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('4010103','2020103') AND STATUSCD='P' AND ACCOUNTNM LIKE '" + prefixText + "%'", conn);
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
            Global.txtAdd(@"SELECT ACCOUNTCD FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('4010103','2020103') AND STATUSCD='P' AND ACCOUNTNM = '" + txtLCName.Text + "'", txtLCCD);
            //string LcId = txtLCName.Text;
            ////Regex.Match("12345<br>", @"\d+").Value;
            //string LcNo = Regex.Match(LcId,@"\d+").Value;
            //txtLCNo.Text = LcNo;
            //Global.lblAdd(@"SELECT CONVERT(NVARCHAR(20),OPENINGDT,103) AS OPENINGDT  FROM GL_ACCHART WHERE ACCOUNTCD LIKE '4010103%' AND STATUSCD='P' AND ACCOUNTNM = '" + txtLCName.Text + "'", lblOpenDT);
            //DateTime LcDate = DateTime.Parse(lblOpenDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            //string LcDT = Global.Dayformat(LcDate);
            //txtLCDate.Text = LcDT;

            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

            string s = "";
            SqlTransaction tran = null;

            DateTime TransDT = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");

            try
            {
                if (btnExpenseEdit.Text == "EDIT")
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();

                    cmd = new SqlCommand("SELECT MAX(TRANSNO) AS TRANSNO FROM LC_EXPMST WHERE TRANSMY = '" + lblMY.Text + "' ", conn);
                    SqlDataReader daIN = cmd.ExecuteReader();
                    string TransNo = "";
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
                            TransNo = Ftrans.ToString();
                            txtNo.Text = TransNo;
                        }
                        else
                        {
                            TransNo = txtNo.Text;
                        }
                    }
                    daIN.Close();
                    conn.Close();

                    if (txtLCCD.Text == "")
                    {
                        Response.Write("<script>alert('Select L/C ID.');</script>");
                        txtLCName.Focus();
                    }
                    else
                    {
                        query = (" INSERT INTO LC_EXPMST (TRANSTP, TRANSDT, TRANSMY, TRANSNO, LCCD,USERPC, USERID, IPADDRESS) " +
                             " VALUES ('MPAY','" + TrDt + "','" + lblMY.Text + "','" + txtNo.Text + "','" + txtLCCD.Text + "','" + PCName + "','" + userName + "','" + ipAddress + "')");

                        comm = new SqlCommand(query, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();
                    }
                }
                else
                {
                    //query = (" INSERT INTO LC_EXPMST (TRANSTP, TRANSDT, TRANSMY, TRANSNO, LCCD,USERPC, USERID, IPADDRESS) " +
                    //         " VALUES ('MPAY','" + TrDt + "','" + lblMY.Text + "','" + ddlNo.Text + "','" + txtLCCD.Text + "','" + PCName + "','" + userName + "','" + ipAddress + "')");

                    //comm = new SqlCommand(query, conn);

                    //conn.Open();
                    //tran = conn.BeginTransaction();
                    //comm.Transaction = tran;
                    //int result = comm.ExecuteNonQuery();
                    //tran.Commit();
                    //conn.Close();
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }

            txtInvoiceNo.Focus();
        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
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

            DateTime TransDT = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");
            try
            {
                if (btnExpenseEdit.Text == "EDIT")
                {
                    query = (" UPDATE LC_EXPMST SET LCINVNO = '" + txtInvoiceNo.Text + "' WHERE  TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt +  "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + txtNo.Text + " AND LCCD = '" + txtLCCD.Text + "' AND USERPC = '" + PCName + "' AND USERID = '" + userName + "' AND IPADDRESS = '" + ipAddress + "'");

                    comm = new SqlCommand(query, conn);

                    conn.Open();
                    tran = conn.BeginTransaction();
                    comm.Transaction = tran;
                    int result = comm.ExecuteNonQuery();
                    tran.Commit();
                    conn.Close();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }

            txtRemarks.Focus();
        }

        protected void txtRemarks_TextChanged(object sender, EventArgs e)
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

            DateTime TransDT = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");
            try
            {
                if (btnExpenseEdit.Text == "EDIT")
                {
                    query = (" UPDATE LC_EXPMST SET REMARKS = '" + txtRemarks.Text + "' WHERE  TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + txtNo.Text + " AND LCCD = '" + txtLCCD.Text + "' AND USERPC = '" + PCName + "' AND USERID = '" + userName + "' AND IPADDRESS = '" + ipAddress + "'");

                    comm = new SqlCommand(query, conn);

                    conn.Open();
                    tran = conn.BeginTransaction();
                    comm.Transaction = tran;
                    int result = comm.ExecuteNonQuery();
                    tran.Commit();
                    conn.Close();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                s = ex.Message;
            }

            TextBox txtChargeNM = (TextBox)gvDetails.FooterRow.FindControl("txtChargeNM");
            txtChargeNM.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListChargeNM(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT CHARGENM FROM LC_CHARGE WHERE CHARGENM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["CHARGENM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtChargeNM_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtChargeNM = (TextBox)row.FindControl("txtChargeNM");
            TextBox txtChargeID = (TextBox)row.FindControl("txtChargeID");
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            
            Global.txtAdd(@"Select CHARGEID from LC_CHARGE where CHARGENM = '" + txtChargeNM.Text + "'", txtChargeID);

            txtAmount.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListCashBank(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD='P'  AND ACCOUNTNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtCashBankNm_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCashBankNm = (TextBox)row.FindControl("txtCashBankNm");
            TextBox txtRemarksGrid = (TextBox)row.FindControl("txtRemarksGrid");

            Global.lblAdd(@"SELECT ACCOUNTCD FROM GL_ACCHART WHERE STATUSCD = 'P' AND LEVELCD = 5 AND ACCOUNTNM = '" + txtCashBankNm.Text + "'", lblCashBank);
            txtRemarksGrid.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListChargeNMEdit(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT CHARGENM FROM LC_CHARGE WHERE CHARGENM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["CHARGENM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtChargeNMEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtChargeNMEdit = (TextBox)row.FindControl("txtChargeNMEdit");
            TextBox txtChrgeIDEdit = (TextBox)row.FindControl("txtChrgeIDEdit");
            TextBox txtAmountEdit = (TextBox)row.FindControl("txtAmountEdit");

            Global.txtAdd(@"Select CHARGEID from LC_CHARGE where CHARGENM = '" + txtChargeNMEdit.Text + "'", txtChrgeIDEdit);

            txtAmountEdit.Focus();
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionListCashBankEdit(string prefixText, int count, string contextKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ACCOUNTNM FROM GL_ACCHART WHERE substring(ACCOUNTCD,1,7) in ('1020101','1020102','2020103') AND STATUSCD='P'  AND ACCOUNTNM LIKE '" + prefixText + "%'", conn);
            SqlDataReader oReader;
            conn.Open();
            List<String> CompletionSet = new List<string>();
            oReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (oReader.Read())
                CompletionSet.Add(oReader["ACCOUNTNM"].ToString());
            return CompletionSet.ToArray();
        }

        protected void txtCashBankEdit_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtCashBankEdit = (TextBox)row.FindControl("txtCashBankEdit");
            TextBox txtRemarksGridEdit = (TextBox)row.FindControl("txtRemarksGridEdit");

            Global.lblAdd(@"SELECT ACCOUNTCD FROM GL_ACCHART WHERE STATUSCD = 'P' AND LEVELCD = 5 AND ACCOUNTNM = '" + txtCashBankEdit.Text + "'", lblCashBank);
            txtRemarksGridEdit.Focus();
        }

        protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (btnExpenseEdit.Text == "EDIT")
            {
                gvDetails.EditIndex = -1;
                GridExpensesShow();
            }
            else
            {
                gvDetails.EditIndex = -1;
                GridExpensesShow_Edit();
            }
        }

        protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            DateTime TransDT = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

            if(e.CommandName.Equals("AddNew"))
            {
                TextBox txtChargeNM = (TextBox)gvDetails.FooterRow.FindControl("txtChargeNM");
                TextBox txtChargeID = (TextBox)gvDetails.FooterRow.FindControl("txtChargeID");
                TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");
                TextBox txtCashBankNm = (TextBox)gvDetails.FooterRow.FindControl("txtCashBankNm");
                TextBox txtRemarksGrid = (TextBox)gvDetails.FooterRow.FindControl("txtRemarksGrid");

                string s = "";
                SqlTransaction tran = null;

                if (txtLCCD.Text == "")
                {
                    Response.Write("<script>alert('Select L/C ID.');</script>");
                    txtLCName.Focus();
                }
                else if(txtChargeID.Text=="")
                {
                    Response.Write("<script>alert('Select Charge Name.');</script>");
                    txtChargeNM.Focus();
                }
                else if (txtAmount.Text == "" || txtAmount.Text==".00")
                {
                    Response.Write("<script>alert('Type Amount.');</script>");
                    txtAmount.Focus();
                }
                else if (lblCashBank.Text == "")
                {
                    Response.Write("<script>alert('Select Cash or Bank Name.');</script>");
                    txtCashBankNm.Focus();
                }
                else
                {
                    if (btnExpenseEdit.Text == "EDIT")
                    {
                        try
                        {
                        query = (" INSERT INTO LC_EXPENSE(TRANSTP, TRANSDT, TRANSMY, TRANSNO, LCCD, LCINVNO, CHARGEID, AMOUNT, CNBCD, REMARKS, USERPC, USERID, IPADDRESS) " +
                                 " VALUES ('MPAY','" + TrDt + "','" + lblMY.Text + "'," + txtNo.Text + ",'" + txtLCCD.Text + "','" + txtInvoiceNo.Text + "','" + txtChargeID.Text + "'," + txtAmount.Text + ",'" + lblCashBank.Text + "','" + txtRemarksGrid.Text + "','" + PCName + "','" + userName + "','" + ipAddress + "')");

                        comm = new SqlCommand(query, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            s = ex.Message;
                        }
                        GridExpensesShow();
                        lblCashBank.Text = "";
                        txtChargeNM.Focus();
                     }
                     else
                     {
                         try
                         {
                             query = (" INSERT INTO LC_EXPENSE(TRANSTP, TRANSDT, TRANSMY, TRANSNO, LCCD, LCINVNO, CHARGEID, AMOUNT, CNBCD, REMARKS, USERPC, USERID, IPADDRESS) " +
                                      " VALUES ('MPAY','" + TrDt + "','" + lblMY.Text + "'," + ddlNo.Text + ",'" + txtLCCD.Text + "','" + txtInvoiceNo.Text + "','" + txtChargeID.Text + "'," + txtAmount.Text + ",'" + lblCashBank.Text + "','" + txtRemarksGrid.Text + "','" + PCName + "','" + userName + "','" + ipAddress + "')");

                             comm = new SqlCommand(query, conn);

                             conn.Open();
                             tran = conn.BeginTransaction();
                             comm.Transaction = tran;
                             int result = comm.ExecuteNonQuery();
                             tran.Commit();
                             conn.Close();
                         }
                         catch (Exception ex)
                         {
                             tran.Rollback();
                             s = ex.Message;
                         }
                         GridExpensesShow_Edit();
                         lblCashBank.Text = "";
                         txtChargeNM.Focus();
                     }
                }
            }

            else if (e.CommandName.Equals("Complete"))
            {
                TextBox txtChargeNM = (TextBox)gvDetails.FooterRow.FindControl("txtChargeNM");
                TextBox txtChargeID = (TextBox)gvDetails.FooterRow.FindControl("txtChargeID");
                TextBox txtAmount = (TextBox)gvDetails.FooterRow.FindControl("txtAmount");
                TextBox txtCashBankNm = (TextBox)gvDetails.FooterRow.FindControl("txtCashBankNm");
                TextBox txtRemarksGrid = (TextBox)gvDetails.FooterRow.FindControl("txtRemarksGrid");

                string s = "";
                SqlTransaction tran = null;

                if (txtLCCD.Text == "")
                {
                    Response.Write("<script>alert('Select L/C ID.');</script>");
                    txtLCName.Focus();
                }
                else if (txtChargeID.Text == "")
                {
                    Response.Write("<script>alert('Select Charge Name.');</script>");
                    txtChargeNM.Focus();
                }
                else if (txtAmount.Text == "" || txtAmount.Text == ".00")
                {
                    Response.Write("<script>alert('Type Amount.');</script>");
                    txtAmount.Focus();
                }
                else if (lblCashBank.Text == "")
                {
                    Response.Write("<script>alert('Select Cash or Bank Name.');</script>");
                    txtCashBankNm.Focus();
                }
                else
                {
                    if (btnExpenseEdit.Text == "EDIT")
                    {
                        try
                        {
                            query = (" INSERT INTO LC_EXPENSE(TRANSTP, TRANSDT, TRANSMY, TRANSNO, LCCD, LCINVNO, CHARGEID, AMOUNT, CNBCD, REMARKS, USERPC, USERID, IPADDRESS) " +
                                     " VALUES ('MPAY','" + TrDt + "','" + lblMY.Text + "'," + txtNo.Text + ",'" + txtLCCD.Text + "','" + txtInvoiceNo.Text + "','" + txtChargeID.Text + "'," + txtAmount.Text + ",'" + lblCashBank.Text + "','" + txtRemarksGrid.Text + "','" + PCName + "','" + userName + "','" + ipAddress + "')");

                            comm = new SqlCommand(query, conn);

                            conn.Open();
                            tran = conn.BeginTransaction();
                            comm.Transaction = tran;
                            int result = comm.ExecuteNonQuery();
                            tran.Commit();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            s = ex.Message;
                        }

                        txtLCName.Text = "";
                        txtLCCD.Text = "";
                        //txtLCNo.Text = "";
                        //txtLCDate.Text = "";
                        txtInvoiceNo.Text = "";
                        txtRemarks.Text = "";
                        txtChargeNM.Text = "";
                        txtChargeID.Text = "";
                        txtAmount.Text = ".00";
                        txtCashBankNm.Text = "";
                        lblCashBank.Text = "";
                        txtRemarksGrid.Text = "";

                        DateTime today = DateTime.Today.Date;
                        string td = Global.Dayformat(today);
                        txtDate.Text = td;
                        DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        string TrDate = transdate.ToString("yyyy/MM/dd");

                        string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
                        string year = today.ToString("yy");
                        lblMY.Text = mon + "-" + year;
                        Global.lblAdd(@"Select max(TRANSNO) FROM LC_EXPMST where TRANSMY='" + lblMY.Text + "' and TRANSTP = 'MPAY'", lblMxNo);
                        if (lblMxNo.Text == "")
                        {
                            txtNo.Text = "1";
                        }
                        else
                        {
                            int iNo = int.Parse(lblMxNo.Text);
                            int totIno = iNo + 1;
                            txtNo.Text = totIno.ToString();
                        }

                        txtLCName.Focus();

                        GridExpensesShow();
                    }
                    else
                    {
                        try
                        {
                            query = (" INSERT INTO LC_EXPENSE(TRANSTP, TRANSDT, TRANSMY, TRANSNO, LCCD, LCINVNO, CHARGEID, AMOUNT, CNBCD, REMARKS, USERPC, USERID, IPADDRESS) " +
                                     " VALUES ('MPAY','" + TrDt + "','" + lblMY.Text + "'," + txtNo.Text + ",'" + txtLCCD.Text + "','" + txtInvoiceNo.Text + "','" + txtChargeID.Text + "'," + txtAmount.Text + ",'" + lblCashBank.Text + "','" + txtRemarksGrid.Text + "','" + PCName + "','" + userName + "','" + ipAddress + "')");

                            comm = new SqlCommand(query, conn);

                            conn.Open();
                            tran = conn.BeginTransaction();
                            comm.Transaction = tran;
                            int result = comm.ExecuteNonQuery();
                            tran.Commit();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            s = ex.Message;
                        }

                        txtLCName.Text = "";
                        txtLCCD.Text = "";
                        //txtLCNo.Text = "";
                        //txtLCDate.Text = "";
                        txtInvoiceNo.Text = "";
                        txtRemarks.Text = "";
                        txtChargeNM.Text = "";
                        txtChargeID.Text = "";
                        txtAmount.Text = ".00";
                        txtCashBankNm.Text = "";
                        lblCashBank.Text = "";
                        txtRemarksGrid.Text = "";

                        //DateTime today = DateTime.Today.Date;
                        //string td = Global.Dayformat(today);
                        //txtDate.Text = td;
                        //DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                        //string TrDate = transdate.ToString("yyyy/MM/dd");

                        //string mon = DateTime.Today.Date.ToString("MMM").ToUpper();
                        //string year = today.ToString("yy");
                        //lblMY.Text = mon + "-" + year;
                        //Global.lblAdd(@"Select max(TRANSNO) FROM LC_EXPMST where TRANSMY='" + lblMY.Text + "' and TRANSTP = 'MPAY'", lblMxNo);
                        //if (lblMxNo.Text == "")
                        //{
                        //    txtNo.Text = "1";
                        //}
                        //else
                        //{
                        //    int iNo = int.Parse(lblMxNo.Text);
                        //    int totIno = iNo + 1;
                        //    txtNo.Text = totIno.ToString();
                        //}

                        //txtLCName.Focus();

                        ddlNo.SelectedIndex = -1;
                        ddlNo.Focus();

                        GridExpensesShow_Edit();
                    }
                }
            }
        }

        protected void GridExpensesShow_Edit()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = transdate.ToString("yyyy/MM/dd");

            Int64 EditTransNo = 0;
            if (ddlNo.Text == "Select")
            {
                EditTransNo = 0;
            }
            else
                EditTransNo = Convert.ToInt64(ddlNo.Text);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT LC_EXPENSE.TRANSTP, LC_EXPENSE.TRANSDT, LC_EXPENSE.TRANSMY, LC_EXPENSE.TRANSNO, LC_EXPENSE.LCCD, LC_EXPENSE.LCINVNO, LC_EXPENSE.CHARGESL, LC_EXPENSE.CHARGEID, LC_EXPENSE.AMOUNT, LC_EXPENSE.CNBCD, LC_EXPENSE.REMARKS, LC_EXPENSE.USERPC, " +
                                            " LC_EXPENSE.USERID, LC_EXPENSE.INTIME, LC_EXPENSE.IPADDRESS, LC_CHARGE.CHARGENM, GL_ACCHART.ACCOUNTNM FROM LC_EXPMST INNER JOIN LC_EXPENSE ON LC_EXPMST.TRANSTP = LC_EXPENSE.TRANSTP AND LC_EXPMST.TRANSMY = LC_EXPENSE.TRANSMY AND " +
                                            " LC_EXPMST.TRANSNO = LC_EXPENSE.TRANSNO INNER JOIN LC_CHARGE ON LC_EXPENSE.CHARGEID = LC_CHARGE.CHARGEID INNER JOIN GL_ACCHART ON LC_EXPENSE.CNBCD = GL_ACCHART.ACCOUNTCD WHERE LC_EXPMST.TRANSTP = 'MPAY' AND LC_EXPMST.TRANSDT = '" + TrDt + "' AND LC_EXPMST.TRANSMY = '" + lblMY.Text + "' AND LC_EXPMST.TRANSNO = " + EditTransNo + " AND " +
                                            " LC_EXPMST.LCCD = '" + txtLCCD.Text + "' ORDER BY LC_EXPENSE.CHARGESL", conn);
            //AND LC_EXPMST.LCINVNO = '" + txtInvoiceNo.Text + "'
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();

                //Global.txtAdd("SELECT MAX(TRANSNO) AS TRANSNO FROM STK_TRANSMST WHERE TRANSMY='" + lblSMY.Text + "' AND TRANSTP='SALE'",txtInNo);
                TextBox txtChargeNM = (TextBox)gvDetails.FooterRow.FindControl("txtChargeNM");
                txtChargeNM.Focus();
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

        protected void btnExpenseEdit_Click(object sender, EventArgs e)
        {
            DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");

            if (btnExpenseEdit.Text == "EDIT")
            {
                ddlNo.Focus();
                btnExpenseEdit.Text = "NEW";
                txtNo.Visible = false;
                ddlNo.Visible = true;
                Global.dropDownAddWithSelect(ddlNo,"SELECT DISTINCT TRANSNO FROM LC_EXPMST WHERE TRANSDT ='" + TrDate + "' AND TRANSMY='" + lblMY.Text + "' and TRANSTP='MPAY'");
                ddlNo.SelectedIndex = -1;
                txtLCName.Text = "";
                txtLCCD.Text = "";
                //txtLCNo.Text = "";
                //txtLCDate.Text = "";
                txtInvoiceNo.Text = "";
                txtRemarks.Text = "";
                //btnUpdate.Visible = false;
                GridExpensesShow_Edit();
            }
            else
            {
                txtLCName.Focus();
                btnExpenseEdit.Text = "EDIT";
                txtNo.Visible = true;
                ddlNo.Visible = false;
                ddlNo.SelectedIndex = -1;
                txtLCName.Text = "";
                txtLCCD.Text = "";
                //txtLCNo.Text = "";
                //txtLCDate.Text = "";
                txtInvoiceNo.Text = "";
                txtRemarks.Text = "";
                //btnUpdate.Visible = false;
                GridExpensesShow();
            }
        }

        protected void ddlNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            DateTime transdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDate = transdate.ToString("yyyy/MM/dd");

            if (ddlNo.Text == "Select")
            {
                gvDetails.Visible = false;
                Response.Write("<script>alert('Select Transaction No.');</script>");
                ddlNo.Focus();
            }
            else
            {
                gvDetails.Visible = true;

                Global.txtAdd(@"SELECT GL_ACCHART.ACCOUNTNM FROM LC_EXPMST INNER JOIN GL_ACCHART ON LC_EXPMST.LCCD = GL_ACCHART.ACCOUNTCD WHERE TRANSTP='MPAY' and TRANSDT = '" + TrDate + "' and TRANSMY = '" + lblMY.Text + "' and TRANSNO =" + ddlNo.Text + "", txtLCName);
                Global.txtAdd(@"SELECT LCCD FROM LC_EXPMST WHERE TRANSTP='MPAY' and TRANSDT = '" + TrDate + "' and TRANSMY = '" + lblMY.Text + "' and TRANSNO =" + ddlNo.Text + "", txtLCCD);

                //string LcId = txtLCName.Text;
                ////Regex.Match("12345<br>", @"\d+").Value;
                //string LcNo = Regex.Match(LcId, @"\d+").Value;
                //txtLCNo.Text = LcNo;
                //Global.lblAdd(@"SELECT OPENINGDT FROM GL_ACCHART WHERE ACCOUNTCD LIKE '4010103%' AND STATUSCD='P' AND ACCOUNTNM = '" + txtLCName.Text + "'", lblOpenDT);
                //DateTime LcDate = DateTime.Parse(lblOpenDT.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //string LcDT = Global.Dayformat(LcDate);
                //txtLCDate.Text = LcDT;

                Global.txtAdd(@"SELECT LCINVNO FROM LC_EXPMST WHERE TRANSTP='MPAY' and TRANSDT = '" + TrDate + "' and TRANSMY = '" + lblMY.Text + "' and TRANSNO =" + ddlNo.Text + "", txtInvoiceNo);
                Global.txtAdd(@"SELECT REMARKS FROM LC_EXPMST WHERE TRANSTP='MPAY' and TRANSDT = '" + TrDate + "' and TRANSMY = '" + lblMY.Text + "' and TRANSNO =" + ddlNo.Text + "", txtRemarks);

                GridExpensesShow_Edit();
                //btnUpdate.Visible = true;
            }
        }

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    string userName = HttpContext.Current.Session["UserName"].ToString();
        //    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
        //    string PCName = HttpContext.Current.Session["PCName"].ToString();
        //    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    string query = "";
        //    SqlCommand comm = new SqlCommand(query, conn);

        //    string s = "";
        //    SqlTransaction tran = null;

        //    DateTime TransDT = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //    string TrDt = TransDT.ToString("yyyy/MM/dd");

        //    try
        //    {
        //        query = (" UPDATE LC_EXPMST SET TRANSTP = 'MPAY', TRANSDT = '" + TrDt + "', TRANSMY = '" + lblMY.Text + "', LCCD = '" + txtLCCD.Text + "', LCINVNO = '" + txtInvoiceNo.Text + "', REMARKS = '" + txtRemarks.Text + "' WHERE  TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + ddlNo.Text + " AND LCCD = '" + txtLCCD.Text + "' AND USERPC = '" + PCName + "' AND USERID = '" + userName + "' AND IPADDRESS = '" + ipAddress + "'");

        //        comm = new SqlCommand(query, conn);

        //        conn.Open();
        //        tran = conn.BeginTransaction();
        //        comm.Transaction = tran;
        //        int result = comm.ExecuteNonQuery();
        //        tran.Commit();
        //        conn.Close();
        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //}

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            DateTime TransDT = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");

            string query = "";
            string query1 = "";
            SqlCommand comm = new SqlCommand(query, conn);
            SqlCommand comm1 = new SqlCommand(query1, conn);

            if (btnExpenseEdit.Text == "EDIT")
            {
                string s = "";
                SqlTransaction tran = null;

                Label lblChargeShow = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblChargeShow");

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT * FROM LC_EXPENSE where TRANSTP = 'MPAY' and TRANSMY='" + lblMY.Text + "' and TRANSNO ='" + txtNo.Text + "' and TRANSDT = '" + TrDt + "' ", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        query = (" DELETE FROM LC_EXPENSE WHERE TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + txtNo.Text + " AND CHARGESL = '" + lblChargeShow.Text + "' ");

                        comm = new SqlCommand(query, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();
                    }
                    else
                    {
                        query = (" DELETE FROM LC_EXPENSE WHERE TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + txtNo.Text + " AND CHARGESL = '" + lblChargeShow.Text + "' ");

                        comm = new SqlCommand(query, conn);

                        query1 = (" DELETE FROM LC_EXPMST WHERE TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + txtNo.Text + " ");

                        comm1 = new SqlCommand(query1, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        comm1.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        int result1 = comm1.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();

                        txtLCName.Text = "";
                        txtLCCD.Text = "";
                        //txtLCNo.Text = "";
                        //txtLCDate.Text = "";
                        txtInvoiceNo.Text = "";
                        txtRemarks.Text = "";
                    }
                    
                    gvDetails.EditIndex = -1;
                    GridExpensesShow();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
            }
            else
            {
                string s = "";
                SqlTransaction tran = null;

                Label lblChargeShow = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblChargeShow");

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(" SELECT * FROM LC_EXPENSE where TRANSTP = 'MPAY' and TRANSMY='" + lblMY.Text + "' and TRANSNO ='" + ddlNo.Text + "' and TRANSDT = '" + TrDt + "' ", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    conn.Close();

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        query = (" DELETE FROM LC_EXPENSE WHERE TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + ddlNo.Text + " AND CHARGESL = '" + lblChargeShow.Text + "' ");

                        comm = new SqlCommand(query, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();
                    }
                    else
                    {
                        query = (" DELETE FROM LC_EXPENSE WHERE TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + ddlNo.Text + " AND CHARGESL = '" + lblChargeShow.Text + "' ");

                        comm = new SqlCommand(query, conn);

                        query1 = (" DELETE FROM LC_EXPMST WHERE TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + ddlNo.Text + " ");

                        comm1 = new SqlCommand(query1, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        comm1.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        int result1 = comm1.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();

                        ddlNo.SelectedIndex = -1;
                        txtLCName.Text = "";
                        txtLCCD.Text = "";
                        //txtLCNo.Text = "";
                        //txtLCDate.Text = "";
                        txtInvoiceNo.Text = "";
                        txtRemarks.Text = "";
                    }

                    gvDetails.EditIndex = -1;

                    GridExpensesShow_Edit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
            }

        }

        protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (btnExpenseEdit.Text == "EDIT")
            {
                gvDetails.EditIndex = e.NewEditIndex;
                GridExpensesShow();

                TextBox txtCashBankEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtCashBankEdit");

                Global.lblAdd(@"SELECT ACCOUNTCD FROM GL_ACCHART WHERE STATUSCD = 'P' AND LEVELCD = 5 AND ACCOUNTNM = '" + txtCashBankEdit.Text + "'", lblCashBank);

                TextBox txtChargeNMEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtChargeNMEdit");
                txtChargeNMEdit.Focus();
            }
            else
            {
                gvDetails.EditIndex = e.NewEditIndex;
                GridExpensesShow_Edit();

                TextBox txtCashBankEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtCashBankEdit");

                Global.lblAdd(@"SELECT ACCOUNTCD FROM GL_ACCHART WHERE STATUSCD = 'P' AND LEVELCD = 5 AND ACCOUNTNM = '" + txtCashBankEdit.Text + "'", lblCashBank);

                TextBox txtChargeNMEdit = (TextBox)gvDetails.Rows[e.NewEditIndex].FindControl("txtChargeNMEdit");
                txtChargeNMEdit.Focus();
            }
        }

        protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            DateTime TransDT = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TrDt = TransDT.ToString("yyyy/MM/dd");

            string query = "";
            SqlCommand comm = new SqlCommand(query, conn);

            TextBox txtChargeNMEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChargeNMEdit");
            TextBox txtChrgeIDEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtChrgeIDEdit");
            TextBox txtAmountEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtAmountEdit");
            TextBox txtCashBankEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtCashBankEdit");
            TextBox txtRemarksGridEdit = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRemarksGridEdit");
            Label lblChargeSlEdit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblChargeSlEdit");

            if (btnExpenseEdit.Text == "EDIT")
            {
                string s = "";
                SqlTransaction tran = null;

                try
                {
                    if (txtLCCD.Text == "")
                    {
                        Response.Write("<script>alert('Select LC ID.');</script>");
                        txtLCName.Focus();
                    }
                    else if (txtChrgeIDEdit.Text == "")
                    {
                        Response.Write("<script>alert('Select Charge Name.');</script>");
                        txtChargeNMEdit.Focus();
                    }
                    else if (txtAmountEdit.Text == "")
                    {
                        Response.Write("<script>alert('Type Amount.');</script>");
                        txtAmountEdit.Focus();
                    }
                    else if (lblCashBank.Text == "")
                    {
                        Response.Write("<script>alert('Select Cash or Bank Name.');</script>");
                        txtCashBankEdit.Focus();
                    }
                    else
                    {
                        query = (" UPDATE LC_EXPENSE SET CHARGEID = '" + txtChrgeIDEdit.Text + "', AMOUNT =" + txtAmountEdit.Text + ", CNBCD = '" + lblCashBank.Text + "', REMARKS = '" + txtRemarksGridEdit.Text + "', USERPC = '" + PCName + "', USERID = '" + userName + "', IPADDRESS = '" + ipAddress + "' " +
                             " WHERE TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + txtNo.Text + " AND LCCD = '" + txtLCCD.Text + "' AND CHARGESL = '" + lblChargeSlEdit.Text + "'");

                        comm = new SqlCommand(query, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();

                        gvDetails.EditIndex = -1;

                        GridExpensesShow();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
            }
            else
            {
                string s = "";
                SqlTransaction tran = null;

                try
                {
                    if (txtLCCD.Text == "")
                    {
                        Response.Write("<script>alert('Select LC ID.');</script>");
                        txtLCName.Focus();
                    }
                    else if(ddlNo.Text=="Select")
                    {
                        Response.Write("<script>alert('Select Transaction No.');</script>");
                        ddlNo.Focus();
                    }
                    else if (txtChrgeIDEdit.Text == "")
                    {
                        Response.Write("<script>alert('Select Charge Name.');</script>");
                        txtChargeNMEdit.Focus();
                    }
                    else if (txtAmountEdit.Text == "")
                    {
                        Response.Write("<script>alert('Type Amount.');</script>");
                        txtAmountEdit.Focus();
                    }
                    else if (lblCashBank.Text == "")
                    {
                        Response.Write("<script>alert('Select Cash or Bank Name.');</script>");
                        txtCashBankEdit.Focus();
                    }
                    else
                    {
                        query = (" UPDATE LC_EXPENSE SET CHARGEID = '" + txtChrgeIDEdit.Text + "', AMOUNT =" + txtAmountEdit.Text + ", CNBCD = '" + lblCashBank.Text + "', REMARKS = '" + txtRemarksGridEdit.Text + "', USERPC = '" + PCName + "', USERID = '" + userName + "', IPADDRESS = '" + ipAddress + "' " +
                             " WHERE TRANSTP = 'MPAY' AND TRANSDT = '" + TrDt + "' AND TRANSMY = '" + lblMY.Text + "' AND TRANSNO = " + ddlNo.Text + " AND LCCD = '" + txtLCCD.Text + "' AND CHARGESL = '" + lblChargeSlEdit.Text + "'");

                        comm = new SqlCommand(query, conn);

                        conn.Open();
                        tran = conn.BeginTransaction();
                        comm.Transaction = tran;
                        int result = comm.ExecuteNonQuery();
                        tran.Commit();
                        conn.Close();

                        gvDetails.EditIndex = -1;

                        GridExpensesShow_Edit();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    s = ex.Message;
                }
            }
        }

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


    }
}