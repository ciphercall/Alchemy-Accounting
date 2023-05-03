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
    public partial class rpt_Periodic_Detailed_Performace_Bill_Date_Wise : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        // To keep track of the previous row Group Identifier    
        string strPreviousRowID_OP = string.Empty;

        string _headname = "";
        int sl = 1;
        // To keep track the Index of Group Total    
        int intSubTotalIndex_OP = 1;

        // To temporarily store Sub Total    
        decimal dblSubTotalDRAMT_OP = 0;
        decimal dblSubTotalCRAMT_OP = 0;
        decimal dblSubTotalDRAMT_PL = 0;
        decimal dblSubTotalCRAMT_RCV = 0;
        decimal dblSubTotalDRAMT_DUE = 0;
        // To temporarily store Grand Total    
        decimal dblGrandTotalDRAMT_OP = 0;
        decimal dblGrandTotalCRAMT_OP = 0;
        decimal dblGrandTotalDRAMT_PL = 0;
        decimal dblGrandTotalCRAMT_RCV = 0;
        decimal dblGrandTotalDRAMT_DUE = 0;
        //string AmountComma = "";
        string dblSubTotalDRAMTComma_OP = "0";
        string dblSubTotalCRAMTComma_OP = "0";
        string dblSubTotalDRAMTComma_PL = "0";
        string dblSubTotalCRAMTComma_RCV = "0";
        string dblSubTotalDRAMTComma_DUE = "0";

        string _dblGrandTotalDramtCommaOp = "0";
        string dblGrandTotalCRAMTComma_OP = "0";
        string _dblGrandTotalDramtCommaPL = "0";
        string dblGrandTotalCRAMTComma_RCV = "0";
        string _dblGrandTotalDramtCommaDUE = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            string brCD = HttpContext.Current.Session["BrCD"].ToString();

            Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblCompNM);
            Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY WHERE COMPID ='" + brCD + "'", lblAddress);

            DateTime PrintDate = Global.Timezone(DateTime.Now);
            string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
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

            DateTime FDate = DateTime.Parse(From, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string FrDT = FDate.ToString("yyyy/MM/dd");

            DateTime TDate = DateTime.Parse(To, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string ToDT = TDate.ToString("yyyy/MM/dd");

            conn.Open();
            string uTp = HttpContext.Current.Session["UserTp"].ToString();
            string brCD = HttpContext.Current.Session["BrCD"].ToString();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand(@"SELECT C.ACCOUNTNM,ROW_NUMBER() OVER(ORDER BY C.ACCOUNTNM) AS SL , C.JOBNO, CONVERT(NVARCHAR(20), C.BILLDT, 103) AS BILLDT, 
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
WHERE (dbo.CNF_JOBBILL.BILLDT BETWEEN @FDT AND @TDT) AND (dbo.CNF_JOBBILL.JOBTP = @JOBTP) AND (dbo.CNF_JOBBILL.COMPID = @COMPID)
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
GROUP BY C.ACCOUNTNM,C.JOBNO, C.BILLDT, C.BILLNO, C.BILLAMT, C.JOBYY, C.JOBTP,  C.EXPAMT , C.REGID,C.JOBCDT", conn);

            cmd.Parameters.AddWithValue("@FDT", FrDT);
            cmd.Parameters.AddWithValue("@TDT", ToDT);
            cmd.Parameters.AddWithValue("@COMPID", companyid);
            cmd.Parameters.AddWithValue("@JOBTP", jobTp);
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
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            bool IsSubTotalRowNeedToAdd_OP = false;
            bool IsGrandTotalRowNeedtoAdd_OP = false;
            if ((strPreviousRowID_OP != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM") != null))
                if (strPreviousRowID_OP != DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString())
                    IsSubTotalRowNeedToAdd_OP = true;
            if ((strPreviousRowID_OP != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM") == null))
            {
                IsSubTotalRowNeedToAdd_OP = true;
                IsGrandTotalRowNeedtoAdd_OP = true;
                intSubTotalIndex_OP = 0;
            }
            //#region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID_OP == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM") != null))
            {
                GridView GridView1 = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "&nbsp;" + DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString(); //////// Sub Header Name
                cell.ColumnSpan = 11;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_OP, row);
                intSubTotalIndex_OP++;
            }
            //#endregion
            if (IsSubTotalRowNeedToAdd_OP)
            {
                //#region Adding Sub Total Row
                GridView GridView1 = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Sub Total : ";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.ColumnSpan = 6;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);



                //Adding DRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalDRAMTComma_OP);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
                //Adding CRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCRAMTComma_OP);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding DRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalDRAMTComma_PL);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);


                //Adding DRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalCRAMTComma_RCV);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding DRAMT Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalDRAMTComma_DUE);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);


                //Adding the Row at the RowIndex position in the Grid      
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_OP, row);
                intSubTotalIndex_OP++;
                //#endregion
                //#region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "&nbsp;" + DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString(); //////// Sub Header Name
                    cell.ColumnSpan = 11;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex_OP, row);
                    intSubTotalIndex_OP++;
                }
                //#endregion
                //#region Reseting the Sub Total Variables
                dblSubTotalDRAMT_OP = 0;
                dblSubTotalCRAMT_OP = 0;
                dblSubTotalDRAMT_PL = 0;
                dblSubTotalCRAMT_RCV = 0;
                dblSubTotalDRAMT_DUE = 0;
                //#endregion
            }
            if (IsGrandTotalRowNeedtoAdd_OP)
            {
                //#region Grand Total Row
                GridView GridView1 = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "Opening Balance Total : ";
                cell.HorizontalAlign = HorizontalAlign.Left;
                //cell.ColumnSpan = 2;
                cell.ColumnSpan = 6;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);





                ////Adding DRAMT Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", _dblGrandTotalDramtCommaOp);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding CRAMT Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalCRAMTComma_OP);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                ////Adding DRAMT Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", _dblGrandTotalDramtCommaPL);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                ////Adding DRAMT Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalCRAMTComma_RCV);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                ////Adding DRAMT Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", _dblGrandTotalDramtCommaDUE);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);


                //Adding the Row at the RowIndex position in the Grid     
                GridView1.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                //#endregion
            }
        }

        /// <summary>    
        /// Event fires when data binds to each row   
        /// Used for calculating Group Total     
        /// </summary>   
        /// /// <param name="sender"></param>    
        /// <param name="e"></param>    
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID_OP = DataBinder.Eval(e.Row.DataItem, "ACCOUNTNM").ToString();
                // string SL = DataBinder.Eval(e.Row.DataItem, "SL").ToString();

                if (strPreviousRowID_OP == _headname)
                {
                    sl++;
                    e.Row.Cells[0].Text = "&nbsp;" + sl;
                }
                else
                {
                    sl = 1;
                    _headname = strPreviousRowID_OP;
                    e.Row.Cells[0].Text = "&nbsp;" + sl;
                }
                //e.Row.Cells[0].Text = "&nbsp;" + SL;

                string JOBNO = DataBinder.Eval(e.Row.DataItem, "JOBNO").ToString();
                e.Row.Cells[1].Text = "&nbsp;" + JOBNO;
                string REGID = DataBinder.Eval(e.Row.DataItem, "REGID").ToString();
                e.Row.Cells[2].Text = "&nbsp;" + REGID;
                string JOBCDT = DataBinder.Eval(e.Row.DataItem, "JOBCDT").ToString();
                e.Row.Cells[3].Text = "&nbsp;" + JOBCDT;
                string BILLNO = DataBinder.Eval(e.Row.DataItem, "BILLNO").ToString();
                e.Row.Cells[4].Text = "&nbsp;" + BILLNO;
                string BILLDT = DataBinder.Eval(e.Row.DataItem, "BILLDT").ToString();
                e.Row.Cells[5].Text = "&nbsp;" + BILLDT;

                //decimal DrAmount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DRAMT").ToString());
                //string DRAMT = SpellAmount.comma(DrAmount);
                //e.Row.Cells[1].Text = DRAMT;
                //decimal dblDRAMT = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DRAMT").ToString());


                decimal billamtDecimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BILLAMT").ToString());
                string BILLAMT = SpellAmount.comma(billamtDecimal);
                e.Row.Cells[6].Text = BILLAMT;
                decimal dblbillamt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "BILLAMT").ToString());

                decimal expamtDecimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXPAMT").ToString());
                string EXPAMT = SpellAmount.comma(expamtDecimal);
                e.Row.Cells[7].Text = EXPAMT;
                decimal dblExpamt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "EXPAMT").ToString());


                decimal plamtDecimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PLAMT").ToString());
                string PLAMT = SpellAmount.comma(plamtDecimal);
                e.Row.Cells[8].Text = PLAMT;
                decimal dblPlamt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PLAMT").ToString());

                decimal rcvamtDecimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RCVAMT").ToString());
                string RCVAMT = SpellAmount.comma(rcvamtDecimal);
                e.Row.Cells[9].Text = RCVAMT;
                decimal dblRcvamt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RCVAMT").ToString());


                decimal dueamtDecimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DUEAMT").ToString());
                string DUEAMT = SpellAmount.comma(dueamtDecimal);
                e.Row.Cells[10].Text = DUEAMT;
                decimal dblDueamt = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DUEAMT").ToString());

                // Cumulating Grand Total           
                dblSubTotalDRAMT_OP += dblbillamt;
                dblSubTotalDRAMTComma_OP = SpellAmount.comma(dblSubTotalDRAMT_OP);
                dblGrandTotalDRAMT_OP += dblbillamt;
                _dblGrandTotalDramtCommaOp = SpellAmount.comma(dblGrandTotalDRAMT_OP);

                // Cumulating Sub Total   
                dblSubTotalCRAMT_OP += dblExpamt;
                dblSubTotalCRAMTComma_OP = SpellAmount.comma(dblSubTotalCRAMT_OP);
                dblGrandTotalCRAMT_OP += dblExpamt;
                dblGrandTotalCRAMTComma_OP = SpellAmount.comma(dblGrandTotalCRAMT_OP);

                // Cumulating Sub Total   
                dblSubTotalDRAMT_PL += dblPlamt;
                dblSubTotalDRAMTComma_PL = SpellAmount.comma(dblSubTotalDRAMT_PL);
                dblGrandTotalDRAMT_PL += dblPlamt;
                _dblGrandTotalDramtCommaPL = SpellAmount.comma(dblGrandTotalDRAMT_PL);

                // Cumulating Sub Total   
                dblSubTotalCRAMT_RCV += dblRcvamt;
                dblSubTotalCRAMTComma_RCV = SpellAmount.comma(dblSubTotalCRAMT_RCV);
                dblGrandTotalCRAMT_RCV += dblRcvamt;
                dblGrandTotalCRAMTComma_RCV = SpellAmount.comma(dblGrandTotalCRAMT_RCV);

                // Cumulating Sub Total   
                dblSubTotalDRAMT_DUE += dblDueamt;
                dblSubTotalDRAMTComma_DUE = SpellAmount.comma(dblSubTotalDRAMT_DUE);
                dblGrandTotalDRAMT_DUE += dblDueamt;
                _dblGrandTotalDramtCommaDUE = SpellAmount.comma(dblGrandTotalDRAMT_DUE);
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