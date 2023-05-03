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
using System.Threading;

namespace AlchemyAccounting.Stock.Report.Report
{
    public partial class rpt_item_closing_stock : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal dblGrandTotalCartonQty = 0;
        decimal dblGrandTotalPieces = 0;
        decimal dblGrandTotalCLQty = 0;
        //decimal dblGrandTotalRate = 0;
        decimal dblGrandTotalAmount = 0;
        //string AmountComma = "";

        string dblGrandTotalCartonQtyComma = "";
        string dblGrandTotalPiecesComma = "";
        string dblGrandTotalCLQtyComma = "";
        string dblGrandTotalAmountComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login/UI/Login.aspx");
            }
            else
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string Date = Session["Date"].ToString();

                DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblDate.Text = asON.ToString("dd-MMM-yyyy");
                showGrid();
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string Date = Session["Date"].ToString();
            DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string aOn = asON.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT B.ITEMID, B.CLQTY, B.AVGRATE, (CASE WHEN B.CLQTY < 0 THEN 0.00 ELSE B.CLQTY * B.AVGRATE END) AS STOCKVALUE, STK_ITEM.ITEMNM, STK_ITEM.PQTY, " +
                     " (CASE WHEN STK_ITEM.PQTY = 0 THEN 0 ELSE FLOOR(B.CLQTY / PQTY) END) AS CARTONQTY, B.CLQTY - (CASE WHEN STK_ITEM.PQTY = 0 THEN 0 ELSE (FLOOR(B.CLQTY / PQTY) * PQTY) END) AS PIECES " +
                     " FROM (SELECT ITEMID, (SUM(INQTY) + SUM(BQTY)) - (SUM(OUTQTY) + SUM(SQTY)) AS CLQTY, SUM(BQTY) AS BQTY, SUM(BAMT) AS BAMT, SUM(SQTY) AS SQTY, SUM(SAMT) AS SAMT, (CASE WHEN SUM(BAMTR) = 0.00 THEN 0.00 ELSE CONVERT(decimal(18, 2), (SUM(BAMTR)) / SUM(BQTYR)) END) AS AVGRATE " +
                                            " FROM (SELECT ITEMID, SUM(QTY) AS BQTY, SUM(AMOUNT) AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 0 AS BAMTR FROM STK_TRANS WHERE (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'BUY') GROUP BY ITEMID " +
                                               " UNION " +
                                               " SELECT ITEMID, 0 AS BQTY, 0 AS BAMT, SUM(QTY) AS SQTY, SUM(AMOUNT) AS SAMT, 0 AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 0 AS BAMTR " +
                                               " FROM         STK_TRANS AS STK_TRANS_1 " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'SALE') " +
                                               " GROUP BY ITEMID " + 
                                               " UNION " +
                                               " SELECT     ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, SUM(QTY) AS INQTY, 0 AS OUTQTY, 0 AS BQTYR, 0 AS BAMTR " +
                                               " FROM         STK_TRANS AS STK_TRANS_2 " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'ITRF') " +
                                               " GROUP BY ITEMID " +
                                               " UNION " +
                                               " SELECT     ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, SUM(QTY) AS OUTQTY, 0 AS BQTYR, 0 AS BAMTR " +
                                               " FROM         STK_TRANS AS STK_TRANS_1 " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'ITRF') " +
                                               " GROUP BY ITEMID " +
                                               " UNION " +
                                               " SELECT     ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 0 AS OUTQTY, SUM(QTY) AS BQTYR, SUM(AMOUNT) AS BAMTR " +
                                               " FROM         STK_TRANS AS STK_TRANS_3 "+
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'BUY') GROUP BY ITEMID) AS A " +
                       " GROUP BY ITEMID) AS B INNER JOIN STK_ITEM ON B.ITEMID = STK_ITEM.ITEMID WHERE (B.CLQTY <> 0)  ORDER BY ITEMNM", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label1.Visible = false;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "No Data Found.";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // This is for cumulating the values       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ItemNM = DataBinder.Eval(e.Row.DataItem, "ITEMNM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + ItemNM;

                decimal Car_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CARTONQTY").ToString());
                string CARTONQTY = SpellAmount.comma(Car_Qty);
                e.Row.Cells[1].Text = CARTONQTY;

                decimal dblCartonQty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CARTONQTY").ToString());

                decimal P_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PIECES").ToString());
                string PIECES = SpellAmount.comma(P_Qty);
                e.Row.Cells[2].Text = PIECES;

                decimal dblPieces = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PIECES").ToString());

                decimal CL_Qty = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CLQTY").ToString());
                string CLQTY = SpellAmount.comma(CL_Qty);
                e.Row.Cells[3].Text = CLQTY;

                decimal dblCLQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CLQTY").ToString());

                decimal Stk_V = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STOCKVALUE").ToString());
                string STOCKVALUE = SpellAmount.comma(Stk_V);
                e.Row.Cells[5].Text = STOCKVALUE;

                decimal dblAmount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STOCKVALUE").ToString());

                decimal rt = (dblAmount / dblCLQTY);
                string RATE = SpellAmount.comma(rt);
                e.Row.Cells[4].Text = RATE;

                // Cumulating Grand Total           
                dblGrandTotalCartonQty += dblCartonQty;
                dblGrandTotalCartonQtyComma = SpellAmount.comma(dblGrandTotalCartonQty);
                dblGrandTotalPieces += dblPieces;
                dblGrandTotalPiecesComma = SpellAmount.comma(dblGrandTotalPieces);
                dblGrandTotalCLQty += dblCLQTY;
                dblGrandTotalCLQtyComma = SpellAmount.comma(dblGrandTotalCLQty);
                //dblGrandTotalRate += rt;
                //dblGrandTotalRateComma = SpellAmount.comma(dblGrandTotalRate);

                dblGrandTotalAmount += dblAmount;
                dblGrandTotalAmountComma = SpellAmount.comma(dblGrandTotalAmount);

                // This is for cumulating the values  
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ddd'");
                //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
                //    e.Row.Attributes.Add("style", "cursor:pointer;");
                //    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                //}
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTAL :";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = dblGrandTotalCartonQtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = dblGrandTotalPiecesComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = dblGrandTotalCLQtyComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = dblGrandTotalAmountComma;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;
            }

            ShowHeader(GridView1);
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