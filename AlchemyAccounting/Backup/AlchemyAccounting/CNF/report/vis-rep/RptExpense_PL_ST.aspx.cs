﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class RptExpense_PL_ST : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        decimal totAmount = 0;
        decimal totAmount2 = 0;
        decimal totAmount3 = 0;

        string totAmountComma = "0";
        string totAmountComma2 = "0";
        string totAmountComma3 = "0";
        string ttAmt = "0";
        string ttAmt2 = "0";
        string ttAmt3 = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/user/Login.aspx");
            }

            else
            {
                if (!IsPostBack)
                {

                    ShowGrid();
                }
            }
        }

        public void ShowGrid()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string fromDate = Session["fromdate"].ToString();
            DateTime FRDT = DateTime.Parse(fromDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FDT = FRDT.ToString("yyyy-MM-dd");

            DateTime dt = DateTime.Now;

            lblPrintDate.Text = dt.ToString("yyyy-MM-dd h:mm:ss tt");

            lblFromdate.Text = fromDate;

            string todate = Session["todate"].ToString();
            DateTime TODT = DateTime.Parse(todate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string TDT = TODT.ToString("yyyy-MM-dd");
            lblTodate.Text = todate;

            string expid = Session["expenseID"].ToString();

            Global.lblAdd("select EXPNM from CNF_EXPENSE where EXPID='" + expid + "'", lblExpenseNM);
            lblExpenseID.Text = expid;

            SqlCommand cmd = new SqlCommand("SELECT GL_ACCHART.ACCOUNTNM, ASL_COMPANY.BRANCHID, CNF_JOBBILL.JOBYY, CNF_JOBBILL.JOBTP, CNF_JOBBILL.JOBNO, CNF_JOBBILL.EXPAMT, CNF_JOBBILL.BILLAMT FROM ASL_COMPANY INNER JOIN CNF_JOBBILL ON ASL_COMPANY.COMPID = CNF_JOBBILL.COMPID INNER JOIN GL_ACCHART ON CNF_JOBBILL.PARTYID = GL_ACCHART.ACCOUNTCD WHERE CNF_JOBBILL.BILLDT BETWEEN @FROMDATE AND @TODATE AND CNF_JOBBILL.EXPID='" + expid + "' ORDER BY CNF_JOBBILL.JOBYY, CNF_JOBBILL.JOBNO", conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@FROMDATE", FDT);
            cmd.Parameters.AddWithValue("@TODATE", TDT);
            conn.Open();
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
                gvReport.DataSource = ds;
                gvReport.DataBind();
                gvReport.Visible = true;
            }
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               

                string JOBNO = DataBinder.Eval(e.Row.DataItem, "JOBNO").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + JOBNO;

                string JOBYY = DataBinder.Eval(e.Row.DataItem, "JOBYY").ToString();
                e.Row.Cells[1].Text = JOBYY;

                string JOBTP = DataBinder.Eval(e.Row.DataItem, "JOBTP").ToString();
                e.Row.Cells[2].Text = JOBTP;

                string BRANCHID = DataBinder.Eval(e.Row.DataItem, "BRANCHID").ToString();
                e.Row.Cells[3].Text = BRANCHID;

                string ACCOUNTNM = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                e.Row.Cells[4].Text =" " + ACCOUNTNM;

                decimal EXPAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXPAMT").ToString());
                string Amnt = SpellAmount.comma(EXPAMT);
                e.Row.Cells[5].Text = Amnt + "&nbsp;";

                totAmount += EXPAMT;
                ttAmt = totAmount.ToString();
                totAmountComma = SpellAmount.comma(totAmount);


                decimal BILLAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BILLAMT").ToString());
                string Amt = SpellAmount.comma(BILLAMT);
                e.Row.Cells[6].Text = Amt + "&nbsp;";

                totAmount2 += BILLAMT;
                ttAmt = totAmount.ToString();
                totAmountComma2 = SpellAmount.comma(totAmount2);


                decimal PLAMT = Convert.ToDecimal(( BILLAMT - EXPAMT).ToString());
                string plAmt = SpellAmount.comma(PLAMT);
                e.Row.Cells[7].Text = plAmt + "&nbsp;";

                totAmount3 += PLAMT;
                ttAmt = totAmount.ToString();
                totAmountComma3 = SpellAmount.comma(totAmount3);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Total : ";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[5].Text = totAmountComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[6].Text = totAmountComma2;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[7].Text = totAmountComma3;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                lblInWords.Text = "";

                decimal dec;
                Boolean ValidInput = Decimal.TryParse(ttAmt, out dec);
                if (!ValidInput)
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Enter the Proper Amount...";
                    return;
                }
                if (ttAmt.ToString().Trim() == "")
                {
                    lblInWords.ForeColor = System.Drawing.Color.Red;
                    lblInWords.Text = "Amount Cannot Be Empty...";
                    return;
                }
                else
                {
                    if (Convert.ToDecimal(ttAmt) == 0)
                    {
                        lblInWords.ForeColor = System.Drawing.Color.Red;
                        lblInWords.Text = "Amount Cannot Be Empty...";
                        return;
                    }
                }

                string x1 = "";
                string x2 = "";

                if (ttAmt.Contains("."))
                {
                    x1 = ttAmt.ToString().Trim().Substring(0, ttAmt.ToString().Trim().IndexOf("."));
                    x2 = ttAmt.ToString().Trim().Substring(ttAmt.ToString().Trim().IndexOf(".") + 1);
                }
                else
                {
                    x1 = ttAmt.ToString().Trim();
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

                ttAmt = x1 + "." + x2;

                if (x2.Length > 2)
                {
                    ttAmt = Math.Round(Convert.ToDouble(ttAmt), 2).ToString().Trim();
                }

                string AmtConv = SpellAmount.MoneyConvFn(ttAmt.ToString().Trim());

                lblInWords.Text = AmtConv.Trim();

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