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
    public partial class Process : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblSerial_Mrec.Visible = false;
                lblSerial_Mpay.Visible = false;
                lblSerial_Jour.Visible = false;
                lblSerial_Cont.Visible = false;
                DateTime today = DateTime.Today.Date;
                string td = Global.Dayformat(today);
                txtDate.Text = td;
                btnProcess.Focus();
            }
        }

        public void ShowGrid()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, CHEQUENO, convert(nvarchar(20),CHEQUEDT,103) as CHEQUEDT, AMOUNT, REMARKS, USERPC, USERID, INTIME, IPADDRESS " +
                                            " FROM dbo.GL_STRANS where TRANSDT = '" + p_Date + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = false;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }

        public void ShowGrid_Multiple()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, SERIALNO, TRANSFOR, COSTPID, TRANSMODE, DEBITCD, CREDITCD, CHEQUENO, convert(nvarchar(20),CHEQUEDT,103) as CHEQUEDT, AMOUNT, REMARKS, USERPC, USERID, INTIME, IPADDRESS " +
                                            " FROM GL_MTRANS where TRANSDT = '" + p_Date + "'", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridMultiple.DataSource = ds;
                gridMultiple.DataBind();
                gridMultiple.Visible = false;
            }
            else
            {
                gridMultiple.DataSource = ds;
                gridMultiple.DataBind();
                gridMultiple.Visible = false;
                //Response.Write("<script>alert('No Data Found');</script>");
                //GridView1.Visible = false;
            }
        }

        public void ShowGrid_Purchase()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, sum(AMOUNT)as AMOUNT " +
                                            " from STK_TRANS where TRANSDT='" + p_Date + "' and LCTP='LOCAL' and TRANSTP='BUY' and TRANSDT <> '2013-04-30 00:00:00' " +
                                            " group by TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.Visible = false;
            }
            else
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.Visible = false;
            }
        }

        public void ShowGrid_Purchase_Ret()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, sum(AMOUNT)as AMOUNT " +
                                            " from STK_TRANS where TRANSDT='" + p_Date + "' and TRANSTP='IRTB' and TRANSDT <> '2013-04-30 00:00:00' " +
                                            " group by TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridPurchase_Ret.DataSource = ds;
                gridPurchase_Ret.DataBind();
                gridPurchase_Ret.Visible = false;
            }
            else
            {
                gridPurchase_Ret.DataSource = ds;
                gridPurchase_Ret.DataBind();
                gridPurchase_Ret.Visible = false;
            }
        }

        public void ShowGrid_Sale()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, sum(AMOUNT)as AMOUNT from STK_TRANS where TRANSDT='" + p_Date + "' and TRANSTP='SALE' " +
                                            " group by TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.Visible = false;
            }
            else
            {
                GridView3.DataSource = ds;
                GridView3.DataBind();
                GridView3.Visible = false;
            }
        }

        public void ShowGrid_Sale_Discount()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT * FROM " +
                                                " (SELECT A.TRANSTP, A.TRANSDT, A.TRANSMY, A.TRANSNO, A.STOREFR, A.PSID, A.REMARKS, (A.DISCAMT+STK_TRANSMST.DISAMT) AS DISAMT " +
                                                " FROM  (SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID, SUM(DISCAMT) AS DISCAMT, REMARKS " +
                                                        " FROM          STK_TRANS " +
                                                        " WHERE (TRANSTP = 'SALE') AND (TRANSDT = '" + p_Date + "') " +
                                                        " GROUP BY TRANSTP, TRANSDT, TRANSMY, TRANSNO, INVREFNO, STOREFR, STORETO, PSID,REMARKS) AS A INNER JOIN " +
                                            " STK_TRANSMST ON A.TRANSTP = STK_TRANSMST.TRANSTP AND A.TRANSDT = STK_TRANSMST.TRANSDT AND A.TRANSMY = STK_TRANSMST.TRANSMY AND " +
                                            " A.TRANSNO = STK_TRANSMST.TRANSNO AND A.INVREFNO = STK_TRANSMST.INVREFNO AND A.STOREFR = STK_TRANSMST.STOREFR AND " +
                                            " A.STORETO = STK_TRANSMST.STORETO AND A.PSID = STK_TRANSMST.PSID) AS B WHERE B.DISAMT<>0", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView4.DataSource = ds;
                GridView4.DataBind();
                GridView4.Visible = false;
            }
            else
            {
                GridView4.DataSource = ds;
                GridView4.DataBind();
                GridView4.Visible = false;
            }
        }

        public void ShowGrid_Sale_Ret()
        {

            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS, sum(AMOUNT)as AMOUNT from STK_TRANS where TRANSDT='" + p_Date + "' and TRANSTP='IRTS' " +
                                            " group by TRANSTP, TRANSDT, TRANSMY, TRANSNO, STOREFR, STORETO, PSID, LCTP, LCID, LCDATE, REMARKS", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridSale_Ret.DataSource = ds;
                gridSale_Ret.DataBind();
                gridSale_Ret.Visible = false;
            }
            else
            {
                gridSale_Ret.DataSource = ds;
                gridSale_Ret.DataBind();
                gridSale_Ret.Visible = false;
            }
        }
        public void ShowGridLC()
        {
            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSTP, TRANSDT, TRANSMY, TRANSNO, LCCD, CNBCD, AMOUNT, (CASE WHEN LCINVNO = '' THEN REMARKS WHEN REMARKS = '' THEN LCINVNO ELSE (REMARKS + ' - ' + LCINVNO)END)AS REMARKS FROM LC_EXPENSE " +
                                            " WHERE (TRANSDT = '" + p_Date + "')", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridLC.DataSource = ds;
                gridLC.DataBind();
                gridLC.Visible = false;
            }
            else
            {
                gridLC.DataSource = ds;
                gridLC.DataBind();
                gridLC.Visible = false;
            }
        }

        public void show_Grid_Job_Expense()
        {
            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT CNF_JOBEXP.TRANSMY, CNF_JOBEXP.TRANSNO, CNF_JOBEXP.COMPID + '0001' AS COSTPID, CNF_JOBEXP.EXPCD, SUM(CNF_JOBEXP.EXPAMT) AS EXPAMT, " +
                       " + ' - ' + CNF_JOBEXP.JOBTP + ' - ' + CONVERT(NVARCHAR(20), CNF_JOBEXP.JOBYY, 10) + ' - ' + CONVERT(NVARCHAR(20), CNF_JOBEXP.JOBNO, 103) + (CASE WHEN CNF_JOBEXPMST.REMARKS IS NULL THEN '' ELSE + ' - ' + CNF_JOBEXPMST.REMARKS END) AS REMARKS " +
                       " FROM CNF_JOBEXP INNER JOIN CNF_JOBEXPMST ON CNF_JOBEXP.TRANSMY = CNF_JOBEXPMST.TRANSMY AND CNF_JOBEXP.TRANSNO = CNF_JOBEXPMST.TRANSNO WHERE (CNF_JOBEXP.TRANSDT = '" + p_Date + "') " +
                       " GROUP BY CNF_JOBEXP.TRANSMY, CNF_JOBEXP.TRANSNO, CNF_JOBEXP.COMPID + '0001', CNF_JOBEXP.EXPCD, + ' - ' + CNF_JOBEXP.JOBTP + ' - ' + CONVERT(NVARCHAR(20), CNF_JOBEXP.JOBYY, 10) + ' - ' + CONVERT(NVARCHAR(20), CNF_JOBEXP.JOBNO, 103) + (CASE WHEN CNF_JOBEXPMST.REMARKS IS NULL THEN '' ELSE + ' - ' + CNF_JOBEXPMST.REMARKS END) " +
                       " ORDER BY CNF_JOBEXP.TRANSNO", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobExpense.DataSource = ds;
                gvJobExpense.DataBind();
                gvJobExpense.Visible = false;
            }
            else
            {
                gvJobExpense.DataSource = ds;
                gvJobExpense.DataBind();
                gvJobExpense.Visible = false;
            }
        }

        public void show_Grid_Job_Receive()
        {
            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT TRANSMY,TRANSNO,TRANSFOR,(COMPID+'0001') AS COMPID,DEBITCD,PARTYID,AMOUNT,(+ ' - ' + JOBTP+' - '+CONVERT(NVARCHAR(20),JOBYY,103)+ (CASE WHEN REMARKS = '' THEN '' ELSE + ' - ' +REMARKS END)) AS REMARKS FROM CNF_JOBRCV " +
                " WHERE TRANSDT='" + p_Date + "' ORDER BY TRANSNO", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobReceive.DataSource = ds;
                gvJobReceive.DataBind();
                gvJobReceive.Visible = false;
            }
            else
            {
                gvJobReceive.DataSource = ds;
                gvJobReceive.DataBind();
                gvJobReceive.Visible = false;
            }
        }

        public void show_Grid_Bill_Process()
        {
            DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string p_Date = Pdate.ToString("yyyy/MM/dd");

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT dbo.CNF_JOBBILL.COMPID, dbo.CNF_JOBBILL.COMPID + '0001' AS COSTPID, 'JOUR' AS TRANSTP, dbo.CNF_JOBBILL.BILLDT, dbo.CNF_JOBBILL.BILLNO, " +
                      " dbo.CNF_JOBBILL.PARTYID, (+ ' - ' + dbo.CNF_JOBBILL.JOBTP + ' - ' + CONVERT(NVARCHAR(20),dbo.CNF_JOBBILL.JOBYY,103) + ' - ' + CONVERT(NVARCHAR(20),dbo.CNF_JOBBILL.JOBNO,103) + ' - ' + 'BILL DATE : ' +  CONVERT(NVARCHAR(20),CNF_JOBBILL.BILLDT,103)) AS REMARKS , SUM(dbo.CNF_JOBBILL.BILLAMT) + dbo.CNF_JOB.COMM_AMT AS AMT  " +
                      " FROM dbo.CNF_JOBBILL INNER JOIN dbo.CNF_JOB ON dbo.CNF_JOBBILL.JOBTP = dbo.CNF_JOB.JOBTP AND dbo.CNF_JOBBILL.JOBYY = dbo.CNF_JOB.JOBYY AND dbo.CNF_JOBBILL.JOBNO = dbo.CNF_JOB.JOBNO AND dbo.CNF_JOBBILL.COMPID = dbo.CNF_JOB.COMPID " +
                      " WHERE     (dbo.CNF_JOBBILL.BILLAMT <> 0) AND (dbo.CNF_JOBBILL.BILLDT = '" + p_Date + "') GROUP BY dbo.CNF_JOBBILL.COMPID, dbo.CNF_JOBBILL.BILLDT, dbo.CNF_JOBBILL.BILLNO, dbo.CNF_JOBBILL.PARTYID, (+ ' - ' + dbo.CNF_JOBBILL.JOBTP + ' - ' + CONVERT(NVARCHAR(20),dbo.CNF_JOBBILL.JOBYY,103) + ' - ' + CONVERT(NVARCHAR(20),dbo.CNF_JOBBILL.JOBNO,103) + ' - ' + 'BILL DATE : ' +  CONVERT(NVARCHAR(20),CNF_JOBBILL.BILLDT,103)), dbo.CNF_JOB.COMM_AMT " +
                      " ORDER BY dbo.CNF_JOBBILL.BILLNO", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBill.DataSource = ds;
                gvBill.DataBind();
                gvBill.Visible = false;
            }
            else
            {
                gvBill.DataSource = ds;
                gvBill.DataBind();
                gvBill.Visible = false;
            }
        }
        //public void ShowGridMicroCreditCollection()
        //{
        //    DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //    string p_Date = Pdate.ToString("yyyy/MM/dd");

        //    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(" SELECT 'MREC' AS TRANSTP, MC_COLLECT_MST.TRANSDT, MC_COLLECT_MST.TRANSMY, MC_COLLECT_MST.TRANSNO, MC_COLLECT_MST.SCHEME_ID, " +
        //              " SUM(MC_COLLECT.COLLECTAMT) AS AMOUNT, MC_COLLECT_MST.REMARKS, MC_SCHEME.SCH_TP, MC_SCHEME.ACC_CD FROM MC_COLLECT_MST INNER JOIN " +
        //              " MC_COLLECT ON MC_COLLECT_MST.TRANSMY = MC_COLLECT.TRANSMY AND MC_COLLECT_MST.TRANSNO = MC_COLLECT.TRANSNO INNER JOIN MC_SCHEME ON MC_COLLECT_MST.SCHEME_ID = MC_SCHEME.SCHEME_ID " +
        //              " WHERE (MC_COLLECT_MST.TRANSDT = '" + p_Date + "') GROUP BY MC_COLLECT_MST.TRANSDT, MC_COLLECT_MST.TRANSMY, MC_COLLECT_MST.TRANSNO, MC_COLLECT_MST.SCHEME_ID, MC_COLLECT_MST.REMARKS, " +
        //              " MC_SCHEME.SCH_TP, MC_SCHEME.ACC_CD", conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    conn.Close();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvMicCollection.DataSource = ds;
        //        gvMicCollection.DataBind();
        //        gvMicCollection.Visible = false;
        //    }
        //    else
        //    {
        //        gvMicCollection.DataSource = ds;
        //        gvMicCollection.DataBind();
        //        gvMicCollection.Visible = false;
        //    }
        //}

        //public void ShowGridMicroCreditCollectionMember()
        //{
        //    DateTime Pdate = DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
        //    string p_Date = Pdate.ToString("yyyy/MM/dd");

        //    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(" SELECT 'MREC' AS TRANSTP, MC_COLLECT_MST.TRANSDT, MC_COLLECT_MST.TRANSMY, MC_COLLECT_MST.TRANSNO, MC_COLLECT_MST.SCHEME_ID, " +
        //              " SUM(MC_COLLECT.COLLECTAMT) AS AMOUNT, MC_COLLECT_MST.REMARKS, MC_COLLECT.MEMBERID, MC_COLLECT.INTERNALID, MC_SCHEME.SCH_TP, MC_SCHEME.ACC_CD " +
        //              " FROM MC_COLLECT_MST INNER JOIN MC_COLLECT ON MC_COLLECT_MST.TRANSMY = MC_COLLECT.TRANSMY AND MC_COLLECT_MST.TRANSNO = MC_COLLECT.TRANSNO INNER JOIN " +
        //              " MC_SCHEME ON MC_COLLECT_MST.SCHEME_ID = MC_SCHEME.SCHEME_ID WHERE (MC_COLLECT_MST.TRANSDT = '" + p_Date + "') GROUP BY MC_COLLECT_MST.TRANSDT, MC_COLLECT_MST.TRANSMY, MC_COLLECT_MST.TRANSNO, MC_COLLECT_MST.SCHEME_ID, MC_COLLECT_MST.REMARKS, " +
        //              " MC_COLLECT.MEMBERID, MC_COLLECT.INTERNALID, MC_SCHEME.SCH_TP, MC_SCHEME.ACC_CD", conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    conn.Close();
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvMicCollectionMember.DataSource = ds;
        //        gvMicCollectionMember.DataBind();
        //        gvMicCollectionMember.Visible = false;
        //    }
        //    else
        //    {
        //        gvMicCollectionMember.DataSource = ds;
        //        gvMicCollectionMember.DataBind();
        //        gvMicCollectionMember.Visible = false;
        //    }
        //}

        public void Buy_process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in GridView2.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "BUY")
                    {
                        Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '11%' ", lblSerial_BUY);
                        if (lblSerial_BUY.Text == "")
                        {
                            serialNo = "11000";
                            iob.Serial_BUY = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_BUY.Text);
                            serial = sl + 1;

                            iob.Serial_BUY = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreTo = grid.Cells[5].Text;

                        iob.Debitcd = "401020100001";
                        iob.Creditcd = grid.Cells[6].Text;
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_BUY(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }

        public void Buy_process_Ret()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gridPurchase_Ret.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "IRTB")
                    {
                        Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '14%' ", lblSerial_BUY);
                        if (lblSerial_BUY.Text == "")
                        {
                            serialNo = "14000";
                            iob.Serial_BUY = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_BUY.Text);
                            serial = sl + 1;

                            iob.Serial_BUY = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreFrom = grid.Cells[5].Text;

                        iob.Debitcd = grid.Cells[6].Text;
                        iob.Creditcd = "401020100002";
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_BUY_Ret(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }

        }

        public void Sale_process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in GridView3.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "SALE")
                    {
                        Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '12%'", lblSerial_SALE);
                        if (lblSerial_SALE.Text == "")
                        {
                            serialNo = "12000";
                            iob.Serial_SALE = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_SALE.Text);
                            serial = sl + 1;

                            iob.Serial_SALE = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreFrom = grid.Cells[4].Text;

                        iob.Debitcd = grid.Cells[6].Text;
                        iob.Creditcd = "301010100001";
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_SALE(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void Sale_Discount_process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in GridView4.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "SALE")
                    {
                        Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '13%'", lblSlSale_Dis);
                        if (lblSlSale_Dis.Text == "")
                        {
                            serialNo = "13000";
                            iob.Sl_Sale_dis = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSlSale_Dis.Text);
                            serial = sl + 1;

                            iob.Sl_Sale_dis = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreFrom = grid.Cells[4].Text;

                        iob.Debitcd = "401030100001";
                        iob.Creditcd = grid.Cells[5].Text;
                        iob.Amount = Convert.ToDecimal(grid.Cells[7].Text);
                        string Remarks = grid.Cells[6].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_SALE_DisCount(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void Sale_process_Ret()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gridSale_Ret.Rows)
            {
                try
                {
                    if (grid.Cells[0].Text == "IRTS")
                    {
                        Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' and SERIALNO like '15%'", lblSerial_SALE);
                        if (lblSerial_SALE.Text == "")
                        {
                            serialNo = "15000";
                            iob.Serial_SALE = serialNo;
                        }
                        else
                        {
                            sl = int.Parse(lblSerial_SALE.Text);
                            serial = sl + 1;

                            iob.Serial_SALE = serial.ToString();
                        }

                        iob.Transtp = "JOUR";
                        iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                        iob.Monyear = grid.Cells[2].Text;
                        iob.TransNo = grid.Cells[3].Text;
                        iob.StoreFrom = grid.Cells[4].Text;

                        iob.Debitcd = "301010100002";
                        iob.Creditcd = grid.Cells[6].Text;
                        iob.Amount = Convert.ToDecimal(grid.Cells[11].Text);
                        string Remarks = grid.Cells[10].Text;
                        if (Remarks == "&nbsp;")
                        {
                            iob.Remarks = "";
                        }
                        else
                            iob.Remarks = Remarks;



                        dob.doProcess_SALE_Ret(iob);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void LC_Process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;
            iob.Userpc = PCName;
            iob.Ipaddress = ipAddress;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gridLC.Rows)
            {
                try
                {
                    Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MPAY' ", lblSerial_LC);

                    if (lblSerial_LC.Text == "")
                    {
                        serialNo = "20000";
                        iob.SerialNo_MREC = serialNo;
                    }
                    else
                    {
                        sl = int.Parse(lblSerial_LC.Text);
                        serial = sl + 1;
                        iob.SerialNo_MREC = serial.ToString();
                    }
                    iob.Transtp = grid.Cells[0].Text;
                    iob.Transdt = DateTime.Parse(grid.Cells[1].Text);
                    iob.Monyear = grid.Cells[2].Text;
                    iob.TransNo = grid.Cells[3].Text;
                    iob.Debitcd = grid.Cells[4].Text;
                    iob.Creditcd = grid.Cells[5].Text;
                    iob.Amount = Convert.ToDecimal(grid.Cells[6].Text);
                    string Remarks = grid.Cells[7].Text;
                    if (Remarks == "&nbsp;")
                    {
                        iob.Remarks = "";
                    }
                    else
                        iob.Remarks = Remarks;

                    dob.doProcess_LC(iob);

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void job_Expense_Process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;
            iob.Userpc = PCName;
            iob.Ipaddress = ipAddress;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gvJobExpense.Rows)
            {
                try
                {
                    lblSerial_JourJobExp.Text = "";
                    Global.lblAdd(@"Select max(SERIALNO) FROM GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP IN ('JOUR','MPAY') AND TABLEID ='CNF_JOBEXP'", lblSerial_JourJobExp);
                    if (lblSerial_JourJobExp.Text == "")
                    {
                        serialNo = "30000";
                        iob.Serial_Job_Exp_Jour = serialNo;
                    }
                    else
                    {
                        sl = int.Parse(lblSerial_JourJobExp.Text);
                        serial = sl + 1;
                        iob.Serial_Job_Exp_Jour = serial.ToString();
                    }
                    iob.Transdt = Transdt;
                    iob.Creditcd = grid.Cells[3].Text;
                    string par = iob.Creditcd.Substring(0, 5);
                    if (par == "10201")
                        iob.Transtp = "MPAY";
                    else
                        iob.Transtp = "JOUR";
                    iob.Monyear = grid.Cells[0].Text;
                    iob.TransNo = grid.Cells[1].Text;
                    iob.Costpid = grid.Cells[2].Text;
                    iob.Debitcd = "401010200001";
                    
                    iob.Amount = Convert.ToDecimal(grid.Cells[4].Text);
                    string Remarks = grid.Cells[5].Text;
                    if (Remarks == "&nbsp;")
                    {
                        iob.Remarks = "";
                    }
                    else
                        iob.Remarks = Remarks;

                    dob.doProcess_job_Expense(iob);

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void job_Receive_Process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;
            iob.Userpc = PCName;
            iob.Ipaddress = ipAddress;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gvJobReceive.Rows)
            {
                try
                {
                    lblSerial_JobRec.Text = "";
                    Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' AND TABLEID ='CNF_JOBRCV'", lblSerial_JobRec);
                    if (lblSerial_JobRec.Text == "")
                    {
                        serialNo = "40000";
                        iob.Serial_Job_Rec = serialNo;
                    }
                    else
                    {
                        sl = int.Parse(lblSerial_JobRec.Text);
                        serial = sl + 1;

                        iob.Serial_Job_Rec = serial.ToString();
                    }
                    iob.Transdt = Transdt;
                    iob.Transtp = "MREC";
                    iob.Monyear = grid.Cells[0].Text;
                    iob.TransNo = grid.Cells[1].Text;
                    iob.Transfor = grid.Cells[2].Text;
                    iob.Costpid = grid.Cells[3].Text;
                    iob.Debitcd = grid.Cells[4].Text;
                    iob.Creditcd = grid.Cells[5].Text;
                    //if (grid.Cells[10].Text == "&nbsp;")
                    //{
                    //    iob.Chequeno = null;
                    //}
                    //else
                    //iob.Chequeno = grid.Cells[10].Text;
                    //iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.Amount = Convert.ToDecimal(grid.Cells[6].Text);
                    string Remarks = grid.Cells[7].Text;
                    if (Remarks == "&nbsp;")
                    {
                        iob.Remarks = "";
                    }
                    else
                        iob.Remarks = Remarks;



                    dob.doProcess_Job_Receive(iob);

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        public void job_Bill_Process()
        {
            string userName = HttpContext.Current.Session["UserName"].ToString();
            string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
            string PCName = HttpContext.Current.Session["PCName"].ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
            AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
            string serialNo = "";
            int sl, serial;

            iob.Username = userName;
            iob.Userpc = PCName;
            iob.Ipaddress = ipAddress;

            DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
            string mon = Transdt.ToString("MMM").ToUpper();
            string year = Transdt.ToString("yy");
            string monyear = mon + '-' + year;
            string trans_DT = Transdt.ToString("yyyy/MM/dd");

            foreach (GridViewRow grid in gvBill.Rows)
            {
                try
                {
                    lblSerial_JobRec.Text = "";
                    Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' AND TABLEID ='CNF_JOBBILL'", lblSerial_Bill);
                    if (lblSerial_Bill.Text == "")
                    {
                        serialNo = "50000";
                        iob.Serial_Job_Bill = serialNo;
                    }
                    else
                    {
                        sl = int.Parse(lblSerial_Bill.Text);
                        serial = sl + 1;

                        iob.Serial_Job_Bill = serial.ToString();
                    }
                    iob.Transdt = Transdt;
                    iob.Transtp = "JOUR";
                    iob.Monyear = monyear;
                    iob.TransNo = grid.Cells[4].Text;
                    if (grid.Cells[0].Text == "C01")
                        iob.Creditcd = "301010100001";
                    else
                        iob.Creditcd = "301010100002";
                    iob.Costpid = grid.Cells[1].Text;
                    iob.Debitcd = grid.Cells[5].Text;
                    
                    //if (grid.Cells[10].Text == "&nbsp;")
                    //{
                    //    iob.Chequeno = null;
                    //}
                    //else
                    //iob.Chequeno = grid.Cells[10].Text;
                    //iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    iob.Amount = Convert.ToDecimal(grid.Cells[7].Text);
                    string Remarks = grid.Cells[6].Text;
                    if (Remarks == "&nbsp;")
                    {
                        iob.Remarks = "";
                    }
                    else
                        iob.Remarks = Remarks;



                    dob.doProcess_Job_Bill(iob);

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }

        //public void MicroCreditCollection_Process()
        //{
        //    string userName = HttpContext.Current.Session["UserName"].ToString();
        //    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
        //    string PCName = HttpContext.Current.Session["PCName"].ToString();
        //    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
        //    AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
        //    string serialNo = "";
        //    int sl, serial;

        //    iob.Username = userName;
        //    iob.Userpc = PCName;
        //    iob.Ipaddress = ipAddress;

        //    iob.Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
        //    DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
        //    string trans_DT = Transdt.ToString("yyyy/MM/dd");

        //    foreach (GridViewRow grid in gvMicCollection.Rows)
        //    {
        //        try
        //        {
        //            if (grid.Cells[0].Text == "MREC")
        //            {
        //                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' AND TABLEID ='MC_COLLECT'", lblSerial_Mrec);
        //                if (lblSerial_Mrec.Text == "")
        //                {
        //                    serialNo = "10000";
        //                    iob.SerialNo_MREC = serialNo;
        //                }
        //                else
        //                {
        //                    sl = int.Parse(lblSerial_Mrec.Text);
        //                    serial = sl + 1;

        //                    iob.SerialNo_MREC = serial.ToString();
        //                }

        //                iob.Transtp = grid.Cells[0].Text;
        //                iob.Monyear = grid.Cells[2].Text;
        //                iob.TransNo = grid.Cells[3].Text;
        //                iob.Debitcd = "102010100001";
        //                iob.Creditcd = grid.Cells[8].Text;
        //                iob.Amount = Convert.ToDecimal(grid.Cells[5].Text);
        //                string Remarks = grid.Cells[6].Text;
        //                if (Remarks == "&nbsp;")
        //                {
        //                    iob.Remarks = "";
        //                }
        //                else
        //                    iob.Remarks = Remarks;

        //                dob.doProcess_MicroCreditCollection(iob);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write(ex.Message);
        //        }
        //    }
        //}

        //public void MicroCreditCollectionMember_Process()
        //{
        //    string userName = HttpContext.Current.Session["UserName"].ToString();
        //    string ipAddress = HttpContext.Current.Session["IpAddress"].ToString();
        //    string PCName = HttpContext.Current.Session["PCName"].ToString();
        //    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connectionString);

        //    AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
        //    AlchemyAccounting.multipurpose.InterFace.multipuposeInterface muliob = new multipuposeInterface();

        //    string serialNo = "";
        //    int sl, serial;

        //    muliob.UserNm = userName;
        //    muliob.UserPc = PCName;
        //    muliob.Ip = ipAddress;

        //    muliob.TransDT = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
        //    DateTime Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
        //    string trans_DT = Transdt.ToString("yyyy/MM/dd");
        //    muliob.Year = Convert.ToInt16(Transdt.ToString("yyyy"));

        //    foreach (GridViewRow grid in gvMicCollectionMember.Rows)
        //    {
        //        try
        //        {
        //            if (grid.Cells[0].Text == "MREC")
        //            {
        //                Global.lblAdd(@"Select max(TRANSSL) FROM  MC_MLEDGER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' AND TABLEID ='MC_COLLECT'", lblSerial_Mrec);
        //                if (lblSerial_Mrec.Text == "")
        //                {
        //                    serialNo = "10000";
        //                    muliob.SerialNo_MREC = serialNo;
        //                }
        //                else
        //                {
        //                    sl = int.Parse(lblSerial_Mrec.Text);
        //                    serial = sl + 1;

        //                    muliob.SerialNo_MREC = serial.ToString();
        //                }

        //                muliob.TransTP = grid.Cells[0].Text;
        //                muliob.TransMY = grid.Cells[2].Text;
        //                muliob.DocNo = Convert.ToInt64(grid.Cells[3].Text);
        //                muliob.SchemeID = grid.Cells[4].Text;
        //                muliob.Amount = Convert.ToDecimal(grid.Cells[5].Text);
        //                muliob.SchTP = grid.Cells[9].Text;
        //                muliob.AccCD = grid.Cells[10].Text;
        //                string Remarks = grid.Cells[6].Text;
        //                if (Remarks == "&nbsp;")
        //                {
        //                    muliob.Remarks = "";
        //                }
        //                else
        //                    muliob.Remarks = Remarks;
        //                muliob.MemberID = grid.Cells[7].Text;
        //                muliob.InternalID = grid.Cells[8].Text;

        //                dob.doProcess_MicroCreditCollectionMember(muliob);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            Response.Write(ex.Message);
        //        }
        //    }
        //}

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            ShowGrid();
            ShowGrid_Purchase();
            ShowGrid_Purchase_Ret();
            ShowGrid_Sale();
            ShowGrid_Sale_Discount();
            ShowGrid_Sale_Ret();
            ShowGridLC();
            ShowGrid_Multiple();
            show_Grid_Job_Expense();
            show_Grid_Job_Receive();
            show_Grid_Bill_Process();
            //ShowGridMicroCreditCollection();
            //ShowGridMicroCreditCollectionMember();
            btnProcess.Focus();
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                if (txtDate.Text == "")
                {
                    Response.Write("<script>alert('Select a Date want to process?');</script>");
                }
                else
                {
                    string userName = HttpContext.Current.Session["UserName"].ToString();
                    AlchemyAccounting.Accounts.DataAccess.SingleVoucher dob = new DataAccess.SingleVoucher();
                    AlchemyAccounting.Accounts.Interface.SingleVoucher iob = new Interface.SingleVoucher();
                    string serialNo = "";
                    int sl, serial;

                    ShowGrid();
                    ShowGrid_Purchase();
                    ShowGrid_Purchase_Ret();
                    ShowGrid_Sale();
                    ShowGrid_Sale_Discount();
                    ShowGrid_Sale_Ret();
                    ShowGridLC();
                    ShowGrid_Multiple();
                    show_Grid_Job_Expense();
                    show_Grid_Job_Receive();
                    show_Grid_Bill_Process();
                    //ShowGridMicroCreditCollection();
                    //ShowGridMicroCreditCollectionMember();

                    iob.Transdt = (DateTime.Parse(txtDate.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal));
                    string trans_DT = iob.Transdt.ToString("yyyy/MM/dd");
                    iob.Username = userName;

                    string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'GL_STRANS' and TRANSTP <> 'OPEN'", conn);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd3 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'GL_MTRANS' and TRANSTP <> 'OPEN'", conn);
                    cmd3.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'STK_TRANS' and TRANSTP='JOUR'", conn);
                    cmd1.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'LC_EXPENSE' and TRANSTP='MPAY'", conn);
                    cmd2.ExecuteNonQuery();

                    SqlCommand cmd4 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'CNF_JOBEXP' and TRANSTP='JOUR'", conn);
                    cmd4.ExecuteNonQuery();

                    SqlCommand cmd5 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'CNF_JOBEXP' and TRANSTP='MPAY'", conn);
                    cmd5.ExecuteNonQuery();

                    SqlCommand cmd6 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'CNF_JOBRCV' and TRANSTP='MREC'", conn);
                    cmd6.ExecuteNonQuery();

                    SqlCommand cmd7 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'CNF_JOBBILL' and TRANSTP='JOUR'", conn);
                    cmd7.ExecuteNonQuery();

                    //SqlCommand cmd4 = new SqlCommand("Delete from GL_MASTER where TRANSDT='" + trans_DT + "' and TABLEID = 'MC_COLLECT' and TRANSTP='MREC'", conn);
                    //cmd4.ExecuteNonQuery();

                    //SqlCommand cmd5 = new SqlCommand("Delete from MC_MLEDGER where TRANSDT='" + trans_DT + "' and TABLEID = 'MC_COLLECT' and TRANSTP='MREC'", conn);
                    //cmd5.ExecuteNonQuery();

                    conn.Close();

                    foreach (GridViewRow grid in GridView1.Rows)
                    {
                        try
                        {
                            if (grid.Cells[0].Text == "MREC")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' ", lblSerial_Mrec);
                                if (lblSerial_Mrec.Text == "")
                                {
                                    serialNo = "1000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Mrec.Text);
                                    serial = sl + 1;

                                    iob.SerialNo_MREC = serial.ToString();
                                }

                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                //if (grid.Cells[10].Text == "&nbsp;")
                                //{
                                //    iob.Chequeno = null;
                                //}
                                //else
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;



                                dob.doProcess_MREC(iob);
                            }
                            else if (grid.Cells[0].Text == "MPAY")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MPAY' ", lblSerial_Mpay);
                                if (lblSerial_Mpay.Text == "")
                                {
                                    serialNo = "2000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Mpay.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_MPAY(iob);
                            }
                            else if (grid.Cells[0].Text == "JOUR")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' ", lblSerial_Jour);
                                if (lblSerial_Jour.Text == "")
                                {
                                    serialNo = "3000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Jour.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_JOUR(iob);
                            }
                            else if (grid.Cells[0].Text == "CONT")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'CONT' ", lblSerial_Cont);
                                if (lblSerial_Cont.Text == "")
                                {
                                    serialNo = "4000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Cont.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;


                                dob.doProcess_CONT(iob);
                            }


                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }

                    foreach (GridViewRow grid in gridMultiple.Rows)
                    {
                        try
                        {
                            if (grid.Cells[0].Text == "MREC")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MREC' ", lblSerial_Mrec);
                                if (lblSerial_Mrec.Text == "")
                                {
                                    serialNo = "1000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Mrec.Text);
                                    serial = sl + 1;

                                    iob.SerialNo_MREC = serial.ToString();
                                }

                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                //if (grid.Cells[10].Text == "&nbsp;")
                                //{
                                //    iob.Chequeno = null;
                                //}
                                //else
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;



                                dob.doProcess_MREC_Multiple(iob);
                            }
                            else if (grid.Cells[0].Text == "MPAY")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'MPAY' ", lblSerial_Mpay);
                                if (lblSerial_Mpay.Text == "")
                                {
                                    serialNo = "2000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Mpay.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_MPAY_Multiple(iob);
                            }
                            else if (grid.Cells[0].Text == "JOUR")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'JOUR' ", lblSerial_Jour);
                                if (lblSerial_Jour.Text == "")
                                {
                                    serialNo = "3000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Jour.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_JOUR_Multiple(iob);
                            }
                            else if (grid.Cells[0].Text == "CONT")
                            {
                                Global.lblAdd(@"Select max(SERIALNO) FROM  GL_MASTER where TRANSDT = '" + trans_DT + "' and TRANSTP = 'CONT' ", lblSerial_Cont);
                                if (lblSerial_Cont.Text == "")
                                {
                                    serialNo = "4000";
                                    iob.SerialNo_MREC = serialNo;
                                }
                                else
                                {
                                    sl = int.Parse(lblSerial_Cont.Text);
                                    serial = sl + 1;
                                    iob.SerialNo_MREC = serial.ToString();
                                }
                                iob.Transtp = grid.Cells[0].Text;
                                iob.Monyear = grid.Cells[2].Text;
                                iob.TransNo = grid.Cells[3].Text;
                                iob.Transfor = grid.Cells[5].Text;
                                iob.Costpid = grid.Cells[6].Text;
                                iob.Transmode = grid.Cells[7].Text;
                                iob.Debitcd = grid.Cells[8].Text;
                                iob.Creditcd = grid.Cells[9].Text;
                                iob.Chequeno = grid.Cells[10].Text;
                                iob.Chequedt = DateTime.Parse(grid.Cells[11].Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                                iob.Amount = Convert.ToDecimal(grid.Cells[12].Text);
                                string Remarks = grid.Cells[13].Text;
                                if (Remarks == "&nbsp;")
                                {
                                    iob.Remarks = "";
                                }
                                else
                                    iob.Remarks = Remarks;

                                dob.doProcess_CONT_Multiple(iob);
                            }


                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }

                    Buy_process();
                    Buy_process_Ret();
                    Sale_process();
                    Sale_Discount_process();
                    Sale_process_Ret();
                    LC_Process();
                    job_Expense_Process();
                    job_Receive_Process();
                    job_Bill_Process();
                    //MicroCreditCollection_Process();
                    //MicroCreditCollectionMember_Process();

                    Response.Write("<script>alert('Process Completed.');</script>");
                }
            }
        }
    }
}