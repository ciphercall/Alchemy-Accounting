using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class rpt_Periodic_summarized_Performace_Bill : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        decimal expAmount = 0;
        string ExpAmountcomma = "0";

        decimal billAmount = 0;
        string BillAmountcomma = "0";

        decimal PLAmount = 0;
        string PLAmountcomma = "0";

        decimal discountAmount = 0;
        string discountAmountcomma = "0";


        decimal recAmount = 0;
        string RecAmountcomma = "0";

        decimal dueAmount = 0;
        string DueAmountcomma = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                string Companyid = Session["Companyid"].ToString();


                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + Companyid + "'", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID='" + Companyid + "'", lblAddress);

                string From = Session["FromDateBillSummary"].ToString();
                string To = Session["TodateBillSummary"].ToString();

                DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblFromdate.Text = FDate.ToString("dd-MMM-yyyy");

                DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblTodate.Text = TDate.ToString("dd-MMM-yyyy");

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                showGrid();
            }

        }
        public void showGrid()
        {
            SqlConnection conn = new SqlConnection(Global.connection);
            string From = Session["FromDateBillSummary"].ToString();
            string To = Session["TodateBillSummary"].ToString();


            DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FrDT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string ToDT = TDate.ToString("yyyy/MM/dd");
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT D.ACCOUNTNM, ROW_NUMBER() OVER(ORDER BY D.ACCOUNTNM ) AS SL, 
SUM(D.BILLAMT) AS BILLAMT,SUM(D.EXPAMT) AS EXPAMT,SUM(D.DISCAMT) AS DISCAMT,SUM(D.RCVAMT) AS RCVAMT,
SUM(D.PLAMT) AS PLAMT,SUM(D.DUEAMT) AS DUEAMT   FROM (
SELECT C.ACCOUNTNM,  ROW_NUMBER() OVER(ORDER BY C.ACCOUNTNM) AS SL ,C.JOBNO, CONVERT(NVARCHAR(20), C.BILLDT, 103) AS BILLDT, 
CONVERT(NVARCHAR(20), C.JOBCDT, 103) AS JOBCDT, C.BILLNO, C.BILLAMT, C.JOBYY, C.JOBTP,  C.REGID,
SUM(ISNULL(C.RCVAMT,0)) AS RCVAMT, SUM(ISNULL(C.DISCAMT,0)) AS DISCAMT, 
(C.BILLAMT - SUM(ISNULL(C.RCVAMT,0)) - SUM(ISNULL(C.DISCAMT,0))) AS DUEAMT,
 ISNULL(C.EXPAMT,0) EXPAMT, C.BILLAMT - ISNULL(C.EXPAMT, 0) AS PLAMT
FROM(
 SELECT B.JOBNO, CONVERT(NVARCHAR(20), B.BILLDT, 103) AS BILLDT, B.BILLNO, B.BILLAMT, B.JOBYY, B.JOBTP, B.REGID,B.JOBCDT,
ISNULL(B.RCVAMT,0) AS RCVAMT, ISNULL(B.DISCAMT,0) AS DISCAMT, B.ACCOUNTNM, 
SUM(ISNULL(dbo.CNF_JOBEXP.EXPAMT, 0)) AS EXPAMT, B.BILLAMT - SUM(ISNULL(dbo.CNF_JOBEXP.EXPAMT, 0)) AS PLAMT
FROM( 
SELECT A.JOBNO, CONVERT(NVARCHAR(20), A.BILLDT, 103) AS BILLDT, A.BILLNO, A.BILLAMT, A.JOBYY, A.JOBTP,A.REGID, A.JOBCDT,
(CASE WHEN TRANSFOR != 'Discount' THEN SUM(ISNULL(dbo.CNF_JOBRCV.AMOUNT,0)) END) AS RCVAMT, (CASE WHEN TRANSFOR = 'Discount' THEN SUM(ISNULL(dbo.CNF_JOBRCV.AMOUNT,0)) END) AS DISCAMT, A.ACCOUNTNM
FROM ( 
SELECT dbo.CNF_JOBBILL.JOBNO, dbo.CNF_JOBBILL.BILLDT, dbo.CNF_JOBBILL.BILLNO, CNF_JOB.REGID,CNF_JOB.JOBCDT,
SUM(ISNULL(dbo.CNF_JOBBILL.BILLAMT, 0)) + ISNULL(dbo.CNF_JOB.COMM_AMT, 0) AS BILLAMT, dbo.CNF_JOBBILL.JOBYY, dbo.CNF_JOBBILL.JOBTP, dbo.GL_ACCHART.ACCOUNTNM
FROM dbo.CNF_JOBBILL 
INNER JOIN dbo.GL_ACCHART ON dbo.CNF_JOBBILL.PARTYID = dbo.GL_ACCHART.ACCOUNTCD 
INNER JOIN dbo.CNF_JOB ON dbo.CNF_JOBBILL.JOBTP = dbo.CNF_JOB.JOBTP AND dbo.CNF_JOBBILL.JOBYY = dbo.CNF_JOB.JOBYY AND dbo.CNF_JOBBILL.JOBNO = dbo.CNF_JOB.JOBNO
WHERE (dbo.CNF_JOBBILL.BILLDT BETWEEN @FDT AND @TDT)
GROUP BY dbo.CNF_JOBBILL.JOBNO, dbo.CNF_JOBBILL.BILLDT, dbo.CNF_JOBBILL.BILLNO, dbo.CNF_JOB.COMM_AMT, 
dbo.CNF_JOBBILL.JOBYY, dbo.CNF_JOBBILL.JOBTP, dbo.GL_ACCHART.ACCOUNTNM,CNF_JOB.REGID,CNF_JOB.JOBCDT
)
 AS A 
 LEFT OUTER JOIN dbo.CNF_JOBRCV ON A.JOBNO = dbo.CNF_JOBRCV.JOBNO AND A.JOBTP = dbo.CNF_JOBRCV.JOBTP AND A.JOBYY = dbo.CNF_JOBRCV.JOBYY 
GROUP BY A.JOBNO, A.BILLDT, A.BILLNO, A.BILLAMT, TRANSFOR, A.JOBYY, A.JOBTP, A.ACCOUNTNM, A.REGID,A.JOBCDT
)
 AS B 
 LEFT OUTER JOIN dbo.CNF_JOBEXP ON B.JOBYY = dbo.CNF_JOBEXP.JOBYY AND B.JOBTP = dbo.CNF_JOBEXP.JOBTP AND B.JOBNO = dbo.CNF_JOBEXP.JOBNO 
GROUP BY B.JOBNO, B.BILLDT, B.BILLNO, B.BILLAMT, B.JOBYY, B.JOBTP, B.ACCOUNTNM, B.RCVAMT,B.DISCAMT , B.REGID,B.JOBCDT
) 
AS C 
GROUP BY C.ACCOUNTNM,C.JOBNO, C.BILLDT, C.BILLNO, C.BILLAMT, C.JOBYY, C.JOBTP,  C.EXPAMT , C.REGID,C.JOBCDT
) AS D
GROUP BY D.ACCOUNTNM ORDER BY D.ACCOUNTNM", conn);

            cmd.Parameters.AddWithValue("@FDT", FrDT);
            cmd.Parameters.AddWithValue("@TDT", ToDT);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //try
                //{
                string JOBNO = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                e.Row.Cells[0].Text = JOBNO;

                string BILLDT = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                e.Row.Cells[1].Text = BILLDT;

                string BILLAMT = DataBinder.Eval(e.Row.DataItem, "BILLAMT").ToString();
                if (BILLAMT == "")
                    BILLAMT = "0.00";
                decimal BILLAMT_C = Convert.ToDecimal(BILLAMT);
                string BAMT = SpellAmount.comma(BILLAMT_C);
                e.Row.Cells[2].Text = BAMT;


                decimal EXPAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXPAMT").ToString());
                string EXAMT = SpellAmount.comma(EXPAMT);
                e.Row.Cells[3].Text = EXAMT;

                decimal DISCAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DISCAMT").ToString());
                string DISCAMT_RAMT = SpellAmount.comma(DISCAMT);
                e.Row.Cells[4].Text = DISCAMT_RAMT;


                decimal PLAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PLAMT").ToString());
                string PLAMT_RAMT = SpellAmount.comma(PLAMT);
                e.Row.Cells[5].Text = PLAMT_RAMT;

                decimal RCVAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RCVAMT").ToString());
                string RAMT = SpellAmount.comma(RCVAMT);
                e.Row.Cells[6].Text = RAMT;

                decimal DUEAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DUEAMT").ToString());
                string DAMT = SpellAmount.comma(DUEAMT);
                e.Row.Cells[7].Text = DAMT;

                if (DUEAMT > 0)
                    e.Row.Cells[7].CssClass = "cellred";
                else
                    e.Row.Cells[7].CssClass = "cellgreen";

                expAmount += EXPAMT;
                ExpAmountcomma = SpellAmount.comma(expAmount);

                billAmount += BILLAMT_C;
                BillAmountcomma = SpellAmount.comma(billAmount);


                discountAmount += DISCAMT;
                discountAmountcomma = SpellAmount.comma(discountAmount);

                PLAmount += PLAMT;
                PLAmountcomma = SpellAmount.comma(PLAmount);

                recAmount += RCVAMT;
                RecAmountcomma = SpellAmount.comma(recAmount);

                dueAmount += DUEAMT;
                DueAmountcomma = SpellAmount.comma(dueAmount);
                //}
                //catch (Exception ex)
                //{
                //    Response.Write(ex.Message);
                //}
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Grand Total : ";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = BillAmountcomma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = ExpAmountcomma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = discountAmountcomma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = PLAmountcomma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = RecAmountcomma;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = DueAmountcomma;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
            }
            MakeGridViewPrinterFriendly(GridView1);
        }

        private void MakeGridViewPrinterFriendly(GridView grid)
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