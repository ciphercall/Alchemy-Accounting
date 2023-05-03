using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlchemyAccounting.CNF.report.vis_rep
{
    public partial class rpt_PartyWiseBill : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);


        decimal billAmount = 0;
        string BillAmountcomma = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
            Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);

            DateTime PrintDate = Global.Timezone(DateTime.Now);
            string td = PrintDate.ToString("dd-MMM-yyyy");
            lblTime.Text = td;

            string From = Session["FromDate"].ToString();
            string To = Session["Todate"].ToString();

            string companyid = Session["Companyid"].ToString();
            string branchid = Session["company"].ToString();
            string jobTp = Session["JOBTP"].ToString();
            string branch = Session["branch"].ToString();
            //Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID='" + companyid + "'", lblSationName);
            lblSationName.Text = branchid;
            lblJobType.Text = jobTp;

            DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblFDate.Text = FDate.ToString("dd-MMM-yyyy");

            DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            lblTDate.Text = TDate.ToString("dd-MMM-yyyy");
            showGrid_OP();
            lblinword.Text= SpellAmount.MoneyConvFn(BillAmountcomma.Trim());
        }
        public void showGrid_OP()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string From = Session["FromDate"].ToString();
            string To = Session["Todate"].ToString();

            string companyid = Session["Companyid"].ToString();
            string branchid = Session["company"].ToString();
            string jobTp = Session["JOBTP"].ToString();
            string branch = Session["branch"].ToString();
            string partyId = Session["partyId"].ToString();
            lblPartyaddress.Text = Global.StringData(@"SELECT ADDRESS FROM CNF_PARTY WHERE (PARTYID = '{partyId}')");
            lblPartyName.Text = Session["partyName"].ToString();

            DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FrDT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string ToDT = TDate.ToString("yyyy/MM/dd");

            conn.Open();
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand(@"SELECT C.ACCOUNTNM,ROW_NUMBER() OVER(ORDER BY C.JOBCDT) AS SL , C.JOBNO, CONVERT(NVARCHAR(20), C.BILLDT, 103) AS BILLDT, 
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
WHERE (dbo.CNF_JOBBILL.BILLDT BETWEEN @FDT AND @TDT) AND (dbo.CNF_JOBBILL.JOBTP = @JOBTP) AND (dbo.CNF_JOBBILL.COMPID = @COMPID)  AND (dbo.CNF_JOBBILL.PARTYID = @PARTYID)
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
GROUP BY C.ACCOUNTNM,C.JOBNO, C.BILLDT, C.BILLNO, C.BILLAMT, C.JOBYY, C.JOBTP,  C.EXPAMT , C.REGID,C.JOBCDT ORDER BY C.JOBCDT", conn);

            cmd.Parameters.AddWithValue("@FDT", FrDT);
            cmd.Parameters.AddWithValue("@TDT", ToDT);
            cmd.Parameters.AddWithValue("@COMPID", companyid);
            cmd.Parameters.AddWithValue("@JOBTP", jobTp);
            cmd.Parameters.AddWithValue("@PARTYID", partyId);
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
                GridView1.Visible = false;
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string jobTp = Session["JOBTP"].ToString();
                try
                {
                    string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();
                    e.Row.Cells[0].Text = SL;

                    string BILLDT = DataBinder.Eval(e.Row.DataItem, "BILLDT").ToString();
                    e.Row.Cells[1].Text = BILLDT;

                    string REGID = DataBinder.Eval(e.Row.DataItem, "REGID").ToString();
                    e.Row.Cells[2].Text = REGID;

                    string JOBNO = DataBinder.Eval(e.Row.DataItem, "JOBNO").ToString();
                    string JOBYY = DataBinder.Eval(e.Row.DataItem, "JOBYY").ToString();
                    string jobtp = jobTp.Substring(0, 3);
                    string jobyear = JOBYY.Substring(2, 2);
                    e.Row.Cells[3].Text = jobtp+"/"+ JOBNO+"/"+ jobyear;
                    
                    string BILLNO = DataBinder.Eval(e.Row.DataItem, "BILLNO").ToString();
                    string billyear = BILLDT.Substring(6, 4);
                    e.Row.Cells[4].Text = jobtp + "/" + BILLNO + "/" + billyear;

                    decimal BILLAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BILLAMT").ToString());
                    string BAMT = SpellAmount.comma(BILLAMT);
                    e.Row.Cells[5].Text = BAMT;
                    
                    billAmount += BILLAMT;
                    BillAmountcomma = SpellAmount.comma(billAmount);
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "BILL AMOUNT : ";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = "Tk."+BillAmountcomma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
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