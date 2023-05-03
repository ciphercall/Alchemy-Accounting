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
    public partial class rptItemStockStore : System.Web.UI.Page
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);

        decimal tot_CartonQty = 0;
        decimal tot_PcQty = 0;
        decimal tot_ClosingQty = 0;
        decimal tot_StockValue = 0;

        string Tot_CartonQtyComma = "";
        string Tot_PcQtyComma = "";
        string Tot_ClosingQtyComma = "";
        string Tot_StockValueComma = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Global.lblAdd(@"SELECT COMPNM FROM ASL_COMPANY", lblCompNM);
                Global.lblAdd(@"SELECT ADDRESS FROM ASL_COMPANY", lblAddress);

                DateTime PrintDate = DateTime.Now;
                string td = PrintDate.ToString("dd-MMM-yyyy hh:mm tt");
                lblTime.Text = td;

                string ItemNM = Session["ItemNM"].ToString();
                string ItemID = Session["ItemID"].ToString();
                string Date = Session["Date"].ToString();

                DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                lblDate.Text = asON.ToString("dd-MMM-yyyy");
                lblItemName.Text = ItemNM;
                showGrid();
            }
            catch
            {
            }
        }

        public void showGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Alchemy_Acc"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            string ItemNM = Session["ItemNM"].ToString();
            string ItemID = Session["ItemID"].ToString();
            string Date = Session["Date"].ToString();
            DateTime asON = DateTime.Parse(Date, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            string aOn = asON.ToString("yyyy/MM/dd");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT B.STORE, B.CATID, B.ITEMID, B.CLQTY, B.AVGRATE, B.CLQTY * B.AVGRATE AS STOCKVALUE, STK_ITEMMST.CATNM, STK_ITEM.ITEMNM, STK_ITEM.PQTY, " +
                                            " FLOOR(B.CLQTY / STK_ITEM.PQTY) AS CARTONQTY, FLOOR(B.CLQTY / STK_ITEM.PQTY) * STK_ITEM.PQTY - B.CLQTY AS PIECES, STK_STORE.STORENM " +
                                            " FROM (SELECT STORE, CATID, ITEMID, (SUM(ISNULL(INQTY, 0)) + SUM(ISNULL(BQTY, 0))) - (SUM(ISNULL(OUTQTY, 0)) + SUM(ISNULL(SQTY, 0))) AS CLQTY, " +
                                            " SUM(ISNULL(BQTY, 0)) AS BQTY, SUM(ISNULL(BAMT, 0)) AS BAMT, SUM(ISNULL(SQTY, 0)) AS SQTY, SUM(ISNULL(SAMT, 0)) AS SAMT, " +
                                            " (CASE WHEN SUM(isnull(BAMT, 0)) = 0.00 THEN 0.00 ELSE CONVERT(decimal(18, 2), (SUM(isnull(BAMT, 0))) / SUM(isnull(BQTY, 0))) END) AS AVGRATE " +
                                            " FROM (SELECT STORETO AS STORE, CATID, ITEMID, SUM(ISNULL(QTY, 0)) AS BQTY, SUM(ISNULL(AMOUNT, 0)) AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, 0 AS OUTQTY " +
                                               " FROM          STK_TRANS " +
                                               " WHERE      (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'BUY') AND (ITEMID = '" + ItemID + "') " +
                                               " GROUP BY STORETO, CATID, ITEMID " +
                                               " UNION " +
                                               " SELECT STOREFR AS STORE, CATID, ITEMID, 0 AS BQTY, 0 AS BAMT, SUM(ISNULL(QTY, 0)) AS SQTY, SUM(ISNULL(AMOUNT, 0)) AS SAMT, 0 AS INQTY, 0 AS OUTQTY " +
                                               " FROM         STK_TRANS AS STK_TRANS_1 " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'SALE') AND (ITEMID = '" + ItemID + "') " +
                                               " GROUP BY STOREFR, CATID, ITEMID " +
                                               " UNION " +
                                               " SELECT     STORETO AS STORE, CATID, ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, SUM(ISNULL(QTY, 0)) AS INQTY, 0 AS OUTQTY " +
                                               " FROM         STK_TRANS AS STK_TRANS_2 " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'ITRF') AND (ITEMID = '" + ItemID + "') " +
                                               " GROUP BY STORETO, CATID, ITEMID " +
                                               " UNION " +
                                               " SELECT     STOREFR AS STORE, CATID, ITEMID, 0 AS BQTY, 0 AS BAMT, 0 AS SQTY, 0 AS SAMT, 0 AS INQTY, SUM(ISNULL(QTY, 0)) AS OUTQTY " +
                                               " FROM         STK_TRANS AS STK_TRANS_1 " +
                                               " WHERE     (TRANSDT <= '" + aOn + "') AND (TRANSTP = 'ITRF') AND (ITEMID = '" + ItemID + "') " +
                                               " GROUP BY STOREFR, CATID, ITEMID) AS A " +
                       " GROUP BY STORE, CATID, ITEMID) AS B INNER JOIN " +
                       " STK_ITEM ON B.CATID = STK_ITEM.CATID AND B.ITEMID = STK_ITEM.ITEMID INNER JOIN " +
                       " STK_ITEMMST ON STK_ITEM.CATID = STK_ITEMMST.CATID INNER JOIN " +
                       " STK_STORE ON B.STORE COLLATE Latin1_General_CI_AS = STK_STORE.STOREID " +
                       " WHERE     (B.CLQTY <> 0)", conn);

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
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string STORENM = DataBinder.Eval(e.Row.DataItem, "STORENM").ToString();
                e.Row.Cells[0].Text = "&nbsp;" + STORENM;

                decimal CARTONQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CARTONQTY").ToString());
                string carQTY = SpellAmount.comma(CARTONQTY);
                e.Row.Cells[1].Text = carQTY;

                decimal PIECES = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PIECES").ToString());
                string piQty = SpellAmount.comma(PIECES);
                e.Row.Cells[2].Text = piQty;

                decimal CLQTY = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "CLQTY").ToString());
                string closQTY = SpellAmount.comma(CLQTY);
                e.Row.Cells[3].Text = closQTY;

                decimal STOCKVALUE = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "STOCKVALUE").ToString());
                string stkValue = SpellAmount.comma(STOCKVALUE);
                e.Row.Cells[4].Text = stkValue;

                tot_CartonQty += CARTONQTY;
                Tot_CartonQtyComma = SpellAmount.comma(tot_CartonQty);

                tot_PcQty += PIECES;
                Tot_PcQtyComma = SpellAmount.comma(tot_PcQty);

                tot_ClosingQty += CLQTY;
                Tot_ClosingQtyComma = SpellAmount.comma(tot_ClosingQty);

                tot_StockValue += STOCKVALUE;
                Tot_StockValueComma = SpellAmount.comma(tot_StockValue);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "TOTAL :";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = Tot_CartonQtyComma;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = Tot_PcQtyComma;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = Tot_ClosingQtyComma;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = Tot_StockValueComma;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
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