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
    public partial class RptExportInfo : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);


        decimal totAmount = 0;
        string totAmountComma = "0";
        string ttAmt = "0";



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

                    DateTime t = DateTime.Now;
                    lblPrintDate.Text = t.ToString("dd/MM/yyy  hh:mm:ss:tt");
                }
            }
        }

        public void ShowGrid()
        {


            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);



            string companyname = Session["companyNM"].ToString();
            string companyID = Session["companyID"].ToString();
            string fwdate = Session["billFwdate"].ToString();
            string branch = Session["branch"].ToString();
            string partyNM = Session["partyNM"].ToString();
            string partyID = Session["partyID"].ToString();
            string remarks = Session["remarks"].ToString();


            lblCompanyNM.Text = companyname;
            lblJobType.Text = "EXPORT";
            lblPartyNM.Text = partyNM;
            lblcomp.Text = companyname;

            Global.lblAdd("select ADDRESS from ASL_COMPANY where COMPID='" + companyID + "' ", lblADD);
            Global.lblAdd("select ADDRESS from CNF_PARTY where PARTYID='" + partyID + "' ", lblBranch);

            SqlCommand cmd = new SqlCommand("SELECT  row_number() over(partition by BILLNO order by BILLNO) AS SL,   CNF_JOB.JOBYY, CNF_JOB.JOBTP, CNF_JOB.JOBNO, CNF_JOB.REGID, CNF_JOBBILL.BILLNO, (SUM(CNF_JOBBILL.BILLAMT) + CNF_JOB.COMM_AMT)  AS BILL  " +
                       " FROM CNF_JOB INNER JOIN " +
                    "  CNF_JOBSTATUS ON CNF_JOB.JOBTP = CNF_JOBSTATUS.JOBTP AND CNF_JOB.JOBNO = CNF_JOBSTATUS.JOBNO AND " +
                     " CNF_JOB.JOBYY = CNF_JOBSTATUS.JOBYY INNER JOIN " +
                     " CNF_JOBBILL ON CNF_JOB.JOBTP = CNF_JOBBILL.JOBTP AND CNF_JOB.JOBYY = CNF_JOBBILL.JOBYY AND CNF_JOB.JOBNO = CNF_JOBBILL.JOBNO " +
                    " WHERE     (CNF_JOB.JOBTP = 'EXPORT') AND  CNF_JOBSTATUS.BILLFDT ='" + fwdate + "' AND CNF_JOB.COMPID = '" + companyID + "' AND CNF_JOB.PARTYID='" + partyID + "' " +
                " GROUP BY CNF_JOB.JOBYY, CNF_JOB.JOBTP, CNF_JOB.JOBNO, CNF_JOB.REGID, CNF_JOB.COMM_AMT, CNF_JOBBILL.BILLNO,CNF_JOB.COMM_AMT", conn);
            cmd.Parameters.Clear();

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

                string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = SL;

                string JOBYY = DataBinder.Eval(e.Row.DataItem, "JOBYY").ToString();
                string REGID = DataBinder.Eval(e.Row.DataItem, "REGID").ToString();

                string BILLNO = DataBinder.Eval(e.Row.DataItem, "BILLNO").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + "FFSL" + '/' + REGID + '/' + "EXP" + '/' + BILLNO + '/' + JOBYY ;

                string JOBNO = DataBinder.Eval(e.Row.DataItem, "JOBNO").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + "FFSL" + '/' + REGID + '/' + "EXP" + '/' + JOBNO + '/' + JOBYY;

                decimal BILL = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BILL").ToString());
                string Amnt = SpellAmount.comma(BILL);
                e.Row.Cells[3].Text = Amnt + "&nbsp;";

                totAmount += BILL;
                ttAmt = totAmount.ToString();
                totAmountComma = SpellAmount.comma(totAmount);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total : ";
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = totAmountComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
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