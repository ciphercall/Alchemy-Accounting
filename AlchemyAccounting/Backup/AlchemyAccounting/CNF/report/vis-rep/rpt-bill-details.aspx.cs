using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class rpt_bill_details : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal totAmount = 0;

        string totAmountComma = "0";
        string ttAmt = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblPrintDate.Text = td;

                string jobno = Session["jobno"].ToString();
                string jobtp = Session["jobtp"].ToString();
                string jbtp = "";
                if (jobtp == "EXPORT")
                    jbtp = "EXP";
                else
                    jbtp = "IMP";
                string jobyear = Session["jobyear"].ToString();
                string compid = Session["compid"].ToString();
                string cmpid = "";
                if (compid == "C01")
                    cmpid = "FFSL-DHK";
                else
                    cmpid = "FFSL-CTG";
                string partyid = Session["partyid"].ToString();
                string jobdt = Session["jobdt"].ToString();

                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + compid + "'", lblCompanyNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + compid + "'", lblAddress);
                Global.lblAdd("SELECT DISTINCT BILLNO FROM CNF_JOBBILL WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + " AND PARTYID= '" + partyid + "'", lblBill);

                Global.lblAdd("SELECT REGID FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBTP ='" + jobtp + "' AND JOBYY =" + jobyear + "", lblReg);

                lblJobTP.Text = jobtp;
                lblJobNo.Text = cmpid + "/" + lblReg.Text + "/" + jbtp + "/" + jobno + "/" + jobyear;
                lblBillNo.Text = cmpid + "/" + jbtp + "/" + lblBill.Text + "/" + jobyear;

                DateTime jdt = DateTime.Parse(jobdt, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                string jd = jdt.ToString("dd-MMM-yy");
                lblJobDate.Text = jd;
                Global.lblAdd("SELECT ACCOUNTNM FROM GL_ACCHART WHERE ACCOUNTCD ='" + partyid + "'", lblAccount);
                Global.lblAdd("SELECT ADDRESS FROM CNF_PARTY WHERE PARTYID ='" + partyid + "'", lblAccAdd);
                Global.lblAdd("SELECT (CASE WHEN PERMITDT = '1900-01-01' THEN PERMITNO ELSE PERMITNO + ' - ' + CONVERT(NVARCHAR(20),PERMITDT,103) END) AS PERMIT FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblPermitNo);
                //Global.lblAdd("SELECT SUPPLIERNM FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblSupplierNm);
                Global.lblAdd("SELECT LCNO FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblLc);
                Global.lblAdd("SELECT (CASE WHEN LCDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20), LCDT,103) END) AS LCDT FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblLCDate);
                //Global.lblAdd("SELECT BENO FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblBE);
                Global.lblAdd("SELECT (CASE WHEN BEDT ='1900-01-01' THEN BENO ELSE BENO + ' - ' + CONVERT(NVARCHAR(20), BEDT,103) END) AS BEDT FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblBENo);
                Global.lblAdd("SELECT DOCINVNO FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblInvoiceNo);
                Global.lblAdd("SELECT (CASE WHEN DOCRCVDT ='1900-01-01' THEN null ELSE CONVERT(NVARCHAR(20), DOCRCVDT,103) END) AS DOCRCVDT FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblInvoiceDt);
                //Global.lblAdd("SELECT  AS DOCRCVDT FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblInDate);
                Global.lblAdd("SELECT GOODS FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblGoodsDesc);
                Global.lblAdd("SELECT PKGS FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblPackages);
                Global.lblAdd("SELECT (CASE WHEN BLDT ='1900-01-01' THEN BLNO ELSE BLNO + ' - ' + CONVERT(NVARCHAR(20), BLDT,103) END) AS BL FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblBLNO);
                Global.lblAdd("SELECT (CASE WHEN AWBDT ='1900-01-01' THEN AWBNO ELSE AWBNO + ' - ' + CONVERT(NVARCHAR(20), AWBDT,103) END) AS AWBNO FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblAwbNo);
                Global.lblAdd("SELECT (CASE WHEN UNTKDT ='1900-01-01' THEN UNTKNO ELSE UNTKNO + ' - ' + CONVERT(NVARCHAR(20), UNTKDT,103) END) AS HBLNO FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblHBLNO);
                Global.lblAdd("SELECT (CASE WHEN HAWBDT ='1900-01-01' THEN HAWBNO ELSE HAWBNO + ' - ' + CONVERT(NVARCHAR(20), HAWBDT,103) END) AS HBLNO FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblHAwbNo);
                //Global.lblAdd("SELECT DOCINVNO FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblShed);
                Global.lblAdd("SELECT CNFV_USD FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblValUSD);
                if (lblValUSD.Text == "")
                    lblValUSD.Text = "0";
                decimal valus = Convert.ToDecimal(lblValUSD.Text);
                lblValUSD.Text = SpellAmount.comma(valus);
                Global.lblAdd("SELECT CNFV_ERT FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblExRt);
                Global.lblAdd("SELECT CNFV_BDT FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblValTk);
                decimal valbd = Convert.ToDecimal(lblValTk.Text);
                if (lblValTk.Text == "")
                    lblValTk.Text = "0";
                lblValTk.Text = SpellAmount.comma(valbd);

                showGrid();

                Global.lblAdd("SELECT COMM_AMT FROM CNF_JOB WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + "", lblAgencyCom);
                if (lblAgencyCom.Text == "")
                    lblAgencyCom.Text = "0";
                decimal comamt = Convert.ToDecimal(lblAgencyCom.Text);
                lblAgencyCom.Text = SpellAmount.comma(comamt);
                decimal tot = Convert.ToDecimal(totAmountComma) + Convert.ToDecimal(lblAgencyCom.Text);
                string tt = tot.ToString("F");
                decimal convtt=Convert.ToDecimal(tt);
                string total = SpellAmount.comma(convtt);
                lblTotal.Text = total;
                Global.lblAdd("SELECT SUM(AMOUNT) AS AMOUNT FROM CNF_JOBRCV WHERE COMPID='" + compid + "' AND JOBYY=" + jobyear + " AND JOBTP='" + jobtp + "' AND JOBNO= " + jobno + " AND TRANSFOR ='Advance'", lblAdvAmt);
                if (lblAdvAmt.Text == "")
                    lblAdvAmt.Text = "0";
                decimal balance = Convert.ToDecimal(lblTotal.Text) - Convert.ToDecimal(lblAdvAmt.Text);
                string bl = balance.ToString("F");
                decimal convbl=Convert.ToDecimal(bl);
                string balamt = SpellAmount.comma(convbl);
                lblBalanceAmt.Text = balamt;

                Global.lblAdd("SELECT COM_REMARKS FROM CNF_JOB WHERE JOBNO =" + jobno + " AND JOBTP ='" + jobtp + "' AND JOBYY =" + jobyear + "", lblCRem);

                lblInWords.Text = "";
                decimal dec;
                Boolean ValidInput = Decimal.TryParse(bl, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (bl.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(bl) == 0)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                }

                string x1 = "";
                string x2 = "";

                if (bl.Contains("."))
                {
                    x1 = bl.ToString().Trim().Substring(0, bl.ToString().Trim().IndexOf("."));
                    x2 = bl.ToString().Trim().Substring(bl.ToString().Trim().IndexOf(".") + 1);
                }
                else
                {
                    x1 = bl.ToString().Trim();
                    x2 = "00";
                }

                if (x1.ToString().Trim() != "")
                {
                    x1 = Convert.ToInt64(x1.Trim()).ToString().Trim();
                }
                else
                {
                    x1 = "0";
                }

                bl = x1 + "." + x2;

                if (x2.Length > 2)
                {
                    bl = Math.Round(Convert.ToDouble(bl), 2).ToString().Trim();
                }

                string AmtConv = SpellAmount.MoneyConvFn(bl.ToString().Trim());

                lblInWords.Text = AmtConv.Trim();
            }
        }

        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(Global.connection);

            string jobno = Session["jobno"].ToString();
            string jobtp = Session["jobtp"].ToString();
            string jobyear = Session["jobyear"].ToString();
            string compid = Session["compid"].ToString();
            string partyid = Session["partyid"].ToString();
            string jobdt = Session["jobdt"].ToString();


            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() Over (Order by CNF_JOBBILL.BILLSL) AS SL, CNF_JOBBILL.BILLSL, CNF_EXPENSE.EXPNM, CNF_JOBBILL.REMARKS, (CASE WHEN CNF_JOBBILL.EXPPDT ='1900-01-01' THEN NULL ELSE CONVERT(NVARCHAR(20),CNF_JOBBILL.EXPPDT,103) END) AS EXPPDT, CNF_JOBBILL.BILLAMT " +
                       " FROM CNF_JOBBILL INNER JOIN CNF_EXPENSE ON CNF_JOBBILL.EXPID = CNF_EXPENSE.EXPID WHERE (CNF_JOBBILL.COMPID = '" + compid + "') AND (CNF_JOBBILL.JOBNO = " + jobno + ") AND " +
                       " (CNF_JOBBILL.JOBTP = '" + jobtp + "') AND (CNF_JOBBILL.JOBYY = " + jobyear + ") AND (CNF_JOBBILL.PARTYID='" + partyid + "') AND (CNF_JOBBILL.BILLAMT<>0.00) ORDER BY CNF_JOBBILL.BILLSL, CNF_JOBBILL.BILLDT", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvReport.DataSource = ds;
                gvReport.DataBind();
                gvReport.Visible = true;
            }
            else
            {
                gvReport.Visible = true;
            }
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = SL;

                string EXPNM = DataBinder.Eval(e.Row.DataItem, "EXPNM").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + EXPNM;

                string REMARKS = DataBinder.Eval(e.Row.DataItem, "REMARKS").ToString();
                e.Row.Cells[2].Text = REMARKS;

                string EXPPDT = DataBinder.Eval(e.Row.DataItem, "EXPPDT").ToString();
                e.Row.Cells[3].Text = EXPPDT;

                decimal BILLAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BILLAMT").ToString());
                string Amnt = SpellAmount.comma(BILLAMT);
                e.Row.Cells[4].Text = Amnt + "&nbsp;";

                totAmount += BILLAMT;
                ttAmt = totAmount.ToString();
                totAmountComma = SpellAmount.comma(totAmount);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[3].Text = "Total : ";
                //e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = totAmountComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;   
            }
            ShowHeader(gvReport);
        }

        private void ShowHeader(GridView grid)
        {
            if (grid.Rows.Count > 0)
            {
                grid.UseAccessibleHeader = true;
                grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                //gridView.HeaderRow.Style["display"] = "table-header-group";
            }
        }
    }
}